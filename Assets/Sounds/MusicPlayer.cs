using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public AudioClip fullSong;
	public float loopStartInSeconds, loopEndInSeconds;
	AudioSource musicPlayer;

	// Nightcore Loops: Start = 10.888f, End = 87.575
	// TurboKiller: Start = 19.6f, End = 124.11

	// Use this for initialization
	void Start () {
		musicPlayer = this.GetComponent<AudioSource> ();
		musicPlayer.clip = fullSong;
		musicPlayer.Play ();
	}

	void Update () {
		if (musicPlayer.time >= loopEndInSeconds) {
			musicPlayer.time = loopStartInSeconds;
		}
	}
}
