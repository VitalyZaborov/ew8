using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self : StaticCondition{

	override protected GameObject getUnit(GameObject owner, GameObject player){
		return owner;
	}
}