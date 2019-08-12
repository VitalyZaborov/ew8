using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageDealer {	

	public static DamageDealer ARC_30 = new DamageCone(30f);
	public static DamageDealer ARROW = new DamageCone(30f);	//TODO: replace with arrow
	
	protected void spawnProjectile(Damage damage, GameObject owner, GameObject projectile){
		float radius = Util.getRadius(owner);

		Vector3 position = owner.transform.position;
		Quaternion rotation = owner.transform.rotation;
		
		if (radius != 0f){
			position += rotation * Vector3.forward * radius;
		}
		
		GameObject o = GameObject.Instantiate(projectile, position, rotation);
		Projectile prj = o.GetComponent<Projectile>();
		prj.init(owner, damage);
	}
	
	public virtual void deal(Damage damage, float range, GameObject target = null, uint team = 0){
		
	}
	
	protected Vector3 getPosition(GameObject attacker){
		Vector3 result = attacker.transform.position;
		Unit unit = attacker.GetComponent<Unit>();
		if (unit != null){
			result.y += unit.height / 2;
		}

		return result;
	}
}
