using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets._2D;

public class GameManager : MonoBehaviour {

	// direct refs to the actual objects.
	public PlayerController player;
	public MusicUIManager musicUIManager;
	public GameObject teleportPrefab; // setthis
	public GameObject attackPrefab; // setthis



	public GameObject checkPoint;

	public bool _teleportMark = false;
	public float projectileForce = 500f;

	public bool _canMovePlayer = true;
	public bool _isUsingEquip = false;
	public bool _isInteracting = false;
	public float _timeBetweenKeyReads = 0.25f;
	public float _lastKeyRead = 0.0f;

	// music ui manager thing..
	List<int> collectedNotes = new List<int>();
	public int numNotesToLookFor = 5;
	
	public static GameManager instance;
	// Use this for initialization
	void Awake(){
		if(instance == null){
			instance = this;
		}else if( instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);

	}

	
	void Update()
	{			
		bool useEquip = (Input.GetButtonUp("UseEquip"));
		bool interact = (Input.GetButtonUp("Interact"));
			
		if(useEquip){
			_isUsingEquip = !_isUsingEquip;
			if( _isUsingEquip ){
				// pulling out the item
				// play the anim
				//_canMovePlayer = false;
				turnOnUsingEquip();
				// Start the UI stuff to be able to play a song
			}else{
				// putting away the item
				//  play the anim
				//_canMovePlayer = true;
				turnOffUsingEquip();
			}
		}

		if(_isUsingEquip){			
			bool node_added_flag= false;
			if(Input.GetButtonUp ("Note1")){
				collectedNotes.Add (1);
				node_added_flag = true;
			}else if(Input.GetButtonUp ("Note2")){		
				collectedNotes.Add (2);
				node_added_flag = true;
			}else if(Input.GetButtonUp ("Note3")){		
				collectedNotes.Add (3);
				node_added_flag = true;
			}else if(Input.GetButtonUp ("Note4")){		
				collectedNotes.Add (4);
				node_added_flag = true;
			}else if(Input.GetButtonUp ("Note5")){		
				collectedNotes.Add (5);
				node_added_flag = true;
			}else if(Input.GetButtonUp ("Note6")){		
				collectedNotes.Add (6);
				node_added_flag = true;
			}

			if(Input.GetButtonDown ("Cancel") ){
				turnOffUsingEquip();
			}else if( collectedNotes.Count > 0 ){
				musicUIManager.setNote(collectedNotes[collectedNotes.Count-1],collectedNotes.Count-1);
				if( node_added_flag){
					SoundManager.instance.PlaySingleNote(collectedNotes[collectedNotes.Count-1]);
				}

				if( collectedNotes.Count >= numNotesToLookFor){
					int action_code = MusicActionManager.instance.getCodeFromList (collectedNotes);
					musicUIManager.PlayRecordedSong(collectedNotes);

					collectedNotes.Clear();
					turnOffUsingEquip();

					// do the required action
					performAction(action_code);
				}
			}
		}

	}

	public IEnumerator delayAddForce(Vector2 force,float delay){
		yield return new WaitForSeconds(delay);
		player.m_Rigidbody2D.AddForce (force);
	}
	
	public void performAction(int action_code){
		if( action_code == 0){
			// attack stuff
			Vector2 pos = player.m_Rigidbody2D.position;
			if( player.m_FacingRight){
				pos.x = pos.x + Vector2.right.x*0.5f;
			}else{
				pos.x = pos.x - Vector2.right.x*0.5f;
			}
			pos.y = pos.y + Vector2.up.y;
			GameObject go = GameObject.Instantiate(attackPrefab,pos,Quaternion.identity) as GameObject;
			Projectile projectile = go.GetComponent<Projectile>();

			if( player.m_FacingRight){
				projectile.rb2d.AddForce(Vector2.right*projectileForce);
			}else{
				projectile.rb2d.AddForce(-Vector2.right*projectileForce);
			}


		}else if(action_code == 1){
			// mega jump
			Vector2 force = Vector2.up*1200f;
			player.m_Rigidbody2D.AddForce (force);

		}else if(action_code == 2){
			// teleport

			if( _teleportMark){
				// teleport to teleport object
				GameObject[] ojs = GameObject.FindGameObjectsWithTag("TeleportTag");
				if( ojs.Length > 0){
					GameObject tele = ojs[0];
					player.Teleport(tele.transform.position);

					foreach( GameObject o in ojs){
						GameObject.Destroy(o);
					}
				}
				_teleportMark = false;
			}else{
				// laydown teleport object

				// animation
				// play particle effect
				GameObject.Instantiate(teleportPrefab,player.m_Rigidbody2D.position,Quaternion.identity);
				_teleportMark = true;
			}

		}

	}


	void FixedUpdate()
	{
		if(_canMovePlayer){
			// handle player movement
			float horiz = Input.GetAxis ("Horizontal");
			bool jump = (Input.GetAxis ("Jump") > 0 ) ? true : false;
			player.Move (horiz,false,jump);
		}
	}

	void turnOnUsingEquip(){
		// play the 'pulling out' anim

		//_canMovePlayer = false;
		player.Move (0,false,false);

		musicUIManager.Show(true);
		_isUsingEquip = true;
	}
	void turnOffUsingEquip(){
		// play the 'putting-away' anim

		//_canMovePlayer = true;

		// music stuff
		_isUsingEquip = false;
		musicUIManager.Show(false);
	}

	public void ResetGame()
	{
		GameObject[] ojs = GameObject.FindGameObjectsWithTag("TeleportTag");
		if(ojs.Length >0){
			foreach( GameObject o in ojs){
				GameObject.Destroy(o);
			}
		}

		//player.Teleport(checkPoint.transform.position);
		player.transform.position = checkPoint.transform.position;
		Camera.main.GetComponent<Camera2DFollow>().Snap(checkPoint.transform.position);
	}
	
}
