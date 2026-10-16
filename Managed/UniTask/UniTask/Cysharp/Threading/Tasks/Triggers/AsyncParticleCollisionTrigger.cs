using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000B6 RID: 182
	[DisallowMultipleComponent]
	public sealed class AsyncParticleCollisionTrigger : AsyncTriggerBase<GameObject>
	{
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000C5BD File Offset: 0x0000A7BD
		private void OnParticleCollision(GameObject other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000C5C6 File Offset: 0x0000A7C6
		public IAsyncOnParticleCollisionHandler GetOnParticleCollisionAsyncHandler()
		{
			return new AsyncTriggerHandler<GameObject>(this, false);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000C5CF File Offset: 0x0000A7CF
		public IAsyncOnParticleCollisionHandler GetOnParticleCollisionAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<GameObject>(this, cancellationToken, false);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000C5D9 File Offset: 0x0000A7D9
		public UniTask<GameObject> OnParticleCollisionAsync()
		{
			return ((IAsyncOnParticleCollisionHandler)new AsyncTriggerHandler<GameObject>(this, true)).OnParticleCollisionAsync();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000C5E7 File Offset: 0x0000A7E7
		public UniTask<GameObject> OnParticleCollisionAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnParticleCollisionHandler)new AsyncTriggerHandler<GameObject>(this, cancellationToken, true)).OnParticleCollisionAsync();
		}
	}
}
