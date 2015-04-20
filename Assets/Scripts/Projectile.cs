using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Rigidbody2D rb2d;
	public float autoDestroyDelay = 5.0f;

	void Init(){
		rb2d = transform.GetComponent<Rigidbody2D>();
	}

	void Awake(){
		Init();
		StartCoroutine(autoDestroy(autoDestroyDelay));
		//rb2d.AddForce (Vector2.right*initForce);
	}


	IEnumerator autoDestroy(float delay){
		yield return new WaitForSeconds(delay);
		Destroy(gameObject);
	}

	void OnCollisionEnter2D(Collision2D other){
		Destroy(gameObject);
	}
	
}
