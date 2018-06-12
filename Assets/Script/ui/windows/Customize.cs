using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customize : Window {
	public GunCustomizer customizer;

	public void onClickStart() {
		LevelData ld = new LevelData() {
			levelId = "Scenes/Levels/level1",
			wdata = customizer.getGun()
		};
		manager.open("BattleLoading", ld);
		close();
	}
}