import os
import math
import json
import vectormath
from settings import *
import xml.etree.ElementTree as ET


def equal(a, b):
	return abs(a - b) < ERROR_RATIO

class Params(object):
	def __init__(self, decomposedMatrix = None):
		self.position = vectormath.Vector2(0, 0)
		self.scale = vectormath.Vector2(1, 1)
		self.rotation = 0
		self.duration = 0

		if decomposedMatrix is not None:
			position = decomposedMatrix['Position']
			scaling = decomposedMatrix['Scaling']
			rotation = decomposedMatrix['Rotation']

			self.position.x = position['x']
			self.position.y = position['y']
			self.scale.x = scaling['x']
			self.scale.y = scaling['y']
			self.rotation = radToDeg(rotation['z'])
	@staticmethod
	def clone(target):
		if target is None:
			return None
		p = Params()
		p.position.x = target.position.x
		p.position.y = target.position.y
		p.scale.x = target.scale.x
		p.scale.y = target.scale.y
		p.rotation = target.rotation
		return p

	def __repr__(self):
		return '[pos: %s, %s rotation: %s scale: %s, %s]' % (self.position.x, self.position.y, self.rotation, self.scale.x, self.scale.y, )

class Animation(object):
	def __init__(self):
		self.poses = []
		self.frames = []
		self.__start = None
		self.__finish = None
		self.__delta = None
		self.__direction = None
		self.__isStatic = True

	def append(self, p):
		self.frames.append(p)

	def make(self, keyframes):
		prevFrame = 0
		prevPose = Params.clone(self.frames[prevFrame])
		if prevPose is not None:
			self.poses.append(prevPose)
		for frame in keyframes + (len(self.frames), ) if keyframes is not None else (len(self.frames), ):
			frame -= 1
			pose = Params.clone(self.frames[frame])
			if prevPose is not None:
				prevPose.duration = frame - prevFrame
			if pose is not None:
				self.poses.append(pose)
			prevPose = pose
			prevFrame = frame

	def __isSamePose(self, p):
		return all(self.__finish.position == p.position)# and all(self.__finish.scale == p.scale)

	def __isSameLine(self, p):
		delta = self.__start.position - p.position
		return self.__direction is not None and delta.length > 0 and all([equal(self.__direction[k], v) for k ,v in enumerate(delta.normalize())])

	def __getProps(self, s, f):
		direction = s - f
		delta = direction.x / direction.y
		return direction, delta

def makeBack(drawOrder):
	result = []
	for bone in drawOrder:
		replacement = BACK_DRAW_ORDER.get(bone)
		if replacement is None:
			result.append(bone)
		elif isinstance(replacement, list):
			for item in replacement:
				if item in drawOrder:
					result.append(item)
		elif replacement in drawOrder:
			result.append(replacement)
	return result

def makeSlots(drawOrder):
	slots = []
	for bone in drawOrder:
		if bone not in SKELETON:
			continue
		content = SLOT_CONTENT.get(bone, None)
		if content is not None:
			for item in content:
				slots.append((bone + '.' + item, bone))
		else:
			slots.append((bone, bone))
	return [{"name": name, "parent": parent} for index, (name, parent) in enumerate(slots)]

def makeSkin(slots, side, gender, weapon, textures):
	result = [{'name': slot["name"], 'display': textures[getDisplay(slot["name"], side, gender, weapon)]} for slot in slots]
	return [{'name': '', 'slot': result}]

def getDisplay(slot, side, gender, weapon):
	if slot == WPN:
		return WEAPONS[weapon]
	elif slot == OFFHAND:
		if weapon == 'b':
			return 'Arrow'
		elif weapon == 'g':
			return WEAPONS[weapon]
		else:
			return side + '.' + 'Shield'
	elif slot == CLOACK:
		return gender + '.' + side + '.a.' + slot
	else:
		slot, type = slot.split('.')
		return gender + '.' + side + '.' + ('a' if type == ARMOR else 's') + '.' + slot
#	print 'ERROR: No such textures:',slot, side, gender, weapon
	return None

def degToRad(deg):
	return (math.pi * deg) / 180

def radToDeg(rad):
	return (rad * 180) / math.pi

def globalToLocal(point, target):
	delta = point - target.position
	angle = degToRad(target.rotation)
	rx = math.cos(angle) * delta.x + math.sin(angle) * delta.y
	ry = -math.sin(angle) * delta.x + math.cos(angle) * delta.y

	rx /= target.scale.x
	ry /= target.scale.y

	return vectormath.Vector2(rx, ry)

def getName(instance):
	instanceName = instance['Instance_Name']
	if instanceName != '':
		return instanceName
	symbolName = instance['SYMBOL_name']
	if symbolName == 'shadow':
		return symbolName
	return 'slash'

def parseSkeleton(data):
	layers = data['ANIMATION']['TIMELINE']['LAYERS']
	bones = {}
	drawOrder = []
	for layer in layers:
		layerName = layer['Layer_name']
		if layerName not in SKELETON:
			continue

		drawOrder.append(layerName)
		frames = [f for f in layer['Frames'] if len(f['elements']) > 0]
		frames.sort(key= lambda f: f['index'])
		frame = frames[0]
		instance = frame['elements'][0]['SYMBOL_Instance']
		boneName = getName(instance)
		decomposedMatrix = instance['DecomposedMatrix']
		bones[boneName] = Params(decomposedMatrix)

	drawOrder.reverse()

	relativeParams = {}
	for boneName, params in bones.iteritems():
		parent = SKELETON[boneName]
		if parent is not None:
			relativeParams[boneName] = globalToLocal(params.position, bones[parent]), params.rotation - bones[parent].rotation

	for boneName, (position, rotation) in relativeParams.iteritems():
		bones[boneName].position = position
		bones[boneName].rotation = rotation

	return bones, drawOrder

def parseAnimation(animationId, data, skeleton):
	symbols = data['SYMBOL_DICTIONARY']
	layers = data['ANIMATION']['TIMELINE']['LAYERS']
	animations = {}
	events = []

	for layer in layers:
		layerName = layer['Layer_name']
		frames = layer['Frames']
		if layerName == ACTIONS_LAYER:
			events = [(LABEL_TO_EVENT.get(frame['name'], frame['name']), frameIndex, ) for frameIndex, frame in enumerate(frames) if frame.get('name') is not None]
			continue
		if layerName not in SKELETON:
			continue
	#	print '--------------- layer',layerName,'---------------'
		frames.sort(key= lambda f: f['index'])
		animation = Animation()
		boneName = ''
		for frameIndex, frame in enumerate(frames):
			elements = frame['elements']
			if len(elements) == 0:
				animation.append(None)
				continue
			instance = elements[0]['SYMBOL_Instance']
			boneName = getName(instance)
			decomposedMatrix = instance['DecomposedMatrix']
			animation.append(Params(decomposedMatrix))

		animation.make(KEYFRAMES.get(animationId[2:]))
		animations[boneName] = animation

	for boneName, animation in animations.iteritems():
		parent = SKELETON[boneName]
		skeletonParams = skeleton.get(boneName)
		if skeletonParams is None:
			continue

		frames = animations[parent].frames if parent is not None else None

		isMoveable = boneName in MOVABLE_BONES
		frameIndex = 0
		for params in animation.poses:
			params.rotation -= skeletonParams.rotation
			if frames is not None:
				parentParams = frames[frameIndex]
				params.rotation -= parentParams.rotation
				if isMoveable:
					position = globalToLocal(params.position, parentParams)
					params.position = position - skeletonParams.position
				else:
					params.position.x = params.position.y = 0
			else:
				params.position -= skeletonParams.position

			frameIndex += params.duration

	return animations, events

def convert(gender, weapon, textures):
	skeletonId = gender + 'f' + weapon
	print 'converting', skeletonId
	# Skeleton
	path = SOURCE_PATH + skeletonId + 's'
	animFile = open(path + '/Animation.json', 'r')
	animData = json.loads(animFile.read())
	animFile.close()

	skeleton, drawOrder = parseSkeleton(animData)
	animations = {}

	# Animations
	for idx, animationName in ANIMATIONS.iteritems():
		path = SOURCE_PATH + skeletonId + idx
		if not os.path.isdir(path):
			continue
		animFile = open(path + '/Animation.json', 'r')
		animData = json.loads(animFile.read())
		animFile.close()

		animations[animationName] = parseAnimation(skeletonId + idx, animData, skeleton)

	#	print 'Processed',idx,animationName

	# BONES

	bone = [{'name':'root', 'transform': {}}]
	animation = []
	for boneName, params in skeleton.iteritems():
		transform = {
			'x': params.position.x * DRAGON_SCALE,
			'y': params.position.y * DRAGON_SCALE,
			'skX': params.rotation,
			'skY': params.rotation,
		}
		bone.append({'name': boneName, 'parent': (SKELETON[boneName] or 'root'), 'transform': transform})

	for animationName, (anim, events) in animations.iteritems():
		boneArray = []
		for boneName, boneAnim in anim.iteritems():
			rotateFrame = []
			translateFrame = []
			scaleFrame = []

			for pose in boneAnim.poses:
				if boneName in MOVABLE_BONES or SKELETON[boneName] is None:
					translateFrame.append({'duration':pose.duration, 'tweenEasing': 0, 'x': pose.position.x * DRAGON_SCALE, 'y': pose.position.y * DRAGON_SCALE})
				rotateFrame.append({'duration': pose.duration, 'tweenEasing': 0, 'rotate': pose.rotation})
				scaleFrame.append({'duration': pose.duration, 'tweenEasing': 0, 'x': pose.scale.x, 'y': pose.scale.y})

			boneData = {
				'name': boneName,
				'rotateFrame': rotateFrame,
				'translateFrame': translateFrame,
				'scaleFrame': scaleFrame,
			}
			boneArray.append(boneData)
		frames = []
		prevEvent = None
		totalDuration = 0
		for (eventName, frameIndex) in events:
			if prevEvent is None:
				prevEvent = {'duration': 0}
				frames.append(prevEvent)
			prevEvent['duration'] = frameIndex - totalDuration
			prevEvent = {'duration': 0, 'events': [{'name': eventName}]}
			frames.append(prevEvent)
			totalDuration = frameIndex
		data = {
			'frame': frames,
			'duration': len(anim['body'].frames),
			'fadeInTime': 0.3,
			'name': animationName,
			'bone': boneArray,
		}
		animation.append(data)

	result = []
	for side in ['f', 'b']:
		order = drawOrder if side == 'f' else makeBack(drawOrder)
		slots = makeSlots(order)
		skin = makeSkin(slots, side, gender, weapon, textures)
		armature = {
			'aabb': {'x':0, 'y': 0, 'width':0, 'height':0},
			'defaultActions': [{'gotoAndPlay': 'stay'}],
			'frameRate': 30,
			'name': gender + side + weapon,
			'type': "Armature",
			'skin': skin,
			'slot': slots,
			'bone': bone,
			'animation': animation,
		}
		result.append(armature)
	print '...done!'
	return result

def convertTextures():
	tree = ET.parse('heroes_tex.xml')
	root = tree.getroot()

	textures = {}
	sprites = []
	pivotX = 0
	pivotY = 0

	for child in root:
		key = child.attrib['name']

		keys = key.split(' ')
		id = keys[0]
		index = keys[-1]
		if id not in textures:
			textures[id] = []
		display = textures[id]

		name = id + '_' + index

		x = float(child.attrib['x'])
		y = float(child.attrib['y'])
		width = float(child.attrib['width'])
		height = float(child.attrib['height'])
		frameX = float(child.attrib.get('frameX', 0))
		frameY = float(child.attrib.get('frameY', 0))
		frameWidth = float(child.attrib.get('frameWidth', width))
		frameHeight = float(child.attrib.get('frameHeight', height))

		if 'pivotX' in child.attrib:
			pivotX = float(child.attrib['pivotX'])
		if 'pivotY' in child.attrib:
			pivotY = float(child.attrib['pivotY'])
		rect = {
			"frameX": frameX,
			"frameY": frameY,
			"x": x,
			"y": y,
			"frameWidth":frameWidth,
			"frameHeight":frameHeight,
			"width":width,
			"height":height,
			"name": name,
		}

		slot = {
			"path": name,
			"type": "image",
			"name": name,
			"transform": {"y": -pivotY + frameHeight / 2, "x": -pivotX + frameWidth / 2}
		}
		display.append(slot)
		sprites.append(rect)

	for display in textures.values():
		display.sort(key = lambda d: d['name'])

	dragon = {
		'width': 4096,
		'height': 4096,
		'name': FILE_NAME,
		'imagePath': FILE_NAME + '_tex.png',#meta['image'],
		'SubTexture': sprites,
	}
	output = open(FILE_NAME + '_tex.json', 'w')
	output.write(json.dumps(dragon))
	output.close()

	return textures


# ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^ ^^^^^
#  :--: :--: :--: :--: :--: :--: :--: D R A G O N   B O N E S :--: :--: :--: :--: :--: :--: :--:
# ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~ ~~~~~

def convertAll():
	textures = convertTextures()
	armatures = []
	for gender in ['m', 'f']:
		for weapon in ['a', 'b', 'c', 'd', 'g', 's', ]:
			front, back = convert(gender, weapon, textures)
			armatures.append(front)
			armatures.append(back)
		# 	break
		# break

	dragon = {
		"frameRate":30,
		"isGlobal":0,
		"name":"heroes",
		"version":"5.5",
		"armature": armatures,
	}
	output = open(FILE_NAME + '_ske.json', 'w')
	output.write(json.dumps(dragon))
	output.close()

convertAll()


'''
armature = convert('ffa')
dragon = {
	"frameRate":30,
	"isGlobal":0,
	"name":"ffaa1",
	"version":"5.5",
	"armature": [armature],
}
output = open('ffa_ske.json', 'w')
output.write(json.dumps(dragon))
output.close()
'''

'''
def explore(path):
	flag = None
	file = open(path, 'r')
	data = json.loads(file.read())

	symbols = data['SYMBOL_DICTIONARY']
	animation = data['ANIMATION']

	# print animation['name']	# Heroes
	timeline = animation['TIMELINE']
	layers = timeline['LAYERS']

	for layerIndex, layer in enumerate(layers):
		layerName = layer['Layer_name']
		frames = layer['Frames']
		print '--------------- layer',layerName,'---------------'
		frames.sort(key= lambda f: f['index'])
		for frameIndex, frame in enumerate(frames):
			elements = frame['elements']
			#duration = frame['duration'] # 1
			print '--', frameIndex
			if len(elements) > 1:
				flag = elements
			for element in elements:
				instance = element['SYMBOL_Instance']

				symbolName = instance['SYMBOL_name']
				instanceName = instance['Instance_Name']
				symbolType = instance['symbolType']
				decomposedMatrix = instance['DecomposedMatrix']
				matrix3D = instance['Matrix3D']
				transformationPoint = instance['transformationPoint']


				print '		',matrix3D.keys()


	print flag


#explore(ANIMATION_PATH)'''


'''
spriteFile = open('Heroes.json', 'r')

	spritesheetRaw = spriteFile.read()
	# Also need UTF-8
	spritesheetData = spritesheetRaw[spritesheetRaw.find('{'):]
	spritesheet = json.loads(spritesheetData)
	spriteFile.close()
	frames = spritesheet['frames']

	textures = {}
	sprites = []
	for key, data in frames.iteritems():
		keys = key.split(' ')
		id = keys[0]
		index = keys[-1]
		frame = data['frame']
		if id not in textures:
			textures[id] = []
		display = textures[id]
'''