//ExitBehavior.cs
//@author: Mario Tommadich
//Date 20.02.2017

using UnityEngine;
using System.Collections;

public class ExitBehaviour : MonoBehaviour {
	private GameObject ball;
	private GameObject arena;
	private GameObject gameManager;
	Arena arenaScript;
	MoveBallnoPhysics ballMovement;
	GameManager managerScript;



	// Use this for initialization
	void Start () {
		Debug.Log ("Exit is starting");
		//find the ball object and assign it to a local variable
		ball = GameObject.FindGameObjectWithTag("ball");

		//find the movement behaviour script component of the ball and store it in a local variable 
		ballMovement = ball.GetComponent<MoveBallnoPhysics> ();

		//if we're running in a tutorial execute the Exit block of the tutorialscript 
		if(ballMovement.isTutorial()){
			Debug.Log ("we're in a tutorial");
			ballMovement.tutorialScript.ExecuteBlock ("Exit");
		}

		//find the arena object and store it in a local variable
		arena = GameObject.FindGameObjectWithTag("arena");

		//find the arena behaviour script and store it in a local variable
		arenaScript = arena.GetComponent<Arena> ();

		//find the GameManager Object and store it in a local variable
		gameManager = GameObject.FindGameObjectWithTag("manager");
		//find the GameManager's GameManager component script and store it in a local variable
		managerScript = gameManager.GetComponent<GameManager> ();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("ball") && ballMovement.speedZ >0) {

			//If we're in a tutorial, show the Gratz block of the tutorial
			if (ballMovement.isTutorial ()) {
				
				ballMovement.tutorialScript.ExecuteBlock ("Gratz");
			} else {
				//phew - mission accomplished.
				managerScript.missionClear ();
			}
		}
	}
}
