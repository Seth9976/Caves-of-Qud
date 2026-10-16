using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001A RID: 26
	public interface IUniTaskAsyncEnumerable<out T>
	{
		// Token: 0x06000073 RID: 115
		IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
}
