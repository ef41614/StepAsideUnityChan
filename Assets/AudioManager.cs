using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static int sAudioPlay = 0; //0:off、 1:on
	public AudioClip GameBGM;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		sAudioPlay = TitleManager.AudioPlay; 
		audioSource = gameObject.GetComponent<AudioSource> ();	
	}

	void Update () {
		if(sAudioPlay==0){
			// Audio使わない(Off)
			//Stop the audio
			audioSource.Stop();

		}else if(sAudioPlay==1){
			// Audio再生(On)
//			audioSource.PlayOneShot (GameBGM);  // ←これにBGMファイルを後で関連付ける
//				audioSource.Play (GameBGM);  // ←これにBGMファイルを後で関連付ける
		}
	}

}
