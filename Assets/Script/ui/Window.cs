using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Window : MonoBehaviour {
	protected WindowsManager manager;
	protected string id;

	virtual public void show(string id, WindowsManager manager, object windowData = null) {
		this.id = id;
		this.manager = manager;
	}

	virtual public void hide() {
		manager = null;
		enabled = false;
		Destroy(gameObject);
	}

	public void close() {
		manager.close(id);
	}
}