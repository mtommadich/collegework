using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {
	public AudioClip[] bgm;
	public GameManager manager;
	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = bgm[SceneManager.GetActiveScene ().buildIndex];
		audioSource.Play ();
	}
	
}
