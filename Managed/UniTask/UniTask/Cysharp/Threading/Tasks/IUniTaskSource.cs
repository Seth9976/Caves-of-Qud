using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000022 RID: 34
	public interface IUniTaskSource : IValueTaskSource
	{
		// Token: 0x0600007E RID: 126
		UniTaskStatus GetStatus(short token);

		// Token: 0x0600007F RID: 127
		void OnCompleted(Action<object> continuation, object state, short token);

		// Token: 0x06000080 RID: 128
		void GetResult(short token);

		// Token: 0x06000081 RID: 129
		UniTaskStatus UnsafeGetStatus();

		// Token: 0x06000082 RID: 130 RVA: 0x00002D09 File Offset: 0x00000F09
		ValueTaskSourceStatus GetStatus(short token)
		{
			return (ValueTaskSourceStatus)this.GetStatus(token);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00002D12 File Offset: 0x00000F12
		void GetResult(short token)
		{
			this.GetResult(token);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002D1B File Offset: 0x00000F1B
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			this.OnCompleted(continuation, state, token);
		}
	}
}
