using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour{
	//private var anim:CharacterAnimation;
	//private var renderList:RenderList = new RenderList();
	private ArrayList<UnitComponent> components = new ArrayList<IUnitComponent>();	// ???

	public uint team;
//	public var st:Creature;

	// Defense component ?
	public DamageModifier resist;
	public int defense;
	public int dodge;

	private int mana;
	private float limit = 0;	//Overdrive, percents, [0-1]
	private float died_at = 0;	//replace died_at (time check) with currentAnim check
	
	private bool frozen;
	private IGameMechanics mechanics;
	private ArrayList<Unit> visible_units = new ArrayList<Unit();
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
	private Health health;
	private Brain brain;

	Caster(){
	//	mechanics = Config.instance.mechanics;
		brain = GetComponent<Brain>();
	}
	//Overrides
	public void enterArea(GameArea ga){
	//	sight_cast = new Fieldcast(ga.tileMap, Tile.CAN_SEE, grabVisibleUnits);
		ga.addUnit(this);
	}
	override public void leaveArea(){
		removeAllStatuses();
		ga.removeUnit(this);
	}
/*	override protected function onEnterTile(tl:Tile):void{
		super.onEnterTile(tl);
		tl.addUnit(this);
		calculateVisibility();
	}
	override protected function onLeaveTile(tl:Tile):void{
		super.onLeaveTile(tl);
		tl.removeUnit(this);
		clearVisibility();
	}*/
	public void Start(){
		health = GetComponent<Health> ();
		brain = GetComponent<Brain> ();
	}
	public void Update(){
		float dt = Time.deltaTime;
		if(isAlive){
			for(int i=components.length-1;i>=0;i--){
				components[i].update(this, dt);
			}
		}
		if(!frozen){
			if(health.isAlive){
				foreach(string cd in cooldowns){
					cooldowns[cd] -= dt;
					if(cooldowns[cd]<=0)delete(cooldowns[cd]);
				}
			}
		}
	}
	//	Components/Render List
/*	public function addComponent(component:IUnitComponent):void{
		components.push(component);
	}
	public function removeComponent(component:IUnitComponent):void{
		var index:int = components.indexOf(component);
		if(index >= 0) components.splice(index, 1);
	}*/
	

	//	Interface IAnimationOwner
	public function onAnimation(id:String, param:int = 0):void{
		if(curr_anim=="die"){
			var pb:Playback = anim.playback;
			pb.playFrame(pb.totalFrames);
			pb.pause = true;
			ga.removeFromUpdateList(this);
			return;
		}
		brain.currentAction.onEndAnim(param, id);
	}
	//	Getters/setters
	public function get maxSP():int{
		return st.SP;
	}
	public function get SP():int{
		return mana;
	}
	public function set SP(new_val:int):void{
		if(mana!=new_val){
			var prev_val:int = mana;
			mana = new_val;
			onManaChange.invoke(this, prev_val, new_val);
		}
	}
	public function get maxHP():int{
		return st.HP;
	}
	public function get HP():int{
		return health;
	}
	public function set HP(new_val:int):void{
		if(health!=new_val){
			health = new_val;
			var prev_val:int = health;
			health = new_val;
			onHealthChange.invoke(this, prev_val, new_val);
		}
	}
	public function get overdrive():Number{
		return limit;
	}
	public function set overdrive(new_val:Number):void{
		if(health!=new_val){
			limit = new_val;
		//	if(limit==100 && !Animation Shown?){Show Animation...}
		}
	}
	public bool freeze{	//Без сознания, герой замер, действие не происходит, но статусы действуют
		get{
			return frozen;
		}
		set{
			frozen = value;
			brain.Enabled = frozen;
		}
	}

	public void addCooldown(string cooldown_id, float time){
		if(time)cooldowns[cooldown_id] = time;
	}
	//	Effects
/*	public void addAttackEffect(IAttackEffect effect){
		atk_eff.push(effect);
	}
	public void removeAttackEffect(IAttackEffect effect){
		var index:int = atk_eff.indexOf(effect);
		if(index >= 0) atk_eff.splice(index, 1);
	}
	public function addDefenseEffect(effect:IDefenseEffect):void{
		def_eff.push(effect);
	}
	public function removeDefenseEffect(effect:IDefenseEffect):void{
		var index:int = def_eff.indexOf(effect);
		if(index >= 0) def_eff.splice(index, 1);
	}
	public function addHealEffect(effect:IHealEffect):void{
		heal_eff.push(effect);
	}
	public function removeHealEffect(effect:IHealEffect):void{
		var index:int = heal_eff.indexOf(effect);
		if(index >= 0) heal_eff.splice(index, 1);
	}*/
	//	Statuses
/*	public function removeStatus(status_id:String):Boolean{
		if(status[status_id]){
			status[status_id].remove();
			return true;
		}
		return false;
	}
	public function removeAllStatuses():void{
		for(var status_id:String in status){
			status[status_id].remove();
		}
		status = {};
	}*/
//	public function attackedBy(un:Unit,allies:Boolean = false):Boolean{
//		return ((team==un.team)==allies) && (un.brain.currentAction && /*(un.currAction is actions.Attack || un.currAction is actions.Strike) &&*/ (un.brain.currentAction.getTarget()==this));
//	}
	//	Damage
	public function getMagicDamage(mod:DamageModifier):Damage{
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
	}
}