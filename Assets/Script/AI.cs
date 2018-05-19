using System.Collections.Generic;
using UnityEngine;
public class AI{

	public static Dictionary<int, AINode[]> ai = new Dictionary<int, AINode[]>{
		{0, new AINode[]{
			new AINode(){
				action = cnxt => new PlayerControl()
			}
		}},
		{1, new AINode[]{
			new AINode(){
				action = cnxt => new Rest()
			},
			new AINode(){
				character = Condition.getConditions("self,clip=0"),
				action = cnxt => new Reload()
			},
			new AINode(){
				target = Condition.getConditions("enemy,nearest"),
				character = Condition.getConditions("self,recoil<5"),
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.write("enemyPos", cnxt.target.transform.position);},
				action = cnxt => new Shoot(){accuracy = 5}
			},
			new AINode(){
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.erase("enemyPos");},
				action = cnxt => new GoTo(cnxt.memory.read<Vector3>("enemyPos"))
			}
		}}
	};
}