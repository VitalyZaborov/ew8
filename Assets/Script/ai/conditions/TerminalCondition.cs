using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TerminalCondition : Condition{
	
	override public void apply(GameObject owner, GameObject player, List<GameObject> list){
		if (list.Count == 0)
			return;
		GameObject best = list[0];

		for (int i = 1; i < list.Count; i++) {
			GameObject unit = list [i];
			if (better (owner, player, best, unit)) {
				best = unit;
			}
		}
		list.Clear ();
		list.Add (best);
	}

	abstract protected bool better (GameObject owner, GameObject player, GameObject best, GameObject unit);
}