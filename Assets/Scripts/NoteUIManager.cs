using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoteUIManager : MonoBehaviour {

	public void setNote(int note){
		Image img = transform.GetComponent<Image>();
		Sprite s = Resources.Load<Sprite>("Note"+note);			
		img.sprite = s;
	}

	public void ResetImage(){
		Image img = transform.GetComponent<Image>();
		Sprite s = Resources.Load<Sprite>("NoteOpen");			
		img.sprite = s;
	}
}
