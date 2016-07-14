using System;
using UnityEngine;
using System.Collections.Generic;
using JPlayer;

namespace JPowerUp
{
	public class PowerupFactory
	{
		public static PowerupFactory INSTANCE = new PowerupFactory();
		private Dictionary<EnumPowerup, IPowerup> powerups = new Dictionary<EnumPowerup, IPowerup>();

		public PowerupFactory () {
			Register (new SimplePowerup(EnumPowerup.NONE, 0f));
			Register (new PowerupDash());
			Register (new PowerupLifeUp());
			Register (new PowerupShield());
		}

		public void Register (IPowerup powerup) {
			powerups.Add (powerup.GetPowerupID(), powerup);
		}

		public IPowerup CallPowerup (EnumPowerup id) {
			IPowerup powerup = null;
			powerups.TryGetValue (id, out powerup);
			return powerup;
		}
	}

	public interface IPowerup {

		EnumPowerup GetPowerupID();
		float GetTimeoutTime();
		void StartPowerup(GameObject player);
		void OnPickUp (GameObject player);
		void OnUpdate (GameObject player);
		void OnTimeout (GameObject player);

	}

	public class SimplePowerup : IPowerup{
		private readonly EnumPowerup ID;
		private readonly float lastTime;

		public SimplePowerup (EnumPowerup ID, float lastTime) {
			this.ID = ID;
			this.lastTime = lastTime;
		}

		public float GetTimeoutTime () {
			return this.lastTime;
		}

		public void StartPowerup(GameObject player) {
			PlayerStats stats = PlayerStatsController.INSTANCE.GetPlayerStats(player.GetComponent<controller> ().stat_id);
			stats.powerupTimer = this.lastTime;
			stats.powerups = this.ID;
		}

		public EnumPowerup GetPowerupID () {
			return this.ID;
		}

		public virtual void OnPickUp (GameObject player) {
			
		}

		public virtual void OnUpdate (GameObject player) {
			
		}
		public virtual void OnTimeout (GameObject player) {
			
		}
	}

	public class PowerupShield : SimplePowerup {
		public PowerupShield () : base(EnumPowerup.SHEILD, 7.5f) {
		}
			
		public override void OnPickUp(GameObject player) {
			PlayerStatsController.INSTANCE.GetPlayerStats(player.GetComponent<controller> ().stat_id).invincible = true;
		}

		public override void OnTimeout (GameObject player) {
			PlayerStatsController.INSTANCE.GetPlayerStats(player.GetComponent<controller> ().stat_id).invincible = false;
		}
	}

	public class PowerupDash : SimplePowerup {
		public PowerupDash () : base(EnumPowerup.DASH, 7.5f) {
		}

		public override void OnPickUp(GameObject player) {
			PlayerStatsController.INSTANCE.GetPlayerStats(player.GetComponent<controller> ().stat_id).delta_movementSpeed += 0.2f;
		}

		public override void OnTimeout (GameObject player) {
			PlayerStatsController.INSTANCE.GetPlayerStats(player.GetComponent<controller> ().stat_id).delta_movementSpeed -= 0.2f;
		}


	}

	public class PowerupLifeUp : SimplePowerup {
		
		public PowerupLifeUp () : base(EnumPowerup.LIFEUP, 0.0f) {
		}

		public override void OnPickUp(GameObject player) {
			PlayerStats stats = PlayerStatsController.INSTANCE.GetPlayerStats(player.GetComponent<controller> ().stat_id);
			stats.lives++;
			stats.powerups = EnumPowerup.NONE;
		}
	}

	public enum EnumPowerup {
		NONE,
		SHEILD,
		DASH,
		LIFEUP,
	}

}

