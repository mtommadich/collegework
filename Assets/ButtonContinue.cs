using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonContinue : MonoBehaviour {

	private GameObject gameManager;
	private GameManager managerScript;
	private Button btnContinue;
	private bool interactable = false;

	// Use this for initialization
	void Start () {
		//find the GameManager Object and store it in a local variable
		gameManager = GameObject.FindGameObjectWithTag("manager");
		//find the GameManager's GameManager component script and store it in a local variable
		managerScript = gameManager.GetComponent<GameManager> ();

		btnContinue = GetComponent<Button> ();

		if(PlayerPrefs.HasKey("unlockedScene") && !PlayerPrefs.GetString("unlockedScene").Equals("mainmenu") && !PlayerPrefs.GetString("unlockedScene").Equals("scene1")){
			btnContinue.interactable=true;
		}
	}
	


}
