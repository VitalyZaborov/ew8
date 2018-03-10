from ai import a as ACTION
from ai import c as CONDITION
from ai import t as TARGET
from ai import p as PROBABILITY
from ai import AI

OUTPUT = '../Assets/Script/AI.cs'


def getAI(ai):
	result = []
	for index, preset in iter(ai.items()):
		result.append(
			'\n		{' + str(index) + ', new AINode[]{' + ','.join(getPreset(preset)) + '\n		}}')
	return result


def getPreset(preset):
	result = []
	for node in preset:
		action = node.get(ACTION)
		condition = node.get(CONDITION)
		target = node.get(TARGET)
		probability = node.get(PROBABILITY)

		string = '\n			new AINode(){'
		if target is not None:
			string += '\n				target = Condition.getConditions("' + target + '"),'
		if probability is not None:
			string += '\n				probability = ' + str(probability) + ','
		if condition is not None:
			string += '\n				character = Condition.getConditions("' + condition + '"),'
		string += '\n				action = ' + getAction(action)
		string += '\n			}'

		result.append(string)

	return result


def getAction(action):
	string = 'cnxt => new ' + action
	if '(' not in action:
		string += '()'

	return string


output = open(OUTPUT, 'w')
output.write('using System.Collections.Generic;\n')
output.write('public class AI{')
output.write('\n\n	public static Dictionary<int, AINode[]> ai = new Dictionary<int, AINode[]>{')
output.write(','.join(getAI(AI)))
output.write('\n	};')
output.write('\n}')
output.close()