using UnityEngine;
using System.Collections;
using JPlayer;
using JAudio;
using JPowerUp;
using Lib;
using UnityEngine.SceneManagement;
using Utils;

public class lion_AI : MonoBehaviour {

	Rigidbody2D rb1;
	public int lion_lives; 
	public float lion_timer;
	private bool go_up;
	GameObject player;

	// Use this for initialization
	void Start () {
		rb1 = GetComponent<Rigidbody2D> ();
		lion_lives = 5;
		go_up = false;
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	// Update is called once per frame
	void Update () {

		if (go_up == true && transform.position.y > 8f) {
			Destroy (gameObject);
			return;
		} else if (go_up == true) {
			gameObject.transform.position += new Vector3 (0, 0.2f, 0);
			return;
		}
		

		if(lion_timer > 0){
			lion_timer -= Time.deltaTime;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
		}

		GameObject thePlayer = GameObject.FindWithTag ("Player");
		Vector2 playerPos = thePlayer.transform.position;

		if (Random.Range (0, 300) == 1) {
			SpawnUtils.SpawnGameObject (Entities.ENEMY_CENTAUR, gameObject);
			AudioManager.INSTANCE.PlayAudio (Audio.ROAR);
		}

		if (thePlayer == null)
			return;
		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, transform.position.y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, transform.position.y);

		if (playerPos.y >= transform.position.y) {
			transform.position += new Vector3(0.5f*(playerPos.x-transform.position.x),0.5f,0) * Time.deltaTime;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
		} else if (playerPos.y < transform.position.y) {
			transform.position -= new Vector3(-0.5f*(playerPos.x-transform.position.x),1f,0) * Time.deltaTime;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(0,0);
		}
	}

	void OnCollisionEnter2D (Collision2D hit){
		string tag = hit.gameObject.tag.ToLower();

		if (tag != "player")
			return;

		if (hit.transform.position.y > transform.position.y && lion_timer <= 0) {
			hit.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(Random.Range(0f,1f), 5.0f);
			lion_lives--;
			lion_timer = 5.0f;
			gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, 5.0f));
			AudioManager.INSTANCE.PlayAudio (Audio.ROAR);
			if (lion_lives == 0) {
				Destroy (gameObject.GetComponent<Rigidbody2D> ());
				go_up = true;
			}
		} else if (hit.transform.position.y <= transform.position.y) {
			AudioManager.INSTANCE.PlayAudio (Audio.ROAR);

			PlayerStats stats = PlayerStatsController.INSTANCE.GetPlayerStats (hit.gameObject.GetComponent<controller> ().stat_id);

			if (stats.invincible)
				return;
			
			stats.lives--;

			AudioManager.INSTANCE.PlayAudio (Audio.DEATH);

			if (stats.powerups != EnumPowerup.NONE)
				PowerupFactory.INSTANCE.CallPowerup (stats.powerups).OnTimeout (hit.gameObject);

			stats.powerups = EnumPowerup.SHEILD;
			IPowerup thePowerup = PowerupFactory.INSTANCE.CallPowerup (EnumPowerup.SHEILD);

			stats.powerupTimer = thePowerup.GetTimeoutTime ();
			thePowerup.OnPickUp (hit.gameObject);

			hit.gameObject.transform.position = new Vector2 (0, 0);

			if (stats.lives < -1) {
				Destroy (hit.gameObject);
				Reference.FINAL_SCORE = stats.score;
				SceneManager.LoadScene (2);
			}
		} else if (lion_timer > 0) {
			hit.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2(Random.Range(0f,1f), 5.0f);
		}
	
	}
	
}
