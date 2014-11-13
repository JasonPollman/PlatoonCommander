using UnityEngine;
using System.Collections;

public class DestroyTarget : MonoBehaviour {

	public string winMessage = "The bridge has been destroyed!";
	public string destroyedSpriteName = "BridgeDestroyed";

	// The bridgeExplosion GameObject (container for explosions)
	GameObject explosion;

	// The message box for when the user wins
	GameObject winBox;

	// Use this for initialization
	void Awake () {
		explosion = GameObject.Find ("Explosions");
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
			
			NGUITools.SetActive(explosion.transform.FindChild(i.ToString()).gameObject, true);
			explosion.transform.FindChild(i.ToString()).audio.Play ();
			yield return new WaitForSeconds(unit.GetComponent<move>().getAdjustedSpeed());

			if(i == 5) StartCoroutine(StopExplosions());
		}

	} // End Explode()

	IEnumerator StopExplosions () {

		yield return new WaitForSeconds(3f);

		// Play Winning Sound...
		audio.Play();

		gameObject.GetComponent<UISprite> ().spriteName = destroyedSpriteName;
		gameObject.GetComponent<UISprite> ().MakePixelPerfect ();
		gameObject.GetComponent<UISprite> ().MarkAsChanged ();
		Console.Push ("Mission Successful!");
		Console.Push (winMessage);

		// Set the Level as won, so we can pop-up the win box...
		GameVars.LevelWon = true;

		for(int i = 1; i < 6; i++) {
			NGUITools.SetActive(explosion.transform.FindChild(i.ToString()).gameObject, false);
		}
	} // End StopExplosions()

} // End BlowTheBridge class
