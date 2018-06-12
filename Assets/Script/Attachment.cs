using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Attachment {
	public const string SCOPE = "scope";
	public const string UNDERSLUG = "underslug";

	private static Dictionary<string, Attachment> dictionary = new Dictionary<string, Attachment> {
		{	"RedDot",		new RedDot()},
		{	"ACOG",			new ACOG()},
		{	"Optical8x",	new Optical8x()},
		{   "M203",			new M203()},
		{   "M870MCS",		new M870MCS()}
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

	private class RedDot : Attachment {
		private const float AIM = 1.1f;
		private const float AIM_TURN = 0.7f;
		override public void add(Weapon.WeaponData wdata) {
			wdata.param.aim *= AIM;
			wdata.param.aimFallTurn *= AIM_TURN;
		}
		override public void remove(Weapon.WeaponData wdata) {
			wdata.param.aim /= AIM;
			wdata.param.aimFallTurn /= AIM_TURN;
		}
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

	private class Optical8x : Attachment {
		private const float AIM = 1.5f;
		private const float AIM_TURN = 0.5f;
		private const float AIM_WALK = 3f;
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

	private class M870MCS : Attachment {
		override public void add(Weapon.WeaponData wdata) {
			wdata.secondary = new Weapon.WeaponData("M870MCS");
		}
		override public void remove(Weapon.WeaponData wdata) {
			wdata.secondary = null;
		}
	}
}
