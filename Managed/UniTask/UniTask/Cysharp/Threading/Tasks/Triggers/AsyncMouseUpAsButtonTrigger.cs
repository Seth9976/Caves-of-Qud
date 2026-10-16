using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000B4 RID: 180
	[DisallowMultipleComponent]
	public sealed class AsyncMouseUpAsButtonTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000C578 File Offset: 0x0000A778
		private void OnMouseUpAsButton()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000C585 File Offset: 0x0000A785
		public IAsyncOnMouseUpAsButtonHandler GetOnMouseUpAsButtonAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000C58E File Offset: 0x0000A78E
		public IAsyncOnMouseUpAsButtonHandler GetOnMouseUpAsButtonAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C598 File Offset: 0x0000A798
		public UniTask OnMouseUpAsButtonAsync()
		{
			return ((IAsyncOnMouseUpAsButtonHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnMouseUpAsButtonAsync();
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000C5A6 File Offset: 0x0000A7A6
		public UniTask OnMouseUpAsButtonAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnMouseUpAsButtonHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnMouseUpAsButtonAsync();
		}
	}
}
