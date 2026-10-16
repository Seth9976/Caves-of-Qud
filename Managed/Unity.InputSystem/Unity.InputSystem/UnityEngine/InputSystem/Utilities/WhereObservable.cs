using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000143 RID: 323
	internal class WhereObservable<TValue> : IObservable<TValue>
	{
		// Token: 0x060011C2 RID: 4546 RVA: 0x00054E96 File Offset: 0x00053096
		public WhereObservable(IObservable<TValue> source, Func<TValue, bool> predicate)
		{
			this.m_Source = source;
			this.m_Predicate = predicate;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00054EAC File Offset: 0x000530AC
		public IDisposable Subscribe(IObserver<TValue> observer)
		{
			return this.m_Source.Subscribe(new WhereObservable<TValue>.Where(this, observer));
		}

		// Token: 0x0400072F RID: 1839
		private readonly IObservable<TValue> m_Source;

		// Token: 0x04000730 RID: 1840
		private readonly Func<TValue, bool> m_Predicate;

		// Token: 0x02000250 RID: 592
		private class Where : IObserver<TValue>
		{
			// Token: 0x0600166D RID: 5741 RVA: 0x000673F9 File Offset: 0x000655F9
			public Where(WhereObservable<TValue> observable, IObserver<TValue> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x0600166E RID: 5742 RVA: 0x0006740F File Offset: 0x0006560F
			public void OnCompleted()
			{
			}

			// Token: 0x0600166F RID: 5743 RVA: 0x00067411 File Offset: 0x00065611
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x06001670 RID: 5744 RVA: 0x00067419 File Offset: 0x00065619
			public void OnNext(TValue evt)
			{
				if (this.m_Observable.m_Predicate(evt))
				{
					this.m_Observer.OnNext(evt);
				}
			}

			// Token: 0x04000CA5 RID: 3237
			private WhereObservable<TValue> m_Observable;

			// Token: 0x04000CA6 RID: 3238
			private readonly IObserver<TValue> m_Observer;
		}
	}
}
