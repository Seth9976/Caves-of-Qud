using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Core.Configuration
{
	// Token: 0x0200000D RID: 13
	internal static class StreamingAssetsUtils
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000024D8 File Offset: 0x000006D8
		public static Task<string> GetFileTextFromStreamingAssetsAsync(string path)
		{
			string text = Path.Combine(Application.streamingAssetsPath, path);
			TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
			try
			{
				string text2 = File.ReadAllText(text);
				taskCompletionSource.SetResult(text2);
			}
			catch (Exception ex)
			{
				taskCompletionSource.SetException(ex);
			}
			return taskCompletionSource.Task;
		}
	}
}
