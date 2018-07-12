using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealth : Status {
	private Unit unit;
	private float range = 0;

	public Stealth() {
	}

	public Stealth(float range) {
		this.range = range;
	}

	override public void apply(GameObject gameObject) {
		base.apply(gameObject);
		unit = gameObject.GetComponent<Unit>();
		unit.visibilityRange = range;
		setAlpha(0.5f);
	}

	override public void remove() {
		unit.visibilityRange = float.MaxValue;
		setAlpha(1.0f);
		base.remove();
	}

	private void setAlpha(float value) {
		SpriteRenderer[] renderers = owner.transform.GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer renderer in renderers) {
			renderer.material.color = new Color(1, 1, 1, value);
		}
	}
}