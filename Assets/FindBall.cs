//@Author Mario Tommadich 
//This script finds the ball in the scene 
//and attaches it to the DepthOfField Component on the camera
//allows us to focus the camera on the ball at all times

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class FindBall : MonoBehaviour {

	private GameObject camera;
	private GameObject ball;
	private DepthOfField dofScript;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("ball");
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		dofScript = camera.GetComponent<DepthOfField> ();

		dofScript.focalTransform = ball.transform;
	}
}
