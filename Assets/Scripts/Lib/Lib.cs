using UnityEngine;
using System.Collections;

namespace Lib {

	public static class Reference { 
		public static readonly string GAME_NAME = "Rejoust";
		public static readonly int BUILD_NUMBER = 0;
		public static readonly string AUTHORS = "The Rejoust Team";
		public static long FINAL_SCORE = 0;

		public static readonly string FILE_SEPARATOR_NIX = "/";
		public static readonly string FILE_SEPARATOR_NT = "\\";
		public static readonly string SAVE_FOLDER_SUFFIX = "saves";
		public static readonly string LEADERBOARDS_FILE_NAME = "leaderboards";
	}

	public static class Scenes {
		public static readonly int MAIN_MENU = 0;
		public static readonly int GAME_SCENE = 1;
		public static readonly int END_SCENE = 2;
	}

	public static class Audio {
		public static readonly AudioClip CROWD_CHEER = Resources.Load<AudioClip> ("Audio/CrowdCheer");
		public static readonly AudioClip DEATH = Resources.Load<AudioClip> ("Audio/Death");
		public static readonly AudioClip FLAP = Resources.Load<AudioClip> ("Audio/Flap");
		public static readonly AudioClip GATE_OPEN = Resources.Load<AudioClip> ("Audio/GateOpen");
		public static readonly AudioClip GEM_PICKUP = Resources.Load<AudioClip> ("Audio/GemPickup");
		public static readonly AudioClip HIT = Resources.Load<AudioClip> ("Audio/Hit");
		public static readonly AudioClip MENU_SONG = Resources.Load<AudioClip>("Audio/MenuSong");
		public static readonly AudioClip POWERUP = Resources.Load<AudioClip> ("Audio/Powerup");
		public static readonly AudioClip POWERUP_APPEAR = Resources.Load<AudioClip> ("Audio/PowerupAppear");
		public static readonly AudioClip ROAR = Resources.Load<AudioClip> ("Audio/Roar");
		public static readonly AudioClip WALK = Resources.Load<AudioClip> ("Audio/Walk");
		public static readonly AudioClip POWERUP_DISAPPEAR = Resources.Load<AudioClip> ("Audio/PowerupDisappear");
		public static readonly AudioClip ENEMY_FLAP = Resources.Load<AudioClip> ("Audio/EnemyFlap");

	}

	public static class Sprites {
		public static readonly Sprite DASH = Resources.Load<Sprite> ("Objects/Powerups/Dash/Dash0");
		public static readonly Sprite EXTRA_LIFE = Resources.Load<Sprite>("Objects/Powerups/ExtraLife/ExtraLife0");
		public static readonly Sprite JUMP = Resources.Load<Sprite> ("Objects/Powerups/Jump/Jump0");
		public static readonly Sprite SHEILD = Resources.Load<Sprite> ("Objects/Powerups/Shield/Shield0");
		public static readonly Sprite SOUL_GEM = Resources.Load<Sprite> ("Objects/Powerups/SoulGem/DarkCrystal/DarkCrystal0");
		public static readonly Sprite SHEILD_FX = Resources.Load<Sprite> ("Objects/Powerups/Shield/Shield_Full");
	}

	public static class Entities {
		public static readonly GameObject BOSS_LION = Resources.Load("Prefab/BossLion") as GameObject;
		public static readonly GameObject ENEMY_MINI_DRAGON = Resources.Load ("Prefab/EnemyMiniDragon") as GameObject;
		public static readonly GameObject ENEMY_RAVEN_KNIGHT = Resources.Load ("Prefab/EnemyRavenKnight") as GameObject;
		public static readonly GameObject ENEMY_CENTAUR = Resources.Load("Prefab/EnemyCentaur") as GameObject;
		public static readonly GameObject ENTITY_POWERUP = Resources.Load ("Prefab/EntityPowerup") as GameObject;
		public static readonly GameObject ENTITY_SOULGEM = Resources.Load ("Prefab/EntitySoulGem") as GameObject;
		public static readonly GameObject ENTITY_SPAWNER = Resources.Load ("Prefab/EntityGemSpawner") as GameObject;
	}

	public static class SpawnTables {

		public static readonly GameObject[] COLOSSEUM_0 = new GameObject[] {
			Entities.ENEMY_MINI_DRAGON
		};

		public static readonly GameObject[] COLOSSEUM_1 = new GameObject[] {
			Entities.ENEMY_MINI_DRAGON,
			Entities.ENEMY_RAVEN_KNIGHT
		};

		public static readonly GameObject[] COLOSSEUM_2 = new GameObject[] {
			Entities.ENEMY_MINI_DRAGON,
			Entities.ENEMY_RAVEN_KNIGHT,
			Entities.ENEMY_CENTAUR
		};
			
		public static readonly GameObject[] COLOSSEUM_BOSS = new GameObject[] {
			Entities.ENEMY_MINI_DRAGON,
			Entities.ENEMY_RAVEN_KNIGHT,
			Entities.ENEMY_CENTAUR
		};


		public static readonly GameObject[] COLOSSEUM_GEM = new GameObject[] {
		};
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
