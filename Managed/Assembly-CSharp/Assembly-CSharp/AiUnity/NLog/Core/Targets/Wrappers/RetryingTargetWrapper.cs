using System;
using System.ComponentModel;
using System.Threading;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000239 RID: 569
	[Target("RetryingWrapper", IsWrapper = true)]
	[Preserve]
	public class RetryingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060014F8 RID: 5368 RVA: 0x0007508B File Offset: 0x0007328B
		public RetryingTargetWrapper()
			: this(null, 3, 100)
		{
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00075097 File Offset: 0x00073297
		public RetryingTargetWrapper(Target wrappedTarget, int retryCount, int retryDelayMilliseconds)
		{
			base.WrappedTarget = wrappedTarget;
			this.RetryCount = retryCount;
			this.RetryDelayMilliseconds = retryDelayMilliseconds;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x000750B4 File Offset: 0x000732B4
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x000750BB File Offset: 0x000732BB
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x000750C3 File Offset: 0x000732C3
		[DefaultValue(3)]
		[Display("Retry Count", "Number of retry attempts on wrapped target.", false, 0)]
		public int RetryCount { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x000750CC File Offset: 0x000732CC
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x000750D4 File Offset: 0x000732D4
		[DefaultValue(100)]
		[Display("Retry Count", "Time to wait between retires in milliseconds.", false, 0)]
		public int RetryDelayMilliseconds { get; set; }

		// Token: 0x060014FF RID: 5375 RVA: 0x000750E0 File Offset: 0x000732E0
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncContinuation continuation = null;
			int counter = 0;
			continuation = delegate(Exception ex)
			{
				if (ex == null)
				{
					logEvent.Continuation(null);
					return;
				}
				int num = Interlocked.Increment(ref counter);
				RetryingTargetWrapper.Logger.Warn("Error while writing to '{0}': {1}. Try {2}/{3}", new object[] { this.WrappedTarget, ex, num, this.RetryCount });
				if (num >= this.RetryCount)
				{
					RetryingTargetWrapper.Logger.Warn("Too many retries. Aborting.", Array.Empty<object>());
					logEvent.Continuation(ex);
					return;
				}
				Thread.Sleep(this.RetryDelayMilliseconds);
				this.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
			};
			base.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
		}
	}
}
