using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anyone : Condition{
	
	override public void apply(GameObject owner, GameObject player, List<GameObject> list){
		if (list.Count == 0)
			return;
		GameObject best = list[Random.Range(0, list.Count)];

		list.Clear ();
		list.Add (best);
	}
}