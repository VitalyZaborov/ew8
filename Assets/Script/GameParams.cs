using System.Collections;
using System.Collections.Generic;

public class GameParams{

	public class GunParam{
		public string id;
		public string caliber;
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
		public string prj;
		public float crit;
		public float aim;
		public float aimFallWalk;
		public float aimFallTurn;
		public float angle;
		public int pellets;

		public GunParam clone(){
			return new GunParam(){
				id = id,
				caliber = caliber,
				name = name,
				category = category,
				sprite = sprite,
				dmgMax = dmgMax,
				dmgMin = dmgMin,
				distMax = distMax,
				distMin = distMin,
				firerate = firerate,
				velocity = velocity,
				clip = clip,
				ammo = ammo,
				recoil = recoil,
				recoilMax = recoilMax,
				recoilReduce = recoilReduce,
				burst = burst,
				burstDelay = burstDelay,
				range = range,
				prj = prj,
				crit = crit,
				aim = aim,
				aimFallWalk = aimFallWalk,
				aimFallTurn = aimFallTurn,
				angle = angle,
				pellets = pellets
			};
		}
	}

	public static Dictionary<string, GunParam> gunParam = new Dictionary<string, GunParam>{
		{"AKM", new GunParam{
			id = "AKM",
			caliber = "7.62x39",
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
			range = 100f,
			prj = "Bullet",
			crit = 2f,
			aim = 1f,
			aimFallWalk = 1f,
			aimFallTurn = 1f,
			angle = 0f,
			pellets = 1
		}},
		{"M16A3", new GunParam{
			id = "M16A3",
			caliber = ".223",
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
			range = 100f,
			prj = "Bullet",
			crit = 2f,
			aim = 1f,
			aimFallWalk = 1f,
			aimFallTurn = 1f,
			angle = 0f,
			pellets = 1
		}},
		{"UMP", new GunParam{
			id = "UMP",
			caliber = ".45 ACP",
			name = "H&K UMP",
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
			range = 100f,
			prj = "Bullet",
			crit = 2f,
			aim = 0.8f,
			aimFallWalk = 1f,
			aimFallTurn = 1f,
			angle = 0f,
			pellets = 1
		}},
		{"M203", new GunParam{
			id = "M203",
			caliber = "40 mm",
			name = "M203",
			category = -1,
			sprite = -1,
			dmgMax = 100,
			dmgMin = 50,
			distMax = 0.5f,
			distMin = 2f,
			firerate = 60,
			velocity = 4f,
			clip = 1,
			ammo = 4,
			recoil = 10f,
			recoilMax = 20f,
			recoilReduce = 20f,
			burst = 0,
			burstDelay = 0f,
			range = 100f,
			prj = "HE40",
			crit = 1f,
			aim = 1f,
			aimFallWalk = 1f,
			aimFallTurn = 1f,
			angle = 0f,
			pellets = 1
		}},
		{"M870", new GunParam{
			id = "M870",
			caliber = ".12",
			name = "M870",
			category = 0,
			sprite = 2,
			dmgMax = 20,
			dmgMin = 10,
			distMax = 5f,
			distMin = 12f,
			firerate = 80,
			velocity = 7f,
			clip = 6,
			ammo = 24,
			recoil = 5f,
			recoilMax = 15f,
			recoilReduce = 15f,
			burst = 0,
			burstDelay = 0f,
			range = 100f,
			prj = "Bullet",
			crit = 1.5f,
			aim = 1f,
			aimFallWalk = 1f,
			aimFallTurn = 1f,
			angle = 10f,
			pellets = 5
		}}
	};
}