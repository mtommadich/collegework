﻿//@author: Mario Tommadich
//Date 20.02.2017

using UnityEngine;
using System.Collections;

public class GravityEffectorBehavior : MonoBehaviour {
	public int targetHealth;
	private GameObject arena;
	private GameObject ball;
	Arena arenaScript;
	public GameObject explosion;
	MoveBallnoPhysics ballScript;



	// Use this for initialization of all variables which need to have values at game start
	void Start () {
		//targetHealth = 3; we may hard code these values once they are final

		//fetching the arena GameObject and arena class
		arena = GameObject.FindGameObjectWithTag("arena");
		arenaScript = arena.GetComponent<Arena> ();
		//fetching the ball
		ball = GameObject.FindGameObjectWithTag("ball");
		ballScript = ball.GetComponent<MoveBallnoPhysics>();


	}

	// Update is called once per frame
	void Update () {

	}

	//on collision with the ball:
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("ball")) {
			//behavior 1 subtract health
			targetHealth--;
			ballScript.toggleGravity ();


			//if target health is 0:
			if (targetHealth < 1) {
				// create explosion object
				Instantiate (explosion, transform.position, Quaternion.identity);
				//subtract 1 target from target counter 
				arenaScript.removeTarget ();

				//finally destroy this target object to free up memory
				Destroy (gameObject);
			}

		}
	}


}
