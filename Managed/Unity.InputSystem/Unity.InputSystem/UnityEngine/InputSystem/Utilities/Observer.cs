using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000140 RID: 320
	internal class Observer<TValue> : IObserver<TValue>
	{
		// Token: 0x060011BA RID: 4538 RVA: 0x00054DFF File Offset: 0x00052FFF
		public Observer(Action<TValue> onNext, Action onCompleted = null)
		{
			this.m_OnNext = onNext;
			this.m_OnCompleted = onCompleted;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00054E15 File Offset: 0x00053015
		public void OnCompleted()
		{
			Action onCompleted = this.m_OnCompleted;
			if (onCompleted == null)
			{
				return;
			}
			onCompleted();
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00054E27 File Offset: 0x00053027
		public void OnError(Exception error)
		{
			Debug.LogException(error);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00054E2F File Offset: 0x0005302F
		public void OnNext(TValue evt)
		{
			Action<TValue> onNext = this.m_OnNext;
			if (onNext == null)
			{
				return;
			}
			onNext(evt);
		}

		// Token: 0x04000729 RID: 1833
		private Action<TValue> m_OnNext;

		// Token: 0x0400072A RID: 1834
		private Action m_OnCompleted;
	}
}
