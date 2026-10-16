using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000031 RID: 49
	internal interface IAsyncOperation<out T> : IEnumerator
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000BB RID: 187
		bool IsDone { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000BC RID: 188
		AsyncOperationStatus Status { get; }

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000BD RID: 189
		// (remove) Token: 0x060000BE RID: 190
		event Action<IAsyncOperation<T>> Completed;

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000BF RID: 191
		Exception Exception { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C0 RID: 192
		T Result { get; }
	}
}
