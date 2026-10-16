using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200005F RID: 95
	public interface IAsyncEndTextSelectionEventHandler<T> : IDisposable
	{
		// Token: 0x0600029D RID: 669
		UniTask<T> OnEndTextSelectionAsync();
	}
}
