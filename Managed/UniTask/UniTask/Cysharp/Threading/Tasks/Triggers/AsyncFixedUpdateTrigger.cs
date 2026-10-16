using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000074 RID: 116
	[DisallowMultipleComponent]
	public sealed class AsyncFixedUpdateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000BD06 File Offset: 0x00009F06
		private void FixedUpdate()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000BD13 File Offset: 0x00009F13
		public IAsyncFixedUpdateHandler GetFixedUpdateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000BD1C File Offset: 0x00009F1C
		public IAsyncFixedUpdateHandler GetFixedUpdateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000BD26 File Offset: 0x00009F26
		public UniTask FixedUpdateAsync()
		{
			return ((IAsyncFixedUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).FixedUpdateAsync();
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000BD34 File Offset: 0x00009F34
		public UniTask FixedUpdateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncFixedUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).FixedUpdateAsync();
		}
	}
}
