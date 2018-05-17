using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	private const float DROP_FORCE = 100.0f;
	public GameObject weaponPickup;
	public int skin;
	public Weapon.WeaponData[] weapons;
	private int wIndex = -1;
	private Animator animator;
	private Weapon weapon;

	void Start () {
		animator = GetComponent<Animator> ();
		weapon = GetComponent<Weapon> ();
		for (int i = 0; i < weapons.Length; i++) {
			Weapon.WeaponData wdata = weapons[i];
			wdata.init();
			wdata.applyAtachments();
		}
		weaponIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
	//	animator.SetBool ("reloading", isReloading);
	}

	public bool takeWeapon(Weapon.WeaponData wdata) {
		if (weapons[wdata.param.category] != null)
			return false;
		weapons[wdata.param.category] = wdata;
		return true;
	}
	public bool dropWeapon(int index = -1) {
		if (index == -1)
			index = wIndex;
		// Can't drop weapon he dont have and the last weapon in the list
		if(weapons[index] == null || index == weapons.Length - 1)
			return false;
		GameObject o = Instantiate(weaponPickup, transform.position, transform.rotation);
		WeaponPickup pickup = o.GetComponent<WeaponPickup>();
		Rigidbody rb = o.GetComponent<Rigidbody>();
		pickup.setWeapon(weapons[index]);
		rb.AddRelativeForce(transform.rotation * Vector3.forward * DROP_FORCE);
		weapons[index] = null;
		return true;
	}
	public int weaponIndex {
		get {
			return wIndex;
		}
		set {
			if (value == wIndex)
				return;
			wIndex = value;
			Weapon.WeaponData wdata = weapons[wIndex];
			weapon.weapon = wdata;
		}
	}
	public int getNextWeaponIndex() {
		for (int i = wIndex + 1; i != wIndex; i++) {
			if (i >= weapons.Length)
				i = 0;
			if (weapons[i] != null)
				return i;
		}
		return wIndex;
	}
	public int getPrevWeaponIndex() {
		for (int i = wIndex - 1; i != wIndex; i--) {
			if (i < 0)
				i = weapons.Length - 1;
			if (weapons[i] != null)
				return i;
		}
		return wIndex;
	}
}
