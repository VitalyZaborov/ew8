import json
import vectormath

FILE_NAME = 'heroes'

#SHADOW = 'Shadow'
SLASH = 'slash'
BODY = 'body'
HEAD = 'head'
HANDL = 'hand_l'
HANDR = 'hand_r'
LEGL = 'leg_l'
LEGR = 'leg_r'
ARML = 'arm_l'
ARMR = 'arm_r'
FOOTL = 'foot_l'
FOOTR = 'foot_r'
WPN = 'wpn'
OFFHAND = 'offhand'
CLOACK = 'cloack'
HAIR = 'hair'

ROOT = 'root'

class Sprite:
	SLASH_SWORD_1 = 'slash_sword_1'
	SLASH_SWORD_2 = 'slash_sword_2'
	SLASH_AXE_1 = 'slash_sword_2'


DRAGON_SCALE = 8.0
MOVABLE_BONES = (HANDL, HANDR, LEGL, LEGR)
SOURCE_PATH = 'EXPORTED/'
ERROR_RATIO = 0.001
SKELETON = {
#	SHADOW: None,
	SLASH: None,
	BODY: None,
	HEAD: BODY,
	HANDL: BODY,
	HANDR: BODY,
	LEGL: BODY,
	LEGR: BODY,
	ARML: HANDL,
	ARMR: HANDR,
	FOOTL: LEGL,
	FOOTR: LEGR,
	WPN: ARMR,
	OFFHAND: ARML,
	CLOACK: BODY,
}

BACK_DRAW_ORDER = {
	LEGL: LEGR,
	LEGR: LEGL,

	FOOTL: FOOTR,
	FOOTR: FOOTL,

	HANDL: HANDR,
	HANDR: HANDL,

	ARML: [ARMR, WPN],
	ARMR: [ARML, CLOACK, OFFHAND],

	WPN: [],
	CLOACK: [],
	OFFHAND: [],
}

SKIN = 'skin'
ARMOR = 'armor'

SLOT_CONTENT = {
	BODY: (SKIN, ARMOR),
	HEAD: (SKIN, HAIR, ARMOR),

	HANDL: (SKIN, ARMOR),
	HANDR: (SKIN, ARMOR),
	ARML: (SKIN, ARMOR),
	ARMR: (SKIN, ARMOR),

	LEGL: (SKIN, ARMOR),
	LEGR: (SKIN, ARMOR),
	FOOTL: (SKIN, ARMOR),
	FOOTR: (SKIN, ARMOR),
}


ANIMATIONS = {
	'a1':	'attack1',
	'a2':	'attack2',
	'a3':	'attack3',
	'a4':	'attack4',
	'a5':	'attack5',
	'a6':	'attack6',
	'h':	'shoot',
	'd':	'damage',
	'c':	'cast',
	'r':	'run',
	's':	'stay',
}

WEAPONS = {
	's':	'Sword',
	'a':	'Axe',
	'b':	'Bow',
	'd':	'Blade',
	'g':	'Glove',
	'c':	'Chakram',
}

KEYFRAMES = {
	'd1': (8, 17, 26, 33, 40, ),
	'd2': (10, 20, ),
	'd3': (10, 20, 30, 35, 40, ),
	'd4': (10, ),
	'd5': (10, 15, ),
	'd6': (10, ),

	'aa1': (6, 12, 17, 18, 22, ),
	'aa2': (10, 15, 16, 20, ),
	'aa3': (7, 11, 12, 16, 20, 24, ),
	'ac': (7, 11, 15, ),
	'ar': (5, 10, 15, ),
	'as': (15, ),

	'ba1': (7, 10, 11, 14, ),
	'ba2': (6, 10, 11, 14, ),
	'ba3': (7, 10, 11, 14, ),
	'bh': (7, 13, 18, 19, 22, ),
	'bc': (6, 10, 11, 16, ),
	'br': (5, 10, 15, ),
	'bs': (15, ),

	'ca1': (7, 13, 14, 23, ),
	'ca2': (7, 13, 14, 23, ),
	'ca3': (8, 13, 14, 22, ),
	'ch': (6, 10, 11, 15, ),
	'cc': (6, 15, ),
	'cr': (5, 10, 15, ),
	'cs': (15, ),

	'da1': (5, 6, ),
	'da2': (5, 6, ),
	'da3': (5, 6, ),
	'da4': (5, 6, ),
	'da5': (5, 6, ),
	'da6': (5, 6, ),
	'dh': (10, 16, 20, 23, 24, 28, 36, 40, 44, ),
	'dc': (6, 10, 14, ),
	'dr': (5, 10, 15, ),
	'ds': (15, ),

	'ga1': (5, 7, 8, 10),
	'ga2': (5, 7, 8, 10),
	'ga3': (5, 7, 8, 12),
	'ga4': (5, 7, 8, 10),
	'ga5': (5, 7, 8, 10),
	'ga6': (5, 7, 8, 10),
	'gh': (6, 11, 17, 18, 22, ),
	'gc': (6, 12, 15, ),
	'gr': (5, 10, 15, ),
	'gs': (15, ),

	'sa1': (7, 9, 10, 13),
	'sa2': (7, 10, 11, 14),
	'sa3': (7, 10, 11, 14),
	'sc': (7, 10, 14, ),
	'sr': (5, 10, 15, ),
	'ss': (15, ),
}

PIVOTS = {
	'f.b.a.body':	(-14.7, -34.3),
	'f.b.a.head':	(-7, -15.1),
	'f.b.a.hand_l':	(-4.55, -3.75),
	'f.b.a.hand_r':	(-4.55, -3.75),
	'f.b.a.arm_l':	(-5.25, -2.7),
	'f.b.a.arm_r':	(-5.25, -2.7),
	'f.b.a.leg_l':	(-4.9, -2.1),
	'f.b.a.leg_r':	(-4.9, -2.1),
	'f.b.a.foot_l':	(-5, -3),
	'f.b.a.foot_r':	(-5, -3),
	'f.b.a.cloack':	(-24.75, 1.25),

	'f.f.a.head':	(-7, -15.1),
	'f.f.a.body':	(-15.9, -34.4),
	'f.f.a.hand_l':	(-4.55, -3.75),
	'f.f.a.hand_r':	(-4.55, -3.75),
	'f.f.a.arm_l':	(-5.25, -2.7),
	'f.f.a.arm_r':	(-5.25, -2.7),
	'f.f.a.leg_l':	(-4.9, -2.1),
	'f.f.a.leg_r':	(-4.9, -2.1),
	'f.f.a.foot_l':	(-5, -3),
	'f.f.a.foot_r':	(-5, -3),
	'f.f.a.cloack':	(-24.75, 1.25),

	'f.f.s.body':	(-13.7, -36.6),
	'f.f.s.head':	(-4.2, -13.7),
	'f.f.s.hair':	(-13, -16.65),
	'f.f.s.hand_l':	(-3.15, -3.1),
	'f.f.s.hand_r':	(-3.15, -3.1),
	'f.f.s.arm_l':	(-2.45, -0.3),
	'f.f.s.arm_r':	(-2.45, -0.3),
	'f.f.s.leg_l':	(-5.05, -5.05),
	'f.f.s.leg_r':	(-5.05, -5.05),
	'f.f.s.foot_l':	(-3.4, -3),
	'f.f.s.foot_r':	(-3.4, -3),

	'f.b.s.body':	(-12.8, -36.6),
	'f.b.s.head':	(-4.2, -13.7),
	'f.b.s.hair':	(-13, -16.65),
	'f.b.s.hand_l':	(-3.15, -3.1),
	'f.b.s.hand_r':	(-3.15, -3.1),
	'f.b.s.arm_l':	(-2.45, -0.3),
	'f.b.s.arm_r':	(-2.45, -0.3),
	'f.b.s.leg_l':	(-5.05, -5.05),
	'f.b.s.leg_r':	(-5.05, -5.05),
	'f.b.s.foot_l':	(-3.4, -3),
	'f.b.s.foot_r':	(-3.4, -3),


}
ACTIONS_LAYER = 'actions'
LABEL_TO_EVENT = {
	'2': 'hit',
	'3': 'chain',
}