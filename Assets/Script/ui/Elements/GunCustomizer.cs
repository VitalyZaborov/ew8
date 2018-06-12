using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCustomizer : MonoBehaviour {

	public UnityEngine.UI.Dropdown gunDD;
	public UnityEngine.UI.Dropdown scopeDD;
	public UnityEngine.UI.Dropdown undersulgDD;

	public string[] guns;
	public string[] scopes;
	public string[] underslugs;
	
	private CustomGunView gunView;
	private Dictionary<string, string[]> attachments = new Dictionary<string, string[]>();
	private Dictionary<string, UnityEngine.UI.Dropdown> dropdowns = new Dictionary<string, UnityEngine.UI.Dropdown>();

	void Start() {
		attachments[Attachment.SCOPE] = scopes;
		attachments[Attachment.UNDERSLUG] = underslugs;

		dropdowns[Attachment.SCOPE] = scopeDD;
		dropdowns[Attachment.UNDERSLUG] = undersulgDD;

		gunDD.onValueChanged.AddListener(delegate { onGunChanged(gunDD.value); });

		scopeDD.onValueChanged.AddListener(delegate { onAttachmentChanged(Attachment.SCOPE, scopeDD.value); });
		undersulgDD.onValueChanged.AddListener(delegate { onAttachmentChanged(Attachment.UNDERSLUG, undersulgDD.value); });

		onGunChanged(gunDD.value);
	}

	public Weapon.WeaponData getGun() {
		Weapon.WeaponData data = new Weapon.WeaponData(guns[gunDD.value]);
		List<string> list = new List<string>();
		if (scopeDD.value != 0)
			list.Add(getAttachment(Attachment.SCOPE));
		if (undersulgDD.value != 0)
			list.Add(getAttachment(Attachment.UNDERSLUG));
		data.attachments = list.ToArray();
		return data;
	}

	public string getAttachment(string slot) {
		UnityEngine.UI.Dropdown dd = dropdowns[slot];
		return dd.value == 0 ? "" : attachments[slot][dd.value - 1];
	}

	private void onAttachmentChanged(string slot, int value) {
		string id = value == 0 ? "" : attachments[slot][value - 1];
		gunView.setAttachment(slot, id);
	}

	private void onGunChanged(int value) {
		if (gunView != null) {
			Destroy(gunView.gameObject);
		}
		string gunID = guns[value];
		GameObject oprefab = Resources.Load<GameObject>("guns/" + gunID);
		GameObject o = Instantiate(oprefab, transform);
		gunView = o.GetComponent<CustomGunView>();

		onAttachmentChanged(Attachment.SCOPE, scopeDD.value);
		onAttachmentChanged(Attachment.UNDERSLUG, undersulgDD.value);
	}
}
