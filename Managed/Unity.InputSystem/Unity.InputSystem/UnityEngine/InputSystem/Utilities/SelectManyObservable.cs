using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000141 RID: 321
	internal class SelectManyObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x060011BE RID: 4542 RVA: 0x00054E42 File Offset: 0x00053042
		public SelectManyObservable(IObservable<TSource> source, Func<TSource, IEnumerable<TResult>> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00054E58 File Offset: 0x00053058
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectManyObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x0400072B RID: 1835
		private readonly IObservable<TSource> m_Source;

		// Token: 0x0400072C RID: 1836
		private readonly Func<TSource, IEnumerable<TResult>> m_Filter;

		// Token: 0x0200024E RID: 590
		private class Select : IObserver<TSource>
		{
			// Token: 0x06001665 RID: 5733 RVA: 0x00067312 File Offset: 0x00065512
			public Select(SelectManyObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x06001666 RID: 5734 RVA: 0x00067328 File Offset: 0x00065528
			public void OnCompleted()
			{
			}

			// Token: 0x06001667 RID: 5735 RVA: 0x0006732A File Offset: 0x0006552A
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x06001668 RID: 5736 RVA: 0x00067334 File Offset: 0x00065534
			public void OnNext(TSource evt)
			{
				foreach (TResult tresult in this.m_Observable.m_Filter(evt))
				{
					this.m_Observer.OnNext(tresult);
				}
			}

			// Token: 0x04000CA1 RID: 3233
			private SelectManyObservable<TSource, TResult> m_Observable;

			// Token: 0x04000CA2 RID: 3234
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
