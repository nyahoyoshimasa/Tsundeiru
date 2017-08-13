using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTitleScript : MonoBehaviour {

	public void MoveTitleScene() {
		PlayerPrefs.Save();
		Application.LoadLevel ("title");
	}

}
