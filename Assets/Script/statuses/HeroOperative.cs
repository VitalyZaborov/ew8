using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOperative : Status, IHitEffect, IDurationEffect {

	private const float STEALTH_TIMEOUT = 2.0f;
	private const float STEALTH_RANGE = 8.0f;
	private const float DODGE_CHANCE = 0.50f;

	private const float SMG_BONUS = 1.20f;
	private const float PISTOL_BONUS = 1.50f;
	private const float PISTOL_AIM_BONUS = 1.80f;

	private float idleTime;
	private Vector3 prevPosition;
	private Animator animator;
	private Weapon weapon;
	private Unit unit;
	private Statuses statuses;

	override public void apply(GameObject gameObject) {
		base.apply(gameObject);
		animator = gameObject.GetComponent<Animator>();
		weapon = gameObject.GetComponent<Weapon>();
		unit = gameObject.GetComponent<Unit>();
		statuses = owner.GetComponent<Statuses>();

		idleTime = Time.time;

		weapon.evWeaponChanged += onWeaponChanged;
		onWeaponChanged(owner, null, weapon.weapon);
	}

	override public void remove() {
		weapon.evWeaponChanged -= onWeaponChanged;
		onWeaponChanged(owner, weapon.weapon, null);
		base.remove();
	}

	override public void add(Status status) {

	}

	public bool update(float dt) {
		if (weapon.shooting) {
			statuses.remove("Stealth");
			idleTime = Time.time;
		} else {
			if(Vector3.Distance(prevPosition, owner.transform.position) > 0.1) {
				prevPosition = owner.transform.position;
				idleTime = Time.time;				
			} else if (Time.time - idleTime > STEALTH_TIMEOUT && !statuses.has("Stealth")) {
				statuses.add(new Stealth(STEALTH_RANGE));
			}
		}
		prevPosition = owner.transform.position;
		return true;
	}

	public bool onHit(Projectile projectile, Damage damage, GameObject attacker, GameObject target) {
		return animator.GetInteger(Unit.ANIMATION) == (int)Unit.Animation.SPRINT && Random.value < DODGE_CHANCE;
	}

	private void onWeaponChanged(GameObject o, Weapon.WeaponData previous, Weapon.WeaponData current) {
		if (previous != null) {
			if (previous.param.type == Weapon.Type.SMG) {
				previous.param.reload *= SMG_BONUS;
				unit.speed /= SMG_BONUS;
			} else if (previous.param.type == Weapon.Type.PISTOL) {
				previous.param.reload *= PISTOL_BONUS;
				previous.param.aim *= PISTOL_AIM_BONUS;
				unit.speed /= PISTOL_BONUS;
			}
		}
		if (current != null) {
			if (current.param.type == Weapon.Type.SMG) {
				current.param.reload /= SMG_BONUS;
				unit.speed *= SMG_BONUS;
			} else if (current.param.type == Weapon.Type.PISTOL) {
				current.param.reload /= PISTOL_BONUS;
				current.param.aim /= PISTOL_AIM_BONUS;
				unit.speed *= PISTOL_BONUS;
			}
		}
	}
}