using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public static SoundManager instance = null;

	// Girl Audio
	public AudioClip Walk1Audio;
	public AudioClip Walk2Audio;
	public AudioClip JumpThrowAudio;
	public AudioClip CrystalCollectAudio;
	public AudioClip DeathAudio;
	private AudioSource walk1Audio;
	private AudioSource walk2Audio;
	private AudioSource jumpThrowAudio;
	private AudioSource crystalCollectAudio;
	private AudioSource deathAudio;

	// Teleporter Audio
	public AudioClip StoneDropAudio;
	public AudioClip TeleportAudio;
	private AudioSource stoneDropAudio;
	private AudioSource teleportAudio;

	// Knight Audio
	public AudioClip Armor1Audio;
	public AudioClip Armor2Audio;
	public AudioClip Armor3Audio;
	public AudioClip KnightAttackAudio;
	private AudioSource armor1Audio;
	private AudioSource armor2Audio;
	private AudioSource armor3Audio;
	private AudioSource knightAttackAudio;

	// Environment Audio
	public AudioClip DoorOpenAudio;
	public AudioClip LeverAudio;
	public AudioClip PlatformAudio;
	private AudioSource doorOpenAudio;
	private AudioSource leverAudio;
	private AudioSource platformAudio;

	private Dictionary<string, AudioSource> sounds;

	void Awake () {

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		sounds = new Dictionary<string, AudioSource> ();

		// Girl Audio
		walk1Audio = gameObject.AddComponent<AudioSource>();
		walk1Audio.clip = Walk1Audio;
		sounds ["footstep-1"] = walk1Audio;
		walk2Audio = gameObject.AddComponent<AudioSource>();
		walk2Audio.clip = Walk2Audio;
		sounds ["footstep-2"] = walk2Audio;
		jumpThrowAudio = gameObject.AddComponent<AudioSource>();
		jumpThrowAudio.clip = JumpThrowAudio;
		sounds ["jump-throw"] = jumpThrowAudio;
		crystalCollectAudio = gameObject.AddComponent<AudioSource>();
		crystalCollectAudio.clip = CrystalCollectAudio;
		sounds ["collect-crystal"] = crystalCollectAudio;
		deathAudio = gameObject.AddComponent<AudioSource>();
		deathAudio.clip = DeathAudio;
		sounds ["death"] = deathAudio;

		// Teleporter Audio
		stoneDropAudio = gameObject.AddComponent<AudioSource>();
		stoneDropAudio.clip = StoneDropAudio;
		sounds ["stone-drop"] = stoneDropAudio;
		teleportAudio = gameObject.AddComponent<AudioSource>();
		teleportAudio.clip = TeleportAudio;
		sounds ["teleport"] = teleportAudio;

		// Knight Audio
		armor1Audio = gameObject.AddComponent<AudioSource>();
		armor1Audio.clip = Armor1Audio;
		sounds ["armor-1"] = armor1Audio;
		armor2Audio = gameObject.AddComponent<AudioSource>();
		armor2Audio.clip = Armor2Audio;
		sounds ["armor-2"] = armor2Audio;
		armor3Audio = gameObject.AddComponent<AudioSource>();
		armor3Audio.clip = Armor3Audio;
		sounds ["armor-3"] = armor3Audio;
		knightAttackAudio = gameObject.AddComponent<AudioSource>();
		knightAttackAudio.clip = KnightAttackAudio;
		sounds ["knight-attack"] = knightAttackAudio;

		// EnvironmentAudio
		doorOpenAudio = gameObject.AddComponent<AudioSource>();
		doorOpenAudio.clip = DoorOpenAudio;
		sounds ["door-open"] = doorOpenAudio;
		leverAudio = gameObject.AddComponent<AudioSource>();
		leverAudio.clip = LeverAudio;
		sounds ["lever"] = leverAudio;
		platformAudio = gameObject.AddComponent<AudioSource>();
		platformAudio.clip = PlatformAudio;
		sounds ["platform"] = platformAudio;
		platformAudio.loop = true;
	}
	
	public void PlaySound(string soundName) {
		AudioSource temp;
		if (sounds.TryGetValue (soundName, out temp)) {
			temp.Play ();
		}
	}

	public void StopSound(string soundName) {
		AudioSource temp;
		if (sounds.TryGetValue (soundName, out temp)) {
			temp.Stop ();
		}
	}
}
