using UnityEngine;
using System.Collections;

public class PlayList : MonoBehaviour {

	public AudioClip[] myMusic = new AudioClip[3];
	
	void Update() { 

		if(!audio.isPlaying) playRandomMusic(); 
	}
	
	void playRandomMusic() { 
		audio.clip = myMusic[Random.Range(0, myMusic.Length)]; audio.Play(); 
	}
}
