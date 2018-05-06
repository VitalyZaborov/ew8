using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	[System.Serializable]
	public class WeaponData {
		public string id;
		public int clip;
		public int ammo;

		public WeaponData() {
			
		}
		public GameParams.GunParam param {
			get {
				return id != "" ? GameParams.gunParam[id] : null;
			}
		}
	}
	private string gunID;
	public Transform spawn;
	public GameObject projectile;

	private GameParams.GunParam param;
	private int _clip;
	private int _ammo;
	private int _burst;
	private float _recoil;
	private float shotAt;
	private float shotTime;
	private bool _shooting;
	private bool _justShot;

	public static int getDamage(GameParams.GunParam param, float dist = 0) {
		float delta = (dist - param.distMin) / (param.distMax - param.distMin);
		delta = 1 - Mathf.Clamp(delta, 0, 1);
		return (int)Mathf.Round(param.dmgMin + delta * (param.dmgMax - param.dmgMin));
	}

	public WeaponData weapon {
		get {
			return new WeaponData { id = gunID, clip = _clip, ammo = _ammo };
		}
		set {
			gunID = value.id;
			param = GameParams.gunParam[gunID];
			_clip = value.clip;
			_ammo = value.ammo;
			_burst = 0;
			shotTime = 60f / param.firerate;
		}
	}

	public bool reload(){
		if (_ammo == 0 || param.clip == _clip)
			return false;
		if (_ammo > param.clip) {
			_ammo -= param.clip - _clip;
			_clip = param.clip;
		} else {
			_clip = _ammo;
			_ammo = 0;
		}
		return true;
	}

	public bool shooting {
		get {
			return _shooting;
		}
		set {
			if (_shooting == value || (value && _clip == 0))
				return;
			_shooting = value;
			if (_shooting && ready) {
				shotAt = Time.time - shotTime;
			}
		}
	}

	protected bool ready {
		get {
			return (Time.time - shotAt) > shotTime;
		}
	}

	public int burst {
		get {
			return _shooting ? _burst : 0;
		}
	}

	public int clip {
		get {
			return _clip;
		}
	}

	public int maxClip {
		get {
			return param.clip;
		}
	}

	public int ammo {
		get {
			return _ammo;
		}
	}

	public int maxAmmo {
		get {
			return param.ammo;
		}
	}

	public float recoil {
		get {
			return _recoil;
		}
	}

	public float maxRecoil {
		get {
			return param.recoilMax;
		}
	}

	public bool justShot {
		get {
			return _justShot;
		}
	}

	public float range {
		get {
			return param.range;
		}
	}

	public GameParams.GunParam gunParam {
		get {
			return param;
		}
	}

	// Use this for initialization
	void Start() {
	}

	// Update is called once per frame
	void Update () {
		_justShot = false;
		if (_shooting || (param.burst > 0 && _burst % param.burst != 0)) {
			while (ready && _clip > 0) {
				doFire();
			}
			if (_clip == 0)
				shooting = false;
		} else {
			if(_recoil > 0){
				_recoil = Mathf.Max (0, _recoil - Time.deltaTime * param.recoilReduce);
			}
		}
	}

	private void doFire(){
		shotAt = Time.time;
		GameObject o = Instantiate (projectile, spawn.position, transform.rotation * Quaternion.Euler(0, Random.Range(-_recoil, _recoil), 0));
		Projectile prj = o.GetComponent<Projectile> ();
		prj.init (gameObject, param.velocity);
		_recoil = Mathf.Min(_recoil + param.recoil, param.recoilMax);
		_burst++;
		_clip--;
		_justShot = true;
		if (param.burst > 0 && _burst % param.burst == 0) {
			shotAt += param.burstDelay;
		}
	}
}
