using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegate {

	public delegate void ComponentEvent(GameObject o);
	public delegate Action ActionGetter(ActionContext cnxt);
}
