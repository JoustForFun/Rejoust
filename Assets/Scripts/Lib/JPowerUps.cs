using System;
using UnityEngine;
using System.Collections.Generic;

namespace JPowerUp
{
	public class PowerupFactory
	{
		private static PowerupFactory INSTANCE = new PowerupFactory();
		private static Dictionary<EnumPowerup, IPowerup> powerups = new Dictionary<EnumPowerup, IPowerup>();

		public PowerupFactory () {
			
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
		void OnPickUp (GameObject player);
		void OnUpdate (GameObject player);
		void OnTimeout (GameObject player);

	}

	public class SimplePowerup : IPowerup{
		private readonly EnumPowerup ID;

		public SimplePowerup (EnumPowerup ID) {
			this.ID = ID;
		}

		public EnumPowerup GetPowerupID () {
			return this.ID;
		}

		public void OnPickUp (GameObject player) {}

		public void OnUpdate (GameObject player) {}

		public void OnTimeout (GameObject player) {}
	}

	public enum EnumPowerup {
		NONE,
		SHEILD,
		DASH,
		LIFEUP,
	}
}

