using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCondition : Condition{
	
	override public void apply(GameObject owner, GameObject player, List<GameObject> list){
		GameObject unit = getUnit (owner, player);
		bool contains = list.Contains(unit);
		list.Clear();
		if (contains)
			list.Add (unit);
	}

	virtual protected GameObject getUnit(GameObject owner, GameObject player){
		return null;
	}
}