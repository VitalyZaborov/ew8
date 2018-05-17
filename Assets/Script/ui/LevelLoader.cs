using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelLoader : MonoBehaviour {

	public Progressbar progressbar;
	private AsyncOperation operation;

	public AsyncOperation load(string levelName) {
		operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelName);
		return operation;
	}

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	private void Update() {
		if(operation != null) {
			progressbar.value = operation.progress;
			if (operation.isDone) {
				Destroy(gameObject);
			}
		}
	}
}