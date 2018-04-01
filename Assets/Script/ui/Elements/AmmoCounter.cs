using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : HUDElement {

	public UnityEngine.UI.Text clip;
	public UnityEngine.UI.Text ammo;
	private Weapon weapon;

	override protected void Start() {
		base.Start();
		weapon = target.GetComponent<Weapon>();
	}

	private void Update() {
		if(clip != null)
			clip.text = weapon.clip.ToString();
		if (ammo != null)
			ammo.text = weapon.ammo.ToString();
	}
}
