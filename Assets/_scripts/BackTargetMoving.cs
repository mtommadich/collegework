using UnityEngine;
using System.Collections;

public class BackTargetMoving : MonoBehaviour {
	public int targetHealth;
	private GameObject arena;
	Arena arenaScript;
	public GameObject explosion;

	// Use this for initialization of all variables which need to have values at game start
	void Start () {
		//targetHealth = 3; we may hard code these values once they are final

		//fetching the arena GameObject and arena class
		arena = GameObject.FindGameObjectWithTag("arena");
		arenaScript = arena.GetComponent<Arena> ();

	}

	// Update is called once per frame
	void Update () {
		//Mathf.Sin

	}

	//on collision with the ball:
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("ball")) {
			//behavior 1 subtract health
			targetHealth--;

			//if target health is 0:
			if (targetHealth < 1) {
				// create explosion object
				Instantiate (explosion, transform.position, Quaternion.identity);
				//subtract 1 target from target counter 
				arenaScript.removeTarget ();
				//finally destroy this target object to free up memory
				Destroy (gameObject);
			}

			//behavior 2
			Vector3 tmpPosition = transform.position;
			int randomPosition = Random.Range(0, arenaScript.backTargetValues.Length);
			tmpPosition.x = arenaScript.backTargetValues[randomPosition].x;
			tmpPosition.y = arenaScript.backTargetValues[randomPosition].y;
			transform.position = tmpPosition;





		}
	}
}
