using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicUIManager : MonoBehaviour {

	// Use this for initialization
	GameObject root;
	GameObject notes_panel;
	List<GameObject> notes = new List<GameObject>();

	void Start () {
		root = transform.Find("Canvas").gameObject;
		notes_panel = transform.Find("Canvas/Notes").gameObject;

		for(int i = 1; i <= 5; ++i){
			notes.Add (transform.Find ("Canvas/Notes/Note" + i).gameObject);
		}
	}
	
	public void setNote(int note, int pos){
		NoteUIManager n = notes[pos].transform.GetComponent<NoteUIManager>();
		n.setNote(note);
	}
	
	public void Show(bool value){
		notes_panel.SetActive(value);
		if( value){
			// reset the notes and stuff
			StartCoroutine(bubbleNotes(0.05f));
		}else{
			for(int i = 0; i < notes.Count;++i)
			{
				NoteUIManager n = notes[i].transform.GetComponent<NoteUIManager>();
				n.ResetImage();
			}

		}
	}
	
	public void PlayRecordedSong(List<int> collectedNotes){
		string s = "";
		foreach(int n in collectedNotes){
			s += n;
		}

	}

	IEnumerator bubbleNotes(float delay)
	{
		Animator anim;
		foreach(GameObject note in notes){
			anim = note.GetComponent<Animator>();
			anim.SetTrigger("NoteRise");
			yield return new WaitForSeconds(delay);
		}
	}
		

}
