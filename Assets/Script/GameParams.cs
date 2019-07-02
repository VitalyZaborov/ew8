using System.Collections;
using System.Collections.Generic;

public class GameParams{
	public class GameParam{}

	public class WeaponParam : GameParam{
		public string id;
		public Weapon.Type type;
		public int sprite;
		public DamageModifier mod;
		public float range;
		public DamageDealer damager;

		public WeaponParam clone(){
			return new WeaponParam(){
				id = id,
				type = type,
				sprite = sprite,
				mod = mod,
				range = range,
				damager = damager
			};
		}
	}

	public static Dictionary<string, WeaponParam> weaponParam = new Dictionary<string, WeaponParam>{
		{"DEFAULT_WEAPON", new WeaponParam{
			id = "DEFAULT_WEAPON",
			type = Weapon.Type.GLOVES,
			sprite = -1,
			mod = new DamageModifier(0,0,1,0,0,0,0,0,0),
			range = 5f,
			damager = DamageDealer.ARC_30
		}},
		{"Redeemer", new WeaponParam{
			id = "Redeemer",
			type = Weapon.Type.SWORD1H,
			sprite = 0,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 5f,
			damager = DamageDealer.ARC_30
		}},
		{"Avenger", new WeaponParam{
			id = "Avenger",
			type = Weapon.Type.SWORD2H,
			sprite = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 6f,
			damager = DamageDealer.ARC_30
		}},
		{"Bloodbath", new WeaponParam{
			id = "Bloodbath",
			type = Weapon.Type.AXE,
			sprite = 2,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 5f,
			damager = DamageDealer.ARC_30
		}},
		{"Eagle", new WeaponParam{
			id = "Eagle",
			type = Weapon.Type.BOW,
			sprite = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 50f,
			damager = DamageDealer.ARROW
		}},
		{"Coward", new WeaponParam{
			id = "Coward",
			type = Weapon.Type.DAGGER,
			sprite = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 5f,
			damager = DamageDealer.ARC_30
		}},
		{"Xena", new WeaponParam{
			id = "Xena",
			type = Weapon.Type.CHAKRAM,
			sprite = 2,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 25f,
			damager = DamageDealer.ARC_30
		}},
		{"Claws", new WeaponParam{
			id = "Claws",
			type = Weapon.Type.GLOVES,
			sprite = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 25f,
			damager = DamageDealer.ARC_30
		}},
		{"Sorrow", new WeaponParam{
			id = "Sorrow",
			type = Weapon.Type.FLAIL,
			sprite = 1,
			mod = new DamageModifier(1,0,0,0,0,0,0,0,0),
			range = 5f,
			damager = DamageDealer.ARC_30
		}},
		{"Bear", new WeaponParam{
			id = "Bear",
			type = Weapon.Type.MACE,
			sprite = 1,
			mod = new DamageModifier(0,1,0,0,0,0,0,0,0),
			range = 5f,
			damager = DamageDealer.ARC_30
		}}
	};

	public class ItemParam : GameParam{
		public string id;
		public Item.Type type;
		public string name;
		public int sprite;
		public Unit.Nation Nation;
		public int Level;
		public int price;

		public ItemParam clone(){
			return new ItemParam(){
				id = id,
				type = type,
				name = name,
				sprite = sprite,
				Nation = Nation,
				Level = Level,
				price = price
			};
		}
	}

	public static Dictionary<string, ItemParam> itemParam = new Dictionary<string, ItemParam>{
		{"DEFAULT_WEAPON", new ItemParam{
			id = "DEFAULT_WEAPON",
			type = Item.Type.WEAPON,
			sprite = -1,
			Nation = Unit.Nation.NONE,
			Level = 0,
			price = 0
		}},
		{"Redeemer", new ItemParam{
			id = "Redeemer",
			type = Item.Type.WEAPON,
			name = "Redeemer",
			sprite = 0,
			Nation = Unit.Nation.ELLADA,
			Level = 2,
			price = 100
		}},
		{"Avenger", new ItemParam{
			id = "Avenger",
			type = Item.Type.WEAPON,
			name = "Avenger",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 3,
			price = 100
		}},
		{"Bloodbath", new ItemParam{
			id = "Bloodbath",
			type = Item.Type.WEAPON,
			name = "Bloodbath",
			sprite = 2,
			Nation = Unit.Nation.ELLADA,
			Level = 4,
			price = 100
		}},
		{"Eagle", new ItemParam{
			id = "Eagle",
			type = Item.Type.WEAPON,
			name = "Eagle",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 5,
			price = 100
		}},
		{"Coward", new ItemParam{
			id = "Coward",
			type = Item.Type.WEAPON,
			name = "Coward",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 6,
			price = 100
		}},
		{"Xena", new ItemParam{
			id = "Xena",
			type = Item.Type.WEAPON,
			name = "Xena",
			sprite = 2,
			Nation = Unit.Nation.ELLADA,
			Level = 1,
			price = 100
		}},
		{"Claws", new ItemParam{
			id = "Claws",
			type = Item.Type.WEAPON,
			name = "Claws",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 2,
			price = 100
		}},
		{"Sorrow", new ItemParam{
			id = "Sorrow",
			type = Item.Type.WEAPON,
			name = "Sorrow",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 3,
			price = 100
		}},
		{"Bear", new ItemParam{
			id = "Bear",
			type = Item.Type.WEAPON,
			name = "Bear",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 4,
			price = 100
		}}
	};

	public class StatParam : GameParam{
		public string id;
		public int dmgMin;
		public int dmgMax;
		public int DEF;
		public int HP;
		public int SP;
		public int crit;
		public float aspd;
		public float critMod;
		public int STR;
		public int DEX;
		public int INT;
		public int VIT;

		public StatParam clone(){
			return new StatParam(){
				id = id,
				dmgMin = dmgMin,
				dmgMax = dmgMax,
				DEF = DEF,
				HP = HP,
				SP = SP,
				crit = crit,
				aspd = aspd,
				critMod = critMod,
				STR = STR,
				DEX = DEX,
				INT = INT,
				VIT = VIT
			};
		}
		public static StatParam operator+(StatParam first, StatParam second){
			return new StatParam(){
				dmgMin = first.dmgMin + second.dmgMin,
				dmgMax = first.dmgMax + second.dmgMax,
				DEF = first.DEF + second.DEF,
				HP = first.HP + second.HP,
				SP = first.SP + second.SP,
				crit = first.crit + second.crit,
				aspd = first.aspd + second.aspd,
				critMod = first.critMod + second.critMod,
				STR = first.STR + second.STR,
				DEX = first.DEX + second.DEX,
				INT = first.INT + second.INT,
				VIT = first.VIT + second.VIT
			};
		}
		public static StatParam operator-(StatParam first, StatParam second){
			return new StatParam(){
				dmgMin = first.dmgMin - second.dmgMin,
				dmgMax = first.dmgMax - second.dmgMax,
				DEF = first.DEF - second.DEF,
				HP = first.HP - second.HP,
				SP = first.SP - second.SP,
				crit = first.crit - second.crit,
				aspd = first.aspd - second.aspd,
				critMod = first.critMod - second.critMod,
				STR = first.STR - second.STR,
				DEX = first.DEX - second.DEX,
				INT = first.INT - second.INT,
				VIT = first.VIT - second.VIT
			};
		}
	}

	public static Dictionary<string, StatParam> statParam = new Dictionary<string, StatParam>{
		{"Redeemer", new StatParam{
			id = "Redeemer",
			dmgMin = 19,
			dmgMax = 25,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 5,
			aspd = 0f,
			critMod = 2f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Avenger", new StatParam{
			id = "Avenger",
			dmgMin = 17,
			dmgMax = 23,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 5,
			aspd = 0f,
			critMod = 2f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Bloodbath", new StatParam{
			id = "Bloodbath",
			dmgMin = 12,
			dmgMax = 28,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 5,
			aspd = 0f,
			critMod = 2f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Eagle", new StatParam{
			id = "Eagle",
			dmgMin = 50,
			dmgMax = 100,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 10,
			aspd = 0f,
			critMod = 1f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Coward", new StatParam{
			id = "Coward",
			dmgMin = 10,
			dmgMax = 20,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 20,
			aspd = 0f,
			critMod = 1.5f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Xena", new StatParam{
			id = "Xena",
			dmgMin = 10,
			dmgMax = 20,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 25,
			aspd = 0f,
			critMod = 1.5f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Claws", new StatParam{
			id = "Claws",
			dmgMin = 20,
			dmgMax = 33,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 5,
			aspd = 0f,
			critMod = 2f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Sorrow", new StatParam{
			id = "Sorrow",
			dmgMin = 100,
			dmgMax = 100,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 1,
			aspd = 0f,
			critMod = 1f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Bear", new StatParam{
			id = "Bear",
			dmgMin = 50,
			dmgMax = 100,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 0,
			aspd = 0f,
			critMod = 0f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Barbarian", new StatParam{
			id = "Barbarian",
			dmgMin = 0,
			dmgMax = 0,
			DEF = 0,
			HP = 100,
			SP = 20,
			crit = 0,
			aspd = 0f,
			critMod = 1f,
			STR = 16,
			DEX = 12,
			INT = 6,
			VIT = 14
		}}
	};
}