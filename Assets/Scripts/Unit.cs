using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit {

	public UnitType Type;
	public GameObject GameObj;

	public bool Alive = true;

	public Unit (UnitType type) {
		Type = type;
		GameVars.AllUnits.Add(this);
	}

	public void InstantiateUnit(Vector3 position, Quaternion rotation) {
		GameObj = (GameObject) UnityEngine.MonoBehaviour.Instantiate(Type.SpriteAnimation, position, rotation);
		GameObj.GetComponent<SpriteRenderer> ().color = Type.UnitColor;
	}
	
	public void KillUnit() {
		Alive = false;
		UnityEngine.MonoBehaviour.Destroy(GameObj);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
