using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Const;

public class ballScript : MonoBehaviour {

	public GameObject ballPrefab;
	public Sprite[] ballSprites;
	private GameObject firstBall;
	private GameObject lastBall;
	private string currentName;
	public int[] num_animal = new int[Const.C0.animal_species];
	List<GameObject> removableBallList = new List<GameObject>();
	public GameObject scoreGUI;  //スコアを表示するGUI(Text)
	private int point = Const.C0.basic_point;
	public GameObject shuffleButton;
	public bool isPlay = true;

	public GameObject cats;
	public GameObject toras;
	public GameObject pandas;
	public GameObject pigs;
	public GameObject monkeys;

	public GameObject used_skills_text;

	public GameObject score_rate_text;

	private int[] num_use_animal = new int[Const.C0.animal_species];
	private double score_rate = 1.0;
	public int animal_skill = -1;
	private int duration_skill = -1;
	private Color originalcolor = new Color(1.0f,1.0f,1.0f,1.0f);
	private Color redcolor = new Color (1.0f, 0.0f, 0.0f, 1.0f);
	void Start () {
		StartCoroutine(DropBall(Const.C0.number_animal));
		used_skills_text.SetActive (false);
		score_rate_text.GetComponent<Text> ().text = "ScoreRate:" + score_rate.ToString ();
	}

	void Update () {
		if (isPlay) {
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);
				if (touch.phase == TouchPhase.Began && firstBall == null)
					OnDragStart ();
				else if (touch.phase == TouchPhase.Ended)
					OnDragEnd ();
				else if (firstBall != null)
					OnDragging ();
			} else {
				if (Input.GetMouseButtonDown (0) && firstBall == null) {
					OnDragStart ();
				} else if (Input.GetMouseButtonUp (0)) {
					OnDragEnd ();
				} else if (firstBall != null) {
					OnDragging ();
				}
			}
			if (animal_skill != -1) {
				switch (animal_skill) {
				case 0://cat
					duration_skill -= 1;
					if (duration_skill < 0) {
						duration_skill = -1;
						animal_skill = -1;
						used_skills_text.SetActive (false);
						score_rate = 1.0;
					}
					break;
				case 1://dog
					break;
				case 2://panda
					duration_skill -= 1;
					if (duration_skill < 0) {
						duration_skill = -1;
						animal_skill = -1;
						used_skills_text.SetActive (false);
						score_rate = 1.0;
					}
					break;
				case 3://pig
					break;
				case 4://monkey
					break;
				}
			}
			if ((num_animal [0] - num_use_animal [0]) > Const.C0.skill_require_num)
				cats.GetComponent<SpriteRenderer> ().color = redcolor;
			if ((num_animal [1] - num_use_animal [1]) > Const.C0.skill_require_num)
				toras.GetComponent<SpriteRenderer> ().color = redcolor;
			if ((num_animal [2] - num_use_animal [2]) > Const.C0.skill_require_num)
				pandas.GetComponent<SpriteRenderer> ().color = redcolor;
			if ((num_animal [3] - num_use_animal [3]) > Const.C0.skill_require_num)
				pigs.GetComponent<SpriteRenderer> ().color = redcolor;
			if ((num_animal [4] - num_use_animal [4]) > Const.C0.skill_require_num)
				monkeys.GetComponent<SpriteRenderer> ().color = redcolor;
			score_rate_text.GetComponent<Text> ().text = "ScoreRate:" + score_rate.ToString ();
		}
	}

	private int GetAnimalIndex(string name) {
		char a = name [Const.C0.num_animal_name];
		int b = int.Parse (a.ToString ());
		return b;
	}

	private void Skills(int b) {
		if ((num_animal [b] - num_use_animal [b]) < Const.C0.skill_require_num) {
			return;
		}
		num_use_animal [b] = num_animal [b];
		animal_skill = b;
		switch (b) {
		case 0://cat(score:1.2times)
			score_rate = 1.2;
			duration_skill = 1000;
			used_skills_text.SetActive (true);
			cats.GetComponent<SpriteRenderer> ().color = originalcolor;
			break;
		case 1://dog(1000point)
			scoreGUI.SendMessage ("AddPoint", 1000);
			animal_skill = -1;
			toras.GetComponent<SpriteRenderer> ().color = originalcolor;
			break;
		case 2://panda
			score_rate = 2.5;
			duration_skill = 5000;
			pandas.GetComponent<SpriteRenderer> ().color = originalcolor;
			break;
		case 3://pig
			pigs.GetComponent<SpriteRenderer> ().color = originalcolor;
			break;
		case 4://monkey
			score_rate += 0.02;
			animal_skill = -1;
			monkeys.GetComponent<SpriteRenderer> ().color = originalcolor;
			break;
		}
	}

	private void OnDragStart() {
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);

		if (hit.collider != null) {
			GameObject hitObj = hit.collider.gameObject;
			string ballName = hitObj.name;
			if (ballName.StartsWith (Const.C0.animal_name)) {
				firstBall = hitObj;
				lastBall = hitObj;
				currentName = hitObj.name;
				removableBallList = new List<GameObject> ();
				PushToList (hitObj);
			} else if (animal_skill == -1 && ballName.StartsWith (Const.C0.skill_icon_ahead)) {
				char a = ballName [Const.C0.skill_ahead_num];
				int b = int.Parse (a.ToString ());
				Skills (b);
			}
		}
	}

	private void OnDragging ()
	{
		RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
		if (hit.collider != null) {
			GameObject hitObj = hit.collider.gameObject;

			//同じブロックをクリックしている時
			if (hitObj.name == currentName && lastBall != hitObj) {
				float distance = Vector2.Distance (hitObj.transform.position, lastBall.transform.position);
				if (distance < 1.3f) {
					//削除対象のオブジェクトを格納
					lastBall = hitObj;
					PushToList(hitObj);
				}
			}
		}
	}

	private void OnDragEnd () {
		int remove_cnt = removableBallList.Count;
		if (remove_cnt >= 3) {
			for (int i = 0; i < remove_cnt; i++) {
				Destroy (removableBallList [i]);
			}
			//remove_cnt*100だけスコアの加点
			var index = GetAnimalIndex(currentName);
			int plusPoint = PlayerPrefs.GetInt ("Lv.ANIMAL" + index.ToString());
			scoreGUI.SendMessage ("AddPoint",(point + plusPoint) * remove_cnt * score_rate);
			if (score_rate >= 2.0) {
				score_rate = 1.0;
				animal_skill = -1;
			}
			StartCoroutine (DropBall (remove_cnt));
			removableBallList.Clear ();
		} else {
			for (int i = 0; i < remove_cnt; i++) {
				ChangeColor (removableBallList[i],1.0f);
			}
		}
		firstBall = null;
		lastBall = null;
	}

	IEnumerator DropBall(int count) {
		if (count == Const.C0.number_animal) {
			StartCoroutine ("RestrictPush");
		} else {
			Debug.Log (currentName);
			var index = GetAnimalIndex (currentName);
			num_animal[index] += count;
		}

		for (int i = 0; i < count; i++) {
			Vector2 pos = new Vector2(Random.Range(-2.0f, 2.0f), 7f);
			GameObject ball = Instantiate(ballPrefab, pos,
				Quaternion.AngleAxis(Random.Range(-40, 40), Vector3.forward)) as GameObject;
			int spriteId = Random.Range(0, Const.C0.animal_species);
			ball.name = "ANIMAL" + spriteId;
			SpriteRenderer spriteObj = ball.GetComponent<SpriteRenderer>();
			spriteObj.sprite = ballSprites[spriteId];
			yield return new WaitForSeconds(0.05f);
		}
	}
		
	IEnumerator RestrictPush () {
		shuffleButton.GetComponent<Button>().interactable = false;
		yield return new WaitForSeconds(5.0f);
		shuffleButton.GetComponent<Button>().interactable = true;
	}

	void PushToList (GameObject obj) {
		removableBallList.Add (obj);
		ChangeColor(obj, 0.5f);
	}

	void ChangeColor (GameObject obj, float transparency) {
		SpriteRenderer ballTexture = obj.GetComponent<SpriteRenderer>();
		ballTexture.color = new Color(ballTexture.color.r, ballTexture.color.g, ballTexture.color.b, transparency);
	}
}