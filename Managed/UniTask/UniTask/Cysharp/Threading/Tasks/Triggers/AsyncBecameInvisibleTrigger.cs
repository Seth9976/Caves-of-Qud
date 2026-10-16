using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000084 RID: 132
	[DisallowMultipleComponent]
	public sealed class AsyncBecameInvisibleTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x0000BF24 File Offset: 0x0000A124
		private void OnBecameInvisible()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000BF31 File Offset: 0x0000A131
		public IAsyncOnBecameInvisibleHandler GetOnBecameInvisibleAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000BF3A File Offset: 0x0000A13A
		public IAsyncOnBecameInvisibleHandler GetOnBecameInvisibleAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000BF44 File Offset: 0x0000A144
		public UniTask OnBecameInvisibleAsync()
		{
			return ((IAsyncOnBecameInvisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnBecameInvisibleAsync();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000BF52 File Offset: 0x0000A152
		public UniTask OnBecameInvisibleAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnBecameInvisibleHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnBecameInvisibleAsync();
		}
	}
}
