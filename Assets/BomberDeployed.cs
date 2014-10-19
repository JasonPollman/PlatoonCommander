using UnityEngine;
using System.Collections;

public class BomberDeployed : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel> ().text = (GameVars.BomberDeployed == true) ? "YES" : "NO";
	}
}
