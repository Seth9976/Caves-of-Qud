using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// Token: 0x0200015C RID: 348
public class AudioEntry
{
	// Token: 0x06000DE6 RID: 3558 RVA: 0x00059B16 File Offset: 0x00057D16
	public virtual void Unload()
	{
		Resources.UnloadAsset(this.Clip);
		this.Clip = null;
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x00059B2C File Offset: 0x00057D2C
	public virtual async UniTask<AudioClip> GetClip()
	{
		AudioClip audioClip;
		UniTask<global::UnityEngine.Object> uniTask;
		if (this.Clip != null)
		{
			audioClip = this.Clip;
		}
		else if (AudioEntry.Tasks.TryGetValue(this, out uniTask))
		{
			UniTask<global::UnityEngine.Object>.Awaiter awaiter = uniTask.GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				UniTask<global::UnityEngine.Object>.Awaiter awaiter2;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<global::UnityEngine.Object>.Awaiter);
			}
			audioClip = (AudioClip)awaiter.GetResult();
		}
		else
		{
			UniTaskCompletionSource<global::UnityEngine.Object> source = new UniTaskCompletionSource<global::UnityEngine.Object>();
			AudioEntry.Tasks.Add(this, source.Task);
			this.Clip = (AudioClip)(await Resources.LoadAsync<AudioClip>(this.Path));
			source.TrySetResult(this.Clip);
			AudioEntry.Tasks.Remove(this);
			audioClip = this.Clip;
		}
		return audioClip;
	}

	// Token: 0x0400099A RID: 2458
	public static Dictionary<AudioEntry, UniTask<global::UnityEngine.Object>> Tasks = new Dictionary<AudioEntry, UniTask<global::UnityEngine.Object>>();

	// Token: 0x0400099B RID: 2459
	public AudioClip Clip;

	// Token: 0x0400099C RID: 2460
	public string Path;

	// Token: 0x0400099D RID: 2461
	public int Variant;
}
