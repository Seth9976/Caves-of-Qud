using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000043 RID: 67
	public readonly struct WhenEachResult<T>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006815 File Offset: 0x00004A15
		public T Result { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000681D File Offset: 0x00004A1D
		public Exception Exception { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00006825 File Offset: 0x00004A25
		public bool IsCompletedSuccessfully
		{
			get
			{
				return this.Exception == null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00006830 File Offset: 0x00004A30
		public bool IsFaulted
		{
			get
			{
				return this.Exception != null;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000683B File Offset: 0x00004A3B
		public WhenEachResult(T result)
		{
			this.Result = result;
			this.Exception = null;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000684B File Offset: 0x00004A4B
		public WhenEachResult(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.Result = default(T);
			this.Exception = exception;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000686E File Offset: 0x00004A6E
		public void TryThrow()
		{
			if (this.IsFaulted)
			{
				ExceptionDispatchInfo.Capture(this.Exception).Throw();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006888 File Offset: 0x00004A88
		public T GetResult()
		{
			if (this.IsFaulted)
			{
				ExceptionDispatchInfo.Capture(this.Exception).Throw();
			}
			return this.Result;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000068A8 File Offset: 0x00004AA8
		public override string ToString()
		{
			if (this.IsCompletedSuccessfully)
			{
				T result = this.Result;
				return ((result != null) ? result.ToString() : null) ?? "";
			}
			return "Exception{" + this.Exception.Message + "}";
		}
	}
}
