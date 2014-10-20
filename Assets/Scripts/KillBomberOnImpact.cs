using UnityEngine;
using System.Collections;

public class KillBomberOnImpact : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D unit) {
		Debug.Log ("HERE");
		unit.gameObject.GetComponent<UnitObject>().KillUnit();
	}
}
