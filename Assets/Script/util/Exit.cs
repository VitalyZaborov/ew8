using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {

	public string level;
	public Transform canvas;
	public GameObject levelLoader;

	private void OnTriggerEnter(Collider other) {
		GameObject o = other.gameObject;
		if (other.tag == "Player") {
			Instantiate(levelLoader, canvas);
			LevelLoader loader = levelLoader.GetComponent<LevelLoader>();
			loader.load(level);
		}
	}
}