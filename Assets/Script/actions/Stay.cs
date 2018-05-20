using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : Action{
	private const float DEFAULT_DURATION = 0.5f;
	private Coroutine routine;
	private float duration;

	public Stay(float duration = DEFAULT_DURATION) {
		this.duration = duration;
	}
	override public bool intercept(){
		brain.StopCoroutine(routine);
		return true;
	}
	override public void perform(GameObject trg){
		base.perform(trg);
		routine = brain.StartCoroutine(onRoutine());
	}
	private IEnumerator onRoutine() {
		yield return new WaitForSeconds(duration);
		complete();
	}
	protected override void complete() {
		brain.StopCoroutine(routine);
		base.complete();
	}
}