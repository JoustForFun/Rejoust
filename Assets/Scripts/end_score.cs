using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Lib; 

public class end_score : MonoBehaviour {
	Text mytext; 

	// Use this for initialization
	void Awake() {
		JPlayer.PlayerStatsController.INSTANCE.ClearPlayers ();
		mytext = gameObject.GetComponent<Text> ();

	}

	void Start () {
		mytext.text = string.Format("YOUR SCORE: {0}", Reference.FINAL_SCORE);
	}

	// Update is called once per frame
	void Update () {
	}
}
