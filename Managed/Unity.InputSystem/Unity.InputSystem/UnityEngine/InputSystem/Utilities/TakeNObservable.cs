using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000142 RID: 322
	internal class TakeNObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x060011C0 RID: 4544 RVA: 0x00054E6C File Offset: 0x0005306C
		public TakeNObservable(IObservable<TValue> source, int count)
		{
			this.m_Source = source;
			this.m_Count = count;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00054E82 File Offset: 0x00053082
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new TakeNObservable<TValue>.Take(this, observer));
		}

		// Token: 0x0400072D RID: 1837
		private IObservable<TValue> m_Source;

		// Token: 0x0400072E RID: 1838
		private int m_Count;

		// Token: 0x0200024F RID: 591
		private class Take : IObserver<TValue>
		{
			// Token: 0x06001669 RID: 5737 RVA: 0x00067394 File Offset: 0x00065594
			public Take(TakeNObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observer = observer;
				this.m_Remaining = observable.m_Count;
			}

			// Token: 0x0600166A RID: 5738 RVA: 0x000673AF File Offset: 0x000655AF
			public void OnCompleted()
			{
			}

			// Token: 0x0600166B RID: 5739 RVA: 0x000673B1 File Offset: 0x000655B1
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x0600166C RID: 5740 RVA: 0x000673B9 File Offset: 0x000655B9
			public void OnNext(TValue evt)
			{
				if (this.m_Remaining <= 0)
				{
					return;
				}
				this.m_Remaining--;
				this.m_Observer.OnNext(evt);
				if (this.m_Remaining == 0)
				{
					this.m_Observer.OnCompleted();
					this.m_Observer = null;
				}
			}

			// Token: 0x04000CA3 RID: 3235
			private IObserver<TValue> m_Observer;

			// Token: 0x04000CA4 RID: 3236
			private int m_Remaining;
		}
	}
}
