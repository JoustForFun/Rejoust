using UnityEngine;
using System.Collections;
using JPowerUp;
using JPlayer;
using Lib;
using JAudio;

public class EntityPowerup : MonoBehaviour {

	private EnumPowerup type = EnumPowerup.NONE;
	private float disappearTime = 0;

	void Awake () {
		type = GetRandomPowerup();
		disappearTime = (float) ((new System.Random ()).NextDouble () * 2.5f + 2.5f);
	}

	// Use this for initialization
	void Start () {
		Sprite theSprite = null;
		Animator animator = gameObject.GetComponentInChildren<Animator>();

		switch (type) {
		case EnumPowerup.DASH:
			theSprite = Sprites.DASH;
			animator.SetInteger ("PowerupID", 1);
			break;
		case EnumPowerup.LIFEUP:
			theSprite = Sprites.EXTRA_LIFE;
			animator.SetInteger ("PowerupID", 0);
			break;
		case EnumPowerup.SHEILD:
			theSprite = Sprites.SHEILD;
			animator.SetInteger ("PowerupID", 2);
			break;
		}
			
		gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = theSprite;
	}

	// Update is called once per frame
	void Update () {
		disappearTime -= 0.5f * Time.deltaTime;

		if (disappearTime <= 0) {
			AudioManager.INSTANCE.PlayAudio (Audio.POWERUP_DISAPPEAR);
			Destroy (gameObject);
		}
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.tag.ToLower() != "player")
			return;
		AudioManager.INSTANCE.PlayAudio(Audio.POWERUP);

		int player = col.gameObject.GetComponent<controller> ().stat_id;
		PlayerStats stats = PlayerStatsController.INSTANCE.GetPlayerStats (player);

		if (stats.powerups != EnumPowerup.NONE)
			PowerupFactory.INSTANCE.CallPowerup (stats.powerups).OnTimeout(col.gameObject);
		
		stats.powerups = this.type;
		IPowerup thePowerup = PowerupFactory.INSTANCE.CallPowerup (this.type);
		stats.powerupTimer = thePowerup.GetTimeoutTime();
		thePowerup.OnPickUp (col.gameObject);

		Destroy (gameObject);
	}

	private EnumPowerup GetRandomPowerup() {
		switch (new System.Random ().Next (0, 3)) {
			case 0:
				return EnumPowerup.SHEILD;
			case 1:
				return EnumPowerup.DASH;
			case 2:
				return EnumPowerup.LIFEUP;
			default:
				return EnumPowerup.NONE; 
		}

	}
}
