using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000BE RID: 190
	[DisallowMultipleComponent]
	public sealed class AsyncPostRenderTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000C6C9 File Offset: 0x0000A8C9
		private void OnPostRender()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		public IAsyncOnPostRenderHandler GetOnPostRenderAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		public IAsyncOnPostRenderHandler GetOnPostRenderAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000C6E9 File Offset: 0x0000A8E9
		public UniTask OnPostRenderAsync()
		{
			return ((IAsyncOnPostRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPostRenderAsync();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
		public UniTask OnPostRenderAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPostRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPostRenderAsync();
		}
	}
}
