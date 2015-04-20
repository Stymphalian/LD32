using UnityEngine;
using System.Collections;

public class FallToFarObject : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Fell too far");
		if(other.gameObject.CompareTag ("Player")){
			GameManager.instance.ResetGame();
		}
	}
}
