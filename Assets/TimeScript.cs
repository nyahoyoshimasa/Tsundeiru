using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class TimeScript : MonoBehaviour {
	Slider timebar;
	public ballScript BallScript;
	public GameObject shuffle_button;
	public GameObject gameover_text;
	public GameObject result_button;
	public ScoreScript scores;
	bool gameover_flag;
	float time = 0;
	// Use this for initialization
	void Start () {
		gameover_text.SetActive (false);
		result_button.SetActive (false);
		timebar = GetComponent <Slider>();
		timebar.value = timebar.maxValue;
		gameover_flag = false;
		time = timebar.maxValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameover_flag) {
			time -= 0.001f;
			timebar.value = time;
			if (timebar.value <= timebar.minValue) {
				gameover_flag = true;
				StartCoroutine ("GameOver");
			}
		}
		if (BallScript.animal_skill == 3) {
			if (timebar.value < (timebar.maxValue - 0.3f)) {
				timebar.value += 0.3f;
				time = timebar.value;
			}
			BallScript.animal_skill = -1;
		}
			
	}
	IEnumerator GameOver() {
		gameover_text.SetActive (true);
		//result_button.SetActive (true);
		//result_button.GetComponent<Button> ().interactable = true;
		shuffle_button.GetComponent<Button> ().interactable = false;
		BallScript.isPlay = false;
		for (int i = 0; i < Const.C0.animal_species; i++) {
			PlayerPrefs.SetInt ("ANIMAL" + i.ToString(),BallScript.num_animal[i]);
		}
		var dummy_score = scores.score;
		PlayerPrefs.SetInt ("dum_score", dummy_score);
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel ("result");
	}
}
