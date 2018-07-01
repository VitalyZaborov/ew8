using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefenseEffect {
	void onDefense(Damage damage, GameObject attacker, GameObject target);
}
