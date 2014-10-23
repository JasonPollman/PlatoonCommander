using UnityEngine;
using System.Collections;

/**
 * Sets the reason for loosing on the GameOver Scene
 */
public class GameOverReason : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel> ().text = GameVars.GameOverReason.ToUpper ();
	}
}
