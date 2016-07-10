using System;
using UnityEngine;
using System.Collections.Generic;

namespace Utils
{
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
}

