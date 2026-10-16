using System;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000033 RID: 51
	internal interface IAsyncOperationAwaiter<out T> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C3 RID: 195
		bool IsCompleted { get; }

		// Token: 0x060000C4 RID: 196
		T GetResult();
	}
}
