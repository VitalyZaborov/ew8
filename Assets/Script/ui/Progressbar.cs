using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Progressbar : MonoBehaviour {

	public Image bar;
	private float currentValue = 1;

	[SerializeField]
	public float value {
		get {
			return currentValue;
		}
		set {
			value = Mathf.Clamp(value, 0, 1);
			if (currentValue == value)
				return;
			currentValue = value;
			if (bar != null)
				bar.fillAmount = value;
		}
	}
}