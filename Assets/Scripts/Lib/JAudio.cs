using System;
using System.Collections;
using UnityEngine;

namespace JAudio
{
	/**
	 * The AudioManager is a utility class that can be used to directly handle audio, please remember to change the camera object when the scene reloads
	 * */
	public class AudioManager
	{
		public static readonly AudioManager INSTANCE = new AudioManager (GameObject.FindGameObjectWithTag("MainCamera"));
		private readonly GameObject audioPref;
		private GameObject camera;

		public AudioManager (GameObject camera) {
			this.audioPref = Resources.Load ("Prefab/Functional/AudioSource") as GameObject;
			this.camera = camera;
		}

		public static void Init() {
		}

		public AudioSource PlayAudio(AudioClip clip, GameObject location, bool loop, float volume) {
			GameObject newAudioPref = UnityEngine.Object.Instantiate (audioPref) as GameObject;
			AudioSource audioSrc = newAudioPref.GetComponent<AudioSource> ();


			audioSrc.loop = loop; //If it should loop
			audioSrc.clip = clip;//Assigns the clip
			audioSrc.volume = volume;

			newAudioPref.name = "AudioSource";
			newAudioPref.transform.position = location.transform.position;//Changes the position to the location
			newAudioPref.transform.SetParent(location.transform);//Sets it as a parent

			audioSrc.Play();

			return audioSrc;
		}

		public AudioSource PlayAudio (AudioClip clip, GameObject location, bool loop){
			return this.PlayAudio (clip, location, loop, 1.0F);
		}

		public AudioSource PlayAudio (AudioClip clip, GameObject location) {
			return this.PlayAudio (clip, location, false);
		}

		public AudioSource PlayAudio (AudioClip clip) {
			return this.PlayAudio (clip, this.camera);
		}

		public AudioSource PlayAudio (AudioClip clip, bool loop) {
			return this.PlayAudio (clip, this.camera, loop);
		}

		public AudioSource PlayAudio (AudioClip clip, bool loop, float volume) {
			return this.PlayAudio (clip, this.camera, loop, volume);
		}

		public AudioSource PlayAudio (AudioClip clip, float volume) {
			return this.PlayAudio (clip, this.camera, false, volume);
		}

		public void SetCamera (GameObject camera) {
			this.camera = camera;
		}

	}
}

