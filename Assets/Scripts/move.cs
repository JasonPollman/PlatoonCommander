using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	// The unit's speed
	public double speed;

	// The unit's default rotation
	private Quaternion defaultRotation;
	
	void Start () {
		gameObject.rigidbody2D.velocity = new Vector2 (getSpeed(),0f);
		defaultRotation = gameObject.transform.rotation;
	}
	
	public float getSpeed() { return(float)speed; }

	public Quaternion getDefaultRotation(){ return defaultRotation; }

	public float getAdjustedSpeed() { return Mathf.Abs (1 / (getSpeed () * 10)) - .5f; }

} // End move class
