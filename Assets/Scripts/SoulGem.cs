using UnityEngine;
using System.Collections;
using Utils;
using JPlayer;
using Lib;
using JAudio;

public class SoulGem : MonoBehaviour {

	private float timer = 5.0f;

	void Awake() {
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= 1.0f * Time.deltaTime;
		GameObject[] enemies = (JSpawner.type != EnumLevelType.GEM_LEVEL) ? SpawnUtils.GetLevelSpawnTable(JSpawner.type) : SpawnUtils.GetLevelSpawnTable(EnumLevelType.DIFFICULTY_3);

		if (timer <= 0) {
			SpawnUtils.SpawnGameObject (enemies [Random.Range (0, enemies.Length)], gameObject);
			Destroy (gameObject);
		}

		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, transform.position.y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, transform.position.y);
	
	
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.ToLower() == "player") {
			AudioManager.INSTANCE.PlayAudio (Audio.GEM_PICKUP);
			Destroy (gameObject);
			PlayerStatsController.INSTANCE.GetPlayerStats (col.gameObject.GetComponent<controller> ().stat_id).score += 200;
		}
	}


}
