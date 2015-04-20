using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MuteButtonUIManager : MonoBehaviour {

	// Use this for initialization
	public Sprite muteDisableSprite ;
	public Sprite muteEnableSprite ;
	public Image img;

	void Awake(){
		muteDisableSprite = Resources.Load<Sprite> ("MuteDisable");
		muteEnableSprite = Resources.Load<Sprite> ("MuteEnable");
		img = transform.GetComponent<Image>();
	}
	
	public void pushButtonHandler(){
		SoundManager.instance.setMuted(!SoundManager.instance.mute_flag);
		if(SoundManager.instance.mute_flag){
			img.sprite = muteEnableSprite;
		}else{
			img.sprite = muteDisableSprite;
		}
	}
}
