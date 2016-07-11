using UnityEngine;
using System.Collections;

namespace Lib {

	public static class Reference { 
		public static readonly string GAME_NAME = "Rejoust";
		public static readonly int BUILD_NUMBER = 0;
		public static readonly string AUTHORS = "The Rejoust Team";

		public static readonly string FILE_SEPARATOR_NIX = "/";
		public static readonly string FILE_SEPARATOR_NT = "\\";
		public static readonly string SAVE_FOLDER_SUFFIX = "saves";
		public static readonly string LEADERBOARDS_FILE_NAME = "leaderboards";
	}

	public static class Scenes {
		public static readonly int MAIN_MENU = 0;
		public static readonly int SINGLEPLAYER = 1;
		public static readonly int MULTIPLAYER = 2;
	}

	public static class UIComponents {
		public static Canvas MAIN_MENU = null;
		public static Canvas SERVER_SELECTION = null;
		public static Canvas TUTORIAL = null;

		public static void Init() {
			UIComponents.MAIN_MENU = GameObject.Find ("CanvasMain").GetComponent<Canvas>();
			UIComponents.SERVER_SELECTION = GameObject.Find ("CanvasServerSelection").GetComponent<Canvas>();
			UIComponents.TUTORIAL = GameObject.Find ("CanvasTutorial").GetComponent<Canvas>();
		}
	}

	public static class AudioClips {
		public static AudioSource PLACEHOLDER = null;

		public static void Init() {
		}
	}
}
