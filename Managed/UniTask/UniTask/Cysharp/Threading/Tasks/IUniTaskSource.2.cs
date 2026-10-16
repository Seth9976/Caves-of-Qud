using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000023 RID: 35
	public interface IUniTaskSource<out T> : IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
	{
		// Token: 0x06000085 RID: 133
		T GetResult(short token);

		// Token: 0x06000086 RID: 134 RVA: 0x00002D26 File Offset: 0x00000F26
		UniTaskStatus GetStatus(short token)
		{
			return this.GetStatus(token);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002D2F File Offset: 0x00000F2F
		void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.OnCompleted(continuation, state, token);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002D3A File Offset: 0x00000F3A
		ValueTaskSourceStatus GetStatus(short token)
		{
			return (ValueTaskSourceStatus)this.GetStatus(token);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002D43 File Offset: 0x00000F43
		T GetResult(short token)
		{
			return this.GetResult(token);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002D4C File Offset: 0x00000F4C
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			this.OnCompleted(continuation, state, token);
		}
	}
}
