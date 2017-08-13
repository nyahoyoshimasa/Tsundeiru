using UnityEngine;
using System.Collections;
using Const;

public class ShuffleScript : MonoBehaviour {
	public ballScript BallScript;

	public void Shuffle () {
		Debug.Log ("shuffle");
		//配列に「respawn」タグのついているオブジェクトを全て格納
		GameObject[] animals = GameObject.FindGameObjectsWithTag("Respawn");
		//全て取り出し、削除
		foreach (GameObject obs in animals) {
			Destroy(obs);
		}
		//ballScriptのDropBallメソッドを実行し、50のひよこを作成
		BallScript.SendMessage("DropBall", Const.C0.number_animal);
	}
}
