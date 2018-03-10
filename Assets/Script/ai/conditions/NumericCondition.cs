using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NumericCondition : GroupCondition{
	
	protected delegate bool CompareFunction(float a);
	protected CompareFunction compare;
	protected float value;
	protected bool percent;

	public NumericCondition(string sign, string param){
		switch (sign) {
		case "=":
			compare = equal;
			break;
		case "<":
			compare = lt;
			break;
		case ">":
			compare = gt;
			break;
		case "<=":
			compare = elt;
			break;
		case ">=":
			compare = egt;
			break;
		}
		if (param [param.Length - 1] == '%') {
			percent = true;
			param = param.Substring (0, param.Length - 1);
		}
		value = float.Parse(param); 

	}

	private bool equal(float a){
		return a == value;
	}

	private bool lt(float a){
		return a < value;
	}

	private bool gt(float a){
		return a > value;
	}

	private bool elt(float a){
		return a <= value;
	}

	private bool egt(float a){
		return a >= value;
	}
}