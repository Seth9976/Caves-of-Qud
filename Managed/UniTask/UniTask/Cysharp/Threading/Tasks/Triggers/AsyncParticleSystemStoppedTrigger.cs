using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000B8 RID: 184
	[DisallowMultipleComponent]
	public sealed class AsyncParticleSystemStoppedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0000C5FE File Offset: 0x0000A7FE
		private void OnParticleSystemStopped()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0000C60B File Offset: 0x0000A80B
		public IAsyncOnParticleSystemStoppedHandler GetOnParticleSystemStoppedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000C614 File Offset: 0x0000A814
		public IAsyncOnParticleSystemStoppedHandler GetOnParticleSystemStoppedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000C61E File Offset: 0x0000A81E
		public UniTask OnParticleSystemStoppedAsync()
		{
			return ((IAsyncOnParticleSystemStoppedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnParticleSystemStoppedAsync();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000C62C File Offset: 0x0000A82C
		public UniTask OnParticleSystemStoppedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleSystemStoppedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnParticleSystemStoppedAsync();
		}
	}
}
