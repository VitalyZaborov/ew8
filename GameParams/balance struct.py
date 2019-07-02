import openpyxl

INPUT = './Balance.xlsm'
OUTPUT = '../Assets/Script/GameParams.cs'
BASIC_TYPES = ('bool', 'int', 'uint', 'float', 'double', 'string', )
CALCULATABLE_TYPES = ('int', 'uint', 'float', 'double', )
GLOBAL_TYPE = type

SETTINGS_ROW = 0
FIELDS_ROW = 1
TYPES_ROW = 2
DATA_ROW = 3

def getData(cell, field, sheet, type):
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
	if type == 'DamageModifier':
		return 'new DamageModifier(' + str(value) + ')'
	if type not in BASIC_TYPES:
		return type + '.' + value

	if isinstance(value, str) or isinstance(value, unicode):
		return '"' + value + '"'
	return str(value) + ('f' if type == 'float' else '')


def getRow(row):
	fields = []
	for cell in row:
		if cell.value is None:
			break
		fields.append(str(cell.value))
	return fields

def getStruct(structName, fields, types, superclass, operators):
	result = '\n\n	public class ' + structName + ' : ' + ('GameParam' if superclass is None else superclass) + '{'
	if superclass is None:
		for field, type in zip(fields, types):
			result += '\n		public ' + type + ' ' + field + ';'
	result += '\n\n		public ' + structName + ' clone(){'
	result += '\n			return new ' + structName + '(){'
	rows = ['\n				' + field + ' = ' + field for field in fields]
	result += ','.join(rows)
	result += '\n			};\n		}'

	result += getOperators(structName, fields, types, operators)
	result += '\n	}'
	return result

def getOperators(structName, fields, types, operators):
	if operators is None:
		return ''

	result = ''
	print 'getOperators',operators
	for operator in operators:
		print '--',operator
		if operator in (' ', '	',):
			continue
		result += '\n		public static ' + structName + ' operator' + operator + '(' + structName + ' first, ' + structName + ' second){'
		result += '\n			return new ' + structName + '(){'
		rows = ['\n				' + field + ' = first.' + field + ' ' + operator + ' second.' + field for field, type in zip(fields, types) if type in CALCULATABLE_TYPES]
		result += ','.join(rows)
		result += '\n			};\n		}'
	return result

def getParam(row, structName, fields, types):
	values = []
	key = None
	for i, field in enumerate(fields):
		value = getData(worksheet.cell(row=row, column=i + 1), field, structName, types[i])
		if i == 0:
			key = value
		if value is None:
			continue
		values.append('\n			' + field + ' = ' + str(value));

	return '\n		{' + str(key) + ', new ' + structName + '{' + ','.join(values) + '\n		}}'


output = open(OUTPUT, 'w')
workbook = openpyxl.load_workbook(filename=INPUT, data_only=True)

output.write('using System.Collections;\nusing System.Collections.Generic;\n\npublic class GameParams{')
output.write('\n	public class GameParam{}')

for worksheet in workbook.worksheets:
	if worksheet.title.startswith('_'):
		continue

	rows = list(worksheet.rows)
	fields = getRow(rows[FIELDS_ROW])
	types = getRow(rows[TYPES_ROW])
	print 'fields',fields
	print 'types',types
	superclass = worksheet.cell(row=SETTINGS_ROW+1, column=1).value
	operators = worksheet.cell(row=SETTINGS_ROW + 1, column=2).value
	print 'operators',operators

	structName = worksheet.title[0].upper() + worksheet.title[1:]

	output.write(getStruct(structName, fields, types, superclass, operators))

	typeDef = 'Dictionary<' + types[0] + ', ' + structName + '>'
	output.write('\n\n	public static ' + typeDef + ' ' + worksheet.title + ' = new ' + typeDef + '{')

	params = []
	i = DATA_ROW + 1
	while worksheet.cell(row=i, column=1).value is not None:
		value = worksheet.cell(row=i, column=1).value
		if not str(value).startswith("#"):
			params.append(getParam(i, structName, fields, types))
		i += 1

	output.write(','.join(params) + '\n	};')

output.write('\n}')
output.close()
