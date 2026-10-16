using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace XRL
{
	// Token: 0x020004DD RID: 1245
	public struct SynchronizationContextAwaiter : INotifyCompletion
	{
		// Token: 0x0600355B RID: 13659 RVA: 0x0010157E File Offset: 0x000FF77E
		public SynchronizationContextAwaiter(SynchronizationContext context)
		{
			this._context = context;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x00101587 File Offset: 0x000FF787
		public bool IsCompleted
		{
			get
			{
				return this._context == SynchronizationContext.Current;
			}
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x00101596 File Offset: 0x000FF796
		public void OnCompleted(Action continuation)
		{
			this._context.Post(SynchronizationContextAwaiter._postCallback, continuation);
		}

		// Token: 0x0600355E RID: 13662 RVA: 0x001015A9 File Offset: 0x000FF7A9
		public void GetResult()
		{
		}

		// Token: 0x04001AE6 RID: 6886
		private static readonly SendOrPostCallback _postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001AE7 RID: 6887
		private readonly SynchronizationContext _context;
	}
}
