using UnityEngine;
using System.Collections;
using JPlayer;
using JPowerUp;
using JAudio;
using Lib;
using UnityEngine.SceneManagement;

public class EntityLava : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if ((col.gameObject.tag == "Enemy" && !col.gameObject.name.ToLower().Contains("lion"))|| col.gameObject.tag == "SoulGem") 
			Destroy (col.gameObject);
		else if (col.gameObject.tag == "Player") {
			PlayerStats stats = PlayerStatsController.INSTANCE.GetPlayerStats (col.gameObject.GetComponent<controller> ().stat_id);
			stats.lives--;

			AudioManager.INSTANCE.PlayAudio (Audio.DEATH);

			if (stats.powerups != EnumPowerup.NONE)
				PowerupFactory.INSTANCE.CallPowerup (stats.powerups).OnTimeout (col.gameObject);

			stats.powerups = EnumPowerup.SHEILD;
			IPowerup thePowerup = PowerupFactory.INSTANCE.CallPowerup (EnumPowerup.SHEILD);

			stats.powerupTimer = thePowerup.GetTimeoutTime();
			thePowerup.OnPickUp (col.gameObject);

			col.gameObject.transform.position = new Vector2(0, 0);

			if (stats.lives < 0) {
				Destroy (col.gameObject);
				Reference.FINAL_SCORE = stats.score;
				SceneManager.LoadScene (2);
			}
		}
	}
}
