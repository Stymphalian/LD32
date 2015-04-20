using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			GameManager.instance.checkPoint = gameObject;
		}
	}

}
