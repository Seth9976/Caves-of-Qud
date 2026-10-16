using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000C8 RID: 200
	[TupleElementNames(new string[] { "source", "destination" })]
	[DisallowMultipleComponent]
	public sealed class AsyncRenderImageTrigger : AsyncTriggerBase<ValueTuple<RenderTexture, RenderTexture>>
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x0000C822 File Offset: 0x0000AA22
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			base.RaiseEvent(new ValueTuple<RenderTexture, RenderTexture>(source, destination));
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000C831 File Offset: 0x0000AA31
		public IAsyncOnRenderImageHandler GetOnRenderImageAsyncHandler()
		{
			return new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, false);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000C83A File Offset: 0x0000AA3A
		public IAsyncOnRenderImageHandler GetOnRenderImageAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, cancellationToken, false);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000C844 File Offset: 0x0000AA44
		[return: TupleElementNames(new string[] { "source", "destination" })]
		public UniTask<ValueTuple<RenderTexture, RenderTexture>> OnRenderImageAsync()
		{
			return ((IAsyncOnRenderImageHandler)new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, true)).OnRenderImageAsync();
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000C852 File Offset: 0x0000AA52
		[return: TupleElementNames(new string[] { "source", "destination" })]
		public UniTask<ValueTuple<RenderTexture, RenderTexture>> OnRenderImageAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnRenderImageHandler)new AsyncTriggerHandler<ValueTuple<RenderTexture, RenderTexture>>(this, cancellationToken, true)).OnRenderImageAsync();
		}
	}
}
