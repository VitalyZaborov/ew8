using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationStatus : Status, IDurationEffect{

	private float duration;

	public DurationStatus(float duration) {
		this.duration = duration;
	}
	public bool update(float dt) {
		duration -= dt;
		return duration > 0;
	}
}
