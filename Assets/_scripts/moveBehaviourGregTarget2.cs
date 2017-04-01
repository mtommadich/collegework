//moveBehaviourGregTarget.cs
//@author: Greg Dunne
//Date 16.03.2017

using UnityEngine;
using System.Collections;

public class moveBehaviourGregTarget2 : MonoBehaviour {

	public int targetHealth;
	private GameObject arena;
	Arena arenaScript;
	public GameObject explosion;
	public Vector3 position;
	public Vector3 localScale;
	public Vector3 rotate;

	public float speed;
	public bool reverseMove = false;

	private float startTime;
	private float journeyLength;

	//left and right boundaries for moving target.
	Vector3 leftBoundary;
	Vector3 rightBoundary;

	// Use this for initialization of all variables which need to have values at game start
	void Start () {
		targetHealth = 1;

		//fetching the arena GameObject and arena class
		arena = GameObject.FindGameObjectWithTag("arena");
		arenaScript = arena.GetComponent<Arena> ();

		//left and right boundary positions.
		leftBoundary = new Vector3 (-2.15f,transform.position.y,transform.position.z);
		rightBoundary = new Vector3 (2.15f,transform.position.y,transform.position.z);

		//Moving the target
		startTime = Time.time;
		journeyLength = Vector3.Distance (leftBoundary, rightBoundary);

	}

	// Update is called once per frame
	void Update () {

		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;

		//go from right to left
		if (reverseMove) {
			transform.position = Vector3.Lerp (rightBoundary, leftBoundary, fracJourney);
		}else{
			//go from left to right
			transform.position = Vector3.Lerp (leftBoundary, rightBoundary, fracJourney);
		}
		//bounce
		if ((Vector3.Distance(transform.position, rightBoundary)==0.0f || Vector3.Distance(transform.position, leftBoundary)==0.0f)){
			if (reverseMove){
				reverseMove=false;
			}
			else{
				reverseMove = true;
			}
			startTime = Time.time;
		}

	}


	//on collision with the ball:
	void OnTriggerEnter(Collider other) {
		//transform.position = new Vector3 (0, 1.5f,0.1f);
		//transform.localScale += new Vector3(0.2f, 0.2f, 0);

		if (other.CompareTag ("ball")) {
			//subtract target health
			targetHealth--;

			//if target health is 0:
			if(targetHealth < 1){
				//subtract 1 target from target counter  
				arenaScript.removeTarget ();
				// create explosion object
				Instantiate (explosion, transform.position, Quaternion.identity);
				//finally destroy this target object to free up memory 
				Destroy (gameObject);
			}
		}
	}

}

