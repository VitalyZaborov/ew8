using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public uint value;
	public uint max;
	public DamageModifier resist = new DamageModifier();
	public int defense;

	//	public event Delegate.ComponentEvent evKilled = new Delegate.ComponentEvent();

	public void receiveDamage(int dmg) {
		if (dmg == 0 || !isAlive)
			return;
		value = (uint)Mathf.Max(0, value - dmg);
		if (value == 0) {
			//	Destroy(gameObject);
			gameObject.SetActive(false);
			//	evKilled (gameObject);
		}
	}

	public void recover(int delta) {
		if (delta <= 0 || value == max)
			return;
		value = (uint)Mathf.Min(max, value + delta);
	}

	public bool isAlive{
		get{
			return value > 0;
		}
	}
}
