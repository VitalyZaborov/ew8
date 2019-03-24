using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour {

	public Dictionary<string, int> skills = new Dictionary<string, int>();
	private readonly Dictionary<string, float> cooldowns = new Dictionary<string, float>();

	private int sp = 0;

	public int SP{
		get{ return sp; }
		set{ sp = value; }
	}

	public void addCooldown(string skillId, float time){
		cooldowns[skillId] = time;
	}
	public float getCooldown(string skillId){
		return cooldowns.ContainsKey(skillId) ? cooldowns[skillId] : 0.0f;
	}

	private void Update(){
		foreach(KeyValuePair<string, float> entry in cooldowns){
			cooldowns[entry.Key] = Mathf.Max(0, entry.Value - Time.deltaTime);
		}
	}
}
