using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class DragonBonesBrain : MonoBehaviour{

	public UnityArmatureComponent armature;
	public Brain brain;
	
	private static Dictionary<string, int> eventMap = new Dictionary<string, int>(){
		{EventObject.LOOP_COMPLETE, Label.STOP},
		{"hit", Label.ATTACK},
		{"chain", Label.CHAIN}
	};
	
	void OnEnable () {
		armature.AddDBEventListener(EventObject.LOOP_COMPLETE, onAnimation);
		armature.AddDBEventListener("hit", onAnimation);
		armature.AddDBEventListener("chain", onAnimation);
	}
	
	void OnDisable () {
		armature.RemoveDBEventListener(EventObject.COMPLETE, onAnimation);
		armature.RemoveDBEventListener("hit", onAnimation);
		armature.RemoveDBEventListener("chain", onAnimation);
	}
	
	private void onAnimation (string type, EventObject e) {
		brain.onAnimation(eventMap[type]);
	}
}
