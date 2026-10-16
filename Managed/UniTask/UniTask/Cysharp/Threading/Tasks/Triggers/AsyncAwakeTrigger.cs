using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200006D RID: 109
	[DisallowMultipleComponent]
	public sealed class AsyncAwakeTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000AF2B File Offset: 0x0000912B
		public UniTask AwakeAsync()
		{
			if (this.calledAwake)
			{
				return UniTask.CompletedTask;
			}
			return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(this, true)).OneShotAsync();
		}
	}
}
