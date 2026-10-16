using System;
using System.ComponentModel;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000238 RID: 568
	[Target("RepeatingWrapper", IsWrapper = true)]
	[Preserve]
	public class RepeatingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060014F3 RID: 5363 RVA: 0x00075014 File Offset: 0x00073214
		public RepeatingTargetWrapper()
			: this(null, 3)
		{
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0007501E File Offset: 0x0007321E
		public RepeatingTargetWrapper(Target wrappedTarget, int repeatCount)
		{
			base.WrappedTarget = wrappedTarget;
			this.RepeatCount = repeatCount;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00075034 File Offset: 0x00073234
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x0007503C File Offset: 0x0007323C
		[DefaultValue(3)]
		[Display("Repeat Count", "Number of times to call target for a given log message.", false, 0)]
		public int RepeatCount { get; set; }

		// Token: 0x060014F7 RID: 5367 RVA: 0x00075048 File Offset: 0x00073248
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncHelpers.Repeat(this.RepeatCount, logEvent.Continuation, delegate(AsyncContinuation cont)
			{
				this.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(cont));
			});
		}
	}
}
