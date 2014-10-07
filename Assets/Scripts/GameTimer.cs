using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	public static float _TimeRemaining;
	public static float _InitialTime = LevelConfig.LevelDuration;

	// Use this for initialization
	void Start () {
		_TimeRemaining = _InitialTime;
	}

	public static float TimeRemaining { get { return _TimeRemaining; } set { _TimeRemaining = value; } }
	public static float InitialTime   { get { return _InitialTime; } set { _InitialTime = value; } }

	// Get the time remaining in the formatted output
	public static string TimeRemainingFormatted () {

		int minutes = (int) Mathf.Floor(_TimeRemaining / 60);
		int seconds = (int) Mathf.Ceil((_TimeRemaining / 60 - minutes) * 60);
		return minutes.ToString() + ":" + ((seconds < 10 ) ? "0" + seconds.ToString() : seconds.ToString());

	}

	// Update is called once per frame
	void Update () {

		if(GameVars.PlayerReady) {
			if(_TimeRemaining > 0) _TimeRemaining -= Time.deltaTime;

			if(_TimeRemaining <= 0){
				GameVars.PlayerReady = false;
				Application.LoadLevel ("GameOver");
				UserData.UserCurrentLevel = 1;
			}

		}
	}
}
