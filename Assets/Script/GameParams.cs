using System.Collections;
using System.Collections.Generic;

public class GameParams{

	public struct GunParam{
		public string name;
		public int dmgMax;
		public int dmgMin;
		public float distMax;
		public float distMin;
		public int firerate;
		public float velocity;
		public int clip;
		public int ammo;
		public float recoil;
		public float recoilMax;
		public float recoilReduce;
		public int burst;
		public float range;
	}

	public static Dictionary<string, GunParam> gunParam = new Dictionary<string, GunParam>{
		{"UMP", new GunParam{
			name = "UMP",
			dmgMax = 28,
			dmgMin = 12,
			distMax = 4,
			distMin = 8,
			firerate = 450,
			velocity = 4,
			clip = 25,
			ammo = 100,
			recoil = 3,
			recoilMax = 15,
			recoilReduce = 10,
			burst = 0,
			range = 10
		}},
		{"AKM", new GunParam{
			name = "AKM",
			dmgMax = 25,
			dmgMin = 19,
			distMax = 2,
			distMin = 10,
			firerate = 600,
			velocity = 6,
			clip = 30,
			ammo = 90,
			recoil = 3,
			recoilMax = 25,
			recoilReduce = 8,
			burst = 0,
			range = 10
		}},
		{"M16A3", new GunParam{
			name = "M16A3",
			dmgMax = 23,
			dmgMin = 17,
			distMax = 6,
			distMin = 10,
			firerate = 800,
			velocity = 7,
			clip = 30,
			ammo = 90,
			recoil = 2,
			recoilMax = 20,
			recoilReduce = 12,
			burst = 3,
			range = 10
		}}
	};
}