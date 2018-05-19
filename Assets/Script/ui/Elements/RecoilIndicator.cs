using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilIndicator : HUDElement {

	public RectTransform imageTransform;
	private Weapon weapon;

	override protected void Start() {
		base.Start();
		weapon = target.GetComponent<Weapon>();
	}

	private void Update() {
		float recoilScale = 0.5f + weapon.recoil * 0.05f;
		imageTransform.localScale = new Vector3(recoilScale, recoilScale, recoilScale);
	}
}
