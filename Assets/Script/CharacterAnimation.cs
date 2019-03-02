using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class CharacterAnimation : MonoBehaviour {

	public UnityDragonBonesData dragonBoneData;
	private UnityArmatureComponent armatureComponent;
	// Use this for initialization
	void Start () {
		UnityFactory.factory.LoadDragonBonesData("heroes_ske");
		UnityFactory.factory.LoadTextureAtlasData("heroes_tex");

		armatureComponent = UnityFactory.factory.BuildArmatureComponent("ffs", "heroes");
		armatureComponent.animation.Play("attack1");
		armatureComponent.name = "FemaleFrontSword";
		armatureComponent.transform.localPosition = new Vector3(3.0f, -1.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
