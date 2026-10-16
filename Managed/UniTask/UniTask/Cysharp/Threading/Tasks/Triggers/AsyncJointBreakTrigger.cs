using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000A4 RID: 164
	[DisallowMultipleComponent]
	public sealed class AsyncJointBreakTrigger : AsyncTriggerBase<float>
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x0000C358 File Offset: 0x0000A558
		private void OnJointBreak(float breakForce)
		{
			base.RaiseEvent(breakForce);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000C361 File Offset: 0x0000A561
		public IAsyncOnJointBreakHandler GetOnJointBreakAsyncHandler()
		{
			return new AsyncTriggerHandler<float>(this, false);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000C36A File Offset: 0x0000A56A
		public IAsyncOnJointBreakHandler GetOnJointBreakAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<float>(this, cancellationToken, false);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000C374 File Offset: 0x0000A574
		public UniTask<float> OnJointBreakAsync()
		{
			return ((IAsyncOnJointBreakHandler)new AsyncTriggerHandler<float>(this, true)).OnJointBreakAsync();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0000C382 File Offset: 0x0000A582
		public UniTask<float> OnJointBreakAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnJointBreakHandler)new AsyncTriggerHandler<float>(this, cancellationToken, true)).OnJointBreakAsync();
		}
	}
}
