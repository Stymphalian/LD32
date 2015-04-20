using UnityEngine;
using System.Collections;

public class TeleportScript : MonoBehaviour {
	public GameObject dest;
	public float delayTeleportTime = 0.5f;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag ("Player")){
			StartCoroutine(delayTeleport(other.gameObject.GetComponent<PlayerController>(),0.5f));
		}
	}

	IEnumerator delayTeleport(PlayerController player,float delay){
		yield return new WaitForSeconds(delay);
		player.Teleport (dest.transform.position);
	}
}
