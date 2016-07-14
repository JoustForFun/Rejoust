using UnityEngine;
using System.Collections;

public class enemyAI_1 : MonoBehaviour {
	
	Rigidbody2D rb1; 
	public float enemy_y; 
	public float player_y;
	public float player_x;
	public float sometimes_x;
	public float sometimes_y;
	public float begin_diff;



	// Use this for initialization
	void Start () {
		rb1 = GetComponent<Rigidbody2D> ();
		enemy_y = transform.position.y;
		begin_diff = player_y - enemy_y;
	    
	}
	
	// Update is called once per frame
	void Update () {
		GameObject thePlayer = GameObject.FindWithTag ("Player");

		if (thePlayer == null)
			return;

		player_y = thePlayer.transform.position.y;

		player_x = thePlayer.transform.position.x;

		//transform.position = new Vector3(player_x, player_y, -1.0f) * Time.deltaTime * 6.0f;
		transform.position += new Vector3((player_x-transform.position.x),0,0) * Time.deltaTime * sometimes_x;

		if (player_y - enemy_y>=begin_diff-0.1f) { //fly upwards if player height is greater than enemy height
			Flying (); 

		}
		else if (player_y - enemy_y<begin_diff-0.1f) { //attack if player height is less than enemy height
			Attacking ();
		} 

	}

	void Flying() {
		gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,  sometimes_y * 0.1f * (player_y - transform.position.y)));
		//transform.position += new Vector3(0,(player_y-transform.position.y)*0.8f,0) * Time.deltaTime * sometimes_y;

		//flies upwards
		//rb1.AddForce(sometimes*transform.up); //adds upwards force to make floaty 
		}

	void AwkwardFlying() {
		gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,  sometimes_y * 0.2f * (player_y - transform.position.y)));
	}

	void Attacking(){


	}
}
