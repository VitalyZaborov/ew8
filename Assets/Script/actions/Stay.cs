using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : Action{

	public override bool intercept(){
		return true;
	}
	public override bool continious{	// Is it a long lasting action, which can be intercepted for AI checks
		get { return true; }
	}
	override public void perform(GameObject trg){
		base.perform(trg);
		animator.SetInteger(Unit.ANIMATION, (int)Unit.Animation.STAY);
	}
}