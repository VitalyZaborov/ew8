using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

	protected class Order {
		public Action action;
		public GameObject target;
		public Condition[] conditions;
	}

	public class Memory {
		private Dictionary<string, object> dictionary = new Dictionary<string, object>();
		public void write(string id, object data) {
			dictionary[id] = data;
		}
		public T read<T>(string id) {
			object value;
			return dictionary.TryGetValue(id, out value) ? (T)value : default(T);
		}
		public object read(string id) {
			object value;
			return dictionary.TryGetValue(id, out value) ? value : null;
		}
	}

	public struct ActionContext {
		public GameObject caster;
		public GameObject target;
		public GameObject player;
		public Memory memory;
	}

	public int pattern = 0;
	public Memory memory = new Memory();

	private AINode[] aiArray;
	protected Action curr_action;
	protected Queue<Order> orders = new Queue<Order>();
	//	Add actions history?

	Brain(){
	}

	void OnDisable(){
		if (curr_action != null) {
			curr_action.intercept ();
			curr_action = null;
		}
	}
	void Update(){
		if(curr_action != null){
		//	Debug.Log("Brain update action " + curr_action.ToString());
			curr_action.update(Time.deltaTime);
		}else{
			think();
		}
	}
	public void setAI(int pattern){
		aiArray = AI.ai[pattern];
		if(curr_action != null && curr_action.intercept()) {
			curr_action = null;
		}
	}
	public Action currentAction{
		get {
			return curr_action;
		}
	}
	public bool performAction(Action action,GameObject target = null){
		if(action.canPerform(target)){
			Action act = action.performPrepareAction(target);
			if(act != null){
				action = act;
			}else{
				action.perform(target);
			}
			curr_action = action;
			return true;
		}
		return false;
	}
	public void onActionComplete(){
		Action prev_action = curr_action;
		curr_action = null;	//Чтобы избежать конфликта с непрерываемыми действиями, вроде падения
	//	history.push(prev_action.id);	// Push completed action id to history here
		while(orders.Count > 0){
			Order order = orders.Dequeue();
			if(performAction(order.action, order.target)){
				return;
			}
		}	
		if(prev_action != null && prev_action.master != null){
			performAction(prev_action.master, prev_action.master.getTarget());
		}
	}
	//	Orders
	public void addOrder(Action a,GameObject t = null){
		if(orders.Count == 0 && curr_action.intercept()){
			performAction(a,t);
		}else{
			orders.Enqueue(new Order(){action = a, target = t});
		}
	}
	public void clearOrders(){
		orders.Clear ();
	}
	public void onAnimation(int param = 0){
		if(curr_action != null){
			curr_action.onAnimation (param);
		}
	}
	//	Staff
	protected void think(){
		Unit unit = gameObject.GetComponent<Unit> ();
		List<GameObject> targets = unit.visibleUnits;
		Action action;
		ActionContext cnxt = new ActionContext() {caster = gameObject, memory = memory };
		for(int i = 0;i < aiArray.Length;i++){
			AINode ai_node = aiArray[i];
			if(ai_node.probability==100 || Random.value * 100 <= ai_node.probability){
				action = ai_node.action(cnxt);
				action.init(gameObject);
				GameObject character = ai_node.character != null ? getTarget(ai_node.character, gameObject, ai_node.target != null ? null : action, targets) : (canDo(gameObject, action) ? gameObject : null);
				if (character != null){
					GameObject target = ai_node.target != null ? getTarget(ai_node.target, gameObject, action, targets) : character;
					if (target != null && performAction(action,target)){
						if(ai_node.extra != null) {
							cnxt.target = target;
							ai_node.extra(cnxt);
						}
						return;
					}
				}
			}
		}
		action = new Stay();
		action.init(gameObject);
		performAction(action);
	}

	protected GameObject getTarget(Condition[] conditions, GameObject owner, Action action, List<GameObject> targets) {
		List<GameObject> pool;
		if (action != null) {
			pool = new List<GameObject>();
			for (int i = 0; i < targets.Count; i++) {
				GameObject target = targets[i];
				if (canDo(target, action)) {
					pool.Add(target);
				}
			}
		} else {
			pool = new List<GameObject>(targets);
		}
		for (int i = 0; i < conditions.Length; i++) {
			Condition condition = conditions[i];
			condition.apply(owner, null, pool);
		}
		return pool.Count != 0 ? pool[0] : null;
	}

	protected bool canDo(GameObject target, Action action) {
		return action.canPerform(target) && action.shouldPerform(target);
	}
}