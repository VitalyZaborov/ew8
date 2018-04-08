using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AINode{
	public Delegate.ActionGetter action;
	public Delegate.ActionExtra extra;
	public Condition[] character;
	public Condition[] target;
	public int probability = 100;
}