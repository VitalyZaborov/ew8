using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour{
	
	public static readonly Weapon DEFAULT_WEAPON = new Weapon("DEFAULT_WEAPON");
	public enum Animation {
		STAY,
		SHOOT,
		CAST,
		DAMAGE,
		RUN,
		ATTACK_1,
		ATTACK_2,
		ATTACK_3,
		ATTACK_4,
		ATTACK_5,
		ATTACK_6
	}
	
	public enum Nation {
		NONE,
		SUN_EMPIRE,
		FOREST_LANDS,
		ELLADA,
		STORMGARD,
		IRON_KINGDOM
	};
	
	public static int ANIMATION = Animator.StringToHash("animation");
	public static int FORWARD = Animator.StringToHash("forward");
	public static int STRAFE = Animator.StringToHash("strafe");

	public uint team;
	public float visibilityRange = float.MaxValue;
	public string characterId;
	public UnityArmatureComponent armature;
	
	public Character character;

	private Weapon weapon;
	private bool frozen = false;
	private Brain brain;
	private GameArea ga;

	Unit(){
	}

	public void Awake(){
		character = new Character(characterId);
		NavMeshAgent nma = GetComponent<NavMeshAgent>();
		if (nma != null){
			nma.radius  = character.characterStats.radius;
		}
		CapsuleCollider collider = GetComponent<CapsuleCollider>();
		if (collider != null){
			collider.radius = character.characterStats.radius;
			collider.height = character.characterStats.height;
			collider.transform.position = new Vector3(0, collider.height / 2, 0);
		}
		weapon = DEFAULT_WEAPON;
		//	health = GetComponent<Health> ();
		brain = GetComponent<Brain> ();
		freeze = false;
		
	//	armature.armature
	}

	public void OnEnable (){
		ga = GameObject.FindGameObjectWithTag("GameArea").GetComponent<GameArea>();
		ga.addUnit(gameObject);
	}

	public void OnDisable (){
		ga.removeUnit(gameObject);
	}

	public bool freeze{	//Без сознания, герой замер, действие не происходит, но статусы действуют
		get{
			return frozen;
		}
		set{
			frozen = value;
			brain.enabled = !frozen;
		}
	}

	public void setWeapon(Weapon wpn){
		weapon = wpn ?? DEFAULT_WEAPON;
	}
	
	public Damage getDamage(){
		GameParams.StatParam stats = character.getStats();
		Damage damage = new Damage{
			min = stats.dmgMin,
			max = stats.dmgMax,
			mod = weapon.weaponParam.mod,
			attacker = gameObject,
			crit = stats.crit,
			crit_mod = stats.critMod
		};
		return damage;
	}

	// ------------------------------------------------------------
	// Accessors
	
	// ------------------------------------------------------------
	public float range => weapon.weaponParam.range;
	public DamageDealer damageDealer => weapon.weaponParam.damager;
	public float speed => character.getStats().speed;
	public float radius => character.characterStats.radius;
	public float height => character.characterStats.height;

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