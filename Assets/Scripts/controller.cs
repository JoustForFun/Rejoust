using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour { 
	public Rigidbody rb; //rigidbody for upwards force
	public float player_y; //stores y value for looping
	public float player_x; 
	public float player_speed; 
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		player_speed = 6.0f; 
	}
	
	// Update is called once per frame
	void Update () {
		player_y = transform.position.y;
		player_x = transform.position.x;

		if (Input.GetKey (KeyCode.LeftArrow)) { //move left
			transform.Translate(Vector3.left * Time.deltaTime * player_speed);
		}

		if (Input.GetKey (KeyCode.RightArrow)) { //move right
			transform.Translate(Vector3.right * Time.deltaTime * player_speed);
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) { // jumping
			transform.Translate (Vector3.up * Time.deltaTime * 16);
			rb.AddForce(transform.up * 124); //makes it floaty! 
		}

		if (transform.position.x > 10) { //looping screen 
			transform.position = new Vector3 (-10.0f, player_y, transform.position.z);

		}
			
		if (transform.position.x < -10) { //looping screen 
			transform.position = new Vector3 (10.0f, player_y, transform.position.z);

		}
	}
}
