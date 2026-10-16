using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000121 RID: 289
	internal class ThrowObserver<T> : IObserver<T>
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0000F948 File Offset: 0x0000DB48
		private ThrowObserver()
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000F950 File Offset: 0x0000DB50
		public void OnCompleted()
		{
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0000F952 File Offset: 0x0000DB52
		public void OnError(Exception error)
		{
			ExceptionDispatchInfo.Capture(error).Throw();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000F95F File Offset: 0x0000DB5F
		public void OnNext(T value)
		{
		}

		// Token: 0x04000157 RID: 343
		public static readonly ThrowObserver<T> Instance = new ThrowObserver<T>();
	}
}
