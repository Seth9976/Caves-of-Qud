using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200011F RID: 287
	internal class ListObserver<T> : IObserver<T>
	{
		// Token: 0x06000688 RID: 1672 RVA: 0x0000F823 File Offset: 0x0000DA23
		public ListObserver(ImmutableList<IObserver<T>> observers)
		{
			this._observers = observers;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000F834 File Offset: 0x0000DA34
		public void OnCompleted()
		{
			IObserver<T>[] data = this._observers.Data;
			for (int i = 0; i < data.Length; i++)
			{
				data[i].OnCompleted();
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000F864 File Offset: 0x0000DA64
		public void OnError(Exception error)
		{
			IObserver<T>[] data = this._observers.Data;
			for (int i = 0; i < data.Length; i++)
			{
				data[i].OnError(error);
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000F894 File Offset: 0x0000DA94
		public void OnNext(T value)
		{
			IObserver<T>[] data = this._observers.Data;
			for (int i = 0; i < data.Length; i++)
			{
				data[i].OnNext(value);
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		internal IObserver<T> Add(IObserver<T> observer)
		{
			return new ListObserver<T>(this._observers.Add(observer));
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		internal IObserver<T> Remove(IObserver<T> observer)
		{
			int num = Array.IndexOf<IObserver<T>>(this._observers.Data, observer);
			if (num < 0)
			{
				return this;
			}
			if (this._observers.Data.Length == 2)
			{
				return this._observers.Data[1 - num];
			}
			return new ListObserver<T>(this._observers.Remove(observer));
		}

		// Token: 0x04000155 RID: 341
		private readonly ImmutableList<IObserver<T>> _observers;
	}
}
