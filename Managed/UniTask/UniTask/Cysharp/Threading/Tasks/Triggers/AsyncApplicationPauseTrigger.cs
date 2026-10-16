using System;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200007E RID: 126
	[DisallowMultipleComponent]
	public sealed class AsyncApplicationPauseTrigger : AsyncTriggerBase<bool>
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000BE57 File Offset: 0x0000A057
		private void OnApplicationPause(bool pauseStatus)
		{
			base.RaiseEvent(pauseStatus);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000BE60 File Offset: 0x0000A060
		public IAsyncOnApplicationPauseHandler GetOnApplicationPauseAsyncHandler()
		{
			return new AsyncTriggerHandler<bool>(this, false);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000BE69 File Offset: 0x0000A069
		public IAsyncOnApplicationPauseHandler GetOnApplicationPauseAsyncHandler(CancellationToken cancellationToken)
		{
			return new AsyncTriggerHandler<bool>(this, cancellationToken, false);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000BE73 File Offset: 0x0000A073
		public UniTask<bool> OnApplicationPauseAsync()
		{
			return ((IAsyncOnApplicationPauseHandler)new AsyncTriggerHandler<bool>(this, true)).OnApplicationPauseAsync();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BE81 File Offset: 0x0000A081
		public UniTask<bool> OnApplicationPauseAsync(CancellationToken cancellationToken)
		{
			return ((IAsyncOnApplicationPauseHandler)new AsyncTriggerHandler<bool>(this, cancellationToken, true)).OnApplicationPauseAsync();
		}
	}
}
