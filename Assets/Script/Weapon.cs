using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public enum Type {
		PISTOL,
		ASSAULT,
		SMG,
		SHOTGUN,
		GRENADE_LAUNCHER,
		MACHINEGUN
	};

	private const float AIM_LOSE_PER_DEGREE = 0.01f;
	private const float AIM_LOSE_PER_DISTANCE = 1f;

	[System.Serializable]
	public class WeaponData {
		public string id = "";
		public int clip = -1;
		public int ammo = -1;
		public GameParams.GunParam param;
		public string[] attachments;
		public Weapon.WeaponData secondary;

		public WeaponData() {
		}
		public WeaponData(string id) {
			this.id = id;
			init();
		}
		public void init() {
			param = GameParams.gunParam[id].clone();
			if (clip < 0) {
				clip = param.clip;
			}
			if (ammo < 0) {
				ammo = param.ammo;
			}
		}

		public void applyAtachments() {
			foreach (string attachmentID in attachments) {
				Attachment attachment = Attachment.get(attachmentID);
				attachment.add(this);
			}
		}

		public void removeAtachments() {
			foreach (string attachmentID in attachments) {
				Attachment attachment = Attachment.get(attachmentID);
				attachment.remove(this);
			}
		}
	}
	public Transform spawn;

	public event Delegate.WeaponChangeEvent evWeaponChanged;
	public event Delegate.ComponentEvent evReloaded;

	private int _burst;
	private GameObject projectile;
	private WeaponData wdata;
	private float shotAt;
	private float shotTime;
	private bool _shooting;
	private bool _justShot;
	private float _recoil;
	private float _aim;
	private Quaternion prevRotation;
	private Vector3 prevPosition;

	public static int getDamage(GameParams.GunParam param, float crit = 0, float dist = 0) {
		float delta = (dist - param.distMin) / (param.distMax - param.distMin);
		delta = 1 - Mathf.Clamp(delta, 0, 1);
		float total = param.dmgMin + delta * (param.dmgMax - param.dmgMin);
		if (Random.value <= crit)
			total *= param.crit;
		return (int)Mathf.Round(total);
	}

	virtual public WeaponData weapon {
		get {
			return wdata;
		}
		set {
			WeaponData previous = wdata;
			wdata = value;
			_burst = 0;
			enabled = wdata != null;
			if (enabled) {
				shotTime = 60f / wdata.param.firerate;
				projectile = Resources.Load<GameObject>("projectiles/" + wdata.param.prj);
				_aim = 0;
			//	_recoil = 0;
				prevRotation = transform.rotation;
			} else {
				projectile = null;
			}
			if(evWeaponChanged != null)
				evWeaponChanged(gameObject, previous, wdata);
		}
	}

	public bool reload(){
		if (wdata == null)
			return false;
		if (wdata.ammo == 0 || wdata.param.clip == wdata.clip)
			return false;
		if (wdata.ammo > wdata.param.clip) {
			wdata.ammo -= wdata.param.clip - wdata.clip;
			wdata.clip = wdata.param.clip;
		} else {
			wdata.clip = wdata.ammo;
			wdata.ammo = 0;
		}
		if (evReloaded != null)
			evReloaded(gameObject);
		return true;
	}

	public bool shooting {
		get {
			return _shooting;
		}
		set {
			if (_shooting == value || (value && wdata.clip == 0))
				return;
			_shooting = value;
			if (_shooting) {
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
			return wdata != null ? wdata.clip : 0;
		}
	}

	public int maxClip {
		get {
			return wdata != null ? wdata.param.clip : 0;
		}
	}

	public int ammo {
		get {
			return wdata != null ? wdata.ammo : 0;
		}
	}

	public int maxAmmo {
		get {
			return wdata != null ?  wdata.param.ammo : 0;
		}
	}

	public float recoil {
		get {
			return _recoil;
		}
	}

	public float maxRecoil {
		get {
			return wdata.param.recoilMax;
		}
	}

	public float aim {
		get {
			return _aim;
		}
	}

	public bool justShot {
		get {
			return _justShot;
		}
	}

	public float range {
		get {
			return wdata != null ? wdata.param.range : 0;
		}
	}

	public bool firing {
		get {
			return _shooting || (wdata.param.burst > 0 && _burst % wdata.param.burst != 0);
		}
	}

	// Use this for initialization
	void Start() {
	}

	protected void Update () {
		_aim -= Quaternion.Angle(transform.rotation, prevRotation) * wdata.param.aimFallTurn * AIM_LOSE_PER_DEGREE;
		_aim -= Vector3.Distance(transform.position, prevPosition) * wdata.param.aimFallWalk * AIM_LOSE_PER_DISTANCE;
		_aim += Time.deltaTime / wdata.param.aim;
		_aim = Mathf.Clamp(_aim, 0, 1);
		prevPosition = transform.position;
		prevRotation = transform.rotation;

		_justShot = false;
		if (firing) {
			while (ready && wdata.clip > 0) {
				doFire();
			}
			if (wdata.clip == 0)
				shooting = false;
		} else {
			if(_recoil > 0){
				_recoil = Mathf.Max (0, _recoil - Time.deltaTime * wdata.param.recoilReduce);
			}
		}
	}

	private void doFire(){
		shotAt = Time.time;
		float recoilAngle = Random.Range(-_recoil, _recoil);
		if(wdata.param.pellets > 1) {
			for (int i = 0; i < wdata.param.pellets; i++) {
				spawnProjectile(wdata.param.angle * i / (wdata.param.pellets - 1) + recoilAngle);
			}
		} else {
			spawnProjectile(recoilAngle);
		}

		_recoil = Mathf.Min(_recoil + wdata.param.recoil, wdata.param.recoilMax);
		_burst++;
		wdata.clip--;
		_justShot = true;
		if (wdata.param.burst > 0 && _burst % wdata.param.burst == 0) {
			shotAt += wdata.param.burstDelay;
		}
	}

	private void spawnProjectile(float angle) {
		GameObject o = Instantiate(projectile, spawn.position, transform.rotation * Quaternion.Euler(0, angle, 0));
		Projectile prj = o.GetComponent<Projectile>();

		prj.init(gameObject, getDamage(), wdata.param.velocity, _aim);
	}

	private Damage getDamage() {
		return new Damage() {
			value = wdata.param.dmgMax,
			decrease = ((float)wdata.param.dmgMin) / ((float)wdata.param.dmgMax),
			distMin = wdata.param.distMin,
			distMax = wdata.param.distMax,
			mod = wdata.param.mod,
			crit_mod = wdata.param.crit,
			attacker = gameObject
		};
	}
}
