using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DamageDealer {	

	public static DamageDealer ARC_30 = new DamageCone(30f, 1f);
	public static DamageDealer ARROW = new DamageCone(30f, 1f);	//TODO: replace with arrow
	
	protected void spawnProjectile(Damage damage, GameObject owner, GameObject projectile){
		NavMeshAgent nma = owner.GetComponent<NavMeshAgent>();
		SphereCollider sc = owner.GetComponent<SphereCollider>();
		float radius = nma != null ? nma.radius : sc != null ? sc.radius : 0f;

		Vector3 position = owner.transform.position;
		Quaternion rotation = owner.transform.rotation;
		
		if (radius != 0f){
			position += rotation * Vector3.forward * radius;
		}
		
		GameObject o = GameObject.Instantiate(projectile, position, rotation);
		Projectile prj = o.GetComponent<Projectile>();
		prj.init(owner, damage);
	}
	
	public virtual void deal(Damage damage, GameObject target = null){
		
	}
}
