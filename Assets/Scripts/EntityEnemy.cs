using UnityEngine;
using System.Collections;

public class EntityEnemy : MonoBehaviour {

	public enum ENEMY_KIND {
		WEAK,
		AGGRESSIVE,
		NORMAL
	};
	public float enemy_y; 
	public ENEMY_KIND enemy;
	public float enemy_speed;
	public float alert;
	private float someScale;

	// Use this for initialization
	GameObject player_pos;

	private float rand;
	void Start() {
		enemy_speed = 2.0f;
		alert = 1.0f;

		player_pos=GameObject.Find("player");
		rand=Random.value;
		if(rand<0.6f){enemy=ENEMY_KIND.WEAK;}
		else if(rand>0.9f){enemy=ENEMY_KIND.AGGRESSIVE;}
		else {enemy=ENEMY_KIND.NORMAL;}

		someScale = transform.localScale.x;
	}

	void Update () {

		if (player_pos == null)
			return;
		
		enemy_y = transform.position.y;

		if (enemy_y < -4.1f)
			transform.position = new Vector2 (-10f, 0f);

		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, enemy_y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, enemy_y);
			switch(enemy)
			{

			case ENEMY_KIND.NORMAL:
			//normal enemy will try to attack within the alert range near player
			if(Vector3.Distance(transform.position,player_pos.transform.position)<alert)
				Chasing();
			else
				Straight(1);
				return;

			case ENEMY_KIND.AGGRESSIVE:
			//aggressive enemy will always try to chase the player
			  Chasing();
				return;

		case ENEMY_KIND.WEAK:
			//stupid player will have 50% chance flying to both direction
			if(rand>0.3f)
			  Straight(1);
			 else
				Straight(-1);
				return;

			default:
				return;

			}
	}


	void Chasing() {
		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, enemy_y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, enemy_y);

		float player_x=player_pos.transform.position.x;
		float player_y=player_pos.transform.position.y;

		if(player_x - transform.position.x < 0)
			transform.localScale = new Vector2(-someScale, transform.localScale.y);
		else if (player_x - transform.position.x > 0)
			transform.localScale = new Vector2(-someScale, transform.localScale.y);

		Vector2 new_dir=new Vector2(player_x-transform.position.x,player_y-transform.position.y+0.1f).normalized;
		GetComponent<Rigidbody2D>().velocity=(enemy_speed*new_dir+new Vector2(0,0.02f*Mathf.Sin(Time.time)));
	
	}

	void Straight(int dir) {
		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, enemy_y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, enemy_y);
		
		Vector2 start_dir=dir*new Vector2(1f,(Random.value-0.5f)*1.5f).normalized;
		if (start_dir.x > 0)
			transform.localScale = new Vector2 (-someScale, transform.localScale.y);

		GetComponent<Rigidbody2D>().velocity=(enemy_speed*start_dir+new Vector2(0,0.02f*Mathf.Sin(0.01f*Time.time)));

	}

	void OnCollision2D(Collision2D col) {
		if (col.gameObject.tag.ToLower () == "enemy" || col.gameObject.tag.ToLower () == "platform")
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f)));
	}


		

}
