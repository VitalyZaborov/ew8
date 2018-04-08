
a = 'ACTION'
c = 'CONDITION'
t = 'TARGET'
p = 'PROBABILITY'
e = 'EXTRA'

AI = {
	# 0 is the player AI
	0: [
		{
			a: 'PlayerControl',
		},
	],

	1: [
		{
			a: 'Rest',
		},
		{
			a: 'Reload',
			c: 'self,clip=0',
		},
		{
			a: 'Shoot(){accuracy = 5}',
			c: 'self,recoil<5',
			t: 'enemy,nearest',
		#	t: 'enemy,hp>50%,nearest',
			e: 'cnxt.memory.write("enemyPos", cnxt.target.transform.position);',
		},
		{
			a: 'GoTo(cnxt.memory.read<Vector3>("enemyPos"))',
			e: 'cnxt.memory.write("enemyPos", null);',
		},
	],
}