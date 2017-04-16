//DestroyByTime
//@Author M.Tommadich
//Quick little script that destroys any object after a set amount of time.

using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {
	public float lifeTime;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeTime);
	}
	

}
