using UnityEngine;
using System.Collections;

public class Console : MonoBehaviour {

	public static string ConsoleStr = "Mission started at " + System.DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

	public static void Push(string msg) {
		ConsoleStr = msg + "\n" + ConsoleStr;
	}

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<UILabel>().text = ConsoleStr;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<UILabel>().text = ConsoleStr;
	}
}
