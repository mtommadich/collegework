//BGMPlayer 
//@Author M. Tommadich
//Scripts finds the music clip attached to an arena and plays it (if audioclip exists).

//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {	
	private AudioSource audioSource;
	private AudioClip bgm;
	private GameObject arena;
	private Arena arenaScript;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		arena = GameObject.FindGameObjectWithTag("arena");
		arenaScript = arena.GetComponent<Arena>();

		if (arenaScript.bgm != null){
			audioSource.clip = arenaScript.bgm;
			audioSource.Play ();
		}else{
			return;
		}
	}
	
}
