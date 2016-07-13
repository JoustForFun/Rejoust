using UnityEngine;
using System.Collections;
using JPowerUp;
using JPlayer;
using Lib;

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

		switch (type) {
		case EnumPowerup.DASH:
			theSprite = Sprites.DASH;
			break;
		case EnumPowerup.LIFEUP:
			theSprite = Sprites.EXTRA_LIFE;
			break;
		case EnumPowerup.SHEILD:
			theSprite = Sprites.SHEILD;
			break;
		}
			
			

		gameObject.GetComponentInChildren<SpriteRenderer> ().sprite = theSprite;
	}

	// Update is called once per frame
	void Update () {
		disappearTime -= 1.0f * Time.deltaTime;

		if (disappearTime <= 0)
			Destroy (gameObject);
	
	}

	void OnCollisionEnter2D(Collision2D col) {

		if (col.gameObject.tag.ToLower() != "player")
			return;
		
		int player = col.gameObject.GetComponent<controller> ().stat_id;
		PlayerStats stats = PlayerStatsController.INSTANCE.GetPlayerStats (player);
		stats.powerups = this.type;
		IPowerup thePowerup = PowerupFactory.INSTANCE.CallPowerup (this.type);
		stats.powerupTimer = thePowerup.GetTimeoutTime();
		thePowerup.OnPickUp (col.gameObject);

		Destroy (gameObject);
	}

	private EnumPowerup GetRandomPowerup() {
		switch (new System.Random ().Next (0, 2)) {
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
