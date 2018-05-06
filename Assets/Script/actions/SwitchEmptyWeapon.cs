using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEmptyWeapon : SwitchWeapon {
	public SwitchEmptyWeapon(): base(0) {
	}
	override public void init(GameObject cst, object param = null) {
		base.init(cst, param);
		switch_to = soldier.weaponIndex;
		for (int i = 0; i < soldier.weaponsArray.Length; i++) {
			Weapon.WeaponData wdata = soldier.weaponsArray[i];
			if (soldier.weaponIndex != i && wdata != null && (wdata.ammo > 0 || wdata.clip > 0)) {
				switch_to = i;
				break;
			}
		}
	}
}