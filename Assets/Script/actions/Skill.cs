using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Action{
	override public void init(GameObject cst){
		base.init(cst);
		Caster casterComponent = caster.GetComponent<Caster> ();
		lv =  casterComponent != null && casterComponent.skills.ContainsKey(id) ? casterComponent.skills[id] : 0;
	}
}