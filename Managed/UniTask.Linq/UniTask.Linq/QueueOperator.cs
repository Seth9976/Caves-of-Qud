using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
	// Token: 0x0200004E RID: 78
	internal sealed class QueueOperator<TSource> : IUniTaskAsyncEnumerable<TSource>
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public QueueOperator(IUniTaskAsyncEnumerable<TSource> source)
		{
			this.source = source;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000BFAF File Offset: 0x0000A1AF
		public IUniTaskAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
		{
			return new QueueOperator<TSource>._Queue(this.source, cancellationToken);
		}

		// Token: 0x0400012A RID: 298
		private readonly IUniTaskAsyncEnumerable<TSource> source;

		// Token: 0x02000186 RID: 390
		private sealed class _Queue : IUniTaskAsyncEnumerator<TSource>, IUniTaskAsyncDisposable
		{
			// Token: 0x06000749 RID: 1865 RVA: 0x0003EB92 File Offset: 0x0003CD92
			public _Queue(IUniTaskAsyncEnumerable<TSource> source, CancellationToken cancellationToken)
			{
				this.source = source;
				this.cancellationToken = cancellationToken;
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x0600074A RID: 1866 RVA: 0x0003EBA8 File Offset: 0x0003CDA8
			public TSource Current
			{
				get
				{
					return this.channelEnumerator.Current;
				}
			}

			// Token: 0x0600074B RID: 1867 RVA: 0x0003EBB8 File Offset: 0x0003CDB8
			public UniTask<bool> MoveNextAsync()
			{
				this.cancellationToken.ThrowIfCancellationRequested();
				if (this.sourceEnumerator == null)
				{
					this.sourceEnumerator = this.source.GetAsyncEnumerator(this.cancellationToken);
					this.channel = Channel.CreateSingleConsumerUnbounded<TSource>();
					this.channelEnumerator = this.channel.Reader.ReadAllAsync(default(CancellationToken)).GetAsyncEnumerator(this.cancellationToken);
					QueueOperator<TSource>._Queue.ConsumeAll(this, this.sourceEnumerator, this.channel).Forget();
				}
				return this.channelEnumerator.MoveNextAsync();
			}

			// Token: 0x0600074C RID: 1868 RVA: 0x0003EC50 File Offset: 0x0003CE50
			private static async UniTaskVoid ConsumeAll(QueueOperator<TSource>._Queue self, IUniTaskAsyncEnumerator<TSource> enumerator, ChannelWriter<TSource> writer)
			{
				object obj = null;
				try
				{
					try
					{
						for (;;)
						{
							UniTask<bool>.Awaiter awaiter = enumerator.MoveNextAsync().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								await awaiter;
								UniTask<bool>.Awaiter awaiter2;
								awaiter = awaiter2;
								awaiter2 = default(UniTask<bool>.Awaiter);
							}
							if (!awaiter.GetResult())
							{
								break;
							}
							writer.TryWrite(enumerator.Current);
						}
						writer.TryComplete(null);
					}
					catch (Exception ex)
					{
						writer.TryComplete(ex);
					}
				}
				catch (object obj)
				{
				}
				self.channelClosed = true;
				await enumerator.DisposeAsync();
				object obj2 = obj;
				if (obj2 != null)
				{
					Exception ex2 = obj2 as Exception;
					if (ex2 == null)
					{
						throw obj2;
					}
					ExceptionDispatchInfo.Capture(ex2).Throw();
				}
				obj = null;
			}

			// Token: 0x0600074D RID: 1869 RVA: 0x0003ECA4 File Offset: 0x0003CEA4
			public async UniTask DisposeAsync()
			{
				if (this.sourceEnumerator != null)
				{
					await this.sourceEnumerator.DisposeAsync();
				}
				if (this.channelEnumerator != null)
				{
					await this.channelEnumerator.DisposeAsync();
				}
				if (!this.channelClosed)
				{
					this.channelClosed = true;
					this.channel.Writer.TryComplete(new OperationCanceledException());
				}
			}

			// Token: 0x04000EAB RID: 3755
			private readonly IUniTaskAsyncEnumerable<TSource> source;

			// Token: 0x04000EAC RID: 3756
			private CancellationToken cancellationToken;

			// Token: 0x04000EAD RID: 3757
			private Channel<TSource> channel;

			// Token: 0x04000EAE RID: 3758
			private IUniTaskAsyncEnumerator<TSource> channelEnumerator;

			// Token: 0x04000EAF RID: 3759
			private IUniTaskAsyncEnumerator<TSource> sourceEnumerator;

			// Token: 0x04000EB0 RID: 3760
			private bool channelClosed;
		}
	}
}
