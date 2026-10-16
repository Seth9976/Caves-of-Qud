using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000277 RID: 631
	public static class AsyncOperationAwaitableExtensions
	{
		// Token: 0x06001FEE RID: 8174 RVA: 0x0003D8D0 File Offset: 0x0003BAD0
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable.Awaiter GetAwaiter(this AsyncOperation op)
		{
			return Awaitable.FromAsyncOperation(op, default(CancellationToken)).GetAwaiter();
		}
	}
}
