//MoveBallNoPhysics.cs
//@author: Mario Tommadich
//Date 22.02.2017

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Fungus;

public class MoveBallnoPhysics : MonoBehaviour {
	
	public bool ballStopped;
	public bool ballPaused;
	public bool isBoosted;
	public bool hasGravity;
	public float fakeGravity;
	public int zmax;
	public int zmin;
	public int xmax;
	public int xmin;
	public int ymax;
	public int ymin;
	public float startSpeedZ;
	public float startSpeedX;
	public float startSpeedY;
	public float speedZ;
	public float speedX;
	public float speedY;
	public float ballZ;
	public float ballX;
	public float ballY;
	private float tmpSpeedX = 0.0f;
	private float tmpSpeedY = 0.0f;
	private float tmpSpeedZ = 0.0f;
	public AudioClip bounceWall;
	public AudioClip bouncePaddle;
	public AudioClip bounceMiss;
	public AudioClip bounceSpring;
	AudioSource audioSource;
	private GameObject paddle;
	private float paddlex;
	private float paddley;
	private float deltax;
	private float deltay;
	Vector3 startPos;
	GameObject camera;
	private GameObject manager;
	private GameManager managerScript;
	private GameObject gravityEffect;
	CameraShake shaker;
	public Flowchart tutorialScript;

	//this boolean is only used for the very first ingame tutorial 
	//it should always default to false!
	private bool tutorial;





	// Use this for initialization
	void Start () {
		
		audioSource = GetComponent <AudioSource> ();
		paddle = GameObject.FindGameObjectWithTag("Player");
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		shaker = camera.GetComponent<CameraShake> ();
		manager = GameObject.FindGameObjectWithTag ("manager");
		managerScript = manager.GetComponent<GameManager> ();


		if (GameObject.FindGameObjectWithTag ("gravityeffect") == null) {
			hasGravity = false;
		} else {
			gravityEffect = GameObject.FindGameObjectWithTag ("gravityeffect");
			gravityEffect.SetActive (false);
		}

		startPos = paddle.transform.position;
		speedX = startSpeedX;
		speedY = startSpeedY;
		speedZ = startSpeedZ;
		tutorial = false;
		ballStopped = true;
		hasGravity = false;
		isBoosted = false;
		fakeGravity = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (ballStopped) {
			Vector3 ballMovement = new Vector3 (paddle.transform.position.x, 
												paddle.transform.position.y, 1.9f);
			transform.position = ballMovement;
		} else {
			Vector3 ballMovement = new Vector3 (transform.position.x + speedX * Time.deltaTime, 
												transform.position.y + speedY * Time.deltaTime, 
												transform.position.z + speedZ * Time.deltaTime);
			transform.position = ballMovement;
		}

		//ball hit the wall in the end
		if (transform.position.z >= zmax - .2f && speedZ >0.0f) {
			speedZ = -speedZ;
			bounceFX (bounceWall);

		//ball hit the screen
		} else if (transform.position.z <= zmin && speedZ < 0.0f) {
			speedZ = startSpeedZ; // reset speed to default
			shaker.Crack(transform.position);
			CameraShake.Shake(0.1f, 0.2f);
			managerScript.subtractLive();
			bounceFX (bounceMiss);

			if (tutorial){
				tutorialScript.ExecuteBlock("Ouch");
			}

		}

		if (transform.position.x >= xmax - .2f && speedX >0.0f){
			speedX = -speedX;
			bounceFX (bounceWall);

		} else if (transform.position.x <= xmin + .2f && speedX <0.0f) {
			speedX = -speedX;
			bounceFX (bounceWall);
		}

		if (transform.position.y >= ymax - .2f && speedY > 0.0f) {
			speedY = -speedY;
			bounceFX (bounceWall);

		} else if (transform.position.y <= ymin + .2f && speedY < 0.0f) {
			speedY = -speedY;
			bounceFX (bounceWall);
		}

		//speedY -= 0.01f; this adds some fake gravity. This is used by the gravity effector target
		speedY -= fakeGravity;
	}
	//detects collision with the paddle
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player") && speedZ<0) {
			paddlex = paddle.transform.position.x;
			paddley = paddle.transform.position.y;
			deltax = transform.position.x - paddlex;
			deltay = transform.position.y - paddley;

			bounceFX (bouncePaddle);
			speedX = deltax * 6.0f;
			speedY = deltay * 6.0f; 

			if (isBoosted) {
				isBoosted = !isBoosted;
				speedZ = startSpeedZ;
			} else {
				speedZ = -speedZ;
				speedZ += 0.1f;
			}
		} else {
			return;
		}
	}

	public void bounceFX (AudioClip fx){
		audioSource.Stop ();
		audioSource.clip = fx;
		audioSource.Play ();
	}

	//starts the ball at the position of the paddle
	public void startBall(){

		speedX = startSpeedX;
		speedY = startSpeedY;
		speedZ = startSpeedZ;
		ballStopped = false;

		//Execute AfterMove block in tutorial script if we are in a tutorial
		if (tutorial){
			tutorialScript.ExecuteBlock("AfterMove");
		}
	}

	//stops the ball when called and puts it at the passed-in stopPosition
	public void stopBall(Vector3 stopPosition){
		speedX = 0.0f;
		speedY = 0.0f;
		speedZ = 0.0f;
		transform.position = stopPosition;
		ballStopped = true;
	}

	public void pauseBall(){		
			tmpSpeedX = speedX;
			speedX = 0.0f;
			tmpSpeedY = speedY;
			speedY = 0.0f;
			tmpSpeedZ = speedZ;
			speedZ = 0.0f;
			ballPaused = true;
		} 

	public void unpauseBall(){
			speedX = tmpSpeedX;
			speedY = tmpSpeedY;
			speedZ = tmpSpeedZ;
			tmpSpeedX = 0.0f;
			tmpSpeedY = 0.0f;
			tmpSpeedZ = 0.0f;
			ballPaused = false;
	}

	public void toggleGravity(){
		hasGravity = !hasGravity;

		if (hasGravity) {
			fakeGravity = 0.04f;
			gravityEffect.SetActive (true);
		} else {
			fakeGravity = 0.0f;
			gravityEffect.SetActive (false);
		}			
	}

	public void speedBoost(){
		bounceFX (bounceSpring);
		speedZ *= 2.0f;
		isBoosted = true;
	}

	public void setTutorial(){
		tutorial = true;
	}

	public bool isTutorial(){
		return tutorial;
	}
}
