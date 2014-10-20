using UnityEngine;
using System.Collections;

public class GameOverReason : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel> ().text = GameVars.GameOverReason.ToUpper ();
	}
}
