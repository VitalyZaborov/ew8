using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

	public enum Type {
		SWORD1H,
		SWORD2H,
		AXE,
		BOW,
		DAGGER,
		CHAKRAM,
		GLOVES,
		FLAIL,
		MACE
	};

	public GameParams.WeaponParam weaponParam;

	public Weapon(string id):base(id){
		GameParams.weaponParam.TryGetValue(id, out weaponParam);
	}
	
	// ------------------------------------------------------------
	// Override
	
	// ------------------------------------------------------------
	public override void onEquipped(Unit target){
		base.onEquipped(target);
		owner.setWeapon(this);
	}

	// ------------------------------------------------------------
	public override void onUnequipped() {
		owner.setWeapon(null);
		base.onUnequipped();
	}
	
	// ------------------------------------------------------------
	public override bool stackable => false;

	// ------------------------------------------------------------
	// Getters
	
	// ------------------------------------------------------------
	public float range => weaponParam.range;
}
