using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimIndicator : HUDElement {

	public RectTransform imageTransform;
	private WeaponHandling handling;

	override protected void Start() {
		base.Start();
		handling = target.GetComponent<WeaponHandling>();
	}

	private void Update() {
		float scale = 1.0f - handling.aim;
		imageTransform.localScale = new Vector3(scale, scale, scale);
	}
}
