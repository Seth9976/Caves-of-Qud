using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200005D RID: 93
	public interface IAsyncValueChangedEventHandler<T> : IDisposable
	{
		// Token: 0x0600029B RID: 667
		UniTask<T> OnValueChangedAsync();
	}
}
