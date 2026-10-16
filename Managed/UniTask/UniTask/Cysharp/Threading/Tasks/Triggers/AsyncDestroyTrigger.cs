using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200006E RID: 110
	[DisallowMultipleComponent]
	public sealed class AsyncDestroyTrigger : MonoBehaviour
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000AF4F File Offset: 0x0000914F
		public CancellationToken CancellationToken
		{
			get
			{
				if (this.cancellationTokenSource == null)
				{
					this.cancellationTokenSource = new CancellationTokenSource();
					if (!this.awakeCalled)
					{
						PlayerLoopHelper.AddAction(PlayerLoopTiming.Update, new AsyncDestroyTrigger.AwakeMonitor(this));
					}
				}
				return this.cancellationTokenSource.Token;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000AF83 File Offset: 0x00009183
		private void Awake()
		{
			this.awakeCalled = true;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000AF8C File Offset: 0x0000918C
		private void OnDestroy()
		{
			this.called = true;
			CancellationTokenSource cancellationTokenSource = this.cancellationTokenSource;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			CancellationTokenSource cancellationTokenSource2 = this.cancellationTokenSource;
			if (cancellationTokenSource2 == null)
			{
				return;
			}
			cancellationTokenSource2.Dispose();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public UniTask OnDestroyAsync()
		{
			if (this.called)
			{
				return UniTask.CompletedTask;
			}
			UniTaskCompletionSource uniTaskCompletionSource = new UniTaskCompletionSource();
			this.CancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
			{
				((UniTaskCompletionSource)state).TrySetResult();
			}, uniTaskCompletionSource);
			return uniTaskCompletionSource.Task;
		}

		// Token: 0x040000EC RID: 236
		private bool awakeCalled;

		// Token: 0x040000ED RID: 237
		private bool called;

		// Token: 0x040000EE RID: 238
		private CancellationTokenSource cancellationTokenSource;

		// Token: 0x02000209 RID: 521
		private class AwakeMonitor : IPlayerLoopItem
		{
			// Token: 0x06000BB5 RID: 2997 RVA: 0x0002AA46 File Offset: 0x00028C46
			public AwakeMonitor(AsyncDestroyTrigger trigger)
			{
				this.trigger = trigger;
			}

			// Token: 0x06000BB6 RID: 2998 RVA: 0x0002AA55 File Offset: 0x00028C55
			public bool MoveNext()
			{
				if (this.trigger.called || this.trigger.awakeCalled)
				{
					return false;
				}
				if (this.trigger == null)
				{
					this.trigger.OnDestroy();
					return false;
				}
				return true;
			}

			// Token: 0x04000542 RID: 1346
			private readonly AsyncDestroyTrigger trigger;
		}
	}
}
