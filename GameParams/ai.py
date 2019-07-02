
a = 'ACTION'
c = 'CONDITION'
t = 'TARGET'
p = 'PROBABILITY'
e = 'EXTRA'

AI = {
	# 0 is the player AI
	'empty': [],

	'player': [
		{
			a: 'PlayerControl',
		},
	],

	'common': [
		{
			a: 'Attack',
			c: 'enemy,nearest',
		#	t: 'enemy,hp>50%,nearest',
			e: 'cnxt.memory.write("enemyPos", cnxt.target.transform.position);',
		},
		{
			a: 'GoTo(cnxt.memory.read<Vector3>("enemyPos"))',
			e: 'cnxt.memory.erase("enemyPos");',
		},
	],
}