using UnityEngine;
using System.Collections;

public class BomberDeployed : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel> ().text = (GameVars.BomberDeployed == true) ? "YES" : "NO";
	}

} // End BomberDeployed class
