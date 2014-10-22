using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit {

	public UnitType Type;
	public GameObject GameObj;
	public bool Instantiated = false;

	public Unit (UnitType type) {
		Type = type;
		GameVars.AllUnits.Add(this);
	}

	public GameObject InstantiateUnit(Vector3 position, Quaternion rotation) {

		GameObj = (GameObject) UnityEngine.MonoBehaviour.Instantiate(Type.SpriteAnimation, position, rotation);
		Instantiated = true;
		GameObj.GetComponent<SpriteRenderer> ().color = Type.UnitColor;
		UnitObject scr = GameObj.GetComponent<UnitObject> ();
		scr.Alive = true;
		scr.HP = Type.HP;
		scr.DP = Type.DP;
		scr.Type = Type.Name;

		return GameObj;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
