using System;
using System.Collections.Generic;

namespace JPlayer
{
	public class PlayerStatsController {

		public static PlayerStatsController INSTANCE = new PlayerStatsController();

		private Dictionary<int, PlayerStats> players = new Dictionary<int, PlayerStats> ();
		private int nextPlayerID = 0;

		public PlayerStatsController ()
		{
			
		}

		public int GetPlayerID() {
			return this.nextPlayerID++;
		}

		public void RemovePlayer(int ID) {
			this.players.Remove (ID);
		}

		/**
		 * Resets the entire dictionary for reuse
		 * */
		public void ClearPlayers () {
			this.players = new Dictionary<int, PlayerStats> ();
			this.nextPlayerID = 0;
		}

		/**
		 * Registers a player in the registery
		 * */
		public int AddPlayer (PlayerStats stats) {
			int id = GetPlayerID ();
			players.Add (id, stats);
			return id;
		}

		public PlayerStats GetPlayerStats (int id) {
			PlayerStats stats = null;
			this.players.TryGetValue (id, out stats);
			return stats;
		}
			
			

	}

	public class PlayerStats {
		public bool invincible;
		public bool isDead;
		public EnumPlayerAnimation playerAnimation;
		public long score;
		public int lives;

		public readonly float base_movementSpeed;
		public readonly float base_jumpHeight;

		public float delta_movementSpeed;
		public float delta_jumpHeight;

		public PlayerStats (float baseMovementSpeed, float baseJumpHeight, int lives) {
			this.base_movementSpeed = baseMovementSpeed;
			this.base_jumpHeight = baseJumpHeight;
			this.lives = lives;
		}

		public float GetMovementSpeed() {
			return this.base_movementSpeed + this.delta_movementSpeed;
		}

		public float GetJumpHeight() {
			return this.base_jumpHeight + this.delta_jumpHeight;
		}



	}

	public enum EnumPlayerAnimation {
		IDLE_GOUND,
		IDLE_SKY,
		MOVE_GROUND,
		MOVE_SKY,
		DEATH,
	}
}

