using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance;
	public AudioClip[] notes;
	public AudioSource efxSource;
	public AudioSource bgMusic;
	public bool mute_flag =false;
	public float bg_music_delay = 5.0f; // in seconds

	// Use this for initialization
	void Awake () {
		if(instance == null){
			instance = this;
		}else if( instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);

		StartCoroutine(beginBGMusic());
	}

	protected IEnumerator beginBGMusic(){
		yield return new WaitForSeconds(bg_music_delay);
		if(mute_flag == false){
			bgMusic.Play ();
		}
	}
		
	public void PlaySingleNote(int note){
		if( mute_flag){return;}

		if( note < 0 || note > 6){
			return;
		}
		// depending on the note being asked to play
		// play that clip
		efxSource.volume = 0.1f;
		efxSource.clip = notes[note-1];
		efxSource.Play();
	}

	public void setMuted(bool mute){
		mute_flag = mute;
		if( mute_flag == true){
			// stop playing all music
			efxSource.Stop();
			bgMusic.Stop ();
			
		}else{
			bgMusic.Play();
		}
	}

}
