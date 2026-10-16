using System;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
	// Token: 0x02000A91 RID: 2705
	public interface IAsyncEnumerator<out T> : IAsyncDisposable
	{
		// Token: 0x06006110 RID: 24848
		ValueTask<bool> MoveNextAsync();

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06006111 RID: 24849
		T Current { get; }
	}
}
