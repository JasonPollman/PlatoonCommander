using UnityEngine;
using System.Collections;

public class ReadyClicked : MonoBehaviour {
	
	void Start () {
		GameVars.PlayerReady = false;
	}

	void OnClick () {
		NGUITools.SetActive (gameObject.transform.parent.gameObject, false);
		GameVars.PlayerReady = true;
	}

} // End ReadyClicked class
