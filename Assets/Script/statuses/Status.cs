using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status {

	public int lv = 0;

	protected GameObject owner;

	public string id {
		get {
			return GetType().Name;
		}
	}

	virtual public void apply(GameObject gameObject) {
		owner = gameObject;
	}

	virtual public void remove() {
		owner = null;
	}

	virtual public void add(Status status) {

	}
}
