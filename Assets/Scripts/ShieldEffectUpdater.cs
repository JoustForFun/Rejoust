using UnityEngine;
using System.Collections;
using JPlayer;
using Lib;

public class ShieldEffectUpdater : MonoBehaviour {

	PlayerStats ps;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		ps = PlayerStatsController.INSTANCE.GetPlayerStats(gameObject.GetComponentInParent<controller> ().stat_id);
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ps.invincible)
			sr.sprite = Sprites.SHEILD_FX;
		else
			sr.sprite = null;
	}
}
