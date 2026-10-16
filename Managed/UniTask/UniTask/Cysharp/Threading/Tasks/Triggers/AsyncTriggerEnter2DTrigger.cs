using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000D4 RID: 212
	[DisallowMultipleComponent]
	public sealed class AsyncTriggerEnter2DTrigger : AsyncTriggerBase<Collider2D>
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0000C9BE File Offset: 0x0000ABBE
		private void OnTriggerEnter2D(Collider2D other)
		{
			base.RaiseEvent(other);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000C9C7 File Offset: 0x0000ABC7
		public IAsyncOnTriggerEnter2DHandler GetOnTriggerEnter2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Collider2D>(this, false);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000C9D0 File Offset: 0x0000ABD0
		public IAsyncOnTriggerEnter2DHandler GetOnTriggerEnter2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collider2D>(this, cancellationToken, false);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		public UniTask<Collider2D> OnTriggerEnter2DAsync()
		{
			return ((IAsyncOnTriggerEnter2DHandler)new AsyncTriggerHandler<Collider2D>(this, true)).OnTriggerEnter2DAsync();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		public UniTask<Collider2D> OnTriggerEnter2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnTriggerEnter2DHandler)new AsyncTriggerHandler<Collider2D>(this, cancellationToken, true)).OnTriggerEnter2DAsync();
		}
	}
}
