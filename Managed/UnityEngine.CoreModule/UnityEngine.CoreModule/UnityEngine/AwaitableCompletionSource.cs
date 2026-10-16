using System;
using System.Threading;

namespace UnityEngine
{
	// Token: 0x02000280 RID: 640
	public class AwaitableCompletionSource
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06002009 RID: 8201 RVA: 0x0003DC1B File Offset: 0x0003BE1B
		// (set) Token: 0x0600200A RID: 8202 RVA: 0x0003DC23 File Offset: 0x0003BE23
		public Awaitable Awaitable { get; private set; } = Awaitable.NewManagedAwaitable();

		// Token: 0x0600200B RID: 8203 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		public void SetResult()
		{
			bool flag = !this.TrySetResult();
			if (flag)
			{
				throw new InvalidOperationException("Can't raise completion of the same Awaitable twice");
			}
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x0003DC54 File Offset: 0x0003BE54
		public void SetCanceled()
		{
			bool flag = !this.TrySetCanceled();
			if (flag)
			{
				throw new InvalidOperationException("Can't raise completion of the same Awaitable twice");
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x0003DC7C File Offset: 0x0003BE7C
		public void SetException(Exception exception)
		{
			bool flag = !this.TrySetException(exception);
			if (flag)
			{
				throw new InvalidOperationException("Can't raise completion of the same Awaitable twice");
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x0003DCA3 File Offset: 0x0003BEA3
		private bool CheckAndAcquireCompletionState()
		{
			return Interlocked.CompareExchange(ref this._state, 1, 0) == 0;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x0003DCB8 File Offset: 0x0003BEB8
		public bool TrySetResult()
		{
			bool flag = !this.CheckAndAcquireCompletionState();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.Awaitable.RaiseManagedCompletion();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x0003DCEC File Offset: 0x0003BEEC
		public bool TrySetCanceled()
		{
			bool flag = !this.CheckAndAcquireCompletionState();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.Awaitable.Cancel();
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0003DD20 File Offset: 0x0003BF20
		public bool TrySetException(Exception exception)
		{
			bool flag = !this.CheckAndAcquireCompletionState();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.Awaitable.RaiseManagedCompletion(exception);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0003DD52 File Offset: 0x0003BF52
		public void Reset()
		{
			this.Awaitable = Awaitable.NewManagedAwaitable();
			this._state = 0;
		}

		// Token: 0x04000988 RID: 2440
		private volatile int _state;
	}
}
