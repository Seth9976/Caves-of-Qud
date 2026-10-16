using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000C2 RID: 194
	[DisallowMultipleComponent]
	public sealed class AsyncPreRenderTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000500 RID: 1280 RVA: 0x0000C753 File Offset: 0x0000A953
		private void OnPreRender()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000C760 File Offset: 0x0000A960
		public IAsyncOnPreRenderHandler GetOnPreRenderAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000C769 File Offset: 0x0000A969
		public IAsyncOnPreRenderHandler GetOnPreRenderAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000C773 File Offset: 0x0000A973
		public UniTask OnPreRenderAsync()
		{
			return ((IAsyncOnPreRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnPreRenderAsync();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000C781 File Offset: 0x0000A981
		public UniTask OnPreRenderAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnPreRenderHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnPreRenderAsync();
		}
	}
}
