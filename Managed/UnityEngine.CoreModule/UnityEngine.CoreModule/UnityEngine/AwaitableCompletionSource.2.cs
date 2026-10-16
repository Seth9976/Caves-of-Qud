using System;
using System.Threading;

namespace UnityEngine
{
	// Token: 0x02000281 RID: 641
	public class AwaitableCompletionSource<T>
	{
		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		// (set) Token: 0x06002015 RID: 8213 RVA: 0x0003DD86 File Offset: 0x0003BF86
		public Awaitable<T> Awaitable { get; private set; } = Awaitable<T>.GetManaged();

		// Token: 0x06002016 RID: 8214 RVA: 0x0003DD90 File Offset: 0x0003BF90
		public void SetResult(in T value)
		{
			bool flag = !this.TrySetResult(in value);
			if (flag)
			{
				throw new InvalidOperationException("Can't raise completion of the same Awaitable twice");
			}
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		public void SetCanceled()
		{
			bool flag = !this.TrySetCanceled();
			if (flag)
			{
				throw new InvalidOperationException("Can't raise completion of the same Awaitable twice");
			}
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0003DDE0 File Offset: 0x0003BFE0
		public void SetException(Exception exception)
		{
			bool flag = !this.TrySetException(exception);
			if (flag)
			{
				throw new InvalidOperationException("Can't raise completion of the same Awaitable twice");
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0003DE07 File Offset: 0x0003C007
		private bool CheckAndAcquireCompletionState()
		{
			return Interlocked.CompareExchange(ref this._state, 1, 0) == 0;
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0003DE1C File Offset: 0x0003C01C
		public bool TrySetResult(in T value)
		{
			bool flag = !this.CheckAndAcquireCompletionState();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				this.Awaitable.SetResultAndRaiseContinuation(value);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0003DE54 File Offset: 0x0003C054
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

		// Token: 0x0600201C RID: 8220 RVA: 0x0003DE88 File Offset: 0x0003C088
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
				this.Awaitable.SetExceptionAndRaiseContinuation(exception);
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0003DEBA File Offset: 0x0003C0BA
		public void Reset()
		{
			this.Awaitable = Awaitable<T>.GetManaged();
			this._state = 0;
		}

		// Token: 0x0400098A RID: 2442
		private volatile int _state;
	}
}
