using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000030 RID: 48
	internal interface IAsyncOperation : IEnumerator
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B6 RID: 182
		bool IsDone { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B7 RID: 183
		AsyncOperationStatus Status { get; }

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000B8 RID: 184
		// (remove) Token: 0x060000B9 RID: 185
		event Action<IAsyncOperation> Completed;

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000BA RID: 186
		Exception Exception { get; }
	}
}
