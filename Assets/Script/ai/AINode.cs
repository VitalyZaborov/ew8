using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINode{
	public Delegate.ActionGetter action;
	public Condition[] character;
	public Condition[] target;
	public int probability = 100;
/*	AINode(obj:Object){
		action = new ActionNode(obj.a);
		if(obj.c){
			character = new CharacterNode(obj.c);
		}
		if(obj.t){
			target = new CharacterNode(obj.t);
		}
		probability = obj.p || 100;
	}*/
}