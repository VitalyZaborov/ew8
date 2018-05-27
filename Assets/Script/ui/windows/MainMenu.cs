using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainMenu : Window {

	public void onClickPlay() {
		manager.open("Missions");
		close();
	}
}