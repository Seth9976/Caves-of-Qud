using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000026 RID: 38
	public abstract class MoveNextSource : IUniTaskSource<bool>, IUniTaskSource, IValueTaskSource, IValueTaskSource<bool>
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002F7F File Offset: 0x0000117F
		public bool GetResult(short token)
		{
			return this.completionSource.GetResult(token);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002F8D File Offset: 0x0000118D
		public UniTaskStatus GetStatus(short token)
		{
			return this.completionSource.GetStatus(token);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002F9B File Offset: 0x0000119B
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.completionSource.OnCompleted(continuation, state, token);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FAB File Offset: 0x000011AB
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.completionSource.UnsafeGetStatus();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002FB8 File Offset: 0x000011B8
		void IUniTaskSource.GetResult(short token)
		{
			this.completionSource.GetResult(token);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00002FC8 File Offset: 0x000011C8
		protected bool TryGetResult<T>(UniTask<T>.Awaiter awaiter, out T result)
		{
			bool flag;
			try
			{
				result = awaiter.GetResult();
				flag = true;
			}
			catch (Exception ex)
			{
				this.completionSource.TrySetException(ex);
				result = default(T);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003010 File Offset: 0x00001210
		protected bool TryGetResult(UniTask.Awaiter awaiter)
		{
			bool flag;
			try
			{
				awaiter.GetResult();
				flag = true;
			}
			catch (Exception ex)
			{
				this.completionSource.TrySetException(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400002D RID: 45
		protected UniTaskCompletionSourceCore<bool> completionSource;
	}
}
