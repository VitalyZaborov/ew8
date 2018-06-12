using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGunView : MonoBehaviour {

	private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();

	private void Start() {
		Sprite[] spritesheet = Resources.LoadAll<Sprite>("Attachments");
		foreach(Sprite sprite in spritesheet) {
			sprites[sprite.name] = sprite;
		}
	}
	
	public void setAttachment (string slot, string id) {
		Transform child = transform.Find(slot);
		UnityEngine.UI.Image image = child.GetComponent<UnityEngine.UI.Image>();
		image.enabled = id != "";
		if (image.enabled) {
			Sprite sprite = sprites[id];
			image.sprite = sprite;
			image.SetNativeSize();
			image.rectTransform.pivot = new Vector2(sprite.pivot.x / sprite.rect.width, sprite.pivot.y / sprite.rect.height);
		}
	}
}
