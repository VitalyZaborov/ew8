using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageModifier {

	private float[] v;

	/*	Damage mods sequence:
	Slashing, Piercing, Blunt, Fire, Frost, Lightning, Poison, Dark, Holy
	e.g. 3 Physical, 3 Elemental, 3 Spirit
	*/

	public DamageModifier(float sl = 0, float pi = 0, float bl = 0, float fr = 0, float ft = 0, float li = 0, float po = 0, float da = 0, float ho = 0) {
		v = new float[] { sl, pi, bl, fr, ft, li, po, da, ho };
	}
	public DamageModifier getCopy(){
		return new DamageModifier(v[0], v[1], v[2], v[3], v[4], v[5], v[6], v[7], v[8]);
	}
	public void add(DamageModifier mod) {
		for(int i = 0; i < v.Length; i++){
			v[i] += mod.v[i];
		}
	}
	public void sub(DamageModifier mod) {
		for (int i = 0; i < v.Length; i++) {
			v[i] -= mod.v[i];
		}
	}
	public void multiply(float mod){
		for (int i = 0; i < v.Length; i++) {
			v[i] *= mod;
		}
	}
	public void decrease(DamageModifier mod) {
		for (int i = 0; i < v.Length; i++) {
			v[i] *= 1  -mod.v[i];
		}
	}
	public void ignore(DamageModifier resist, float mod = 1) {  //Игнорирование защиты
		for (int i = 0; i < v.Length; i++) {
			v[i] /= (1-resist.v[i]* mod);
		}
	}
	public float getModifier(DamageModifier resist){	//расчет итогового множителя
		float mod = 0;
		for (int i = 0; i < v.Length; i++) {
			mod += v[i] * (1-resist.v[i]);
		}
		return mod;
	}
	public float this[Damage.Type type] {
		get {
			return v[(int)type];
		}
		set {
			v[(int)type] = value;
		}
	}
	public float part(Damage.Type type) {
		return v[(int)type];
	}
	public float physical{	//Используется для выяснения чья броня толще в выборке по def/mdef
		get {
			return slashing + piercing + blunt;
		}
	}
	public float magical{
		get {
			return fire + frost + lightning + poison + dark + holy;
		}
	}
	public float slashing {
		get {
			return this[Damage.Type.SLASHING];
		}
		set {
			this[Damage.Type.SLASHING] = value;
		}
	}
	public float piercing {
		get {
			return this[Damage.Type.PIERCING];
		}
		set {
			this[Damage.Type.PIERCING] = value;
		}
	}
	public float blunt {
		get {
			return this[Damage.Type.BLUNT];
		}
		set {
			this[Damage.Type.BLUNT] = value;
		}
	}
	public float fire {
		get {
			return this[Damage.Type.FIRE];
		}
		set {
			this[Damage.Type.FIRE] = value;
		}
	}
	public float frost {
		get {
			return this[Damage.Type.FROST];
		}
		set {
			this[Damage.Type.FROST] = value;
		}
	}
	public float lightning {
		get {
			return this[Damage.Type.LIGHTNING];
		}
		set {
			this[Damage.Type.LIGHTNING] = value;
		}
	}
	public float poison {
		get {
			return this[Damage.Type.POISON];
		}
		set {
			this[Damage.Type.POISON] = value;
		}
	}
	public float dark {
		get {
			return this[Damage.Type.DARK];
		}
		set {
			this[Damage.Type.DARK] = value;
		}
	}
	public float holy {
		get {
			return this[Damage.Type.HOLY];
		}
		set {
			this[Damage.Type.HOLY] = value;
		}
	}
	public string toString(){
		return "["+v[0]+","+v[1]+","+v[2]+","+v[3]+","+v[4]+","+v[5]+","+v[6]+","+v[7]+","+v[8]+"]";
	}
	public string description{
		get {
			string str = "";
			if (slashing != 0) {
				str += "\nSlashing: " + Mathf.Round(slashing * 100) + "%";
			}
			if (piercing != 0) {
				str += "\nPiercing: " + Mathf.Round(piercing * 100) + "%";
			}
			if (blunt != 0) {
				str += "\nBlunt: " + Mathf.Round(blunt * 100) + "%";
			}
			if (fire != 0) {
				str += "\n<FONT color=\"#CC0000\">Fire</FONT>: " + Mathf.Round(fire * 100) + '%';
			}
			if (frost != 0) {
				str += "\n<FONT color=\"#0099FF\">Frost</FONT>: " + Mathf.Round(frost * 100) + '%';
			}
			if (lightning != 0) {
				str += "\n<FONT color=\"#FF9900\">Lightning</FONT>: " + Mathf.Round(lightning * 100) + '%';
			}
			if (poison != 0) {
				str += "\n<FONT color=\"#00CC00\">Poison</FONT>: " + Mathf.Round(poison * 100) + '%';
			}
			if (dark != 0) {
				str += "\n<FONT color=\"#990099\">Dark</FONT>: " + Mathf.Round(dark * 100) + '%';
			}
			if (holy != 0) {
				str += "\n<FONT color=\"#999999\">Holy</FONT>: " + Mathf.Round(holy * 100) + '%';
			}
			return str;
		}
	}
}
