using System;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200007D RID: 125
	public interface IAsyncOnApplicationPauseHandler
	{
		// Token: 0x06000411 RID: 1041
		UniTask<bool> OnApplicationPauseAsync();
	}
}
