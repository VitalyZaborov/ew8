using System.Collections.Generic;
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
				target = Condition.getConditions("enemy,hp>50%,nearest"),
				character = Condition.getConditions("self,recoil<5"),
				action = cnxt => new Shoot(){accuracy = 5}
			}
		}}
	};
}