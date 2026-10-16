using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000013 RID: 19
	public abstract class ChannelReader<T>
	{
		// Token: 0x06000059 RID: 89
		public abstract bool TryRead(out T item);

		// Token: 0x0600005A RID: 90
		public abstract UniTask<bool> WaitToReadAsync(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005B RID: 91
		public abstract UniTask Completion { get; }

		// Token: 0x0600005C RID: 92 RVA: 0x00002ADC File Offset: 0x00000CDC
		public virtual UniTask<T> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			T t;
			if (this.TryRead(out t))
			{
				return UniTask.FromResult<T>(t);
			}
			return this.ReadAsyncCore(cancellationToken);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B04 File Offset: 0x00000D04
		private async UniTask<T> ReadAsyncCore(CancellationToken cancellationToken = default(CancellationToken))
		{
			UniTask<bool>.Awaiter awaiter = this.WaitToReadAsync(cancellationToken).GetAwaiter();
			if (!awaiter.IsCompleted)
			{
				await awaiter;
				UniTask<bool>.Awaiter awaiter2;
				awaiter = awaiter2;
				awaiter2 = default(UniTask<bool>.Awaiter);
			}
			T t;
			if (awaiter.GetResult() && this.TryRead(out t))
			{
				return t;
			}
			throw new ChannelClosedException();
		}

		// Token: 0x0600005E RID: 94
		public abstract IUniTaskAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default(CancellationToken));
	}
}
