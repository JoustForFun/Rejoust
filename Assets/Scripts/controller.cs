using UnityEngine;
using System.Collections;
using JPowerUp;
using UnityEngine.UI;
using JPlayer;

public class controller : MonoBehaviour { 
	public Rigidbody2D rb; //rigidbody for upwards force
	public float player_y; //stores y value for looping
	public float player_x; 

	public int stat_id;
	public float enemy_y;

	private PlayerStats stats;

	//public float player_speed; 
	public int scoreValue = 100;
	//private EnumPowerup powerup = EnumPowerup.NONE;

	void Awake () {
		stat_id = PlayerStatsController.INSTANCE.AddPlayer (new PlayerStats (6.0f, 16f, 3));
		score.myPlayer = stat_id;
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		stats = PlayerStatsController.INSTANCE.GetPlayerStats (stat_id);
		player_y = transform.position.y;
		player_x = transform.position.x;



		if (Input.GetKey (KeyCode.LeftArrow)) { //move left
			transform.Translate(Vector2.left * Time.deltaTime * stats.GetMovementSpeed());
		}

		if (Input.GetKey (KeyCode.RightArrow)) { //move right
			transform.Translate(Vector2.right * Time.deltaTime * stats.GetMovementSpeed());
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) { // jumping
			transform.Translate (Vector2.up * Time.deltaTime * stats.GetJumpHeight());
			rb.AddForce(transform.up * 124); //makes it floaty! 
		}

		if (transform.position.x > 10) { //looping screen 
			transform.position = new Vector2 (-10.0f, player_y);

		}
			
		if (transform.position.x < -10) { //looping screen 
			transform.position = new Vector2 (10.0f, player_y);

		}


	}

	void OnCollisionEnter2D (Collision2D col) {
		stats = PlayerStatsController.INSTANCE.GetPlayerStats (stat_id);
		string tag = col.gameObject.tag.ToLower();

		switch (tag) {
		case "powerup":
			//Write code here. 
			break;
		case "enemy":
			GameObject enemy = col.gameObject;
			enemy_y = enemy.transform.position.y;
			if (player_y > enemy_y) {
				Destroy (enemy);
				stats.score += scoreValue;
			}
			if (player_y < enemy_y) {
				Destroy (gameObject);
			} else {
				//Destroy (gameObject);
				//TODO: Add backwards/opposite force when bouncing off. 
			}
			break;
		}
	}
}
