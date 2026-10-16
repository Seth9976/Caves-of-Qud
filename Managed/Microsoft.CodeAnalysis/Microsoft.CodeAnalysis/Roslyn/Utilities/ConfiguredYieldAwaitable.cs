using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Roslyn.Utilities
{
	// Token: 0x02000036 RID: 54
	internal readonly struct ConfiguredYieldAwaitable
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00004AE0 File Offset: 0x00002CE0
		public ConfiguredYieldAwaitable(YieldAwaitable awaitable, bool continueOnCapturedContext)
		{
			this._awaitable = awaitable;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public ConfiguredYieldAwaitable.ConfiguredYieldAwaiter GetAwaiter()
		{
			return new ConfiguredYieldAwaitable.ConfiguredYieldAwaiter(this._awaitable.GetAwaiter(), this._continueOnCapturedContext);
		}

		// Token: 0x04000039 RID: 57
		private readonly YieldAwaitable _awaitable;

		// Token: 0x0400003A RID: 58
		private readonly bool _continueOnCapturedContext;

		// Token: 0x020004F2 RID: 1266
		public readonly struct ConfiguredYieldAwaiter : INotifyCompletion, ICriticalNotifyCompletion
		{
			// Token: 0x060032B6 RID: 12982 RVA: 0x0008FFDC File Offset: 0x0008E1DC
			public ConfiguredYieldAwaiter(YieldAwaitable.YieldAwaiter awaiter, bool continueOnCapturedContext)
			{
				this._awaiter = awaiter;
				this._continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x17000EA7 RID: 3751
			// (get) Token: 0x060032B7 RID: 12983 RVA: 0x0008FFEC File Offset: 0x0008E1EC
			public bool IsCompleted
			{
				get
				{
					return this._awaiter.IsCompleted;
				}
			}

			// Token: 0x060032B8 RID: 12984 RVA: 0x00090008 File Offset: 0x0008E208
			public void GetResult()
			{
				this._awaiter.GetResult();
			}

			// Token: 0x060032B9 RID: 12985 RVA: 0x00090024 File Offset: 0x0008E224
			[NullableContext(1)]
			public void OnCompleted(Action continuation)
			{
				if (this._continueOnCapturedContext)
				{
					this._awaiter.OnCompleted(continuation);
					return;
				}
				ThreadPool.QueueUserWorkItem(ConfiguredYieldAwaitable.ConfiguredYieldAwaiter.s_runContinuation, continuation);
			}

			// Token: 0x060032BA RID: 12986 RVA: 0x00090058 File Offset: 0x0008E258
			[NullableContext(1)]
			public void UnsafeOnCompleted(Action continuation)
			{
				if (this._continueOnCapturedContext)
				{
					this._awaiter.UnsafeOnCompleted(continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(ConfiguredYieldAwaitable.ConfiguredYieldAwaiter.s_runContinuation, continuation);
			}

			// Token: 0x040013DC RID: 5084
			[Nullable(1)]
			private static readonly WaitCallback s_runContinuation = delegate(object continuation)
			{
				((Action)continuation).Invoke();
			};

			// Token: 0x040013DD RID: 5085
			private readonly YieldAwaitable.YieldAwaiter _awaiter;

			// Token: 0x040013DE RID: 5086
			private readonly bool _continueOnCapturedContext;
		}
	}
}
