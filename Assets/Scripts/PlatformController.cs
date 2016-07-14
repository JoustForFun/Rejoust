using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	private bool loadInProcess = false;
	private bool markedForDelete = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (loadInProcess) {
			gameObject.transform.position -= new Vector3 (0, 0.5f);

			if (gameObject.transform.position.y <= -3.5f) {
				gameObject.transform.position = new Vector3 (-1.6f, -3.5f);
				loadInProcess = false;
				GameObject.FindGameObjectWithTag ("Spawner").GetComponent<JSpawner> ().RefreshSpawns ();
			}
			return;
		} else if (markedForDelete) {
			gameObject.transform.position -= new Vector3 (0, 0.5f);
			if (gameObject.transform.position.y <= -12f) {
				markedForDelete = false;
				GameObject.FindGameObjectWithTag ("Spawner").GetComponent<JSpawner> ().RefreshSpawns ();
				Destroy (gameObject);
			}
			return;
		}
	}

	public void MarkLoad () {
		loadInProcess = true;
	}

	public void MarkDelete() {
		markedForDelete = true;
	}
}
