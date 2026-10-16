using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000122 RID: 290
	internal class DisposedObserver<T> : IObserver<T>
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0000F96D File Offset: 0x0000DB6D
		private DisposedObserver()
		{
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000F975 File Offset: 0x0000DB75
		public void OnCompleted()
		{
			throw new ObjectDisposedException("");
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000F981 File Offset: 0x0000DB81
		public void OnError(Exception error)
		{
			throw new ObjectDisposedException("");
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000F98D File Offset: 0x0000DB8D
		public void OnNext(T value)
		{
			throw new ObjectDisposedException("");
		}

		// Token: 0x04000158 RID: 344
		public static readonly DisposedObserver<T> Instance = new DisposedObserver<T>();
	}
}
