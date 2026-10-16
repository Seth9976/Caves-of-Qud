using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x020000A6 RID: 166
	[DisallowMultipleComponent]
	public sealed class AsyncJointBreak2DTrigger : AsyncTriggerBase<Joint2D>
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0000C399 File Offset: 0x0000A599
		private void OnJointBreak2D(Joint2D brokenJoint)
		{
			base.RaiseEvent(brokenJoint);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
		public IAsyncOnJointBreak2DHandler GetOnJointBreak2DAsyncHandler()
		{
			return new AsyncTriggerHandler<Joint2D>(this, false);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C3AB File Offset: 0x0000A5AB
		public IAsyncOnJointBreak2DHandler GetOnJointBreak2DAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Joint2D>(this, cancellationToken, false);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0000C3B5 File Offset: 0x0000A5B5
		public UniTask<Joint2D> OnJointBreak2DAsync()
		{
			return ((IAsyncOnJointBreak2DHandler)new AsyncTriggerHandler<Joint2D>(this, true)).OnJointBreak2DAsync();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0000C3C3 File Offset: 0x0000A5C3
		public UniTask<Joint2D> OnJointBreak2DAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnJointBreak2DHandler)new AsyncTriggerHandler<Joint2D>(this, cancellationToken, true)).OnJointBreak2DAsync();
		}
	}
}
