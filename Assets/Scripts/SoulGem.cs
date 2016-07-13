using UnityEngine;
using System.Collections;
using Utils;
using JPlayer;

public class SoulGem : MonoBehaviour {

	private float timer = 5.0f;
	private GameObject[] enemies;

	void Awake() {
		enemies = new GameObject[] {
			Resources.Load ("Prefab/enemy2") as GameObject,
			Resources.Load ("Prefab/enemy1") as GameObject
		};
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer -= 1.0f * Time.deltaTime;

		if (timer <= 0) {
			SpawnUtils.SpawnGameObject (enemies [Random.Range (0, 2)], gameObject);
			Destroy (gameObject);
		}
	
	
	
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag.ToLower() == "player") {
			Destroy (gameObject);
			PlayerStatsController.INSTANCE.GetPlayerStats (col.gameObject.GetComponent<controller> ().stat_id).score += 200;
		}
	}


}
