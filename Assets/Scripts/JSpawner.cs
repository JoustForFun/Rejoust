using UnityEngine;
using System.Collections;
using Utils;

public class JSpawner : MonoBehaviour {

	GameObject[] enemies;

	void Awake () {
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

		if (GameObject.FindGameObjectsWithTag ("Enemy").Length >= 4 || Random.Range(0,9) <= 2)
			return;

		SpawnUtils.SpawnGameObject(enemies[Random.Range(0,2)], gameObject);

	}
}
