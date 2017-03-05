//ExitBehavior.cs (very) work in progress
//@author: Mario Tommadich
//Date 20.02.2017

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	public string unlockedScene;
	public int difficulty;
	public int maxLives;
	public int currLives;
	public int hiScore;
	public int score;
	public GameObject loadingimage;


	// Use this for initialization
	void Start () {
		unlockedScene = "scene1";
		maxLives = 8;
		currLives = 8;
		difficulty=1;
		hiScore = 0;
		score = 0;


		if (PlayerPrefs.GetString ("scene") == null) {
			PlayerPrefs.SetString ("scene", unlockedScene);
			PlayerPrefs.SetInt ("difficulty", difficulty);
			PlayerPrefs.SetInt ("maxLives", maxLives);
			PlayerPrefs.SetInt ("currentLives", maxLives);
			PlayerPrefs.SetInt ("hiscore", hiScore);
			Debug.Log("no save data found. Creating data now");
			return;
		} else {
			Debug.Log("loading save data");
			unlockedScene = PlayerPrefs.GetString("scene");
			maxLives = PlayerPrefs.GetInt ("maxLives");
			currLives = PlayerPrefs.GetInt ("currenLives");
			difficulty = PlayerPrefs.GetInt ("difficulty");
			hiScore = PlayerPrefs.GetInt ("hiScore");
		}
	}

	public void loadAnyScene(string scene){
		SceneManager.LoadScene (scene);
	}

	public void newGame(){
		loadingimage.SetActive (true);
		SceneManager.LoadScene ("scene1");
	}

	public int getHiScore(){
		return hiScore;
	}

	public void setHiScore(){
		if (hiScore >= score) {
			return;
		} else {
			hiScore = score;
		}
	}

	public void saveGame(string unlockedScene, int currLives){
		PlayerPrefs.SetString ("scene", unlockedScene);
		PlayerPrefs.SetInt ("maxLives", maxLives);
		PlayerPrefs.SetInt ("difficulty", difficulty);
		PlayerPrefs.SetInt ("hiScore", hiScore);
		PlayerPrefs.SetInt ("currentLives", currLives);

	}

	public void setUnlockedScene (string newScene){
		unlockedScene = newScene;
	}
	

	public void exit(){
		Application.Quit();
	}

	public void subtractLive(){
		if (currLives > 1) {
			currLives -= 1;
		} else {
		//	gameOver ();
			Debug.Log("Game Over");
		}
	}


	//TO DO:
	//public void gameOver(){};
	//
}
