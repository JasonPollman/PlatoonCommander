using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	public List<GameObject> Units = new List<GameObject>();

	public Camera cam;
	public UnitObject hp;
	Vector2 unitPos;

	private string hpPrint;
	private string dpPrint;
	public GUIStyle dpStyle;
	public GUIStyle hpStyle;
	// Use this for initialization
	void Start () {
		cam = transform.GetComponentInChildren<Camera>();

	}
	
	void OnGUI(){
		foreach(GameObject unit in Units) {
			unitPos = cam.WorldToScreenPoint(unit.transform.position);
			hp = unit.GetComponent("UnitObject") as UnitObject;
			if(hp) {
				hpPrint = hp.HP.ToString();
				dpPrint = hp.DP.ToString();
			}
			if(hp && hp.Alive){
				GUI.Label(new Rect(unitPos.x + 3 , Screen.height - unitPos.y, 100, 25), hpPrint, hpStyle);
				GUI.Label(new Rect(unitPos.x - 15, Screen.height - unitPos.y - 15, 100, 25), dpPrint, dpStyle);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		GetUnits();
	}

	void GetUnits() {
		foreach(GameObject unit in GameObject.FindGameObjectsWithTag ("unit")) {
			if(!Units.Contains (unit)) {
				Units.Add (unit);
			}
		}
	}
}
