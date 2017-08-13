using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThisTimeScoreTextScript : MonoBehaviour {
	private int score = 0;
	// Use this for initialization
	void Start () {
		score = PlayerPrefs.GetInt ("dum_score", 0);
		GetComponent<Text> ().text = "This Time Score:" + score.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
