using System;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000232 RID: 562
	[Target("AutoFlushWrapper", IsWrapper = true)]
	[Preserve]
	public class AutoFlushTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060014CC RID: 5324 RVA: 0x00074AC0 File Offset: 0x00072CC0
		public AutoFlushTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00074AC9 File Offset: 0x00072CC9
		public AutoFlushTargetWrapper(Target wrappedTarget)
		{
			base.WrappedTarget = wrappedTarget;
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00074AD8 File Offset: 0x00072CD8
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(AsyncHelpers.PrecededBy(logEvent.Continuation, new AsynchronousAction(base.WrappedTarget.Flush))));
		}
	}
}
