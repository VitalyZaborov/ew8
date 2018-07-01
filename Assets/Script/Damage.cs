using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {
	public enum Type {
		SLASHING,
		PIERCING,
		BLUNT,
		FIRE,
		FROST,
		LIGHTNING,
		POISON,
		DARK,
		HOLY
	};

	public DamageModifier mod;
	public int value;
	public float decrease;
	public float distMin;
	public float distMax;
	public List<Status> status_effects;
	public GameObject attacker;
	public float crit;	//Critical strike chance, precent, 0-100++
	public float crit_mod;

	/*public Damage(GameObject attacker, Weapon.WeaponData wdata, float crit, DamageModifier mod, float crit_mod) {
		value = wdata.param.dmgMin;
		valueMax = wdata.param.dmgMax;
		distMin = wdata.param.distMin;
		distMax = wdata.param.distMax;
		this.mod = mod;
		this.crit = crit;
		this.crit_mod = crit_mod;
		this.attacker = attacker;
	}*/
	public void addStatus(Status st){
		if(status_effects == null){
			status_effects = new List<Status>();
		}
		status_effects.Add(st); //Нет проверки на добавление двух одинаковых статусов, так как они сложатся при наложении их на цель
	}

	public int getDamageValue(float crit = 0, float dist = 0) {
		float delta = (dist - distMin) / (distMax - distMin);
		delta = 1 - Mathf.Clamp(delta, 0, 1);

		float total = value * (decrease + delta * (1 - decrease));

		if (Random.value <= crit)
			total *= crit_mod;
		return (int)Mathf.Round(total);
	}
}
