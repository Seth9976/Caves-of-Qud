using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x0200015D RID: 349
public class ModAudioEntry : AudioEntry
{
	// Token: 0x06000DEA RID: 3562 RVA: 0x00059B84 File Offset: 0x00057D84
	public async UniTask<AudioClip> SendURIRequest()
	{
		string text = this.Path;
		AudioClip audioClip;
		if (text.IsNullOrEmpty())
		{
			audioClip = null;
		}
		else
		{
			AudioType audioTypeFromFile = SoundManager.GetAudioTypeFromFile(text);
			if (!text.StartsWith("file://", StringComparison.Ordinal))
			{
				text = ((text[0] == '/') ? "file://" : "file:///") + text;
			}
			using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(text, audioTypeFromFile))
			{
				await www.SendWebRequest();
				if (www.result != UnityWebRequest.Result.ConnectionError)
				{
					return DownloadHandlerAudioClip.GetContent(www);
				}
				MetricsManager.LogError(www.error);
			}
			UnityWebRequest www = null;
			audioClip = null;
		}
		return audioClip;
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x00059BC8 File Offset: 0x00057DC8
	public override async UniTask<AudioClip> GetClip()
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
			this.Clip = await this.SendURIRequest();
			source.TrySetResult(this.Clip);
			AudioEntry.Tasks.Remove(this);
			audioClip = this.Clip;
		}
		return audioClip;
	}
}
