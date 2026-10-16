using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using XRL.Collections;

// Token: 0x0200015E RID: 350
public class AudioEntrySet : Rack<AudioEntry>
{
	// Token: 0x06000DED RID: 3565 RVA: 0x00059C14 File Offset: 0x00057E14
	public virtual void Initialize(string Path)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(Path);
		if (textAsset)
		{
			AudioEntrySet.Config config = JsonUtility.FromJson<AudioEntrySet.Config>(textAsset.text);
			if (config != null && config.Shuffle != -1)
			{
				this.Shuffle = config.Shuffle == 1;
			}
		}
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x00059C58 File Offset: 0x00057E58
	public int IndexOfVariant(int Variant)
	{
		for (int i = 0; i < this.Length; i++)
		{
			if (this.Items[i].Variant == Variant)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x00059C8C File Offset: 0x00057E8C
	public async UniTask Load()
	{
		if (!this.Initialized)
		{
			UniTask uniTask;
			if (AudioEntrySet.Tasks.TryGetValue(this, out uniTask))
			{
				await uniTask;
			}
			else
			{
				UniTaskCompletionSource source = new UniTaskCompletionSource();
				AudioEntrySet.Tasks.Add(this, source.Task);
				for (int i = 0; i < this.Length; i++)
				{
					(await this.Items[i].GetClip()).LoadAudioData();
				}
				this.Initialized = true;
				source.TrySetResult();
				AudioEntrySet.Tasks.Remove(this);
			}
		}
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x00059CD0 File Offset: 0x00057ED0
	public void Unload()
	{
		for (int i = 0; i < this.Length; i++)
		{
			this.Items[i].Unload();
		}
		this.Initialized = false;
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x00059D04 File Offset: 0x00057F04
	public AudioEntry Next()
	{
		int length = this.Length;
		if (length == 0)
		{
			return null;
		}
		AudioEntry audioEntry = this.Items[this.Index];
		if (this.Index < length - 1)
		{
			this.Index++;
			return audioEntry;
		}
		if (this.Shuffle && length > 1)
		{
			base.ShuffleInPlace(AudioEntrySet.ClipShuffler);
			this.Index = 0;
		}
		return audioEntry;
	}

	// Token: 0x0400099E RID: 2462
	public static Dictionary<AudioEntrySet, UniTask> Tasks = new Dictionary<AudioEntrySet, UniTask>();

	// Token: 0x0400099F RID: 2463
	public int Index;

	// Token: 0x040009A0 RID: 2464
	public bool Shuffle = true;

	// Token: 0x040009A1 RID: 2465
	public bool Initialized;

	// Token: 0x040009A2 RID: 2466
	private static global::System.Random ClipShuffler = new global::System.Random();

	// Token: 0x02001605 RID: 5637
	public class Config
	{
		// Token: 0x040058B5 RID: 22709
		public int Shuffle = -1;
	}
}
