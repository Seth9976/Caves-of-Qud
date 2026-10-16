using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000120 RID: 288
	internal class EmptyObserver<T> : IObserver<T>
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x0000F92E File Offset: 0x0000DB2E
		private EmptyObserver()
		{
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000F936 File Offset: 0x0000DB36
		public void OnCompleted()
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000F938 File Offset: 0x0000DB38
		public void OnError(Exception error)
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000F93A File Offset: 0x0000DB3A
		public void OnNext(T value)
		{
		}

		// Token: 0x04000156 RID: 342
		public static readonly EmptyObserver<T> Instance = new EmptyObserver<T>();
	}
}
