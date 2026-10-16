using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200008A RID: 138
	[DisallowMultipleComponent]
	public sealed class AsyncOnCanvasGroupChangedTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x0000BFF3 File Offset: 0x0000A1F3
		private void OnCanvasGroupChanged()
		{
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000C000 File Offset: 0x0000A200
		public IAsyncOnCanvasGroupChangedHandler GetOnCanvasGroupChangedAsyncHandler()
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, false);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C009 File Offset: 0x0000A209
		public IAsyncOnCanvasGroupChangedHandler GetOnCanvasGroupChangedAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, false);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C013 File Offset: 0x0000A213
		public UniTask OnCanvasGroupChangedAsync()
		{
			return ((IAsyncOnCanvasGroupChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, true)).OnCanvasGroupChangedAsync();
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000C021 File Offset: 0x0000A221
		public UniTask OnCanvasGroupChangedAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCanvasGroupChangedHandler)new AsyncTriggerHandler<AsyncUnit>(this, cancellationToken, true)).OnCanvasGroupChangedAsync();
		}
	}
}
