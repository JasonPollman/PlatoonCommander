using UnityEngine;
using System.Collections;

public class StopObjects : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D obj) {
		if(obj.gameObject.rigidbody2D) {
			obj.gameObject.rigidbody2D.velocity = Vector2.zero;
			if(obj.gameObject.GetComponent<UnitObject>().Type != "bomber") {
				obj.gameObject.GetComponent<UnitObject>().Alive = false;
			}
		}
	}
}
