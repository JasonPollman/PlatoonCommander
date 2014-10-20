using UnityEngine;
using System.Collections;

public class StopObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit2D(Collider2D obj) {
		if(obj.gameObject.rigidbody2D) {
			obj.gameObject.rigidbody2D.velocity = Vector2.zero;
		}
	}
}
