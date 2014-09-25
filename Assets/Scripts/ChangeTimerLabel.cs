using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ChangeTimerLabel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UILabel timerText = GameObject.Find("TimerText").GetComponent<UILabel>();
		timerText.text = GameTimer15Min.TimeRemainingFormatted();
	}
	
	// Update is called once per frame
	void Update () {
		UILabel timerText = GameObject.Find("TimerText").GetComponent<UILabel>();
		timerText.text = GameTimer15Min.TimeRemainingFormatted();

		Debug.Log(GameTimer15Min.TimeRemaining);
		if(GameTimer15Min.TimeRemaining <= 10 && !audio.isPlaying) {
			audio.Play();
		}
	}
	
}
