using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000051 RID: 81
	public class AutoResetUniTaskCompletionSource : IUniTaskSource, IValueTaskSource, ITaskPoolNode<AutoResetUniTaskCompletionSource>, IPromise, IResolvePromise, IRejectPromise, ICancelPromise
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006EAB File Offset: 0x000050AB
		public ref AutoResetUniTaskCompletionSource NextNode
		{
			get
			{
				return ref this.nextNode;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006EB3 File Offset: 0x000050B3
		static AutoResetUniTaskCompletionSource()
		{
			TaskPool.RegisterSizeGetter(typeof(AutoResetUniTaskCompletionSource), () => AutoResetUniTaskCompletionSource.pool.Size);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006ED4 File Offset: 0x000050D4
		private AutoResetUniTaskCompletionSource()
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006EDC File Offset: 0x000050DC
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource Create()
		{
			AutoResetUniTaskCompletionSource autoResetUniTaskCompletionSource;
			if (!AutoResetUniTaskCompletionSource.pool.TryPop(out autoResetUniTaskCompletionSource))
			{
				autoResetUniTaskCompletionSource = new AutoResetUniTaskCompletionSource();
			}
			autoResetUniTaskCompletionSource.version = autoResetUniTaskCompletionSource.core.Version;
			return autoResetUniTaskCompletionSource;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006F10 File Offset: 0x00005110
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource CreateFromCanceled(CancellationToken cancellationToken, out short token)
		{
			AutoResetUniTaskCompletionSource autoResetUniTaskCompletionSource = AutoResetUniTaskCompletionSource.Create();
			autoResetUniTaskCompletionSource.TrySetCanceled(cancellationToken);
			token = autoResetUniTaskCompletionSource.core.Version;
			return autoResetUniTaskCompletionSource;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006F3C File Offset: 0x0000513C
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource CreateFromException(Exception exception, out short token)
		{
			AutoResetUniTaskCompletionSource autoResetUniTaskCompletionSource = AutoResetUniTaskCompletionSource.Create();
			autoResetUniTaskCompletionSource.TrySetException(exception);
			token = autoResetUniTaskCompletionSource.core.Version;
			return autoResetUniTaskCompletionSource;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006F68 File Offset: 0x00005168
		[DebuggerHidden]
		public static AutoResetUniTaskCompletionSource CreateCompleted(out short token)
		{
			AutoResetUniTaskCompletionSource autoResetUniTaskCompletionSource = AutoResetUniTaskCompletionSource.Create();
			autoResetUniTaskCompletionSource.TrySetResult();
			token = autoResetUniTaskCompletionSource.core.Version;
			return autoResetUniTaskCompletionSource;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00006F90 File Offset: 0x00005190
		public UniTask Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask(this, this.core.Version);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006FA3 File Offset: 0x000051A3
		[DebuggerHidden]
		public bool TrySetResult()
		{
			return this.version == this.core.Version && this.core.TrySetResult(AsyncUnit.Default);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006FCA File Offset: 0x000051CA
		[DebuggerHidden]
		public bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.version == this.core.Version && this.core.TrySetCanceled(cancellationToken);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006FED File Offset: 0x000051ED
		[DebuggerHidden]
		public bool TrySetException(Exception exception)
		{
			return this.version == this.core.Version && this.core.TrySetException(exception);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007010 File Offset: 0x00005210
		[DebuggerHidden]
		public void GetResult(short token)
		{
			try
			{
				this.core.GetResult(token);
			}
			finally
			{
				this.TryReturn();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007044 File Offset: 0x00005244
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return this.core.GetStatus(token);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007052 File Offset: 0x00005252
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return this.core.UnsafeGetStatus();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000705F File Offset: 0x0000525F
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			this.core.OnCompleted(continuation, state, token);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000706F File Offset: 0x0000526F
		[DebuggerHidden]
		private bool TryReturn()
		{
			this.core.Reset();
			return AutoResetUniTaskCompletionSource.pool.TryPush(this);
		}

		// Token: 0x040000A8 RID: 168
		private static TaskPool<AutoResetUniTaskCompletionSource> pool;

		// Token: 0x040000A9 RID: 169
		private AutoResetUniTaskCompletionSource nextNode;

		// Token: 0x040000AA RID: 170
		private UniTaskCompletionSourceCore<AsyncUnit> core;

		// Token: 0x040000AB RID: 171
		private short version;
	}
}
