﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character{
	public GameParams.StatParam coreStats = new GameParams.StatParam();
	public GameParams.StatParam bonusStats = new GameParams.StatParam();
	public GameParams.CharacterParam characterStats;
	public Weapon weapon;
	public readonly string id;
	
	public Character(string id){
		this.id = id;
		coreStats = GameParams.statParam[id];
		characterStats = GameParams.characterParam[id];
	}

	public GameParams.StatParam getStats(){
		return coreStats + bonusStats;
	}
}