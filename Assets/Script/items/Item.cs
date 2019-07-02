using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

	public enum Type {
		WEAPON,
		ARMOR,
		RING,
		SHIELD
	};


	public int amount = 1;
	public readonly string id = "";
	public readonly Status[] statuses;
	
	protected Unit owner;
	protected GameParams.StatParam statParam;
	protected GameParams.ItemParam itemParam;

	public Item (string id){
		this.id = id;
		GameParams.statParam.TryGetValue(id, out statParam);
		GameParams.itemParam.TryGetValue(id, out itemParam);
	}
	
	public virtual void onEquipped(Unit target){
		owner = target;
		owner.character.bonusStats += statParam;
		for (int i = 0; i < statuses.Length; i++){
			statuses[i].apply(owner.gameObject);
		}
	}

	public virtual void onUnequipped() {
		for (int i = 0; i < statuses.Length; i++){
			statuses[i].remove();
		}
		owner.character.bonusStats -= statParam;
		owner = null;
	}

	public Type type{
		get{ return itemParam.type; }
	}

	public bool stackable{
		get{ return true; }
	}
}
