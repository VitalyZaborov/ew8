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
	public int min;
	public int max;
	public bool useAtk = true;
	public List<Status> statusEffects;
	public GameObject attacker;
	public float crit = 0;	//Critical strike chance, precent, 0-100++
	public float crit_mod = 1;
	
	public void addStatus(Status st){
		if(statusEffects == null){
			statusEffects = new List<Status>();
		}
		statusEffects.Add(st); //Нет проверки на добавление двух одинаковых статусов, так как они сложатся при наложении их на цель
	}

	public int getDamageValue() {
		return Random.Range(min, max + 1);
	}
	public int calculate(Health health, bool crit){
		return (int) Mathf.Max(0, getDamageValue() * mod.getModifier(health.resist) * (crit ? crit_mod : 1) - (crit || health.defense <= 0 ? 0 : health.defense));
	}
}
