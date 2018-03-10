import random

result = [0, 0, 0, 0]

for i in range(64000):
	count = 0
	for j in range(3):
		if random.random() < 0.25:
			count += 1
	result[count] += 1

print ('Results:', result)
