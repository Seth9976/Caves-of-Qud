using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRL.Sound;
using XRL.UI;

// Token: 0x0200000C RID: 12
public class CampfireSounds : MonoBehaviour
{
	// Token: 0x06000047 RID: 71 RVA: 0x00003344 File Offset: 0x00001544
	public void Open()
	{
		this.bPlaying = true;
		if (Options.GetOption("OptionSound", "") != "Yes")
		{
			return;
		}
		this.extinguishSource.GetComponent<AudioSource>().pitch = global::UnityEngine.Random.value * 0.1f - 0.05f + 1f;
		this.harmonicaSource.GetComponent<AudioSource>().pitch = global::UnityEngine.Random.value * 0.1f - 0.05f + 1f;
		foreach (MusicSource musicSource in SoundManager.MusicSources.Values)
		{
			this.originalMusicVolumes[musicSource.Channel] = musicSource.TargetVolume;
			musicSource.TargetVolume = 0f;
		}
		this.flourishTimer = 0f;
		this.harmonicaStart = -0.75f;
		this.extinguishSource.SetActive(false);
		this.harmonicaSource.SetActive(false);
		base.StopCoroutine(this.Closeout());
		this.sources.SetActive(true);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003470 File Offset: 0x00001670
	public void Close()
	{
		this.bPlaying = false;
		this.extinguishSource.SetActive(true);
		this.extinguishSource.GetComponent<AudioSource>().Play();
		base.StartCoroutine(this.Closeout());
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000034A2 File Offset: 0x000016A2
	private IEnumerator Closeout()
	{
		yield return new WaitForSeconds(2f);
		foreach (MusicSource musicSource in SoundManager.MusicSources.Values)
		{
			float num;
			if (this.originalMusicVolumes.TryGetValue(musicSource.Channel, out num))
			{
				musicSource.TargetVolume = num;
			}
		}
		this.sources.SetActive(false);
		this.bPlaying = false;
		yield break;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000034B1 File Offset: 0x000016B1
	private void Start()
	{
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000034B4 File Offset: 0x000016B4
	private void Update()
	{
		if (!this.bPlaying)
		{
			return;
		}
		this.harmonicaStart += Time.deltaTime;
		if ((double)this.harmonicaStart > 0.25 && !this.harmonicaSource.activeSelf)
		{
			if (global::UnityEngine.Random.value <= 0.1f)
			{
				this.harmonicaSource.SetActive(true);
			}
			this.harmonicaStart = 0f;
		}
		this.flourishTimer += Time.deltaTime;
		if (this.flourishTimer > 1f)
		{
			this.flourishTimer = 0f;
			if (global::UnityEngine.Random.value <= 0.2f)
			{
				SoundManager.PlaySound(this.flourishes[global::UnityEngine.Random.Range(0, this.flourishes.Length - 1)], 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			}
		}
	}

	// Token: 0x0400002C RID: 44
	public GameObject sources;

	// Token: 0x0400002D RID: 45
	public GameObject harmonicaSource;

	// Token: 0x0400002E RID: 46
	public GameObject extinguishSource;

	// Token: 0x0400002F RID: 47
	public Dictionary<string, float> originalMusicVolumes = new Dictionary<string, float>();

	// Token: 0x04000030 RID: 48
	public bool bPlaying;

	// Token: 0x04000031 RID: 49
	private float flourishTimer;

	// Token: 0x04000032 RID: 50
	private float harmonicaStart;

	// Token: 0x04000033 RID: 51
	private string[] flourishes = new string[] { "Cooking_Chopping_1_N", "Cooking_Chopping_4_N", "Cooking_Chopping_6_N", "Cooking_Pan_1", "Cooking_Pan2" };
}
