using UnityEngine;
using System.Collections;

public class StopObjects : MonoBehaviour {

	void OnTriggerExit2D(Collider2D obj) {
		if(obj.gameObject.rigidbody2D) {
			obj.gameObject.rigidbody2D.velocity = Vector2.zero;
		}
	}
}
