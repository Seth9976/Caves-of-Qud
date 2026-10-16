using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000E4 RID: 228
	[DisallowMultipleComponent]
	public sealed class AsyncUpdateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x0000CBD2 File Offset: 0x0000ADD2
		private void Update()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000CBDF File Offset: 0x0000ADDF
		public IAsyncUpdateHandler GetUpdateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public IAsyncUpdateHandler GetUpdateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		public UniTask UpdateAsync()
		{
			return ((IAsyncUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).UpdateAsync();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public UniTask UpdateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).UpdateAsync();
		}
	}
}
