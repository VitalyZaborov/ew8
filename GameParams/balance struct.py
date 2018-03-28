import openpyxl

INPUT = './Balance.xlsm'
OUTPUT = '../Assets/Script/GameParams.cs'


def getData(cell, field, sheet):
	value = cell.value
	if value is None:
		return None
	if field in ('skills', 'chants'):
		array = value.split(',')
		res = 'new {'
		for i in range(0, len(array), 2):
			if i != 0:
				res += ', '
			skill = array[i]
			level = array[i + 1]
			res += skill + ' = ' + level
		res += '}'
		return res
	if field in ('mod', 'resist'):
		return 'new DamageModifier(' + str(value) + ')'
	if isinstance(value, str):
		return '"' + value + '"'
	return str(value)


def getRow(row):
	fields = []
	for cell in row:
		if cell.value is None:
			break
		fields.append(str(cell.value))
	return fields

def getStruct(structName, fields, types):
	result = '\n\n	public struct ' + structName + '{'
	for field, type in zip(fields, types):
		result += '\n		public ' + type + ' ' + field + ';'
	result += '\n	}'
	return result

def getParam(row, structName, fields, types):
	values = []
	key = None
	for i, field in enumerate(fields):
		value = getData(worksheet.cell(row=row, column=i + 1), field, structName)
		if i == 0:
			key = value
		values.append('\n			' + field + ' = ' + str(value));

	return '\n		{' + str(key) + ', new ' + structName + '{' + ','.join(values) + '\n		}}'


output = open(OUTPUT, 'w')
workbook = openpyxl.load_workbook(filename=INPUT, data_only=True)

output.write('using System.Collections;\nusing System.Collections.Generic;\n\npublic class GameParams{')

for worksheet in workbook.worksheets:
	if worksheet.title.startswith('_'):
		continue

	fields = getRow(worksheet.rows[0])
	types = getRow(worksheet.rows[1])
	structName = worksheet.title[0].upper() + worksheet.title[1:]

	output.write(getStruct(structName, fields, types))

	typeDef = 'Dictionary<' + types[0] + ', ' + structName + '>'
	output.write('\n\n	public static ' + typeDef + ' ' + worksheet.title + ' = new ' + typeDef + '{')

	params = []
	i = 3
	while worksheet.cell(row=i, column=2).value is not None:
		params.append(getParam(i, structName, fields, types))
		i += 1

	output.write(','.join(params) + '\n	};')

output.write('\n}')
output.close()
