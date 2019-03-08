using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoundManager : SingletonBehaviour<SoundManager>
{
	public float volume = 0.5f;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

	protected override void Awake()
	{
		if (inst != this)
		{
			Destroy(gameObject);
			return;
		}
		SetStatic();
		
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");
        foreach (AudioClip clip in clips)
        {
			if (audioClips.ContainsKey(clip.name))
                continue;
            audioClips.Add(clip.name, clip);
        }
		
    }

	public void ChangeBGM(AudioClip clip)
	{
		Camera.main.GetComponent<AudioSource>().Stop();
		Camera.main.GetComponent<AudioSource>().clip = clip;
		Camera.main.GetComponent<AudioSource>().Play();
	}

    public void PlaySFX(AudioClip audio, GameObject target = null, float volume = -1)
    {
        if (audio == null)
            return;
		if (target == null)
			target = Camera.main.gameObject;
		if (volume < 0)
			volume = this.volume;
        StartCoroutine(Play(target, audio, volume));
    }

	public void PlaySFXLoop(GameObject target, AudioClip audio, int loopTime, float volume = -1)
	{
		if (audio == null)
			return;
		if (volume < 0)
			volume = this.volume;
		StartCoroutine(PlayLoop(target, audio, loopTime, volume));

	}

    private IEnumerator Play(GameObject target, AudioClip audio, float volume)
    {
        AudioSource audioSource = target.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.clip = audio;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.spatialBlend = 1;
        audioSource.maxDistance = 200f;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        Destroy(audioSource);
    }

	private IEnumerator PlayLoop(GameObject target, AudioClip audio, int loopTime, float volume)
	{
		AudioSource audioSource = target.AddComponent<AudioSource>();
		audioSource.volume = volume;
		audioSource.clip = audio;
		audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
		audioSource.spatialBlend = 1;
		audioSource.maxDistance = 200f;
		audioSource.loop = true;
		audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length * loopTime);
		Destroy(audioSource);
	}
}