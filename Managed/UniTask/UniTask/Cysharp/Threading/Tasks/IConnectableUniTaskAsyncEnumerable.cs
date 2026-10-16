using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001E RID: 30
	public interface IConnectableUniTaskAsyncEnumerable<out T> : IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x0600007A RID: 122
		IDisposable Connect();
	}
}
