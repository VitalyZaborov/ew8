using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public uint value;
	public uint max;

	public event Delegate.ComponentEvent evKilled;

	public void receiveDamage (int dmg) {
		if (value == 0)
			return;
		value = (uint)Mathf.Max (0, value - dmg);
		if (value == 0) {
			evKilled (gameObject);
		}
	}

	public bool isAlive{
		get{
			return value > 0;
		}
	}
}
