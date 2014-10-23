using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	public List<GameObject> Units = new List<GameObject>();

	public Camera cam;
	public UnitObject hp;
	Vector2 unitPos;

	private string hpPrint;

	// Use this for initialization
	void Start () {
		cam = transform.GetComponentInChildren<Camera>();
	}
	
	void OnGUI(){
		foreach(GameObject unit in Units) {
			unitPos = cam.WorldToScreenPoint(unit.transform.position);
			hp = unit.GetComponent("UnitObject") as UnitObject;
			hpPrint = hp.HP.ToString();
			if(hp.Alive){
				GUI.Label(new Rect(unitPos.x , Screen.height - unitPos.y, 100, 25), hpPrint);
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
