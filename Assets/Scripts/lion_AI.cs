using UnityEngine;
using System.Collections;

public class lion_AI : MonoBehaviour {

	Rigidbody2D rb1; 
	public float lion_y; 
	public float player_y;
	public float player_x;
	public float sometimes_x;
	public float sometimes_y;
	public float begin_diff;
	public float lion_alert; 
	public float lion_speed;
	public float lion_lives; 
	GameObject player; 

	private bool go_up; 



	// Use this for initialization
	void Start () {
		rb1 = GetComponent<Rigidbody2D> ();
		lion_y = transform.position.y;
		begin_diff = player_y - lion_y;
		lion_alert = 5.0f;
		lion_speed = 2.0f;
		lion_lives = 4.0f; 
		player = GameObject.FindGameObjectWithTag ("Player");

	}

	// Update is called once per frame
	void Update () {
		GameObject thePlayer = GameObject.FindWithTag ("Player");

		if (thePlayer == null)
			return;
		lion_y = transform.position.y; 
		player_y = thePlayer.transform.position.y;

		player_x = thePlayer.transform.position.x;

		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, lion_y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, lion_y);
		//transform.position = new Vector3(player_x, player_y, -1.0f) * Time.deltaTime * 6.0f;
		transform.position += new Vector3((player_x-transform.position.x),0,0) * Time.deltaTime * sometimes_x;

		if (player_y - lion_y>=begin_diff-0.1f) { //fly upwards if player height is greater than enemy height
			Flying (); 

		}
		else if (player_y - lion_y<begin_diff-0.1f) { //attack if player height is less than enemy height
			Attacking ();
		} 

	}

	void Flying() {
		gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,  sometimes_y * 0.1f * (player_y - transform.position.y)));
		Vector2 new_dir=new Vector2(player_x-transform.position.x,player_y-transform.position.y+0.1f).normalized;
		GetComponent<Rigidbody2D>().velocity=(lion_speed*new_dir+new Vector2(0,0.02f*Mathf.Sin(Time.time)));
		//transform.position += new Vector3(0,(player_y-transform.position.y)*0.8f,0) * Time.deltaTime * sometimes_y;

		//flies upwards
		//rb1.AddForce(sometimes*transform.up); //adds upwards force to make floaty 
	}

	//void AwkwardFlying() {
		//gameObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2(0,  sometimes_y * 0.2f * (player_y - transform.position.y)));
	//}

	void Attacking(){


	}
		
	void OnCollisionEnter (Collision hit){
		string tag = hit.gameObject.tag.ToLower();

		if (tag == "player"){
			Debug.Log ("beginning"); 
			lion_lives--;
			if (lion_lives == 0){
				go_up = true; 
				if (go_up == true){
					transform.position = new Vector2 (11.0f, 7.0f);
					Destroy (gameObject); 
				}
			}
		}
	
	}
	
}
