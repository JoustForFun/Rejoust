using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net.Sockets;
using System.ComponentModel;
using System.Net;
using Utils;

namespace JNetwork {

	public class NetworkManager {
		/**
		 * The server instance of the network manager
		 * */
		public readonly static NetworkManager INSTANCE_SERVER = new NetworkManager (Side.SERVER);

		/**
		 * The client instance of the network manager
		 * */
		public readonly static NetworkManager INSTANCE_CLIENT = new NetworkManager (Side.CLIENT);

		public readonly Side side;

		private readonly Dictionary <int, INetworkChannel> channels;

		/**
		 * The network manager is a object that in change of sending and recieving network packets. Network packets should be wrapped inside channels and processed with the OnSend and OnRecieve functions
		 * The side tells the network manager which side the packet should be sent from and channels can be registered with NetworkManager.RegisterNetworkChannel
		 * */
		public NetworkManager(Side side) {
			this.side = side;
			channels = new Dictionary<int, INetworkChannel> ();
		}

		public static void RegisterNetworkChannel(int channelID,INetworkChannel channel) {
			INSTANCE_CLIENT.channels.Add (channelID, channel);
			INSTANCE_SERVER.channels.Add (channelID, channel);
		}

		public void OnSend() {

			byte[] data = null;

			ByteUtils.AddByte (ref data, 1); //ByteBuffer Header

			foreach (KeyValuePair<int, INetworkChannel> pair in channels) {

				INetworkChannel channel = pair.Value;

				if (this.side == channel.GetSide()) {

					byte[] channelData = null;

					//Adds the id of the channel
					ByteUtils.AddInt (ref data, pair.Key);

					//Adds the data of the channel
					channelData = channel.OnSend ();
					ByteUtils.AddBytes (ref data, channelData);
				}

			}

			ByteUtils.AddByte (ref data, 1);//ByteBuffer Footer

			//TODO send data here
		}

		public void OnRecieve(byte[] data)
		{
			byte header = ByteUtils.ReadByte (ref data);
			if (header == 1) {

				int channel = 0;
				byte[] channelData = null;

				while (data.Length > 1) {

					channel = ByteUtils.ReadInt (ref data);
					channelData = ByteUtils.ReadBytes (ref data);

					channels [channel].OnRecieve(channelData);
				}

			}

		}
	}

	/**
	 * The INetworkChannel is used to create a custom network channel, which processes how data should be send and recieved by the network manager
	 * A easier way to create network channels is by instanciating the SimpleNetworkChannel object
	 * */
	public interface INetworkChannel {

		Side GetSide ();

		byte[] OnSend();

		void OnRecieve(byte[] data);

	}

	public abstract class SimpleNetworkChannel : INetworkChannel {
		private readonly Side side;

		public SimpleNetworkChannel(Side side) {

			this.side = side;
		
		}

		public Side GetSide() {
			return this.side;
		}

		public abstract byte[] OnSend ();

		public abstract void OnRecieve (byte[] data);

	}
		

}
