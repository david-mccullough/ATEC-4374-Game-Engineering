using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAdjustments : MonoBehaviour {

	AudioSource aud;
	void Awake()
	{
		aud = GetComponent<AudioSource> ();
		aud.pitch = Random.Range (.8f, 1.2f);
	}
}
