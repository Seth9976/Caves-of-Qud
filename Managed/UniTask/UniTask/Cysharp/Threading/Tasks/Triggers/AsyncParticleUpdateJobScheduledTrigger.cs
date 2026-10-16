using System;
using System.Threading;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000BC RID: 188
	[DisallowMultipleComponent]
	public sealed class AsyncParticleUpdateJobScheduledTrigger : AsyncTriggerBase<ParticleSystemJobData>
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x0000C688 File Offset: 0x0000A888
		private void OnParticleUpdateJobScheduled(ParticleSystemJobData particles)
		{
			base.RaiseEvent(particles);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000C691 File Offset: 0x0000A891
		public IAsyncOnParticleUpdateJobScheduledHandler GetOnParticleUpdateJobScheduledAsyncHandler()
		{
			return new AsyncTriggerHandler<ParticleSystemJobData>(this, false);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0000C69A File Offset: 0x0000A89A
		public IAsyncOnParticleUpdateJobScheduledHandler GetOnParticleUpdateJobScheduledAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<ParticleSystemJobData>(this, cancellationToken, false);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public UniTask<ParticleSystemJobData> OnParticleUpdateJobScheduledAsync()
		{
			return ((IAsyncOnParticleUpdateJobScheduledHandler)new AsyncTriggerHandler<ParticleSystemJobData>(this, true)).OnParticleUpdateJobScheduledAsync();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000C6B2 File Offset: 0x0000A8B2
		public UniTask<ParticleSystemJobData> OnParticleUpdateJobScheduledAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleUpdateJobScheduledHandler)new AsyncTriggerHandler<ParticleSystemJobData>(this, cancellationToken, true)).OnParticleUpdateJobScheduledAsync();
		}
	}
}
