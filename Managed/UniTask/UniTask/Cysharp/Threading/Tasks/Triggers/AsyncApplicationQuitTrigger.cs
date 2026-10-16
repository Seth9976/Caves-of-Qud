using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000080 RID: 128
	[DisallowMultipleComponent]
	public sealed class AsyncApplicationQuitTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x0000BE98 File Offset: 0x0000A098
		private void OnApplicationQuit()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000BEA5 File Offset: 0x0000A0A5
		public IAsyncOnApplicationQuitHandler GetOnApplicationQuitAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		public IAsyncOnApplicationQuitHandler GetOnApplicationQuitAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public UniTask OnApplicationQuitAsync()
		{
			return ((IAsyncOnApplicationQuitHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnApplicationQuitAsync();
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000BEC6 File Offset: 0x0000A0C6
		public UniTask OnApplicationQuitAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnApplicationQuitHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnApplicationQuitAsync();
		}
	}
}
