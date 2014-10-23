using UnityEngine;
using System.Collections;

public class MessageBox : MonoBehaviour {

	public string NextLevel;

	void OnClick () {
		NGUITools.SetActive (gameObject.transform.parent.gameObject, false);
		Application.LoadLevel (NextLevel);
	}

} // End MessageBox class
