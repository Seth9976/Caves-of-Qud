using System;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000058 RID: 88
	internal static class UnityWebRequestUtils
	{
		// Token: 0x06000198 RID: 408 RVA: 0x00004198 File Offset: 0x00002398
		public static bool HasSucceeded(this UnityWebRequest self)
		{
			return self.result == UnityWebRequest.Result.Success;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000041A4 File Offset: 0x000023A4
		public static Task<string> GetTextAsync(string uri)
		{
			UnityWebRequestUtils.<>c__DisplayClass2_0 CS$<>8__locals1 = new UnityWebRequestUtils.<>c__DisplayClass2_0();
			CS$<>8__locals1.completionSource = new TaskCompletionSource<string>();
			UnityWebRequest.Get(uri).SendWebRequest().completed += CS$<>8__locals1.<GetTextAsync>g__CompleteFetchTaskOnRequestCompleted|0;
			return CS$<>8__locals1.completionSource.Task;
		}

		// Token: 0x04000079 RID: 121
		public const string JsonContentType = "application/json";
	}
}
