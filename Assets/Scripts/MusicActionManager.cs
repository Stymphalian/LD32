using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicActionManager : MonoBehaviour {

	// Use this for initialization
	public static MusicActionManager instance;

	public Dictionary<string,int> _stringToActionDict = new Dictionary<string,int>();

	void Awake(){
		if(instance == null){
			instance = this;
		}else if( instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(this);
	}

	void Start(){
		// string to action 
		_stringToActionDict["11223"] = 0;
		_stringToActionDict["44556"] = 1;
		_stringToActionDict["12312"] = 2;
	}


	// return -1 if bad code..
	public int getCodeFromString(string hash){
		if(_stringToActionDict.ContainsKey (hash) == false){
			return -1;
		}
		return _stringToActionDict[hash];
	}
	public string getStringFromcode(int code){
		foreach(KeyValuePair<string,int> entry in _stringToActionDict){
			if( entry.Value == code){
				return entry.Key;
			}
		}
		return "";
	}

	public int getCodeFromList(List<int> collectedNotes){
		string s = "";
		foreach(int n in collectedNotes){
			s += n;
		}
		return getCodeFromString(s);
	}

	public void doActionCode(int code){
		switch(code){
		case(0):
			break;
		case(1):
			break;
		case(2):
			break;		
		}
	}
}
