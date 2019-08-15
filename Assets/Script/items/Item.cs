using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

	public enum Type {
		OTHER,
		WEAPON,
		ARMOR,
		RING,
		SHIELD
	};
	
	public enum Slot {
		WEAPON,
		ARMOR,
		RING_1,
		RING_2,
		SHIELD
	};
	
	public enum EquipResult {
		OK,
		ALREADY_DONE,
		SLOT_OCCUPIED,
		NOT_EQUIPPABLE,
		WRONG_STATS
	};


	public int amount = 1;
	public readonly string id = "";
	public readonly GameParams.StatParam statParam;
	public readonly GameParams.ItemParam itemParam;
	public readonly Status[] statuses;
	
	protected Unit owner;

	public Item (string id){
		this.id = id;
		GameParams.statParam.TryGetValue(id, out statParam);
		GameParams.itemParam.TryGetValue(id, out itemParam);
	}
	
	// ------------------------------------------------------------
	// Equip / Unequip
	
	// ------------------------------------------------------------
	public virtual void onEquipped(Unit target){
		owner = target;
		owner.character.bonusStats += statParam;
		for (int i = 0; i < statuses.Length; i++){
			statuses[i].apply(owner.gameObject);
		}
	}

	// ------------------------------------------------------------
	public virtual void onUnequipped() {
		for (int i = 0; i < statuses.Length; i++){
			statuses[i].remove();
		}
		owner.character.bonusStats -= statParam;
		owner = null;
	}

	// ------------------------------------------------------------
	// Getters
	
	// ------------------------------------------------------------
	public Type type => itemParam.type;

	// ------------------------------------------------------------
	// Virtual
	
	// ------------------------------------------------------------
	public virtual bool stackable => true;

	// ------------------------------------------------------------
	public virtual bool canEquip(Character character){
		return true;
	}
}
