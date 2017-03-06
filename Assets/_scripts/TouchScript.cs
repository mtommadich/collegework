//TouchScript.cs
//@author: Mario Tommadich
//Date 20.02.2017

using UnityEngine;
using System.Collections;

public class TouchScript : MonoBehaviour {
	Vector3 mousePos;
	private GameObject paddle;
	GameObject ball;
	MoveBallnoPhysics ballMover;

	void Start(){
		ball = GameObject.FindGameObjectWithTag ("ball");
		ballMover = ball.GetComponent<MoveBallnoPhysics> ();
		paddle = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		if (ballMover.ballStopped && Input.GetMouseButtonUp(0)) {
			ballMover.ballStopped = false;
		}

		if (Input.GetMouseButton (0)) {
			mousePos = Input.mousePosition;
			mousePos.z = 3.63f;
			mousePos = Camera.main.ScreenToWorldPoint (mousePos);
			paddle.transform.position = mousePos;
		}
	}
}