using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameArea : MonoBehaviour{
	public const float VISION_RANGE = 30;
	private List<GameObject> units;

	GameArea(){
		units = new List<GameObject>();
	}

	public void Start(){
		
	}

	public void addUnit(GameObject unit){
		Debug.Assert (!units.Contains (unit));
	//	Debug.Log("GameArea add " + unit.ToString() +" to " + units.Count.ToString());
		Unit u = unit.GetComponent<Unit>();
		units.Add (unit);
	}

	public void removeUnit(GameObject unit){
		Debug.Assert (units.Contains (unit));
	//	Debug.Log("GameArea remove " + unit.ToString() + " from " + units.Count.ToString());
		units.Remove (unit);
	}
}