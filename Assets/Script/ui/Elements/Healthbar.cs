using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : HUDElement {

	private Progressbar bar;
	private Health health;

	override protected void Start() {
		base.Start();
		bar = GetComponent<Progressbar>();
		health = target.GetComponent<Health>();
	}

	private void Update() {
		bar.value = health.value / (float)health.max;
	}
}
