using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000BA RID: 186
	[DisallowMultipleComponent]
	public sealed class AsyncParticleTriggerTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000C643 File Offset: 0x0000A843
		private void OnParticleTrigger()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000C650 File Offset: 0x0000A850
		public IAsyncOnParticleTriggerHandler GetOnParticleTriggerAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000C659 File Offset: 0x0000A859
		public IAsyncOnParticleTriggerHandler GetOnParticleTriggerAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0000C663 File Offset: 0x0000A863
		public UniTask OnParticleTriggerAsync()
		{
			return ((IAsyncOnParticleTriggerHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnParticleTriggerAsync();
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0000C671 File Offset: 0x0000A871
		public UniTask OnParticleTriggerAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleTriggerHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnParticleTriggerAsync();
		}
	}
}
