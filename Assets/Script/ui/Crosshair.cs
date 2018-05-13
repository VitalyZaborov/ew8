using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : HUDElement {

	override protected void Start() {
		base.Start();
	}

	// Update is called once per frame
	private void OnGUI() {
		transform.position = Input.mousePosition;
	}
}
