using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001B RID: 27
	public interface IUniTaskAsyncEnumerator<out T> : IUniTaskAsyncDisposable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000074 RID: 116
		T Current { get; }

		// Token: 0x06000075 RID: 117
		UniTask<bool> MoveNextAsync();
	}
}
