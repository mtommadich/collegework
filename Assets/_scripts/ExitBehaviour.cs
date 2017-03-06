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
		//find the ball object and assign it to a local variable
		ball = GameObject.FindGameObjectWithTag("ball");

		//find the movement behaviour script component of the ball and store it in a local variable 
		ballMovement = ball.GetComponent<MoveBallnoPhysics> ();

		//find the arena object and store it in a local variable
		arena = GameObject.FindGameObjectWithTag("arena");

		//find the arena behaviour script and store it in a local variable
		arenaScript = arena.GetComponent<Arena> ();

		//find the GameManager Object and store it in a local variable
		gameManager = GameObject.FindGameObjectWithTag("manager");
		//find the GameManager's GameManager component script and store it in a local variable
		managerScript = gameManager.GetComponent<GameManager> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("ball") && ballMovement.speedZ >0) {
			
			//we do this for now - this behaviour will become the "Endless mode" once mission based gameplay has been developed
			//arenaScript.spawnTargets ();
			//Destroy (gameObject);


			//but we should be doing this:
			//ballMovement.stopBall (transform.position);
			managerScript.missionClear();


			}

	}
}
