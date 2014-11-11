using UnityEngine;
using System.Collections;

public class KillUnitOnImpact : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D unit) {
		unit.gameObject.GetComponent<UnitObject>().TakeHit(999, "standard", null);
	}
}
