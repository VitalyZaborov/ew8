using System.Collections.Generic;
using UnityEngine;
public class AI{

	public static Dictionary<string, AINode[]> ai = new Dictionary<string, AINode[]>{
		{"player", new AINode[]{
			new AINode(){
				action = cnxt => new PlayerControl()
			}
		}},
		{"common", new AINode[]{
			new AINode(){
				character = Condition.getConditions("enemy,nearest"),
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.write("enemyPos", cnxt.target.transform.position);},
				action = cnxt => new Attack()
			},
			new AINode(){
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.erase("enemyPos");},
				action = cnxt => new GoTo(cnxt.memory.read<Vector3>("enemyPos"))
			}
		}},
		{"empty", new AINode[]{
		}}
	};
}