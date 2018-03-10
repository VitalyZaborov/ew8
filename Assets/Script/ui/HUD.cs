using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

	public GameObject target;
	public UnityEngine.UI.Text clip;
	public UnityEngine.UI.Text ammo;
	
	// Update is called once per frame
	void Update () {
		Weapon weapon = target.GetComponent<Weapon>();
		clip.text = weapon.clip.ToString();
		ammo.text = weapon.ammo.ToString();
	}
}
