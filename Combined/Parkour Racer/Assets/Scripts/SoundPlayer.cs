using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {

	public AudioClip [] audioclip;

	public void PlaySound (int clip)
	{
		GetComponent<AudioSource> ().PlayOneShot (audioclip [clip]);
	}
}
