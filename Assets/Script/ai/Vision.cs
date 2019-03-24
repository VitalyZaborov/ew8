using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

	public bool canSee(GameObject other) {
		if (other == gameObject)
			return true;

		Unit unit = other.GetComponent<Unit>();
		float distance = Vector3.Distance(transform.position, other.transform.position);
		if (distance > Mathf.Min(GameArea.VISION_RANGE, unit.visibilityRange)) {
			//	Debug.Log("Can't see: too far " + distance.ToString());
			return false;
		}
		//	Debug.DrawRay(transform.Find("MEyes").position, transform.Find("MEyes").transform.forward * ViewDistance);

		if (!Physics.Raycast(transform.position, other.transform.position - transform.position, distance, 1 << 8)) {
			return true;
		}
		
		//	Debug.Log("Can't see:" + (hit.transform != null && hit.transform.gameObject == gameObject).ToString());
		return false;
	}
	
	public List<GameObject> visibleUnits{
		get{
			Unit[] units = GameObject.FindObjectsOfType<Unit>();
			List<GameObject> result = new List<GameObject>();
			for(int i = 0; i < units.Length; i++){
				GameObject obj = units[i].gameObject;
				if (canSee(obj)) {
					result.Add(obj);
				}
			}
			return result;
		}
	}
}
