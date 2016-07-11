using UnityEngine;
using System.Collections;
using Lib;

/*
 * This module is used to initialized some of the public variable/objects within Lib
 * Initialization should always be in the awake method.
 * */
public class JMenuInitController : MonoBehaviour {

	void Awake () {

		UIComponents.Init ();
		AudioClips.Init ();

	}

	// Use this for initialization
	void Start () {
		UIComponents.MAIN_MENU.enabled = true;
		UIComponents.SERVER_SELECTION.enabled = false;
		UIComponents.TUTORIAL.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
