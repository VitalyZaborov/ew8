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
		public Item.Slot[] slots;

		public ItemParam clone(){
			return new ItemParam(){
				id = id,
				type = type,
				name = name,
				sprite = sprite,
				Nation = Nation,
				Level = Level,
				price = price,
				slots = slots
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
			price = 0,
			slots = new []{Item.Slot.WEAPON, Item.Slot.SHIELD}
		}},
		{"Redeemer", new ItemParam{
			id = "Redeemer",
			type = Item.Type.WEAPON,
			name = "Redeemer",
			sprite = 0,
			Nation = Unit.Nation.ELLADA,
			Level = 2,
			price = 100,
			slots = new []{Item.Slot.WEAPON, Item.Slot.SHIELD}
		}},
		{"Avenger", new ItemParam{
			id = "Avenger",
			type = Item.Type.WEAPON,
			name = "Avenger",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 3,
			price = 100,
			slots = new []{Item.Slot.WEAPON, Item.Slot.SHIELD}
		}},
		{"Bloodbath", new ItemParam{
			id = "Bloodbath",
			type = Item.Type.WEAPON,
			name = "Bloodbath",
			sprite = 2,
			Nation = Unit.Nation.ELLADA,
			Level = 4,
			price = 100,
			slots = new []{Item.Slot.WEAPON}
		}},
		{"Eagle", new ItemParam{
			id = "Eagle",
			type = Item.Type.WEAPON,
			name = "Eagle",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 5,
			price = 100,
			slots = new []{Item.Slot.WEAPON}
		}},
		{"Coward", new ItemParam{
			id = "Coward",
			type = Item.Type.WEAPON,
			name = "Coward",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 6,
			price = 100,
			slots = new []{Item.Slot.WEAPON}
		}},
		{"Xena", new ItemParam{
			id = "Xena",
			type = Item.Type.WEAPON,
			name = "Xena",
			sprite = 2,
			Nation = Unit.Nation.ELLADA,
			Level = 1,
			price = 100,
			slots = new []{Item.Slot.WEAPON}
		}},
		{"Claws", new ItemParam{
			id = "Claws",
			type = Item.Type.WEAPON,
			name = "Claws",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 2,
			price = 100,
			slots = new []{Item.Slot.WEAPON, Item.Slot.SHIELD}
		}},
		{"Sorrow", new ItemParam{
			id = "Sorrow",
			type = Item.Type.WEAPON,
			name = "Sorrow",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 3,
			price = 100,
			slots = new []{Item.Slot.WEAPON, Item.Slot.SHIELD}
		}},
		{"Bear", new ItemParam{
			id = "Bear",
			type = Item.Type.WEAPON,
			name = "Bear",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 4,
			price = 100,
			slots = new []{Item.Slot.WEAPON, Item.Slot.SHIELD}
		}},
		{"Hood", new ItemParam{
			id = "Hood",
			type = Item.Type.ARMOR,
			name = "Hood",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 1,
			price = 101,
			slots = new []{Item.Slot.ARMOR}
		}},
		{"Plate", new ItemParam{
			id = "Plate",
			type = Item.Type.ARMOR,
			name = "Hood",
			sprite = 1,
			Nation = Unit.Nation.ELLADA,
			Level = 1,
			price = 101,
			slots = new []{Item.Slot.ARMOR}
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
		public float critMod;
		public float aspd;
		public float speed;
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
				critMod = critMod,
				aspd = aspd,
				speed = speed,
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
				critMod = first.critMod + second.critMod,
				aspd = first.aspd + second.aspd,
				speed = first.speed + second.speed,
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
				critMod = first.critMod - second.critMod,
				aspd = first.aspd - second.aspd,
				speed = first.speed - second.speed,
				STR = first.STR - second.STR,
				DEX = first.DEX - second.DEX,
				INT = first.INT - second.INT,
				VIT = first.VIT - second.VIT
			};
		}
	}

	public static Dictionary<string, StatParam> statParam = new Dictionary<string, StatParam>{
		{"DEFAULT_WEAPON", new StatParam{
			id = "DEFAULT_WEAPON",
			dmgMin = 5,
			dmgMax = 10,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 0,
			critMod = 1f,
			aspd = 0f,
			speed = 0f,
			STR = 0,
			DEX = 0,
			INT = 0,
			VIT = 0
		}},
		{"Redeemer", new StatParam{
			id = "Redeemer",
			dmgMin = 19,
			dmgMax = 25,
			DEF = 0,
			HP = 0,
			SP = 0,
			crit = 5,
			critMod = 2f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 2f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 2f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 1f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 1.5f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 1.5f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 2f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 1f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 0f,
			aspd = 0f,
			speed = 0f,
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
			critMod = 1f,
			aspd = 0f,
			speed = 3.5f,
			STR = 16,
			DEX = 12,
			INT = 6,
			VIT = 14
		}}
	};

	public class CharacterParam : GameParam{
		public string id;
		public float radius;
		public float height;
		public int skin;
		public Unit.Nation Nation;

		public CharacterParam clone(){
			return new CharacterParam(){
				id = id,
				radius = radius,
				height = height,
				skin = skin,
				Nation = Nation
			};
		}
	}

	public static Dictionary<string, CharacterParam> characterParam = new Dictionary<string, CharacterParam>{
		{"Barbarian", new CharacterParam{
			id = "Barbarian",
			radius = 2f,
			height = 8f,
			skin = 0,
			Nation = Unit.Nation.ELLADA
		}}
	};
}