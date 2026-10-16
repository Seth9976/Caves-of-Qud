using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200001F RID: 31
	public static class UniTaskAsyncEnumerableExtensions
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002CD8 File Offset: 0x00000ED8
		public static UniTaskCancelableAsyncEnumerable<T> WithCancellation<T>(this IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new UniTaskCancelableAsyncEnumerable<T>(source, cancellationToken);
		}
	}
}
