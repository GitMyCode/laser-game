using UnityEngine;
using System.Collections;

public class fadeinAudio : MonoBehaviour {

[RequireComponent(typeof(AudioSource))]
 
public class EasyFadeIn : MonoBehaviour {
 
 
	public int approxSecondsToFade = 10;
 
	void FixedUpdate ()
	{
		if (audio.volume < 1)
		{
			audio.volume = audio.volume + (Time.deltaTime / (approxSecondsToFade + 1));
		}
		else
		{
			Destroy (this);
		}
	}
}
}
