﻿using UnityEngine;
using System.Collections;
using JPowerUp;
using UnityEngine.UI;
using JPlayer;
using Utils;
using Lib;
using JAudio;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour { 
	Rigidbody2D rb; //rigidbody for upwards force
	public float player_y; //stores y value for looping
	public float player_x; 
	public Vector2 jump_force;
	public float speed_limitX = 1.0f;
	private float someScale;
	private bool OnGround;

	public int stat_id;
	public float enemy_y;

	private PlayerStats stats;

	//public float player_speed; 
	public int scoreValue = 100;
	//private EnumPowerup powerup = EnumPowerup.NONE;

	void Awake () {
		stat_id = PlayerStatsController.INSTANCE.AddPlayer (new PlayerStats (0.6f, 11.25f, 3));
		score.myPlayer = stat_id;
		OnGround = true;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		someScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		Animator ani = gameObject.GetComponent<Animator> ();
//		ani.SetInteger ("State", 0);
		stats = PlayerStatsController.INSTANCE.GetPlayerStats (stat_id);
		player_y = transform.position.y;
		player_x = transform.position.x;

		IPowerup thePowerup = PowerupFactory.INSTANCE.CallPowerup (stats.powerups);

		if (stats.powerupTimer <= 0 && stats.powerups != EnumPowerup.NONE) {
			stats.powerupTimer = 0;
			stats.powerups = EnumPowerup.NONE;
			thePowerup.OnTimeout(gameObject);
		} else {
			thePowerup.OnUpdate(gameObject);
			stats.powerupTimer -= Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.LeftArrow)) { //move left
			if (OnGround)
				ani.SetInteger ("State", 1);
			else
				ani.SetInteger ("State", 2);
			if (Mathf.Abs (rb.velocity.x) < speed_limitX) {
				//flip sprite
				transform.localScale = new Vector2 (-someScale, transform.localScale.y);
				rb.AddForce (new Vector2 (-stats.GetMovementSpeed(), 0), ForceMode2D.Impulse);
			}
			//transform.Translate(Vector2.left * Time.deltaTime * stats.GetMovementSpeed());
		} else if (Input.GetKey (KeyCode.RightArrow)) { //move right
			if (OnGround)
				ani.SetInteger ("State", 1);
			else
				ani.SetInteger ("State", 2);
			if (Mathf.Abs (rb.velocity.x) < speed_limitX) {
				transform.localScale = new Vector2 (someScale, transform.localScale.y);
				rb.AddForce (new Vector2 (stats.GetMovementSpeed(), 0), ForceMode2D.Impulse);
			}
			//transform.Translate(Vector2.right * Time.deltaTime * stats.GetMovementSpeed());
		} else {
			ani.SetInteger ("State", 0);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.LeftControl)) { // jumping
			transform.Translate (Vector2.up * Time.deltaTime * stats.GetJumpHeight());
			rb.AddForce(transform.up * 124); //makes it floaty! 
			AudioManager.INSTANCE.PlayAudio (Audio.FLAP);
			OnGround = false;
		}

		if (transform.position.x > 10) { //looping screen 
			transform.position = new Vector2 (-10.0f, player_y);

		} else if (transform.position.x < -10) { //looping screen 
			transform.position = new Vector2 (10.0f, player_y);

		} else if (transform.position.y > 5.85f) {
			transform.position = new Vector2 (5f, transform.position.y);
		}
	}

	void OnCollisionEnter2D (Collision2D col) {
		stats = PlayerStatsController.INSTANCE.GetPlayerStats (stat_id);
		string tag = col.gameObject.tag.ToLower();

		if (col.gameObject.name.ToLower ().Contains ("lion"))
			return;

		switch (tag) {
		case "powerup":
			//Write code here. 
			break;
		case "enemy":
			GameObject enemy = col.gameObject;
			enemy_y = enemy.transform.position.y;
			if (player_y - 0.2f > enemy_y|| stats.invincible == true) {
				SpawnUtils.SpawnGameObject (Entities.ENTITY_SOULGEM, enemy);
				Destroy (enemy);
				stats.score += scoreValue;
			} else if (player_y - 0.2f < enemy_y && stats.invincible == false) {
				stats.lives--;
				AudioManager.INSTANCE.PlayAudio (Audio.DEATH);

				if (stats.powerups != EnumPowerup.NONE)
					PowerupFactory.INSTANCE.CallPowerup (stats.powerups).OnTimeout (gameObject);
					
				stats.powerups = EnumPowerup.SHEILD;
				IPowerup thePowerup = PowerupFactory.INSTANCE.CallPowerup (EnumPowerup.SHEILD);

				stats.powerupTimer = thePowerup.GetTimeoutTime();
				thePowerup.OnPickUp (gameObject);

				gameObject.transform.position = new Vector2(0, 0);
				gameObject.GetComponent<Animator> ().SetInteger ("State", 3);

				if (stats.lives < 0) {
					Destroy (gameObject);
					Reference.FINAL_SCORE = stats.score;
					Debug.Log (Reference.FINAL_SCORE);
					SceneManager.LoadScene (2);
				}
				
			} else {
				//Destroy (gameObject);
				//TODO: Add backwards/opposite force when bouncing off. 
			}
			break;
		case "platform":
			AudioManager.INSTANCE.PlayAudio (Audio.HIT);
			OnGround = true;
			break;
		}
	}
}
