using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Battle : Window {
	public HUD hud;

	override public void show(string id, WindowsManager manager, object windowData = null) {
		base.show(id, manager, windowData);
		LevelData ld = (LevelData)windowData;
		hud.target = GameObject.FindGameObjectWithTag("Player");
		Soldier soldier = hud.target.GetComponent<Soldier>();
		soldier.setWeapons(new Weapon.WeaponData[] { ld.wdata });
	}
}