using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000045 RID: 69
	internal sealed class WhenEachEnumerable<T> : IUniTaskAsyncEnumerable<WhenEachResult<T>>
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00006906 File Offset: 0x00004B06
		public WhenEachEnumerable(IEnumerable<UniTask<T>> source)
		{
			this.source = source;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006915 File Offset: 0x00004B15
		public IUniTaskAsyncEnumerator<WhenEachResult<T>> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new WhenEachEnumerable<T>.Enumerator(this.source, cancellationToken);
		}

		// Token: 0x04000099 RID: 153
		private IEnumerable<UniTask<T>> source;

		// Token: 0x020001C3 RID: 451
		private sealed class Enumerator : IUniTaskAsyncEnumerator<WhenEachResult<T>>, IUniTaskAsyncDisposable
		{
			// Token: 0x06000A79 RID: 2681 RVA: 0x00024EA5 File Offset: 0x000230A5
			public Enumerator(IEnumerable<UniTask<T>> source, CancellationToken cancellationToken)
			{
				this.source = source;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00024EBB File Offset: 0x000230BB
			public WhenEachResult<T> Current
			{
				get
				{
					return this.channelEnumerator.Current;
				}
			}

			// Token: 0x06000A7B RID: 2683 RVA: 0x00024EC8 File Offset: 0x000230C8
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				if (this.state == WhenEachState.NotRunning)
				{
					this.state = WhenEachState.Running;
					this.channel = Channel.CreateSingleConsumerUnbounded<WhenEachResult<T>>();
					this.channelEnumerator = this.channel.Reader.ReadAllAsync(default(CancellationToken)).GetAsyncEnumerator(this.cancellationToken);
					UniTask<T>[] array = this.source as UniTask<T>[];
					if (array != null)
					{
						WhenEachEnumerable<T>.Enumerator.ConsumeAll(this, array, array.Length);
					}
					else
					{
						using (ArrayPoolUtil.RentArray<UniTask<T>> rentArray = ArrayPoolUtil.Materialize<UniTask<T>>(this.source))
						{
							WhenEachEnumerable<T>.Enumerator.ConsumeAll(this, rentArray.Array, rentArray.Length);
						}
					}
				}
				return this.channelEnumerator.MoveNextAsync();
			}

			// Token: 0x06000A7C RID: 2684 RVA: 0x00024F8C File Offset: 0x0002318C
			private static void ConsumeAll(WhenEachEnumerable<T>.Enumerator self, UniTask<T>[] array, int length)
			{
				for (int i = 0; i < length; i++)
				{
					WhenEachEnumerable<T>.Enumerator.RunWhenEachTask(self, array[i], length).Forget();
				}
			}

			// Token: 0x06000A7D RID: 2685 RVA: 0x00024FBC File Offset: 0x000231BC
			private static async UniTaskVoid RunWhenEachTask(WhenEachEnumerable<T>.Enumerator self, UniTask<T> task, int length)
			{
				try
				{
					T t = await task;
					self.channel.Writer.TryWrite(new WhenEachResult<T>(t));
				}
				catch (Exception ex)
				{
					self.channel.Writer.TryWrite(new WhenEachResult<T>(ex));
				}
				if (Interlocked.Increment(ref self.completeCount) == length)
				{
					self.state = WhenEachState.Completed;
					self.channel.Writer.TryComplete(null);
				}
			}

			// Token: 0x06000A7E RID: 2686 RVA: 0x00025010 File Offset: 0x00023210
			public async UniTask DisposeAsync()
			{
				if (this.channelEnumerator != null)
				{
					await this.channelEnumerator.DisposeAsync();
				}
				if (this.state != WhenEachState.Completed)
				{
					this.state = WhenEachState.Completed;
					this.channel.Writer.TryComplete(new OperationCanceledException());
				}
			}

			// Token: 0x040003A7 RID: 935
			private readonly IEnumerable<UniTask<T>> source;

			// Token: 0x040003A8 RID: 936
			private CancellationToken cancellationToken;

			// Token: 0x040003A9 RID: 937
			private Channel<WhenEachResult<T>> channel;

			// Token: 0x040003AA RID: 938
			private IUniTaskAsyncEnumerator<WhenEachResult<T>> channelEnumerator;

			// Token: 0x040003AB RID: 939
			private int completeCount;

			// Token: 0x040003AC RID: 940
			private WhenEachState state;
		}
	}
}
