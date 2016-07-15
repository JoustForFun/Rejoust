using UnityEngine;
using System.Collections;
using JAudio;
using Lib;

public class start_audio : MonoBehaviour {


	void Awake() {
		AudioManager.INSTANCE.PlayAudio (Audio.MENU_SONG, true); 

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
