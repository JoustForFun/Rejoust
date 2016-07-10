using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Lib;

public class JMenuController : MonoBehaviour {

	public void LoadSingleplayerScene () {
		//SceneManager.LoadScene (Scenes.SINGLEPLAYER);
	}

	public void LoadMultiplayerScene () {
		//Init Network manager here
		//SceneManager.LoadScene (Scenes.MULTIPLAYER);
	}

	public void SwapToMainMenu () {
	}

	public void SwapToServerSelection () {
	}

	public void SwapToOptions () {
	}

	public void SwapToTutorial () {
	}

	public void ExitGame () {
		Application.Quit ();
	}
	
}
