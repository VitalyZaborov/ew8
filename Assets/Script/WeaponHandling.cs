using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandling : MonoBehaviour {

	private const float AIM_LOSE_PER_DEGREE = 0.01f;
	private const float AIM_LOSE_PER_DISTANCE = 1f;

	private Weapon weapon;
	private float _recoil;
	private float _aim;
	private Quaternion prevRotation;
	private Vector3 prevPosition;

	public float recoil {
		get {
			return _recoil;
		}
	}

	public float maxRecoil {
		get {
			return weapon.weapon != null ? weapon.weapon.param.recoilMax : 0;
		}
	}

	public float aim {
		get {
			return _aim;
		}
	}

	private void Start() {
		weapon = GetComponent<Weapon>();
	}

	private void Update() {
		Weapon.WeaponData wdata = weapon.weapon;
		_aim -= Quaternion.Angle(transform.rotation, prevRotation) * wdata.param.aimFallTurn * AIM_LOSE_PER_DEGREE;
		_aim -= Vector3.Distance(transform.position, prevPosition) * wdata.param.aimFallWalk * AIM_LOSE_PER_DISTANCE;
		_aim += Time.deltaTime / wdata.param.aim;
		_aim = Mathf.Clamp(_aim, 0, 1);
		prevPosition = transform.position;
		prevRotation = transform.rotation;

		if (!weapon.firing) {
			if (_recoil > 0) {
				_recoil = Mathf.Max(0, _recoil - Time.deltaTime * wdata.param.recoilReduce);
			}
		}
	}

	public void addRecoil(float value) {
		Weapon.WeaponData wdata = weapon.weapon;
		_recoil = Mathf.Min(_recoil + value, wdata.param.recoilMax);
	}

	public void reset() {
		_aim = 0;
		_recoil = 0;
		prevRotation = transform.rotation;
	}

}
