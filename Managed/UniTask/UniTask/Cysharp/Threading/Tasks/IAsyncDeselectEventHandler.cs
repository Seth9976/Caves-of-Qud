using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000061 RID: 97
	public interface IAsyncDeselectEventHandler<T> : IDisposable
	{
		// Token: 0x0600029F RID: 671
		UniTask<T> OnDeselectAsync();
	}
}
