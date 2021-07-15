using System.Collections;
using System.Collections.Generic;

public class GameParams{

	public class GunParam{
		public string id;
		public Weapon.Type type;
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
		public DamageModifier mod;
		public float reload;

		public GunParam clone(){
			return new GunParam(){
				id = id,
				type = type,
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
				pellets = pellets,
				mod = mod,
				reload = reload
			};
		}
	}

	public static Dictionary<string, GunParam> gunParam = new Dictionary<string, GunParam>{
		{"AKM", new GunParam{
			id = "AKM",
			type = Weapon.Type.ASSAULT,
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
			pellets = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0.7f
		}},
		{"M16A3", new GunParam{
			id = "M16A3",
			type = Weapon.Type.ASSAULT,
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
			pellets = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0.6f
		}},
		{"UMP", new GunParam{
			id = "UMP",
			type = Weapon.Type.SMG,
			caliber = ".45 ACP",
			name = "H&K UMP",
			category = 0,
			sprite = 2,
			dmgMax = 28,
			dmgMin = 12,
			distMax = 12f,
			distMin = 24f,
			firerate = 450,
			velocity = 10f,
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
			pellets = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0.4f
		}},
		{"M203", new GunParam{
			id = "M203",
			type = Weapon.Type.GRENADE_LAUNCHER,
			caliber = "40 mm",
			name = "M203",
			category = -1,
			sprite = -1,
			dmgMax = 100,
			dmgMin = 50,
			distMax = 0.5f,
			distMin = 2f,
			firerate = 60,
			velocity = 6f,
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
			pellets = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0.5f
		}},
		{"M870MCS", new GunParam{
			id = "M870MCS",
			type = Weapon.Type.SHOTGUN,
			caliber = ".12",
			name = "M870 MCS",
			category = -1,
			sprite = -1,
			dmgMax = 20,
			dmgMin = 10,
			distMax = 3f,
			distMin = 9f,
			firerate = 80,
			velocity = 7f,
			clip = 4,
			ammo = 12,
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
			pellets = 5,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0.6f
		}},
		{"M870", new GunParam{
			id = "M870",
			type = Weapon.Type.SHOTGUN,
			caliber = ".12",
			name = "M870",
			category = 0,
			sprite = 2,
			dmgMax = 20,
			dmgMin = 10,
			distMax = 5f,
			distMin = 12f,
			firerate = 80,
			velocity = 8f,
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
			pellets = 5,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0.6f
		}},
		{"M1914", new GunParam{
			id = "M1914",
			type = Weapon.Type.MACHINEGUN,
			caliber = ".30-06",
			name = "Browning .30",
			category = 0,
			sprite = 0,
			dmgMax = 33,
			dmgMin = 20,
			distMax = 5f,
			distMin = 20f,
			firerate = 450,
			velocity = 13f,
			clip = 500,
			ammo = 2000,
			recoil = 2f,
			recoilMax = 10f,
			recoilReduce = 20f,
			burst = 0,
			burstDelay = 0f,
			range = 100f,
			prj = "Bullet",
			crit = 2f,
			aim = 1f,
			aimFallWalk = 1f,
			aimFallTurn = 1f,
			angle = 0f,
			pellets = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 1.3f
		}},
		{"Knife", new GunParam{
			id = "Knife",
			type = Weapon.Type.MELEE,
			caliber = null,
			name = "Knife",
			category = -1,
			sprite = 1,
			dmgMax = 100,
			dmgMin = 100,
			distMax = 2f,
			distMin = 2f,
			firerate = 60,
			velocity = 0f,
			clip = 0,
			ammo = 0,
			recoil = 0f,
			recoilMax = 0f,
			recoilReduce = 0f,
			burst = 0,
			burstDelay = 0f,
			range = 2f,
			prj = null,
			crit = 0f,
			aim = 0f,
			aimFallWalk = 0f,
			aimFallTurn = 0f,
			angle = 0f,
			pellets = 0,
			mod = new DamageModifier(1,0,0,0,0,0,0,0,0),
			reload = 0f
		}},
		{"Grenade", new GunParam{
			id = "Grenade",
			type = Weapon.Type.GRENADE,
			caliber = null,
			name = "Grenade",
			category = -1,
			sprite = 1,
			dmgMax = 100,
			dmgMin = 50,
			distMax = 0.5f,
			distMin = 2f,
			firerate = 60,
			velocity = 4f,
			clip = 0,
			ammo = 0,
			recoil = 0f,
			recoilMax = 0f,
			recoilReduce = 0f,
			burst = 0,
			burstDelay = 0f,
			range = 100f,
			prj = "Grenade",
			crit = 0f,
			aim = 0f,
			aimFallWalk = 0f,
			aimFallTurn = 0f,
			angle = 0f,
			pellets = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			reload = 0f
		}}
	};

	public class AttachmentParam{
		public string id;
		public string name;
		public string slot;

		public AttachmentParam clone(){
			return new AttachmentParam(){
				id = id,
				name = name,
				slot = slot
			};
		}
	}

	public static Dictionary<string, AttachmentParam> attachmentParam = new Dictionary<string, AttachmentParam>{
		{"M203", new AttachmentParam{
			id = "M203",
			name = "M203",
			slot = "underslug"
		}},
		{"M870MCS", new AttachmentParam{
			id = "M870MCS",
			name = "M870",
			slot = "underslug"
		}},
		{"RedDot", new AttachmentParam{
			id = "RedDot",
			name = "Red Dot",
			slot = "scope"
		}},
		{"ACOG", new AttachmentParam{
			id = "ACOG",
			name = "ACOG",
			slot = "scope"
		}},
		{"Optical8x", new AttachmentParam{
			id = "Optical8x",
			name = "8X Optical",
			slot = "scope"
		}}
	};
}