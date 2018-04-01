using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public string gunID;
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

	public bool reload(){
		if (_ammo == 0)
			return false;
		if (_ammo > param.clip) {
			_clip = param.clip;
			_ammo -= param.clip;
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
			if (_shooting) {
				if (ready) {
					shotAt = Time.time - shotTime;
				}
			} else {
				_burst = 0;
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
			return _burst;
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
	void Start () {
		param = GameParams.gunParam[gunID];
		_clip = param.clip;
		_ammo = param.ammo;
		shotTime = 60f / param.firerate;
	}
	
	// Update is called once per frame
	void Update () {
		_justShot = false;
		if (_shooting) {
			while (ready && _clip > 0) {
				doFire();
			}
			if (_clip == 0 || (param.burst > 0 && _burst >= param.burst))
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
	}
}
