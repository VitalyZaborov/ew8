using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegate {
	public delegate Action ActionGetter(Brain.ActionContext cnxt);
	public delegate void ActionExtra(Brain.ActionContext cnxt);
	public delegate void ActionComplete(Action action);
}
