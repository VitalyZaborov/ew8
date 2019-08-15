using System;
using System.Collections.Generic;

public class Inventory{

	private List<Item> storage = new List<Item>();
	private Dictionary<Item.Type, List<Item>> categories = new Dictionary<Item.Type, List<Item>>();
	private Dictionary<string, Item> stackable = new Dictionary<string, Item>();

	public Inventory(){
		foreach(Item.Type type in Enum.GetValues(typeof(Item.Type))){
			categories[type] = new List<Item>();
		}
	}
	
	// ------------------------------------------------------------
	// Public
	
	// ------------------------------------------------------------
	public bool add(Item item){
		if (item.stackable){
			if (stackable.ContainsKey(item.id)){
				stackable[item.id].amount += item.amount;
				return true;
			}
			stackable[item.id] = item;
		}

		if (storage.Contains(item)){
			return false;
		}
		List<Item> category = categories[item.type];
		category.Add(item);
		storage.Add(item);
		return true;
	}
	
	// ------------------------------------------------------------
	public bool remove(Item item){
		if (item.stackable){
			if (!stackable.ContainsKey(item.id)){
				return false;
			}

			if (stackable[item.id].amount > item.amount){
				stackable[item.id].amount -= item.amount;
				return true;
			}

			item.amount = stackable[item.id].amount;
			stackable[item.id] = null;
		}

		if (!storage.Contains(item)){
			return false;
		}
		List<Item> category = categories[item.type];
		category.Remove(item);
		storage.Remove(item);
		return true;
	}
}
