﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilIndicator : HUDElement {

	public RectTransform imageTransform;
	private WeaponHandling handling;

	override protected void Start() {
		base.Start();
		handling = target.GetComponent<WeaponHandling>();
	}

	private void Update() {
		float recoilScale = 0.5f + handling.recoil * 0.05f;
		imageTransform.localScale = new Vector3(recoilScale, recoilScale, recoilScale);
	}
}
