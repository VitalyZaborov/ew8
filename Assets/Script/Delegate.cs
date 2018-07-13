using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delegate {

	public delegate void ComponentEvent(GameObject o);
	public delegate void WeaponChangeEvent(GameObject o, Weapon.WeaponData previous, Weapon.WeaponData current);
	public delegate Action ActionGetter(Brain.ActionContext cnxt);
	public delegate void ActionExtra(Brain.ActionContext cnxt);
	public delegate void ActionComplete(Action action);
}
