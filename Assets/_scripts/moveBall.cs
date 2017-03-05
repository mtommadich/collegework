using UnityEngine;
using System.Collections;

public class moveBall : MonoBehaviour {

	public AudioClip bounceWall;
	public AudioClip bouncePaddle;
	public AudioClip bounceMiss;
	public float ballForce;
	Rigidbody rb;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent <AudioSource> ();
		rb = GetComponent<Rigidbody> ();
		rb.AddForce(transform.forward * ballForce, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void FixedUpdate () {



	}

	void OnCollisionEnter(Collision other) {

		if (other.gameObject.CompareTag ("Player")){			
			bounceFX (bouncePaddle);
		}
		if (other.gameObject.CompareTag ("walls")) {
			bounceFX (bounceWall);
		}
		if (other.gameObject.CompareTag ("miss")) {
			bounceFX (bounceMiss);
		}
		if (other.gameObject.CompareTag ("front")) {
			bounceFX (bounceWall);
			//rb.AddForce(transform.forward * -1, ForceMode.Impulse);
		}
	}

	void bounceFX (AudioClip fx){
		
		audioSource.clip = fx;
		audioSource.Play ();
	}




}
