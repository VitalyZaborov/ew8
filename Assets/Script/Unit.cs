using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour{
	public uint team;

	private bool frozen = false;
	private bool _playerControllable = false;
	private Brain brain;
	private GameArea ga;

	Unit(){
	}

	public void Awake(){
		//	health = GetComponent<Health> ();
		brain = GetComponent<Brain> ();
		freeze = false;
		playerControllable = _playerControllable;
	}

	public void OnEnable (){
		ga = GameObject.FindGameObjectWithTag("GameArea").GetComponent<GameArea>();
		ga.addUnit(gameObject);
	}

	public void OnDisable (){
		ga.removeUnit(gameObject);
	}

	public bool playerControllable
	{   //Без сознания, герой замер, действие не происходит, но статусы действуют
		get
		{
			return _playerControllable;
		}
		set
		{
			_playerControllable = value;
			MoveHuman mh = gameObject.GetComponent<MoveHuman>();
			RotateHuman rh = gameObject.GetComponent<RotateHuman>();
			mh.enabled = rh.enabled = _playerControllable;
			brain.enabled = !(frozen || _playerControllable);
		}
	}

	public bool freeze{	//Без сознания, герой замер, действие не происходит, но статусы действуют
		get{
			return frozen;
		}
		set{
			frozen = value;
			brain.enabled = !(frozen || _playerControllable);
		}
	}

	public List<GameObject> visibleUnits{
		get{
			return ga.getVisibleUnits(gameObject);
		}
	}

	public bool turn(Vector3 position) {
		NavMeshAgent nma = GetComponent<NavMeshAgent>();
		Quaternion targetRotation = Quaternion.LookRotation(position - transform.position);
		float step = nma.angularSpeed * Time.deltaTime;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
		return Quaternion.Angle(transform.rotation, targetRotation) < 0.1;
		/*	float angle = Vector3.Angle(transform.position, position);
			if (angle == 0)
				return true;
			NavMeshAgent nma = GetComponent<NavMeshAgent>();
			Debug.Log("Turn: " + angle.ToString() + transform.position.ToString() + " -> " + position.ToString());
			angle = Mathf.Min(angle, Time.deltaTime * nma.angularSpeed);
			transform.Rotate(Vector3.up, angle);*/
		/*	bool updatePosition = nma.updatePosition;
			bool updateRotation = nma.updateRotation;
			nma.updatePosition = false;
			nma.updateRotation = true;
			nma.SetDestination (position);
			nma.updatePosition = updatePosition;
			nma.updateRotation = updateRotation;*/
	}

	public bool canSee(GameObject unit) {
		if (unit == gameObject)
			return true;

		float distance = Vector3.Distance(transform.position, unit.transform.position);
		if (distance > GameArea.VISION_RANGE) {
		//	Debug.Log("Can't see: too far " + distance.ToString());
			return false;
		}
		//	Debug.DrawRay(transform.Find("MEyes").position, transform.Find("MEyes").transform.forward * ViewDistance);

		if (!Physics.Raycast(transform.position, unit.transform.position - transform.position, distance, 1 << 8)) {
			return true;
		}
		
	//	Debug.Log("Can't see:" + (hit.transform != null && hit.transform.gameObject == gameObject).ToString());
		return false;
	}

	//	==========================================================================================================

	//private var anim:CharacterAnimation;
	//private var renderList:RenderList = new RenderList();

	//	public var st:Creature;

	// Defense component ?
	/*	public DamageModifier resist;
		public int defense;
		public int dodge;*/

	/*	private int mana;
		private float limit = 0;	//Overdrive, percents, [0-1]
		private float died_at = 0;	//replace died_at (time check) with currentAnim check
		*/
	/*	private IGameMechanics mechanics;
		private ArrayList<Unit> visible_units = new ArrayList<Unit();*/
	//	private var sight_cast:Fieldcast;

	/*	private ArrayList<Weapon> weapons = new ArrayList<Weapon>();
		public Dictionary<string, Status> status = new Dictionary<string, Status>();
		public Dictionary<string, float> cooldowns = new Dictionary<string, float>();
		public ArrayList<IAttackEffect> atk_eff = new ArrayList<IAttackEffect>();
		public ArrayList<IDefenseEffect> def_eff = new ArrayList<IDefenseEffect>();
		public ArrayList<IHealEffect> heal_eff = new ArrayList<IHealEffect>();*/

	//	Invokers
	/*	public var onHealthChange:Invoker = new Invoker();
		public var onManaChange:Invoker = new Invoker();
		public var onMiss:Invoker = new Invoker();
		public var onDamageTaken:Invoker = new Invoker();
		public var onHeal:Invoker = new Invoker();
		public var onDeath:Invoker = new Invoker();
		public var onRevive:Invoker = new Invoker();*/

	// Required Components
	//	private Health health;

	//	Components/Render List
	/*	public function addComponent(component:IUnitComponent):void{
			components.push(component);
		}
		public function removeComponent(component:IUnitComponent):void{
			var index:int = components.indexOf(component);
			if(index >= 0) components.splice(index, 1);
		}*/

	//	Damage
	/*	public function getMagicDamage(mod:DamageModifier):Damage{
			var dmg:Damage = st.getMagicDamage();
			dmg.mod = mod;
			dmg.attacker = this;
			return dmg;
		}
		public function receiveDamage(dmg:Damage):Boolean{
			if(!HP){
				return true;	//Мертвые не боятся урона
			}
			var attacker:Unit = dmg.attacker;
			//Attack and defense effects
			if(dmg.useAtk && attacker && attacker.atk_eff){
				for(var i:int=attacker.atk_eff.length-1;i>=0;i--){	//Идем от конца в начало - на случай, если какой-то одноразовый эффект сам себя удалит
					dmg = attacker.atk_eff[i].onAttack(dmg,attacker,this);
				}
			}
			//Evasion calculation
			if(!mechanics.hitOrMiss(dmg.hit, dodge)){	//Widely adjustable
				onMiss.invoke(this, attacker);
				return false;
			}
			if(dmg.useDef && def_eff){
				for(i=def_eff.length-1;i>=0;i--){
					dmg = def_eff[i].onDefense(dmg,attacker,this);
				}
			}
			//Actual damage calculation
			var crit:Boolean = Utils.percent(dmg.crit);
			var total_damage:int = mechanics.calculateDamage(dmg,resist,defense,crit);
			HP = Math.min(maxHP, Math.max(0, HP - total_damage));
			if(total_damage){
				onDamageTaken.invoke(this, attacker, total_damage, crit);
			}
			if(!HP){	//То есть, если помер
				die();
			}else{
				if(dmg.status_effects){
					for(i=0;i<dmg.status_effects.length;i++){
						dmg.status_effects[i].apply(this);
					}
				}
			}
			//after effects
			if(dmg.useAtk && attacker && attacker.atk_eff){
				for(i=attacker.atk_eff.length-1;i>=0;i--){
					attacker.atk_eff[i].afterAttack(dmg,attacker, this,total_damage,crit);
				}
			}
			return isAlive;
		}
		public function receiveHeal(heal_val:int, effect:uint = 0):void{
			if(!HP){
				return;	//Мертвым уже не помочь
			}
			if(heal_eff){
				for(var i:int=heal_eff.length-1;i>=0;i--){
					heal_val = heal_eff[i].onHeal(heal_val, this);
				}
			}
			HP = Math.max(0, Math.min(HP + heal_val, maxHP));
			if(!HP){	//Вдруг зомби помрет от лечения?
				die();
			}
			onHeal.invoke(this, heal_val, effect);

		}
		public function recoverSP(sp_val:int):void{
			SP = Math.max(0, Math.min(SP + sp_val, maxSP));
		}
		public function die():void{
			if(died_at){	//Не выполняем, если die() уже выполнилось раньше
				return;
			}
			died_at = ga.timer;
			onDeath.invoke(this);
			HP = 0;
			removeAllStatuses();
			if(frozen){
				freeze = false;
			}
			brain = null;
			playAnimation("die");
		}
		public function revive():void{
			died_at = 0;
			HP = 1;
			brain = new Brain(this);
			brain.setAI(st.AI);
			anim.playback.pause = false;
			ga.addToUpdateList(this);
			onRevive.invoke(this);
		}
		//	Other
		public function get visible():Boolean{
			return true;
		}
		public function canSee(un:Unit):Boolean{
		//	if(Vector3D.distance(position, un.position) > sightRange) return false;	// + hide range
			return visible_units.indexOf(un) >= 0
		}
		public function toString():String{
			return '[Unit #'+uid+' '+st.name+']';
		}*/
}