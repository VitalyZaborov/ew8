using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StaticCondition{

	override protected GameObject getUnit(GameObject owner, GameObject player){
		return player;
	}
}