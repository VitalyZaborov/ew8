using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	private const float DROP_FORCE = 100.0f;
	public GameObject weaponPickup;
	public int skin;
	public string[] weapons;
	public Weapon.WeaponData[] weaponsArray;
	private int wIndex = -1;
	private Animator animator;
	private Weapon weapon;

	void Start () {
		animator = GetComponent<Animator> ();
		weapon = GetComponent<Weapon> ();
		weaponsArray = new Weapon.WeaponData[weapons.Length];
		for(int i = 0; i < weapons.Length; i++) {
			string gunID = weapons[i];
			GameParams.GunParam param = GameParams.gunParam[gunID];
			weaponsArray[i] = new Weapon.WeaponData { id = gunID, clip = param.clip, ammo = param.ammo };
		}
		weaponIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
	//	animator.SetBool ("reloading", isReloading);
	}

	public bool takeWeapon(Weapon.WeaponData wdata) {
		if (weaponsArray[wdata.param.category] != null)
			return false;
		weaponsArray[wdata.param.category] = wdata;
		return true;
	}
	public bool dropWeapon(int index = -1) {
		if (index == -1)
			index = wIndex;
		// Can't drop weapon he dont have and the last weapon in the list
		if(weaponsArray[index] == null || index == weaponsArray.Length - 1)
			return false;
		GameObject o = Instantiate(weaponPickup, transform.position, transform.rotation);
		WeaponPickup pickup = o.GetComponent<WeaponPickup>();
		Rigidbody rb = o.GetComponent<Rigidbody>();
		pickup.setWeapon(weaponsArray[index], gameObject);
		rb.AddRelativeForce(transform.rotation * Vector3.forward * DROP_FORCE);
		weaponsArray[index] = null;
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
			weapon.weapon = weaponsArray[wIndex];
		}
	}
	public int getNextWeaponIndex() {
		for (int i = wIndex + 1; i != wIndex; i++) {
			if (i >= weaponsArray.Length)
				i = 0;
			if (weaponsArray[i] != null)
				return i;
		}
		return wIndex;
	}
	public int getPrevWeaponIndex() {
		for (int i = wIndex - 1; i != wIndex; i--) {
			if (i < 0)
				i = weaponsArray.Length - 1;
			if (weaponsArray[i] != null)
				return i;
		}
		return wIndex;
	}
}
