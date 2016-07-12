using UnityEngine;
using System.Collections;
using JPowerUp;
using UnityEngine.UI;

public class controller : MonoBehaviour { 
	public Rigidbody2D rb; //rigidbody for upwards force
	public float player_y; //stores y value for looping
	public float player_x; 
	public float player_speed; 
	public float enemy_y; 
	public int scoreValue = 100; 

	private EnumPowerup powerup = EnumPowerup.NONE;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		player_speed = 6.0f; 
	}
	
	// Update is called once per frame
	void Update () {
		player_y = transform.position.y;
		player_x = transform.position.x;



		if (Input.GetKey (KeyCode.LeftArrow)) { //move left
			transform.Translate(Vector2.left * Time.deltaTime * player_speed);
		}

		if (Input.GetKey (KeyCode.RightArrow)) { //move right
			transform.Translate(Vector2.right * Time.deltaTime * player_speed);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) { // jumping
			transform.Translate (Vector2.up * Time.deltaTime * 16);
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
		string tag = col.gameObject.tag;

		switch (tag) {
		case "powerup":
			//Write code here. 
			break;
		case "enemy":
			GameObject enemy = col.gameObject;
			enemy_y = enemy.transform.position.y;
			if (player_y > enemy_y) {
				Destroy (enemy);
				score.scr += scoreValue;
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
