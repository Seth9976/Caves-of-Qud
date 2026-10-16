using System;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200007B RID: 123
	public interface IAsyncOnApplicationFocusHandler
	{
		// Token: 0x0600040A RID: 1034
		UniTask<bool> OnApplicationFocusAsync();
	}
}
