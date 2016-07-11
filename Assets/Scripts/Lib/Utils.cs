using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Lib;

namespace Utils
{
	/**
	 * The SystemUtils class contains methods that handle file creation and IO
	 * */
	public static class SystemUtils {

		private static string saveFolder = null;

		public static string GetFileSeparator () {
			if (SystemInfo.operatingSystem.ToLower().Contains("win"))
				return Reference.FILE_SEPARATOR_NT;
			return Reference.FILE_SEPARATOR_NIX;
		}

		private static string GetSaveFolderString () {
			if (saveFolder == null)
				saveFolder = String.Format ("{0}{1}{2}", Directory.GetCurrentDirectory(), GetFileSeparator(), Reference.SAVE_FOLDER_SUFFIX);
			return saveFolder;

		}

		/**
		 * Appends the text array as lines in a text file
		 * The save folder is automatically applied
		 * */
		public static void WriteLinesToFile (string fileName, string[] text) {
			FileInfo file = new FileInfo (String.Format("{0}{1}{2}", GetSaveFolderString(), GetFileSeparator(), fileName));
			string[] toWrite = text;

			try {
				if (file.Exists) {
					string[] old = File.ReadAllLines (file.FullName);
					List<string> oldList = new List<string> (old);
					List<string> addList = new List<string> (toWrite);
					oldList.AddRange(addList);
					toWrite = oldList.ToArray();
				} else
					file.Directory.Create ();
				File.WriteAllLines(file.FullName, toWrite);

			} catch (Exception e){ //Gotta catch'em all! 
				Debug.LogError (e);
			}
		}

		/**
		 * Reads all the text from a file
		 * Every line is a string in an array
		 * */
		public static string[] ReadLinesFromFile (string fileName) {
			FileInfo file = new FileInfo (String.Format("{0}{1}{2}", GetSaveFolderString(), GetFileSeparator(), fileName));

			string[] dataRead = null;

			try {
				if (file.Exists) {
					dataRead = File.ReadAllLines(file.FullName);
				}
			} catch (Exception e) {
				Debug.LogError (e);
			}

			return dataRead;
		}
	}

	/**
	 * The ByteUtil class contains ways of reading data from a byte array, which is helpful for saving data and handling packets
	 * Add functions adds the variable/object to the end of an existing byte array, the read functions return the value and remove them from the array
	 * 
	 * Note from author:
	 * Please don't read byte arrays wrong. First of all, I will be very sad if you do. Second of all, it will crash with an array out of bounds exception
	 * */
	public static class ByteUtils {

		//Add functions
		public static void AddBool(ref byte[] byteBuf, bool b) {

			byte[] byteb = BitConverter.GetBytes (b);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (byteb);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = byteb;
		}

		public static void AddByte(ref byte[] byteBuf, byte b) {


			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);

				oldBytes.Add (b);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = new byte[] {b};
		}

		public static void AddShort(ref byte[] byteBuf, short s) {

			byte[] bytes = BitConverter.GetBytes (s);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (bytes);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = bytes;
		}

		public static void AddInt(ref byte[] byteBuf, int i) {

			byte[] bytei = BitConverter.GetBytes (i);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (bytei);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = bytei;
		}

		public static void AddLong(ref byte[] byteBuf, long l) {

			byte[] byteData = BitConverter.GetBytes (l);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (byteData);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = byteData;
		}

		public static void AddFloat(ref byte[] byteBuf, float f) {

			byte[] byteData = BitConverter.GetBytes (f);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (byteData);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = byteData;
		}

		public static void AddDouble(ref byte[] byteBuf, double d) {

			byte[] byteData = BitConverter.GetBytes (d);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (byteData);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = byteData;
		}

		public static void AddBytes(ref byte[] byteBuf, byte[] b) {

			AddInt (ref byteBuf, b.Length);

			List<byte> oldBytes = new List<byte> (byteBuf);
			List<byte> newBytes = new List<byte> (b);

			oldBytes.AddRange (newBytes);
			
			byteBuf = oldBytes.ToArray ();

		}

		public static void AddChar(ref byte[] byteBuf, char c) {

			byte[] byteData = BitConverter.GetBytes (c);

			if (byteBuf != null) {

				List<byte> oldBytes = new List<byte> (byteBuf);
				List<byte> newBytes = new List<byte> (byteData);

				oldBytes.AddRange (newBytes);

				byteBuf = oldBytes.ToArray ();
			} else
				byteBuf = byteData;
		}

		public static void AddString(ref byte[] byteBuf, string s) {

			byte[] byteData = System.Text.Encoding.UTF8.GetBytes (s);
			AddInt (ref byteBuf, byteData.Length);

			//If this somehow ends as a null, I will literally jump off a bridge

			List<byte> oldBytes = new List<byte> (byteBuf);
			List<byte> newBytes = new List<byte> (byteData);

			oldBytes.AddRange (newBytes);

			byteBuf = oldBytes.ToArray (); 

		}

		public static void AddVector3(ref byte[] byteBuf, Vector3 v) {
			AddFloat (ref byteBuf, v.x);
			AddFloat (ref byteBuf, v.y);
			AddFloat (ref byteBuf, v.z);
		}

		public static void AddQuaternion(ref byte[] byteBuf, Quaternion q) {
			AddFloat (ref byteBuf, q.x);
			AddFloat (ref byteBuf, q.y);
			AddFloat (ref byteBuf, q.z);
			AddFloat (ref byteBuf, q.w);
		}

		public static void AddTransform(ref byte[] byteBuf, Transform t) {
			AddVector3(ref byteBuf, t.position);
			AddQuaternion(ref byteBuf, t.rotation);
		}

		//Read functions

		public static bool ReadBool(ref byte[] byteBuf) {

			byte[] boolBytes = null;
			Array.Copy (byteBuf, boolBytes, 1);

			int newByteBufLength = byteBuf.Length - 1;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 1, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToBoolean (boolBytes, 0);
		}

		public static byte ReadByte(ref byte[] byteBuf) {

			byte byteBytes = byteBuf [0];

			int newByteBufLength = byteBuf.Length - 1;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 1, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return byteBytes;

		}

		public static short ReadShort(ref byte[] byteBuf) {

			byte[] shortBytes = null;
			Array.Copy (byteBuf, shortBytes, 2);

			int newByteBufLength = byteBuf.Length - 2;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 2, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToInt16 (shortBytes, 0);
		}

		public static int ReadInt(ref byte[] byteBuf) {

			byte[] intBytes = null;
			Array.Copy (byteBuf, intBytes, 4);

			int newByteBufLength = byteBuf.Length - 4;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 4, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToInt32 (intBytes, 0);
		}

		public static long ReadLong(ref byte[] byteBuf) {

			byte[] longBytes = null;
			Array.Copy (byteBuf, longBytes, 8);

			int newByteBufLength = byteBuf.Length - 8;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 8, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToInt64 (longBytes, 0);
		}

		public static float ReadFloat(ref byte[] byteBuf) {

			byte[] floatBytes = null;
			Array.Copy (byteBuf, floatBytes, 4);

			int newByteBufLength = byteBuf.Length - 4;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 4, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToSingle (floatBytes, 0);
		}

		public static double ReadDouble(ref byte[] byteBuf) {

			byte[] doubleBytes = null;
			Array.Copy (byteBuf, doubleBytes, 8);

			int newByteBufLength = byteBuf.Length - 8;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 8, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToDouble (doubleBytes, 0);
		}

		public static byte[] ReadBytes(ref byte[] byteBuf) {

			int arrayLength = ReadInt (ref byteBuf);

			byte[] bytes = null;
			Array.Copy (byteBuf, bytes, arrayLength);

			int newByteBufLength = byteBuf.Length - arrayLength;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, arrayLength, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return bytes;

		}

		public static char ReadChar(ref byte[] byteBuf) {

			byte[] charBytes = null;
			Array.Copy (byteBuf, charBytes, 2);

			int newByteBufLength = byteBuf.Length - 2;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, 2, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return BitConverter.ToChar (charBytes, 0);
		}

		public static String ReadString(ref byte[] byteBuf) {

			int arrayLength = ReadInt (ref byteBuf);

			byte[] charBytes = null;
			Array.Copy (byteBuf, charBytes, arrayLength);

			int newByteBufLength = byteBuf.Length - arrayLength;
			byte[] newByteBuf = new byte[newByteBufLength];

			Array.Copy (byteBuf, arrayLength, newByteBuf, 0, newByteBufLength);
			byteBuf = newByteBuf;

			return new string(System.Text.Encoding.UTF8.GetChars (charBytes));
		}

		public static Vector3 ReadVector3(ref byte[] byteBuf) {
			float x = ReadFloat (ref byteBuf);
			float y = ReadFloat (ref byteBuf);
			float z = ReadFloat (ref byteBuf);

			return new Vector3 (x, y, z);
		}

		public static Quaternion ReadQuaternion(ref byte[] byteBuf) {

			float x = ReadFloat (ref byteBuf);
			float y = ReadFloat (ref byteBuf);
			float z = ReadFloat (ref byteBuf);
			float w = ReadFloat (ref byteBuf);

			return new Quaternion(x, y, z, w);
		}

		/**
		 * Since transform is protected, we can only change values of an existing transform
		 * */
		public static void ReadToTransform(ref byte[] byteBuf, ref Transform t) {

			Vector3 v = ReadVector3 (ref byteBuf);
			Quaternion q = ReadQuaternion (ref byteBuf);

			t.position = v;
			t.rotation = q;
		}
	}

	/**
	 * The side enum is used to decide which side the data should be processed on
	 * */
	public enum Side {
		CLIENT,
		SERVER
	}

	public class LeaderBoards {

		public static readonly LeaderBoards INSTANCE = new LeaderBoards();

		private List<Ranking> data = null;

		public LeaderBoards () {
			LoadData ();
		}

		private void LoadData() {
			data = new List<Ranking> ();
			string[] unprocessed = SystemUtils.ReadLinesFromFile (Reference.LEADERBOARDS_FILE_NAME);

			foreach (string raw in unprocessed) {
				Ranking rank = Ranking.CreateRankingFromText (raw);
				if (rank != null)
					data.Add (rank);
			}
		}

		public void AddRanking(Ranking rank) {
			data.Add (rank);
			if(!rank.GetUserName().Contains(":"))
				SystemUtils.WriteLinesToFile (new string[] {rank.ToString()});
		}

		public void AddRanking (string username, long score) {
			Ranking rank = new Ranking (username, score);
			AddRanking (rank);
		}

		public Ranking[] GetRanking() {
			SortData ();
			return data.ToArray ();
		}

		private void SortData() {
			data.Sort ((r1, r2) => {return r1.GetScore().CompareTo(r2.GetScore());});
		}
	}

	public class Ranking {

		private readonly long score;
		private readonly string username;

		public Ranking (string username, long score) {
			this.username = username;
			this.score = score;
		}

		//TODO Validate if fix worked
		public static Ranking CreateRankingFromText(string text) {
			int separator = text.IndexOf(":");

			if (separator != -1) {
				try {
					string username = text.Substring (0, separator);
					long score = Int64.TryParse(text.Substring (separator + 1, text.Length - separator - 1));
					return new Ranking(username, score);
				} catch (Exception e) {
					Debug.Log ("Failed to parse username and score from string!");
					Debug.LogError (e);
				}
			}

			return null;
		}

		public long GetScore() {
			return this.score;
		}

		public string GetUserName() {
			return this.username;
		}

		public override string ToString ()
		{
			return string.Format ("{0}:{1}", username, score);
		}

	}


}

