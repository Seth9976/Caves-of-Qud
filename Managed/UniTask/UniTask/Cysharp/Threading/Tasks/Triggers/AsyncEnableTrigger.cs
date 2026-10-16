using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000A0 RID: 160
	[DisallowMultipleComponent]
	public sealed class AsyncEnableTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0000C2CE File Offset: 0x0000A4CE
		private void OnEnable()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000C2DB File Offset: 0x0000A4DB
		public IAsyncOnEnableHandler GetOnEnableAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public IAsyncOnEnableHandler GetOnEnableAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000C2EE File Offset: 0x0000A4EE
		public UniTask OnEnableAsync()
		{
			return ((IAsyncOnEnableHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnEnableAsync();
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		public UniTask OnEnableAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnEnableHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnEnableAsync();
		}
	}
}
