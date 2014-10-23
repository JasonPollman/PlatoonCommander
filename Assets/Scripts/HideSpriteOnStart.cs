using UnityEngine;
using System.Collections;

public class HideSpriteOnStart : MonoBehaviour {

	void Start () { NGUITools.SetActive (gameObject, false); }

}
