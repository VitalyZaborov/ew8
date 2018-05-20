using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIndicator : HUDElement {

	private UnityEngine.UI.Text text;
	private Weapon weapon;

	override protected void Start() {
		base.Start();
		text = GetComponent<UnityEngine.UI.Text>();
		weapon = target.GetComponent<Weapon>();
	}

	private void Update() {
		text.text = weapon.weapon.param.name;
	}
}
