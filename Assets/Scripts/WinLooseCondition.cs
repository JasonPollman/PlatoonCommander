using UnityEngine;
using System.Collections;

public class WinLooseCondition : MonoBehaviour {
	
	GameObject bridgeExplosion;
	GameObject bridge;
	GameObject winBox;

	// Use this for initialization
	void Awake () {
		bridgeExplosion = GameObject.Find ("BridgeExplosion");
		bridge = GameObject.Find ("Bridge");
		winBox = GameObject.Find ("MissionCompleteBox");
	}
	
	// Update is called once per frame
	void Update () {
		if(GameVars.BomberDeployed && GameVars.BomberUnit != null && GameVars.BomberUnit.GameObj.GetComponent<UnitObject>().Alive == false) { // Game Over, the Bomber is Dead...
			StartCoroutine(DeathNextLevel());
			GameVars.GameInPlay = false;
		}
	}

	IEnumerator DeathNextLevel() {

		yield return new WaitForSeconds (2);
		GameVars.GameOverReason = "Your bomber was destroyed!";
		Application.LoadLevel("GameOver");
	}

	void OnTriggerExit2D (Collider2D unit) {

		if(unit.gameObject.Equals(GameVars.BomberUnit.GameObj)) { // The bomber has crossed the bridge
			StartCoroutine(Explode(unit.gameObject));
			GameVars.GameInPlay = false;


		}
	}

	IEnumerator Explode (GameObject unit) {

		for(int i = 1; i < 6; i++) {
			
			NGUITools.SetActive(bridgeExplosion.transform.FindChild(i.ToString()).gameObject, true);
			yield return new WaitForSeconds(unit.GetComponent<move>().getAdjustedSpeed());

			if(i == 5) {
				StartCoroutine(StopExplosions());
			}
		}
	}

	IEnumerator StopExplosions () {

		yield return new WaitForSeconds(3f);

		bridge.GetComponent<UISprite> ().spriteName = "BridgeDestroyed";
		Console.Push ("Mission Successful!");
		Console.Push ("The bridge has been destroyed");

		NGUITools.SetActive(winBox, true);

		for(int i = 1; i < 6; i++) {
			NGUITools.SetActive(bridgeExplosion.transform.FindChild(i.ToString()).gameObject, false);
		}
	}
}
