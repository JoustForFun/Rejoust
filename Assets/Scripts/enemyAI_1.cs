using UnityEngine;
using System.Collections;

public class enemyAI_1 : MonoBehaviour {
	
	public Rigidbody rb1; 
	public float enemy_y; 
	public float player_y;
	public float player_x;

	// Use this for initialization
	void Start () {
		rb1 = GetComponent<Rigidbody> ();
		enemy_y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		player_y = GameObject.Find ("player").GetComponent<controller> ().player_y;
		player_x = GameObject.Find ("player").GetComponent<controller> ().player_x;

		transform.position = new Vector3(player_x, player_y, -1.0f) * Time.deltaTime * 6.0f;

		if (player_y > enemy_y) { //fly upwards if player height is greater than enemy height
			Flying (); 
		}
		if (player_y == enemy_y) { //fly if player height equals enemy height
			Flying ();
		}
		if (player_y < enemy_y) { //attack if player height is less than enemy height
			Attacking (); 
		} else {
			Flying ();
		}

	}

	void Flying() {
		transform.Translate(Vector3.up * Time.deltaTime * 6); //flies upwards
		rb1.AddForce(transform.up); //adds upwards force to make floaty 
		}

	void Attacking(){


	}
}
