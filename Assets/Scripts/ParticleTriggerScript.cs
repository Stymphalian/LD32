using UnityEngine;
using System.Collections;

public class ParticleTriggerScript : MonoBehaviour {
	public GameObject pe;
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			pe.SetActive (true);			
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			pe.SetActive (false);
		}
	}
}
