using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Const;

public class ScoreAnimalScript : MonoBehaviour {
	public GameObject catText;
	public GameObject dogText;
	public GameObject pandaText;
	public GameObject pigText;
	public GameObject monkeyText;
	// Use this for initialization
	void Start () {
		int[] num_animal = new int[Const.C0.animal_species];
		for (int i = 0; i < Const.C0.animal_species; i++)
			num_animal [i] = PlayerPrefs.GetInt ("ANIMAL" + i.ToString (), 0);
		int[] total_num_animal = new int[Const.C0.animal_species];
		for (int i = 0; i < Const.C0.animal_species; i++) {
			total_num_animal [i] = PlayerPrefs.GetInt ("BESTANIMAL" + i.ToString (), 0) + num_animal [i];
			PlayerPrefs.SetInt ("Lv.ANIMAL" + i.ToString (), (total_num_animal[i]/100) * 20);
		}
		catText.GetComponent<Text> ().text = "CAT:" + num_animal[0].ToString () + "\n(Total:" + total_num_animal[0].ToString() + "  |  Lv:" + (total_num_animal[0]/100).ToString() + ")";
		dogText.GetComponent<Text> ().text = "TORA:" + num_animal[1].ToString () + "\n(Total:" + total_num_animal[1].ToString() + "  |  Lv:" + (total_num_animal[1]/100).ToString() + ")";
		pandaText.GetComponent<Text> ().text = "PANDA:" + num_animal[2].ToString () + "\n(Total:" + total_num_animal[2].ToString() + "  |  Lv:" + (total_num_animal[2]/100).ToString() + ")";
		pigText.GetComponent<Text> ().text = "PIG:" + num_animal[3].ToString () + "\n(Total:" + total_num_animal[3].ToString() + "  |  Lv:" + (total_num_animal[3]/100).ToString() + ")";
		monkeyText.GetComponent<Text> ().text = "MONKEY:" + num_animal[4].ToString () + "\n(Total:" + total_num_animal[4].ToString() + "  |  Lv:" + (total_num_animal[4]/100).ToString() + ")";
		PlayerPrefs.Save ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
