using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroupCondition : Condition{

	override public void apply(GameObject owner, GameObject player, List<GameObject> list){
		for (int i = list.Count - 1; i >= 0 ; i--) {
			GameObject unit = list [i];
			if (!check (owner, player, unit)) {
				list.RemoveAt (i);
			}
		}
	}

	abstract protected bool check (GameObject owner, GameObject player, GameObject unit);
}