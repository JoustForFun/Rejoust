using UnityEngine;
using System.Collections;

public class collision_outcome : MonoBehaviour {
	public float player_y;
	public float player_x; 
	public float enemy_y; 
	public GameObject enemy1;
	public GameObject player; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		player_y = GameObject.Find ("player").GetComponent<controller> ().player_y;
		player_x = GameObject.Find ("player").GetComponent<controller> ().player_x;
		enemy_y = GameObject.Find ("enemy1").GetComponent<enemyAI_1> ().enemy_y;
	
	}

	void OnTriggerEnter (Colli attack){
		if (player_y > enemy_y) {
			Destroy (enemy1);
		}
		if (player_y < enemy_y) {
			Destroy (player);
		} else {
			Destroy (enemy1);
		}
	}
}
