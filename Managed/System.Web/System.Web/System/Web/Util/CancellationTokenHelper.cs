using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x0200010D RID: 269
	internal sealed class CancellationTokenHelper : IDisposable
	{
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0002535B File Offset: 0x0002355B
		public CancellationTokenHelper(bool canceled)
		{
			if (canceled)
			{
				this._cts.Cancel();
			}
			this._state = (canceled ? 2 : 0);
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00025389 File Offset: 0x00023589
		internal bool IsCancellationRequested
		{
			get
			{
				return this._cts.IsCancellationRequested;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00025396 File Offset: 0x00023596
		internal CancellationToken Token
		{
			get
			{
				return this._cts.Token;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x000253A3 File Offset: 0x000235A3
		public void Cancel()
		{
			if (Interlocked.CompareExchange(ref this._state, 1, 0) == 0)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
				{
					try
					{
						this._cts.Cancel();
					}
					catch
					{
					}
					finally
					{
						if (Interlocked.CompareExchange(ref this._state, 2, 1) == 3)
						{
							this._cts.Dispose();
							Interlocked.Exchange(ref this._state, 4);
						}
					}
				}, null);
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000253C8 File Offset: 0x000235C8
		public void Dispose()
		{
			switch (Interlocked.Exchange(ref this._state, 3))
			{
			case 0:
			case 2:
				this._cts.Dispose();
				Interlocked.Exchange(ref this._state, 4);
				return;
			case 1:
			case 3:
				break;
			case 4:
				Interlocked.Exchange(ref this._state, 4);
				break;
			default:
				return;
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00025423 File Offset: 0x00023623
		private static CancellationTokenHelper GetStaticDisposedHelper()
		{
			CancellationTokenHelper cancellationTokenHelper = new CancellationTokenHelper(false);
			cancellationTokenHelper.Dispose();
			return cancellationTokenHelper;
		}

		// Token: 0x04001197 RID: 4503
		private const int STATE_CREATED = 0;

		// Token: 0x04001198 RID: 4504
		private const int STATE_CANCELING = 1;

		// Token: 0x04001199 RID: 4505
		private const int STATE_CANCELED = 2;

		// Token: 0x0400119A RID: 4506
		private const int STATE_DISPOSING = 3;

		// Token: 0x0400119B RID: 4507
		private const int STATE_DISPOSED = 4;

		// Token: 0x0400119C RID: 4508
		internal static readonly CancellationTokenHelper StaticDisposed = CancellationTokenHelper.GetStaticDisposedHelper();

		// Token: 0x0400119D RID: 4509
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		// Token: 0x0400119E RID: 4510
		private int _state;
	}
}
