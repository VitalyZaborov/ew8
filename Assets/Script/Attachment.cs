using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Attachment {
	private static Dictionary<string, Attachment> dictionary = new Dictionary<string, Attachment> {
		{	"ACOG",		new ACOG()},
		{   "M203",		new M203()}
	};
	public static Attachment get(string id) {
		return dictionary[id];
	}

	protected Attachment() {

	}
	virtual public void add(Weapon.WeaponData wdata) {

	}
	virtual public void remove(Weapon.WeaponData wdata) {

	}

	private class ACOG : Attachment {
		private const float AIM = 1.2f;
		private const float AIM_TURN = 0.8f;
		private const float AIM_WALK = 1.2f;
		override public void add(Weapon.WeaponData wdata) {
			wdata.param.aim *= AIM;
			wdata.param.aimFallTurn *= AIM_TURN;
			wdata.param.aimFallWalk *= AIM_WALK;
		}
		override public void remove(Weapon.WeaponData wdata) {
			wdata.param.aim /= AIM;
			wdata.param.aimFallTurn /= AIM_TURN;
			wdata.param.aimFallWalk /= AIM_WALK;
		}
	}

	private class M203 : Attachment {
		override public void add(Weapon.WeaponData wdata) {
			wdata.secondary = new Weapon.WeaponData("M203");
		}
		override public void remove(Weapon.WeaponData wdata) {
			wdata.secondary = null;
		}
	}
}
