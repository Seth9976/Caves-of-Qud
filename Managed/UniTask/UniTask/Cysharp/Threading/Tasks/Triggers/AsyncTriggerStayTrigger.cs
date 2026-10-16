using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000DA RID: 218
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerStayTrigger : AsyncTriggerBase<Collider>
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x0000CA81 File Offset: 0x0000AC81
		private void OnTriggerStay(Collider other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000CA8A File Offset: 0x0000AC8A
		public IAsyncOnTriggerStayHandler GetOnTriggerStayAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider>(this, false);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000CA93 File Offset: 0x0000AC93
		public IAsyncOnTriggerStayHandler GetOnTriggerStayAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider>(this, cancellationToken, false);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000CA9D File Offset: 0x0000AC9D
		public UniTask<Collider> OnTriggerStayAsync()
		{
			return ((IAsyncOnTriggerStayHandler)new AsyncTriggerHandler<Collider>(this, true)).OnTriggerStayAsync();
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000CAAB File Offset: 0x0000ACAB
		public UniTask<Collider> OnTriggerStayAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerStayHandler)new AsyncTriggerHandler<Collider>(this, cancellationToken, true)).OnTriggerStayAsync();
		}
	}
}
