using System;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002B RID: 43
	internal struct AsyncOperationAwaiter<T> : IAsyncOperationAwaiter<T>, ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00002620 File Offset: 0x00000820
		public AsyncOperationAwaiter(IAsyncOperation<T> asyncOperation)
		{
			this.m_Operation = asyncOperation;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000262C File Offset: 0x0000082C
		public void OnCompleted(Action continuation)
		{
			this.m_Operation.Completed += delegate(IAsyncOperation<T> obj)
			{
				continuation();
			};
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002660 File Offset: 0x00000860
		public void UnsafeOnCompleted(Action continuation)
		{
			this.m_Operation.Completed += delegate(IAsyncOperation<T> obj)
			{
				continuation();
			};
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00002691 File Offset: 0x00000891
		public bool IsCompleted
		{
			get
			{
				return this.m_Operation.IsDone;
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000269E File Offset: 0x0000089E
		public T GetResult()
		{
			if (this.m_Operation.Status == AsyncOperationStatus.Failed || this.m_Operation.Status == AsyncOperationStatus.Cancelled)
			{
				throw this.m_Operation.Exception;
			}
			return this.m_Operation.Result;
		}

		// Token: 0x04000026 RID: 38
		private IAsyncOperation<T> m_Operation;
	}
}
