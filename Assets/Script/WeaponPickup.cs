using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponPickup : Pickup {

	public Weapon.WeaponData wdata;
	public SpriteRenderer spriteRenderer;

	private GameObject droppedBy;
	private static Sprite[] sprites;

	public void setWeapon(Weapon.WeaponData wd, GameObject o = null) {
		Debug.Assert(wd != null && wd.param != null, "[WeaponPickup] Attempt to set a null weapon to pickup.");
		wdata = wd;
		droppedBy = o;
		
		spriteRenderer.sprite = sprites[wdata.param.sprite];
	}
	private void Awake() {
		if(sprites == null)
			sprites = Resources.LoadAll<Sprite>("guns");
	}
	private void Start() {
		setWeapon(wdata);
	}
	override protected bool pickUp(GameObject o) {
		Rigidbody rb = GetComponent<Rigidbody>();
		if (rb.velocity != Vector3.zero)
			return false;
		Soldier soldier = o.GetComponent<Soldier>();
		return droppedBy != o && soldier != null && soldier.takeWeapon(wdata);
	}
	void OnTriggerExit(Collider other) {
		if (droppedBy == other.gameObject) {
			droppedBy = null;
		}
	}
}
