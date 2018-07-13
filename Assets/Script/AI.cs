using System.Collections.Generic;
using UnityEngine;
public class AI{

	public static Dictionary<string, AINode[]> ai = new Dictionary<string, AINode[]>{
		{"common", new AINode[]{
			new AINode(){
				action = cnxt => new Rest()
			},
			new AINode(){
				character = Condition.getConditions("self,clip=0"),
				action = cnxt => new Reload()
			},
		/*	new AINode(){
				target = Condition.getConditions("enemy,nearest"),
				character = Condition.getConditions("self,recoil<5"),
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.write("enemyPos", cnxt.target.transform.position);},
				action = cnxt => new Shoot(){accuracy = 5}
			},*/
			new AINode(){
				character = Condition.getConditions("enemy,nearest"),
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.write("enemyPos", cnxt.target.transform.position);},
				action = cnxt => new Strike()
			},
			new AINode(){
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.erase("enemyPos");},
				action = cnxt => new GoTo(cnxt.memory.read<Vector3>("enemyPos"))
			}
		}},
		{"empty", new AINode[]{
		}},
		{"gunner", new AINode[]{
			new AINode(){
				action = cnxt => new Rest()
			},
			new AINode(){
				character = Condition.getConditions("self,clip=0"),
				action = cnxt => new Reload()
			},
			new AINode(){
				character = Condition.getConditions("enemy,nearest"),
				action = cnxt => new Shoot(){accuracy = 5}
			}
		}},
		{"player", new AINode[]{
			new AINode(){
				action = cnxt => new PlayerControl()
			}
		}},
		{"driver", new AINode[]{
			new AINode(){
				character = Condition.getConditions("enemy,nearest"),
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.write("enemyPos", cnxt.target.transform.position);},
				action = cnxt => new LookAt()
			},
			new AINode(){
				extra = delegate(Brain.ActionContext cnxt) {cnxt.memory.erase("enemyPos");},
				action = cnxt => new GoTo(cnxt.memory.read<Vector3>("enemyPos"))
			}
		}}
	};
}