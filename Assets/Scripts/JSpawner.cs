using UnityEngine;
using System.Collections;
using Utils;

public class JSpawner : MonoBehaviour {

	private GameObject[] enemies;
	private GameObject powerup;

	void Awake () {
		enemies = new GameObject[] {
			Resources.Load ("Prefab/enemy2") as GameObject,
			Resources.Load ("Prefab/enemy1") as GameObject
		};

		powerup = Resources.Load ("Prefab/EntityPowerup") as GameObject;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Random.Range (0, 2000) == 0 && GameObject.FindGameObjectWithTag ("Powerup") == null) {
			SpawnUtils.SpawnGameObject (powerup, gameObject);
		}

		if (GameObject.FindGameObjectsWithTag ("Enemy").Length >= 4 || Random.Range(0,9) <= 2)
			return;

		SpawnUtils.SpawnGameObject(enemies[Random.Range(0,2)], gameObject);

	}
}
