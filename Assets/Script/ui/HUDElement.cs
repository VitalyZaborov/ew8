using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDElement : MonoBehaviour {

	protected GameObject target;

	virtual protected void Start () {
		target = transform.parent.gameObject.GetComponent<HUD>().target;
	}
}
