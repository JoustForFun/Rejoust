using UnityEngine;
using System.Collections;
using Utils;
using Lib;

public class EntityGemSpawner : MonoBehaviour {

	private float dropTime = 7.5f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Random.Range (0, 10) == 5)
			SpawnUtils.SpawnGameObject (Entities.ENTITY_SOULGEM, gameObject);

		dropTime -= Time.deltaTime;
		transform.position += new Vector3(1.0f,0,0);

		if (transform.position.x > 10)  //looping screen 
			transform.position = new Vector2 (-10.0f, transform.position.y);

		if (transform.position.x < -10)  //looping screen 
			transform.position = new Vector2 (10.0f, transform.position.y);

		if (dropTime <= 0)
			Destroy (gameObject);
	}
}
