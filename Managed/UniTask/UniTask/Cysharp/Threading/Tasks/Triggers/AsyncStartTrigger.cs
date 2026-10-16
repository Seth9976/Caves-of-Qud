using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Triggers
{
	// Token: 0x0200006F RID: 111
	[DisallowMultipleComponent]
	public sealed class AsyncStartTrigger : AsyncTriggerBase<AsyncUnit>
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000B013 File Offset: 0x00009213
		private void Start()
		{
			this.called = true;
			base.RaiseEvent(AsyncUnit.Default);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000B027 File Offset: 0x00009227
		public UniTask StartAsync()
		{
			if (this.called)
			{
				return UniTask.CompletedTask;
			}
			return ((IAsyncOneShotTrigger)new AsyncTriggerHandler<AsyncUnit>(this, true)).OneShotAsync();
		}

		// Token: 0x040000EF RID: 239
		private bool called;
	}
}
