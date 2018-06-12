using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoading : Window {

	public Progressbar progressbar;

	private AsyncOperation operation;
	private LevelData levelData;

	override public void show(string id, WindowsManager manager, object windowData = null) {
		base.show(id, manager, windowData);
		levelData = (LevelData)windowData;
		operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelData.levelId);
	}

	private void Update() {
		if (operation != null) {
			progressbar.value = operation.progress;
			if (operation.isDone) {
				manager.open("Battle", levelData);
				close();
			}
		}
	}
}
