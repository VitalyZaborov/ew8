
a = 'ACTION'
c = 'CONDITION'
t = 'TARGET'
p = 'PROBABILITY'

AI = {
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
		#	t: 'enemy,hp>50%,nearest',
			t: 'enemy,hp>50%,nearest',
		},
	],
}