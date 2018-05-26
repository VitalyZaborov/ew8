using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WindowsManager : MonoBehaviour {

	public string startWindow;
	private Dictionary<string, GameObject> windows = new Dictionary<string, GameObject>();

	WindowsManager() {

	}

	private void Start() {
		if(startWindow != "") {
			open(startWindow);
		}
	}

	public Window open(string windowName, object windowData = null) {
		if (windows.ContainsKey(windowName)) {
			return windows[windowName].GetComponent<Window>();
		}
		GameObject prefab = Resources.Load<GameObject>("windows/" + windowName);
		GameObject obj = Instantiate<GameObject>(prefab, gameObject.transform);
		windows[windowName] = obj;
		Window window = obj.GetComponent<Window>();
		window.show(windowName, this, windowData);
	//	Debug.Log("open " + windowName + obj + window);
		return window;
	}

	public Window close(string windowName) {
		if (!windows.ContainsKey(windowName)) {
			return null;
		}
		Window window = windows[windowName].GetComponent<Window>();
		window.hide();
		windows.Remove(windowName);
		return window;
	}
}