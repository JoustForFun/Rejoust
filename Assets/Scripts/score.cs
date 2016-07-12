using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour {
	public static int scr = 0; 
	Text mytext; 
	public GameObject enemy;
	// Use this for initialization
	void Awake() {
		mytext = GetComponent<Text> ();

	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		mytext.text = "Score: " + scr;
	}
	public void AddScore(int delta){
		scr += delta; 
		mytext.text = "Score: " + scr; 
	}
}
