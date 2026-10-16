using System;
using System.Collections.Generic;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x0200023B RID: 571
	[Target("SplitGroup", IsCompound = true, IsWrapper = true)]
	[Preserve]
	public class SplitGroupTarget : CompoundTargetBase
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x000751EC File Offset: 0x000733EC
		public SplitGroupTarget()
			: this(new Target[0])
		{
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000751FA File Offset: 0x000733FA
		public SplitGroupTarget(params Target[] targets)
			: base(targets)
		{
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00075203 File Offset: 0x00073403
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0007520C File Offset: 0x0007340C
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncHelpers.ForEachItemSequentially<Target>(base.Targets, logEvent.Continuation, delegate(Target t, AsyncContinuation cont)
			{
				t.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(cont));
			});
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00075248 File Offset: 0x00073448
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			SplitGroupTarget.Logger.Trace("Writing {0} events", new object[] { logEvents.Length });
			for (int i = 0; i < logEvents.Length; i++)
			{
				logEvents[i].Continuation = SplitGroupTarget.CountedWrap(logEvents[i].Continuation, base.Targets.Count);
			}
			foreach (Target target in base.Targets)
			{
				SplitGroupTarget.Logger.Trace("Sending {0} events to {1}", new object[] { logEvents.Length, target });
				target.WriteAsyncLogEvents(logEvents);
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00075310 File Offset: 0x00073510
		private static AsyncContinuation CountedWrap(AsyncContinuation originalContinuation, int counter)
		{
			if (counter == 1)
			{
				return originalContinuation;
			}
			List<Exception> exceptions = new List<Exception>();
			return delegate(Exception ex)
			{
				if (ex != null)
				{
					List<Exception> exceptions2 = exceptions;
					lock (exceptions2)
					{
						exceptions.Add(ex);
					}
				}
				int num = Interlocked.Decrement(ref counter);
				if (num == 0)
				{
					Exception combinedException = AsyncHelpers.GetCombinedException(exceptions);
					SplitGroupTarget.Logger.Trace("Combined exception: {0}", new object[] { combinedException });
					originalContinuation(combinedException);
					return;
				}
				SplitGroupTarget.Logger.Trace("{0} remaining.", new object[] { num });
			};
		}
	}
}
