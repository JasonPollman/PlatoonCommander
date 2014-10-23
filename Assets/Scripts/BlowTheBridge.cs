using UnityEngine;
using System.Collections;

public class BlowTheBridge : MonoBehaviour {

	// The bridgeExplosion GameObject (container for explosions)
	GameObject bridgeExplosion;

	// The brige game object
	GameObject bridge;

	// The message box for when the user wins
	GameObject winBox;

	// Use this for initialization
	void Awake () {

		// Grab what we need
		bridgeExplosion = GameObject.Find ("BridgeExplosion");
		bridge = GameObject.Find ("Bridge");

	} // End Awake()


	void OnTriggerExit2D (Collider2D unit) {

		if(unit.gameObject.Equals(GameVars.BomberUnit.GameObj)) { // The bomber has crossed the bridge
			StartCoroutine(Explode(unit.gameObject));
			GameVars.GameInPlay = false;

			// Stop Level Audio
			GameObject.Find ("UI Root (2D)").audio.Stop();
		}

	} // End OnTriggerExit2D()

	IEnumerator Explode (GameObject unit) {

		for(int i = 1; i < 6; i++) {
			
			NGUITools.SetActive(bridgeExplosion.transform.FindChild(i.ToString()).gameObject, true);
			yield return new WaitForSeconds(unit.GetComponent<move>().getAdjustedSpeed());

			if(i == 5) StartCoroutine(StopExplosions());
		}

	} // End Explode()

	IEnumerator StopExplosions () {

		yield return new WaitForSeconds(3f);

		// Play Winning Sound...
		audio.Play();

		bridge.GetComponent<UISprite> ().spriteName = "BridgeDestroyed";
		Console.Push ("Mission Successful!");
		Console.Push ("The bridge has been destroyed");

		// Set the Level as won, so we can pop-up the win box...
		GameVars.LevelWon = true;

		for(int i = 1; i < 6; i++) {
			NGUITools.SetActive(bridgeExplosion.transform.FindChild(i.ToString()).gameObject, false);
		}
	} // End StopExplosions()

} // End BlowTheBridge class
