using UnityEngine;
using System.Collections;
using Utils;
using System.Collections.Generic;
using Lib;
using UnityEngine.UI;

public class JSpawner : MonoBehaviour {

	private Vector3[] spawnLoc;
	private float breakTimer = 2.5f;
	private int wave = 1;
	public static EnumLevelType type = EnumLevelType.DIFFICULTY_3;
	private GameObject WaveScreen;

//	private GameObject[] enemies;
//	private GameObject powerup;

//	void Awake () {
//		enemies = new GameObject[] {
//			Resources.Load ("Prefab/enemy2") as GameObject,
//			Resources.Load ("Prefab/enemy1") as GameObject
//		};

//		powerup = Resources.Load ("Prefab/EntityPowerup") as GameObject;
//	}

	// Use this for initialization
	void Start () {
		WaveScreen = GameObject.FindGameObjectWithTag ("WaveScreen");
		WaveScreen.GetComponent<Canvas> ().enabled = false;
		RefreshSpawns ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectWithTag ("Enemy") == null && GameObject.FindGameObjectWithTag ("SoulGem") == null) {
			if (breakTimer == 2.5f) {
				WaveScreen.GetComponent<Canvas> ().enabled = true;
				if (wave % 5 == 0) {
					WaveScreen.GetComponentInChildren<Text> ().text = string.Format ("Boss Wave : {0}", wave);
				} else if (wave % 3 == 0) {
					WaveScreen.GetComponentInChildren<Text> ().text = string.Format ("Bonus Wave : {0}", wave);
				} else {
					WaveScreen.GetComponentInChildren<Text> ().text = string.Format ("Wave : {0}", wave);
				}
			}
			breakTimer -= Time.deltaTime;
			if (breakTimer < 0) {
				breakTimer = 2.5f;
				SpawnEnemies (wave++);
			}
		}

		if (GameObject.FindGameObjectWithTag("Powerup") == null && Random.Range (0, 300) == 250)
			SpawnUtils.SpawnGameObject (Entities.ENTITY_POWERUP, spawnLoc [Random.Range(0, spawnLoc.Length)]);
	}

	public void RefreshSpawns () {
		GameObject[] platforms = GameObject.FindGameObjectsWithTag ("Platform");

		List<Vector3> spawns = new List<Vector3> ();

		foreach(GameObject obj in platforms) {
			Vector3 platPos = obj.transform.position;
			if (platPos.y >= -4.0f && platPos.y < 5.0f)
				spawns.Add (platPos + new Vector3 (0, 0.8f, 0));		
		}

		spawnLoc = spawns.ToArray ();
	}

	private void SpawnEnemies(int wave) {
		if (wave % 5 == 0) {
			WaveScreen.GetComponent<Canvas> ().enabled = false;
		} else if (wave % 3 == 0) {
			SpawnUtils.SpawnGameObject (Entities.ENTITY_SPAWNER, new Vector3 (-10f, 4f, 10f));
			WaveScreen.GetComponent<Canvas> ().enabled = false;
		} else if (wave == 1) {
			SpawnNormals (CalculateEnemyCount (wave), EnumLevelType.DIFFICULTY_1);
		} else if (wave == 2) {
			SpawnNormals (CalculateEnemyCount (wave), EnumLevelType.DIFFICULTY_2);
		}
		else {
			SpawnNormals (CalculateEnemyCount (wave), EnumLevelType.DIFFICULTY_3);
		}
	}

	private int CalculateEnemyCount(int wave) {
		return wave * wave + wave;
	}

	private void SpawnNormals (int count, EnumLevelType leveltype){
		type = leveltype;
		for (int i = 0; i < count; i++) {
			GameObject[] table = SpawnUtils.GetLevelSpawnTable (type);
			SpawnUtils.SpawnGameObject (table[Random.Range(0, table.Length)], spawnLoc[Random.Range(0, spawnLoc.Length)]);
			WaveScreen.GetComponent<Canvas> ().enabled = false;
		}
	}


}
