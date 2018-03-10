using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 1;	// RPS
	public GameObject projectile;
	public int maxClip = 10;
	public int maxAmmo = 100;
	public float maxRecoil = 5;
	public float recoilStep = 1;
	public float recoilReduce = 5;
	public float range = 10;
	public int _burst = 0;
	public Transform spawn;

	private int _clip;
	private int _ammo;
	private float _recoil;
	private float shotAt;
	private float shotTime;
	private bool _shooting;
	private bool _justShot;

	public bool reload(){
		if (_ammo == 0)
			return false;
		if (_ammo > maxClip) {
			_clip = maxClip;
			_ammo -= maxClip;
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

	public int ammo
	{
		get
		{
			return _ammo;
		}
	}

	public float recoil {
		get {
			return _recoil;
		}
	}

	public bool justShot {
		get {
			return _justShot;
		}
	}

	// Use this for initialization
	void Start () {
		_clip = maxClip;
		_ammo = maxAmmo;
		shotTime = 1 / fireRate;
	}
	
	// Update is called once per frame
	void Update () {
		_justShot = false;
		if (_shooting) {
			while (ready && _clip > 0) {
				doFire();
			}
			if (_clip == 0)
				shooting = false;
		} else {
			if(_recoil > 0){
				_recoil = Mathf.Max (0, _recoil - Time.deltaTime * recoilReduce);
			}
		}
	}

	private void doFire(){
		shotAt = Time.time;
		GameObject o = Instantiate (projectile, spawn.position, transform.rotation * Quaternion.Euler(0, Random.Range(-_recoil, _recoil), 0));
		Projectile prj = o.GetComponent<Projectile> ();
		prj.init (gameObject);
		_recoil = Mathf.Min(_recoil + recoilStep, maxRecoil);
		_burst++;
		_clip--;
		_justShot = true;
	}
}
