using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D2 RID: 210
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerEnterTrigger : AsyncTriggerBase<Collider>
	{
		// Token: 0x06000538 RID: 1336 RVA: 0x0000C97D File Offset: 0x0000AB7D
		private void OnTriggerEnter(Collider other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000C986 File Offset: 0x0000AB86
		public IAsyncOnTriggerEnterHandler GetOnTriggerEnterAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider>(this, false);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000C98F File Offset: 0x0000AB8F
		public IAsyncOnTriggerEnterHandler GetOnTriggerEnterAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000C999 File Offset: 0x0000AB99
		public UniTask<Collider> OnTriggerEnterAsync()
		{
			return ((IAsyncOnTriggerEnterHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerEnterAsync();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000C9A7 File Offset: 0x0000ABA7
		public UniTask<Collider> OnTriggerEnterAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerEnterHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerEnterAsync();
		}
	}
}
