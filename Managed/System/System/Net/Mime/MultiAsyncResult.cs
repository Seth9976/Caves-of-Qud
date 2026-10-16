using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x020007E4 RID: 2020
	internal sealed class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x06004099 RID: 16537 RVA: 0x000DCF08 File Offset: 0x000DB108
		internal MultiAsyncResult(object context, AsyncCallback callback, object state)
			: base(context, state, callback)
		{
			this._context = context;
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600409A RID: 16538 RVA: 0x000DCF1A File Offset: 0x000DB11A
		internal object Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x000DCF22 File Offset: 0x000DB122
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x000DCF2A File Offset: 0x000DB12A
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x000DCF32 File Offset: 0x000DB132
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x000DCF41 File Offset: 0x000DB141
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this._outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x000DCF5D File Offset: 0x000DB15D
		private void Increment()
		{
			Interlocked.Increment(ref this._outstanding);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000DCF2A File Offset: 0x000DB12A
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000DCF6B File Offset: 0x000DB16B
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x040026CE RID: 9934
		private readonly object _context;

		// Token: 0x040026CF RID: 9935
		private int _outstanding;
	}
}
