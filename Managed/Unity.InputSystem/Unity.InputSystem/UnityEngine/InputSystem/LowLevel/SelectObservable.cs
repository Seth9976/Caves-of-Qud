using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000FF RID: 255
	internal class SelectObservable<TSource, TResult> : IObservable<TResult>
	{
		// Token: 0x06000F1F RID: 3871 RVA: 0x0004A6B3 File Offset: 0x000488B3
		public SelectObservable(IObservable<TSource> source, Func<TSource, TResult> filter)
		{
			this.m_Source = source;
			this.m_Filter = filter;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0004A6C9 File Offset: 0x000488C9
		public IDisposable Subscribe(IObserver<TResult> observer)
		{
			return this.m_Source.Subscribe(new SelectObservable<TSource, TResult>.Select(this, observer));
		}

		// Token: 0x04000652 RID: 1618
		private readonly IObservable<TSource> m_Source;

		// Token: 0x04000653 RID: 1619
		private readonly Func<TSource, TResult> m_Filter;

		// Token: 0x0200022A RID: 554
		private class Select : IObserver<TSource>
		{
			// Token: 0x06001594 RID: 5524 RVA: 0x00064161 File Offset: 0x00062361
			public Select(SelectObservable<TSource, TResult> observable, IObserver<TResult> observer)
			{
				this.m_Observable = observable;
				this.m_Observer = observer;
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x00064177 File Offset: 0x00062377
			public void OnCompleted()
			{
			}

			// Token: 0x06001596 RID: 5526 RVA: 0x00064179 File Offset: 0x00062379
			public void OnError(Exception error)
			{
				Debug.LogException(error);
			}

			// Token: 0x06001597 RID: 5527 RVA: 0x00064184 File Offset: 0x00062384
			public void OnNext(TSource evt)
			{
				TResult tresult = this.m_Observable.m_Filter(evt);
				this.m_Observer.OnNext(tresult);
			}

			// Token: 0x04000BE1 RID: 3041
			private SelectObservable<TSource, TResult> m_Observable;

			// Token: 0x04000BE2 RID: 3042
			private readonly IObserver<TResult> m_Observer;
		}
	}
}
