using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreScript : MonoBehaviour {
	private int now_score = 0;
	private int best_score = 0;
	// Use this for initialization
	void Start () {
		now_score = PlayerPrefs.GetInt("dum_score", 0);
		best_score = PlayerPrefs.GetInt("best_score", 0);
		if (best_score < now_score) {
			best_score = now_score;
			PlayerPrefs.SetInt ("best_score", best_score);
			GetComponent<Text> ().text = "Best Score:" + best_score.ToString () + "(update!)";
		} else {
			GetComponent<Text> ().text = "Best Score:" + best_score.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
