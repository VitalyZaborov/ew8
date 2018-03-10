using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameArea : MonoBehaviour{
	public const float VISION_RANGE = 10;
	private List<GameObject> units;

	GameArea(){
		units = new List<GameObject>();
	}

	public void Start(){
		
	}

	public void addUnit(GameObject unit){
		Debug.Assert (!units.Contains (unit));
		Debug.Log("GameArea add " + unit.ToString() +" to " + units.Count.ToString());
		Unit u = unit.GetComponent<Unit>();
		u.playerControllable = unit.tag == "Player";
		units.Add (unit);
	}

	public void removeUnit(GameObject unit){
		Debug.Assert (units.Contains (unit));
		Debug.Log("GameArea remove " + unit.ToString() + " from " + units.Count.ToString());
		units.Remove (unit);
	}

	public List<GameObject> getVisibleUnits(GameObject target){
		Unit unit = target.GetComponent<Unit>();
		List<GameObject> result = new List<GameObject>();
		for(int i = 0; i < units.Count; i++){
			GameObject obj = units[i];
			if (unit.canSee(obj)) {
				result.Add(obj);
			}
		}
		Debug.Log("GameArea get " + units.Count.ToString());
		return result;
	}
}