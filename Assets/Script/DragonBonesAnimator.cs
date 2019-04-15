using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class DragonBonesAnimator : StateMachineBehaviour{

	public string animation = "stay";
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		UnityArmatureComponent armature = animator.gameObject.GetComponent<UnityArmatureComponent>();
		armature.animation.FadeIn(animation, stateInfo.length, 0);
	}
}
