using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000A2 RID: 162
	[DisallowMultipleComponent]
	public sealed class AsyncGUITrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0000C313 File Offset: 0x0000A513
		private void OnGUI()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000C320 File Offset: 0x0000A520
		public IAsyncOnGUIHandler GetOnGUIAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000C329 File Offset: 0x0000A529
		public IAsyncOnGUIHandler GetOnGUIAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000C333 File Offset: 0x0000A533
		public UniTask OnGUIAsync()
		{
			return ((IAsyncOnGUIHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnGUIAsync();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000C341 File Offset: 0x0000A541
		public UniTask OnGUIAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnGUIHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnGUIAsync();
		}
	}
}
