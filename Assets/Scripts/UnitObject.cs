using UnityEngine;
using System.Collections;

public class UnitObject : MonoBehaviour {

	public bool Alive = true;
	public int HP;
	public int DP;

	// Use this for initialization
	void Start () {
	
	}

	void Update () {

	}

	public void KillUnit () {

		Alive = false;
		NGUITools.SetActive (gameObject, false);

	}

	public void TakeHit () {

		HP = (HP > 0) ? HP - 1 : 0;

		if(HP <= 0) KillUnit();
	
	}
}
