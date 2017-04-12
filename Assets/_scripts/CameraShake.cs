//CameraShake script by nicotroia 7 Aug 2016
//source: https://gist.github.com/ftvs/5822103
// instatiate by calling CameraShake.Shake(0.25f, 4f);
// this script shakes the camera if that effect is needed

using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraShake : MonoBehaviour {

	public static CameraShake instance;

	private Vector3 _originalPos;
	private float _timeAtCurrentFrame;
	private float _timeAtLastFrame;
	private float _fakeDelta;
	public GameObject[] cracks;
	private BlurOptimized blur;
	private GameObject camera;

	void Awake()
	{
		instance = this;


	}

	void Start(){
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		blur = camera.GetComponent<BlurOptimized> ();
	}

	void Update() {
		// Calculate a fake delta time, so we can Shake while game is paused.
		_timeAtCurrentFrame = Time.realtimeSinceStartup;
		_fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
		_timeAtLastFrame = _timeAtCurrentFrame; 
	}

	public static void Shake (float duration, float amount) {
		
		instance._originalPos = instance.gameObject.transform.localPosition;
		instance.StopAllCoroutines();
		instance.StartCoroutine(instance.cShake(duration, amount));
	}
	//Cracks the screen / @author Mario Tommadich 
	public void Crack (Vector3 crackPosition){
		crackPosition.z = 0.0f;
		Instantiate (cracks[Random.Range(0,cracks.Length)], crackPosition, Quaternion.identity);

	}

	public IEnumerator cShake (float duration, float amount) {
		float endTime = Time.time + duration;
		blur.enabled = true;
		while (duration > 0) {
			transform.localPosition = _originalPos + Random.insideUnitSphere * amount;

			duration -= _fakeDelta;

			yield return null;
		}

		transform.localPosition = _originalPos;
		blur.enabled = false;
	}

}