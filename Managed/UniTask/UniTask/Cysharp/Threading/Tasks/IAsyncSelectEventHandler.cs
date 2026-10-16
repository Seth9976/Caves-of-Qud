using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000062 RID: 98
	public interface IAsyncSelectEventHandler<T> : IDisposable
	{
		// Token: 0x060002A0 RID: 672
		UniTask<T> OnSelectAsync();
	}
}
