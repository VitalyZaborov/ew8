using System.Collections;
using System.Collections.Generic;

public class GameParams{

	public class GunParam{
		public string name;
		public int category;
		public int sprite;
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
		public float burstDelay;
		public float range;
	}

	public static Dictionary<string, GunParam> gunParam = new Dictionary<string, GunParam>{
		{"AKM", new GunParam{
			name = "AKM",
			category = 0,
			sprite = 0,
			dmgMax = 25,
			dmgMin = 19,
			distMax = 6f,
			distMin = 30f,
			firerate = 600,
			velocity = 12f,
			clip = 30,
			ammo = 90,
			recoil = 3f,
			recoilMax = 25f,
			recoilReduce = 16f,
			burst = 0,
			burstDelay = 0f,
			range = 100f
		}},
		{"M16A3", new GunParam{
			name = "M16A3",
			category = 0,
			sprite = 1,
			dmgMax = 23,
			dmgMin = 17,
			distMax = 18f,
			distMin = 30f,
			firerate = 900,
			velocity = 14f,
			clip = 30,
			ammo = 90,
			recoil = 2f,
			recoilMax = 20f,
			recoilReduce = 20f,
			burst = 3,
			burstDelay = 0.2f,
			range = 100f
		}},
		{"UMP", new GunParam{
			name = "UMP",
			category = 0,
			sprite = 2,
			dmgMax = 28,
			dmgMin = 12,
			distMax = 12f,
			distMin = 24f,
			firerate = 450,
			velocity = 8f,
			clip = 25,
			ammo = 100,
			recoil = 3f,
			recoilMax = 15f,
			recoilReduce = 25f,
			burst = 0,
			burstDelay = 0f,
			range = 100f
		}}
	};
}