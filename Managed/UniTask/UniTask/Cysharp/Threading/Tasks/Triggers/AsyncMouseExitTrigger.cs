using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000AE RID: 174
	[DisallowMultipleComponent]
	public sealed class AsyncMouseExitTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000C4A9 File Offset: 0x0000A6A9
		private void OnMouseExit()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000C4B6 File Offset: 0x0000A6B6
		public IAsyncOnMouseExitHandler GetOnMouseExitAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000C4BF File Offset: 0x0000A6BF
		public IAsyncOnMouseExitHandler GetOnMouseExitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C4C9 File Offset: 0x0000A6C9
		public UniTask OnMouseExitAsync()
		{
			return ((IAsyncOnMouseExitHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseExitAsync();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		public UniTask OnMouseExitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseExitHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseExitAsync();
		}
	}
}
