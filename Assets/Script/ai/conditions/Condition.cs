using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition{
	private static readonly string [] SIGNS = {"=", ">", "<", ">=", "<="};
	public Condition(){
		
	}

	public virtual void apply(GameObject owner, GameObject player, List<GameObject> list){
		
	}
	public static Condition[] getConditions(string raw){
		string[] parts = raw.Split (',');
		Condition[] result = new Condition[parts.Length];
		for (int i = 0; i < parts.Length; i++) {
			result [i] = getCondition (parts [i]);
		}
		return result;
	}
	private static Condition getCondition(string raw){
		string criteria = raw;
		string sign = "";
		string param = "";
		for (int i = 0; i < SIGNS.Length; i++) {
			string s = SIGNS [i];
			int index = raw.IndexOf (s);
			if (index >= 0) {
				criteria = raw.Substring (0, index);
				sign = s;
				param = raw.Substring (index + s.Length);
				break;
			}
		}

		switch (criteria) {
		// Terminal
	//	case "best": return new Best();
		case "nearest": return new Nearest();
		case "furthest": return new Furthest();
	//	case "max_sp": return new MaxSP();
	//	case "min_sp": return new MinSP();
		case "max_hp": return new MaxHP();
		case "min_hp": return new MinHP();
	//	case "weakest": return new Weakest();
	//	case "strongest": return new Strongest();
			//	case "max_def": return getHighestDef(group,un);
			//	case "min_def": return getLowestDef(group,un);
			//	case "max_mdef": return getHighestMdef(group,un);
			//	case "min_mdef": return getLowestMdef(group,un);
	//	case "fastest": return getFastest(group,un);
	//	case "slowest": return getSlowest(group,un);
			//	case "max_eva": return getMaxEvasion(group,un);
			//	case "min_eva": return getMinEvasion(group,un);
			//	case "max_buff": return getMaxBuff(group,un);
			//	case "min_buff": return getMinBuff(group,un);
			//	case "max_debuff": return getMaxDebuff(group,un);
			//	case "min_debuff": return getMinDebuff(group,un);
		case "random": return new Anyone();
		case "self": return new Self();
		case "player": return new Player();
			
		case "dist": return new Distance(sign, param);
		case "hp": return new HP(sign, param);
	//	case "sp": return new SP(sign, param);
	//	case "att": return new Attackers(sign, param);
		case "ally": return new Ally();
		case "enemy": return new Enemy();
		case "clip": return new Clip(sign, param);
		case "ammo":return new Ammo(sign, param);
		case "recoil":return new Recoil(sign, param);
	//	case "attacks_me": return owner.attackedBy(un);
	//	case "prs": return Condition.compare(un.resist.physical*100,cnd.value,cnd.sign);
	//	case "mrs": return Condition.compare(un.resist.magical*100,cnd.value,cnd.sign);
	//	case "status": return Condition.compare(un.status[cnd.value]?1:0,1,cnd.sign);
			//	case "debuff": return Condition.compare(un.getDebuffsAmount(),cnd.value,cnd.sign);
			//	case "buff": return Condition.compare(un.getBuffsAmount(),cnd.value,cnd.sign);
			//	case "prev_act": return un.brain.history(?)==cnd.sign;
	//	case "rare": return Condition.compare(un.st.rare,cnd.value,cnd.sign);
		}
		Debug.Assert(false, "No such criteria:" + criteria + " for " + raw);
		return null;
	}
}