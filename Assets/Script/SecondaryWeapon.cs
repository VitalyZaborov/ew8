using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : Weapon {
	override public WeaponData weapon {
		set {
			base.weapon = value;
			enabled = value != null;
		}
	}
}
