using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{
	public const uint SELFCAST = 1;
	public const uint ANOTHER_ALLY = 2;
	public const uint ANY_ENEMY = 4;
	public const uint ANY_ALLY = 3;	//ANOTHER_ALLY & SELFCAST
	public const uint ANYONE = 7;	//ANY_ENEMY & ANOTHER_ALLY & SELFCAST
	public const uint ANYONE_ELSE = 6;	//ANY_ENEMY & ANOTHER_ALLY

	private Action master_action;
	
	protected GameObject caster;
	protected GameObject target;
	protected int defaultLevel = 1;
	protected Animator animator;
	
	public string id;	//Присваивается в getAction
	public int lv;

	public Action(){
	}
	public virtual float range {
		get{ return 0; }
	}
	public virtual string name {
		get{ return ""; }
	}
	public virtual uint targetType {
		get{ return ANYONE; }
	}
	public virtual int spCost {
		get{ return 0; }
	}
	public virtual float cooldown {
		get{ return 0; }
	}
	public virtual Action nextAction{
		get{return null;}
	}
	public virtual bool enabled{	// Is it possible to perform the action at all (all weapon compatibility checks go here)
		get { return true; }
	}
	public virtual bool intercept(){
		return false;
	}

	public Action master{
		get{ return master_action; }
	}
	public virtual void init(GameObject cst, object param = null){
		caster = cst;
		animator = caster.GetComponent<Animator> ();
		Caster casterComponent = caster.GetComponent<Caster> ();
		lv = defaultLevel != 0 ? defaultLevel : casterComponent != null && casterComponent.skills != null && casterComponent.skills[id] != 0 ? casterComponent.skills[id] : 0;
	}
	public virtual void update(float dt){
	}
	public virtual Action performPrepareAction(GameObject trg){
		if(trg){
			Action act;
			float rng = range;
			float dist = Vector3.Distance(caster.transform.position, trg.transform.position);
			if(rng<dist){
				act = new Follow(false);
				act.init(caster,rng);
				return makePrepareAction(act,trg,trg);
			}
			Vision vision = caster.GetComponent<Vision> ();
			if(vision && !vision.canSee(trg)){
				act = new Follow(false);
				act.init(caster,0.5);
				return makePrepareAction(act,trg,trg);
			}
		}
		return null;
	}
	protected virtual Action makePrepareAction(Action act, GameObject act_target, GameObject next_target){
		act.master_action = this;
		target = next_target;
		act.perform(act_target);	//Без проверки на canPerform(), заменяющее действие должно быть выполняемо
		return act;
	}
	public virtual void perform(GameObject trg){
		target = trg;
	/*	Caster casterComponent = caster.GetComponent<Caster> ();
		if(casterComponent != null){
			casterComponent.addCooldown(id, cooldown);
			casterComponent.SP -= spCost;
		}*/
	}
	protected virtual void complete(){
		Brain brain = caster.GetComponent<Brain> ();
		brain.onActionComplete();
	}
	//virtual
	public virtual bool canPerform(GameObject trg){
		return true;
	/*	if(!lv){return false;}	//Нулевой уровень - это отсутствие скилла у кастера
		if(targetType != ANYONE){
			switch(true){
				case (!trg) || (caster==trg): if(!(targetType & SELFCAST)){return false;}break;
				case (caster.team & trg.team)!=0: if(!(targetType & ANOTHER_ALLY)){return false;}break;
				case (caster.team & trg.team)==0: if(!(targetType & ANY_ENEMY)){return false;}break;
			}
		}
		if(caster.cooldowns[id]){return false;}
		return (caster.SP >= spCost);	//Sp cost check;*/
	}
	public virtual bool shouldPerform(GameObject trg){
		return true;
	}
	public virtual void onAnimation(int param = 0){}
	public GameObject getTarget(){
		return target;
	}
	public virtual string getDescription(){
		string str = "<B>"+name+"\n";
		if(range != 0){str += "Range: "+range+"\n";}
		if(spCost != 0)str += "'<FONT color=\"#0033CC\">SP cost: "+spCost+"</FONT></B>\n";
		if(cooldown != 0)str += "<FONT color=\"#0033CC\">Cooldown: "+cooldown+"</FONT></B>\n";
		str += simpleDescription();
		return str;
	}
	protected virtual string simpleDescription(){
		return "";
	}
	protected string hlt(object str){	//HighlLight Text
		return "<B>"+str.ToString()+"</B>";
	}
}