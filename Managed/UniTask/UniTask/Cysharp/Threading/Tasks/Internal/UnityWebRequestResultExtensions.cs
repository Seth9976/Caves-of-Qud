using System;
using UnityEngine.Networking;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000119 RID: 281
	internal static class UnityWebRequestResultExtensions
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static bool IsError(this UnityWebRequest unityWebRequest)
		{
			UnityWebRequest.Result result = unityWebRequest.result;
			return result == UnityWebRequest.Result.ConnectionError || result == UnityWebRequest.Result.DataProcessingError || result == UnityWebRequest.Result.ProtocolError;
		}
	}
}
