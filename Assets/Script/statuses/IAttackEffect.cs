using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackEffect {
	void onAttack(Damage damage, GameObject attacker, GameObject target);
}