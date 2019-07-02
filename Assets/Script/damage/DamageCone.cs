using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCone : DamageDealer{
	private float arc;
	private float range;
	
	public DamageCone(float arc, float range){
		this.arc = arc;
		this.range = range;
	}
	
	public virtual void deal(Damage damage, GameObject target = null){
		GameObject attacker = damage.attacker;
		Collider[] collisions = Physics.OverlapSphere(attacker.transform.position, range);
		for (int i = 0; i < collisions.Length; i++){
			GameObject o = collisions[i].gameObject;
			if(o == attacker)
				continue;
			Vector3 direction = o.transform.position - attacker.transform.position;
			float angle = Vector3.Angle(attacker.transform.rotation * Vector3.forward, direction);
			if (angle <= arc){
				Statuses.processHit(damage, attacker, o);
			}
		}
//		if (param.prj != ""){
//			GameObject projectilePrefab = Resources.Load<GameObject>("projectiles/" + param.prj);
//			GameObject o = Instantiate(projectilePrefab, spawn.position, transform.rotation * Quaternion.Euler(0, angle, 0));
//			Projectile prj = o.GetComponent<Projectile>();
//
//			prj.init(gameObject, getDamage(gameObject));
//		}
//		shotAt = Time.time;
//		float recoilAngle = Random.Range(-_recoil, _recoil);
//		if(wdata.param.pellets > 1) {
//			for (int i = 0; i < wdata.param.pellets; i++) {
//				spawnProjectile(wdata.param.angle * i / (wdata.param.pellets - 1) + recoilAngle);
//			}
//		} else {
//			spawnProjectile(recoilAngle);
//		}
//
//		_recoil = Mathf.Min(_recoil + wdata.param.recoil, wdata.param.recoilMax);
//		_burst++;
//		wdata.clip--;
//		_justShot = true;
//		if (wdata.param.burst > 0 && _burst % wdata.param.burst == 0) {
//			shotAt += wdata.param.burstDelay;
//		}
	}
}
