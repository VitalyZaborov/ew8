using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitEffect {
	bool onHit(Projectile projectile, Damage damage, GameObject attacker, GameObject target);
}