using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Unit {

	public UnitType Type;
	public Object GameObj;

	public bool Alive = true;

	public Unit (UnitType type) {
		Type = type;
		GameVars.AllUnits.Add(this);
	}

	public void InstantiateUnit(Vector3 position, Quaternion rotation) {
		GameObj = (GameObject) UnityEngine.MonoBehaviour.Instantiate(Type.SpriteAnimation, position, rotation);
	}
	
	public void KillUnit() {
		Alive = false;
		UnityEngine.MonoBehaviour.Destroy(GameObj);
	}

	public static Unit GetFirstUnitOfType(string type) {

		if(GameVars.AllUnits.Count > 0) {
			foreach (Unit x in GameVars.AllUnits) if(x.Type.Name == type) return x;
		}
		
		return null;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
