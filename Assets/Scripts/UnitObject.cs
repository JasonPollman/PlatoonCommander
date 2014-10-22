using UnityEngine;
using System.Collections;

public class UnitObject : MonoBehaviour {

	public bool Alive = true;
	public int HP;
	public int DP;
	public string Type;

	// Use this for initialization
	void Start () {
	}

	void Update () {

	}

	public void KillUnit () {

		Console.Push ("Unit " + Type + " has been killed!");
		Alive = false;
		NGUITools.SetActive (gameObject, false);

	}

	public void TakeHit () {

		HP--;

		if(HP <= 0) KillUnit();
	
	}
}
