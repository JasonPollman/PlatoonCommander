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
		Debug.Log (unit.GetComponent<UnitObject>().HP.ToString() + " PRE");
		unit.gameObject.GetComponent<UnitObject>().TakeHit();
		Debug.Log (unit.GetComponent<UnitObject>().HP.ToString() + " POST");
	}
}
