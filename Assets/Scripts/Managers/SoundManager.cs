using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SoundManager : SingletonBehaviour<SoundManager>
{
	public float volume = 0.5f;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
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
		Camera.main.GetComponent<AudioSource>().clip = clip;
	}

    public void PlaySFX(GameObject target, AudioClip audio, float volume = -1)
    {
        if (audio == null)
            return;
		if (volume < 0)
			volume = this.volume;
        StartCoroutine(Play(target, audio, volume));
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

}