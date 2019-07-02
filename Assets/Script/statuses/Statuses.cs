using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Statuses : MonoBehaviour {

	public List<IHitEffect> hitEffects = new List<IHitEffect>();
	public List<IAttackEffect> attackEffects = new List<IAttackEffect>();
	public List<IDefenseEffect> defenseEffects = new List<IDefenseEffect>();
	public List<IHealEffect> healEffects = new List<IHealEffect>();
	private List<IDurationEffect> durationEffects = new List<IDurationEffect>();
	private Dictionary<string, Status> statuses = new Dictionary<string, Status>();

	public string[] initial;

	private void Start() {
		foreach (string st in initial) {
			Type t = Type.GetType(st);
			Status status = Activator.CreateInstance(t) as Status;
			Debug.Assert(status != null, "No such status: " + st);
			add(status);
		}
	}
	public void add(Status status) {
		if (statuses.ContainsKey(status.id)) {
			statuses[status.id].add(status);
			return;
		}
		statuses[status.id] = status;
		status.apply(gameObject);
		if (status is IHitEffect)
			hitEffects.Add(status as IHitEffect);
		if (status is IAttackEffect)
			attackEffects.Add(status as IAttackEffect);
		if (status is IDefenseEffect)
			defenseEffects.Add(status as IDefenseEffect);
		if (status is IHealEffect)
			healEffects.Add(status as IHealEffect);
		if (status is IDurationEffect)
			durationEffects.Add(status as IDurationEffect);
	}

	public void remove(string statusId) {
		if (!statuses.ContainsKey(statusId)) {
			return;
		}
		Status status = statuses[statusId];
		status.remove();
		statuses.Remove(status.id);
		if (status is IHitEffect)
			hitEffects.Remove(status as IHitEffect);
		if (status is IAttackEffect)
			attackEffects.Remove(status as IAttackEffect);
		if (status is IDefenseEffect)
			defenseEffects.Remove(status as IDefenseEffect);
		if (status is IHealEffect)
			healEffects.Remove(status as IHealEffect);
		if (status is IDurationEffect)
			durationEffects.Remove(status as IDurationEffect);
	}

	public bool has(string statusId) {
		return statuses.ContainsKey(statusId);
	}

	// Update is called once per frame
	private void Update() {
		for (int i = durationEffects.Count - 1; i >= 0; i--){
			if (!durationEffects[i].update(Time.deltaTime)) {
				durationEffects.RemoveAt(i);
			}
		}
	}

	public static bool processProjectile(Projectile projectile, Damage damage, GameObject attacker, GameObject target) {
		Statuses statuses = target.GetComponent<Statuses>();
		if (statuses != null) {
			foreach (IHitEffect effect in statuses.hitEffects) {
				if (effect.onHit(projectile, damage, attacker, target)) {
					return false;
				}
			}
		}

		return processHit(damage, attacker, target);
	}

	public static bool processHit(Damage damage, GameObject attacker, GameObject target) {
				
		// Can't deal damage to an object with no health
		Health targetHealth = target.GetComponent<Health>();
		if (targetHealth == null){
			return false;
		}
		
		// Attack Effects
		Statuses attackerStatuses = attacker.GetComponent<Statuses>();
		if (damage.useAtk && attackerStatuses != null) {
			foreach (IAttackEffect effect in attackerStatuses.attackEffects) {
				effect.onAttack(damage, attacker, target);
			}
		}
		
		// Defense Effects
		Statuses targetStatuses = target.GetComponent<Statuses>();
		if (targetStatuses != null) {
			foreach (IDefenseEffect effect in targetStatuses.defenseEffects) {
				effect.onDefense(damage, attacker, target);
			}
		}
		
		// Damage calculation
		bool crit = Util.percent(damage.crit);
		int totalDamage = damage.calculate(targetHealth, crit);
		targetHealth.receiveDamage(totalDamage);
		
		// Apply damage statuses
		if(targetHealth.isAlive){
			if(damage.statusEffects != null){
				foreach (Status st in damage.statusEffects) {
					st.apply(target);
				}
			}
		}
		
		//After Effects
		if (damage.useAtk && attackerStatuses != null) {
			foreach (IAttackEffect effect in attackerStatuses.attackEffects) {
				effect.afterAttack(damage, attacker, target, totalDamage, crit);
			}
		}
		
		return true;
	}
}
