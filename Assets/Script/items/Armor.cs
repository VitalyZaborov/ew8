using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item {

	public enum Type {
		BODY
	};

	public Armor(string id):base(id){
		
	}
	
	// ------------------------------------------------------------
	// Override
	
	// ------------------------------------------------------------
	public override bool stackable => false;
}
