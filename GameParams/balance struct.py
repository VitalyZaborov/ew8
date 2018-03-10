
INPUT = './Balance.xlsm'
OUTPUT = '../Assets/Script/AI.cs'


def getData(cell, field, type, sheet):
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


output = open(OUTPUT, 'w')
workbook = openpyxl.load_workbook(filename=INPUT, data_only=True)

output.write('public class GameParams{')

for worksheet in workbook.worksheets:
	if worksheet.title.startswith('_'):
		continue

	fields = getRow(worksheet.rows[0])
	types = getRow(worksheet.rows[1])
	structName = worksheet.title[0].upper() + worksheet.title[1:]

	# if worksheet != workbook.worksheets[0]:
	#	output.write('\n\n')

	output.write('\n\n	public struct ' + structName + '{')

	for field, type in zip(fields, types):
		output.write('\n		public ' + type + ' ' + field + ';')

	output.write('\n	}')

	output.write('\n	public static ' + structName + '[] ' + worksheet.title + ' = new ' + structName + '[]{')

	i = 3
	while worksheet.cell(row=i, column=2).value is not None:
		if i != 3:
			output.write(',')

		output.write('\n		new ' + structName + '{')
		for j, field in enumerate(fields):
			data = getData(worksheet.cell(row=i, column=j + 1), field, type, worksheet.title)
			if data is not None:
				if j != 0:
					output.write(',')
				output.write('\n			' + field + '= ' + data)

		output.write('\n		}')
		i += 1

	output.write('\n	};')

output.write('\n}')
output.close()