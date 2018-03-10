using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : Action{
	override public bool intercept(){return true;}
	override public void perform(GameObject trg){
		base.perform(trg);
		animator.Play("idle");
	}
	override public void onAnimation(int param = 0){
		complete();	//Thinks every second when stays
	}
}