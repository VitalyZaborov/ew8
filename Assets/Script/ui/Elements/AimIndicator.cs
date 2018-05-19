using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIndicator : HUDElement {

	public RectTransform imageTransform;
	private Weapon weapon;

	override protected void Start() {
		base.Start();
		weapon = target.GetComponent<Weapon>();
	}

	private void Update() {
		float scale = 1.0f - weapon.aim;
		imageTransform.localScale = new Vector3(scale, scale, scale);
	}
}
