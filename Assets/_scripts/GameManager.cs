//ExitBehavior.cs (very) work in progress
//@author: Mario Tommadich
//Date 20.02.2017

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public string unlockedScene;
	public int difficulty;
	public int maxLives;
	public int currLives;
	public int hiScore;
	public int score;
	public GameObject loadingimage;
	public GameObject camera;
	private TouchScript touchScript;
//	public bool canContinue;
	public bool isPaused;
	public GameObject pauseUI;



	// Use this for initialization
	void Start () {		
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		touchScript = camera.GetComponent<TouchScript> ();

		if (!SceneManager.GetActiveScene ().name.Equals("Credits")) {
			unlockedScene = SceneManager.GetActiveScene ().name;
		}
		maxLives = 8;
		currLives = 8;
		difficulty=1;
		hiScore = 0;
		score = 0;
		isPaused = false;

		if (!PlayerPrefs.HasKey ("unlockedScene")) {
				Debug.Log("no save data found. Creating data now");
			PlayerPrefs.SetString ("unlockedScene", unlockedScene);
			Debug.Log("unlocked: " +PlayerPrefs.GetString("unlockedScene")+" Index: "+SceneManager.GetActiveScene().buildIndex);
			PlayerPrefs.SetInt ("difficulty", difficulty);
				Debug.Log("difficulty: " +PlayerPrefs.GetString("difficulty"));
			PlayerPrefs.SetInt ("maxLives", maxLives);
				Debug.Log("maxLives: " +PlayerPrefs.GetString("maxLives"));
			PlayerPrefs.SetInt ("currentLives", maxLives);
				Debug.Log("currLives: " +PlayerPrefs.GetString("currentLives"));
			PlayerPrefs.SetInt ("hiscore", hiScore);
			Debug.Log ("DONE!");

		} else {
			
			Debug.Log("Contents of save data:");

			if(SceneManager.GetActiveScene().buildIndex != 0 
				&& !SceneManager.GetActiveScene ().name.Equals("Credits")){
				PlayerPrefs.SetString ("unlockedScene", SceneManager.GetActiveScene ().name);
			}

			unlockedScene = PlayerPrefs.GetString("unlockedScene");
			Debug.Log ("unlocked Scene = "+unlockedScene);
			maxLives = PlayerPrefs.GetInt ("maxLives");
			Debug.Log ("Max Lives: "+maxLives);
			currLives = PlayerPrefs.GetInt ("currentLives");
			Debug.Log ("Current Lives: "+currLives);
			difficulty = PlayerPrefs.GetInt ("difficulty");
			Debug.Log ("Difficulty: "+difficulty);
			hiScore = PlayerPrefs.GetInt ("hiScore");
			Debug.Log ("hiScore: "+hiScore);
		}

		if (GameObject.FindGameObjectWithTag ("pauseUI") == null) {
			Debug.Log ("pauseUI is null");
			return;
		} else {
			pauseUI = GameObject.FindGameObjectWithTag ("pauseUI");
			pauseUI.SetActive (false);
		Debug.Log (pauseUI.name+" deactivated");
		}

	}
	//for debug purposes
	public void deletePlayerPrefs(){
		PlayerPrefs.DeleteKey ("unlockedScene");
		PlayerPrefs.DeleteKey ("difficulty");
		PlayerPrefs.DeleteKey ("maxLives");
		PlayerPrefs.DeleteKey ("currentLives");
		PlayerPrefs.DeleteKey ("hiScore");
		Debug.Log ("PlayerPrefs deleted");
	}

	public void loadAnyScene(string scene){
		SceneManager.LoadScene (scene);
	}

//	public void setCanContinue (){
//		if (SceneManager.GetSceneByName (unlockedScene).buildIndex > 2) {
//			canContinue = true;
//		} else {
//			canContinue = false;
//		}
//	}

	public void continueGame(){
		SceneManager.LoadScene (unlockedScene);
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

	public void saveGameStats(){
		//PlayerPrefs.SetString ("unlockedScene", SceneManager.GetActiveScene().name);

		PlayerPrefs.SetInt ("maxLives", maxLives);
		PlayerPrefs.SetInt ("difficulty", difficulty);
		PlayerPrefs.SetInt ("hiScore", hiScore);
		//We don't need to save currLives as they get reset to maximum at gamestart. 
		//CurrLives only determine if a mission is failed.
		//PlayerPrefs.SetInt ("currentLives", currLives);

	}

	public void loadNextScene (){
		if ((SceneManager.GetActiveScene ().buildIndex + 1).Equals( null)) {
			Debug.Log ("This is the perfect place to end the game and run the credits");
		}
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);

	}
	

	public void exit(){
		Application.Quit();
	}

	public void subtractLive(){
		if (currLives > 1) {
			currLives -= 1;
			Debug.Log ("Lives Left: " + currLives);
		} else {
			gameOver ();
			Debug.Log("Game Over");
		}
	}

	public void missionClear(){
		saveGameStats ();
		Debug.Log ("PlayerPrefs saved");
		loadNextScene ();

	}
	//TO DO:
	public void gameOver(){
		//here we should present the user with the option to continue the current progress 
		//in exchange for watching an advertisement or to start the mission over.
		//Right now though, we finish the game and return to the main menu.
		SceneManager.LoadScene("Credits");
	}

	public void togglePause(){
		isPaused = !isPaused;

		if (isPaused) {
			pauseUI.SetActive (true);
			touchScript.enabled = false;
			Time.timeScale = 0;

			//touchScript.enabled = false; - buggy
		} else {
			Time.timeScale = 1;
			pauseUI.SetActive (false);
			touchScript.enabled = true;
		//	touchScript.enabled = true; - buggy
		}
	}

}
