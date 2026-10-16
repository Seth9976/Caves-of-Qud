using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x02000094 RID: 148
	[DisallowMultipleComponent]
	public sealed class AsyncCollisionStayTrigger : AsyncTriggerBase<Collision>
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0000C13C File Offset: 0x0000A33C
		private void OnCollisionStay(Collision coll)
		{
			base.RaiseEvent(coll);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000C145 File Offset: 0x0000A345
		public IAsyncOnCollisionStayHandler GetOnCollisionStayAsyncHandler()
		{
			return new AsyncTriggerHandler<Collision>(this, false);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000C14E File Offset: 0x0000A34E
		public IAsyncOnCollisionStayHandler GetOnCollisionStayAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<Collision>(this, cancellationToken, false);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000C158 File Offset: 0x0000A358
		public UniTask<Collision> OnCollisionStayAsync()
		{
			return ((IAsyncOnCollisionStayHandler)new AsyncTriggerHandler<Collision>(this, true)).OnCollisionStayAsync();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000C166 File Offset: 0x0000A366
		public UniTask<Collision> OnCollisionStayAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnCollisionStayHandler)new AsyncTriggerHandler<Collision>(this, cancellationToken, true)).OnCollisionStayAsync();
		}
	}
}
