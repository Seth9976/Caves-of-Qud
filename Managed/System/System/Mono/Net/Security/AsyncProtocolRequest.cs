using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Net.Security
{
	// Token: 0x0200008E RID: 142
	internal abstract class AsyncProtocolRequest
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600022B RID: 555 RVA: 0x000063EE File Offset: 0x000045EE
		public MobileAuthenticatedStream Parent { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000063F6 File Offset: 0x000045F6
		public bool RunSynchronously { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600022D RID: 557 RVA: 0x000063FE File Offset: 0x000045FE
		public int ID
		{
			get
			{
				return ++AsyncProtocolRequest.next_id;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000640D File Offset: 0x0000460D
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000641A File Offset: 0x0000461A
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00006422 File Offset: 0x00004622
		public int UserResult { get; protected set; }

		// Token: 0x06000231 RID: 561 RVA: 0x0000642B File Offset: 0x0000462B
		public AsyncProtocolRequest(MobileAuthenticatedStream parent, bool sync)
		{
			this.Parent = parent;
			this.RunSynchronously = sync;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000644C File Offset: 0x0000464C
		internal void RequestRead(int size)
		{
			object obj = this.locker;
			lock (obj)
			{
				this.RequestedSize += size;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006494 File Offset: 0x00004694
		internal void RequestWrite()
		{
			this.WriteRequested = 1;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000064A0 File Offset: 0x000046A0
		internal async Task<AsyncProtocolResult> StartOperation(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.Started, 1, 0) != 0)
			{
				throw new InvalidOperationException();
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				await this.ProcessOperation(cancellationToken).ConfigureAwait(false);
				asyncProtocolResult = new AsyncProtocolResult(this.UserResult);
			}
			catch (Exception ex)
			{
				asyncProtocolResult = new AsyncProtocolResult(this.Parent.SetException(ex));
			}
			return asyncProtocolResult;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000064EC File Offset: 0x000046EC
		private async Task ProcessOperation(CancellationToken cancellationToken)
		{
			AsyncOperationStatus status = AsyncOperationStatus.Initialize;
			while (status != AsyncOperationStatus.Complete)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int? num = await this.InnerRead(cancellationToken).ConfigureAwait(false);
				if (num != null)
				{
					int? num2 = num;
					int num3 = 0;
					if ((num2.GetValueOrDefault() == num3) & (num2 != null))
					{
						status = AsyncOperationStatus.ReadDone;
					}
					else
					{
						num2 = num;
						num3 = 0;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new IOException("Remote prematurely closed connection.");
						}
					}
				}
				if (status <= AsyncOperationStatus.ReadDone)
				{
					AsyncOperationStatus newStatus;
					try
					{
						newStatus = this.Run(status);
						goto IL_011C;
					}
					catch (Exception ex)
					{
						throw MobileAuthenticatedStream.GetSSPIException(ex);
					}
					goto IL_0116;
					IL_011C:
					if (Interlocked.Exchange(ref this.WriteRequested, 0) != 0)
					{
						await this.Parent.InnerWrite(this.RunSynchronously, cancellationToken).ConfigureAwait(false);
					}
					status = newStatus;
					continue;
				}
				IL_0116:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006538 File Offset: 0x00004738
		private async Task<int?> InnerRead(CancellationToken cancellationToken)
		{
			int? totalRead = null;
			int num2;
			for (int requestedSize = Interlocked.Exchange(ref this.RequestedSize, 0); requestedSize > 0; requestedSize += num2)
			{
				int num = await this.Parent.InnerRead(this.RunSynchronously, requestedSize, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					return new int?(num);
				}
				if (num > requestedSize)
				{
					throw new InvalidOperationException();
				}
				totalRead += num;
				requestedSize -= num;
				num2 = Interlocked.Exchange(ref this.RequestedSize, 0);
			}
			return totalRead;
		}

		// Token: 0x06000238 RID: 568
		protected abstract AsyncOperationStatus Run(AsyncOperationStatus status);

		// Token: 0x06000239 RID: 569 RVA: 0x00006583 File Offset: 0x00004783
		public override string ToString()
		{
			return string.Format("[{0}]", this.Name);
		}

		// Token: 0x0400022D RID: 557
		private int Started;

		// Token: 0x0400022E RID: 558
		private int RequestedSize;

		// Token: 0x0400022F RID: 559
		private int WriteRequested;

		// Token: 0x04000230 RID: 560
		private readonly object locker = new object();

		// Token: 0x04000231 RID: 561
		private static int next_id;
	}
}
