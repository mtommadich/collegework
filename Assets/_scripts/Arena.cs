/* Arena.cs
 * Rev 1
 * 29/01/2016
 * @author Mario Tommadich, 15007740 
 */ 

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Arena : MonoBehaviour {
	public GameObject[] backTargets;
	public GameObject[] leftTargets;
	public GameObject[] rightTargets;
	public Vector3[] backTargetValues;
	public Vector3[] leftTargetValues;
	public Vector3[] rightTargetValues;
	public GameObject[] obstacles; //not used
	public GameObject[] hazards; //not used
	private GameObject gameManager;
	GameManager managerScript;
	public GameObject exit;
	public int targetCount;
	private GameObject ball;
	Light ballLight;
	public AudioClip bgm;


	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("ball");
		ballLight = ball.GetComponent<Light> ();
		//find the GameManager object and make it update the healthUI
		gameManager = GameObject.FindGameObjectWithTag ("manager");
		managerScript = gameManager.GetComponent<GameManager> ();
		managerScript.UpdateLifeUI ();

		// Debug.Log ("Arena.cs - Entering "+SceneManager.GetActiveScene().name);

		// Populate the arena with targets 
		 
		spawnTargets ();
	}	


	//this subtracts 1 from the total target count each time it is called and instantiates an exit object if targetCount is 0
	public void removeTarget(){
		targetCount--;
		if (targetCount < 1) {
			ballLight.enabled = false;
			Instantiate (exit);
		}
	}


	/* Array randomizer "Fisher and Yates shuffle" by Ronald Fisher and Frank Yates
	 * Uses the Fisher and Yates shuffling algorithm.
	 */
	public static void randomizeArr<Target>(Target[] arr){
		for (int i = arr.Length - 1; i > 0; i--){
			int r = Random.Range (0, i);
			Target tmp = arr [i];
			arr [i] = arr [r];
			arr [r] = tmp;
		}
	}

	/* Iterate over the arrays of Targets and instantiate each of them at a
	 * target position from the randomized TargetValues arrays. This way 
	 * the targets will show in different positions each time the game is started,
	 * without having to worry about accidentally placing more than one 
	 * target at the same position.
	*/
	public void spawnTargets(){
		/* First of all shuffle the coordinate value arrays of the targets 
		 * in order to get a randomized array of possible target positions.
		 */
		targetCount = backTargets.Length + leftTargets.Length + rightTargets.Length;
		randomizeArr (backTargetValues);
		randomizeArr (leftTargetValues);
		randomizeArr (rightTargetValues);

		//place the three target types at their random positions
		for (var i = 0; i < backTargets.Length; i++) {
			GameObject backTarget = backTargets [i];
			Vector3 targetPosition = backTargetValues [i];
			Instantiate (backTarget, targetPosition, Quaternion.identity);
		}

		for (var i = 0; i < leftTargets.Length; i++) {
			GameObject leftTarget = leftTargets [i];
			Vector3 targetPosition = leftTargetValues [i];
			Instantiate (leftTarget, targetPosition, Quaternion.identity);
		}

		for (var i = 0; i < rightTargets.Length; i++) {
			GameObject rightTarget = rightTargets [i];
			Vector3 targetPosition = rightTargetValues [i];
			Instantiate (rightTarget, targetPosition, Quaternion.identity);
		}

	}
}
