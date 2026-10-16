using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000060 RID: 96
	public interface IAsyncTextSelectionEventHandler<T> : IDisposable
	{
		// Token: 0x0600029E RID: 670
		UniTask<T> OnTextSelectionAsync();
	}
}
