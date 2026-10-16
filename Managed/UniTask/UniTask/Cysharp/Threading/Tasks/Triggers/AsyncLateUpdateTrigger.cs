using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000076 RID: 118
	[DisallowMultipleComponent]
	public sealed class AsyncLateUpdateTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000BD4B File Offset: 0x00009F4B
		private void LateUpdate()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000BD58 File Offset: 0x00009F58
		public IAsyncLateUpdateHandler GetLateUpdateAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000BD61 File Offset: 0x00009F61
		public IAsyncLateUpdateHandler GetLateUpdateAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000BD6B File Offset: 0x00009F6B
		public UniTask LateUpdateAsync()
		{
			return ((IAsyncLateUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).LateUpdateAsync();
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000BD79 File Offset: 0x00009F79
		public UniTask LateUpdateAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncLateUpdateHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).LateUpdateAsync();
		}
	}
}
