using UnityEngine;
using System.Collections;

public class GotoLevel : MonoBehaviour {

	void OnClick () {
		Application.LoadLevel (GameVars.PlayerCurrentLevelName);
	}

} // End GotoLevel1 class
