using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statuses : MonoBehaviour {

	public List<IHitEffect> hitEffects = new List<IHitEffect>();
	public List<IAttackEffect> attackEffects = new List<IAttackEffect>();
	public List<IDefenseEffect> defenseEffects = new List<IDefenseEffect>();
	public List<IHealEffect> healEffects = new List<IHealEffect>();
	public List<IDurationEffect> durationEffects = new List<IDurationEffect>();

	public Dictionary<string, Status> statuses = new Dictionary<string, Status>();

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

	public void remove(Status status) {
		if (!statuses.ContainsKey(status.id)) {
			return;
		}
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
		Statuses attackerStatuses = attacker.GetComponent<Statuses>();
		if (attackerStatuses != null) {
			foreach (IAttackEffect effect in attackerStatuses.attackEffects) {
				effect.onAttack(damage, attacker, target);
			}
		}
		Statuses targetStatuses = target.GetComponent<Statuses>();
		if (targetStatuses != null) {
			foreach (IDefenseEffect effect in targetStatuses.defenseEffects) {
				effect.onDefense(damage, attacker, target);
			}
		}
		return true;
	}
}
