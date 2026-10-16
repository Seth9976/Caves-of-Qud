using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Common;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x0200023C RID: 572
	[Preserve]
	public abstract class WrapperTargetBase : Target
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00075358 File Offset: 0x00073558
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00075360 File Offset: 0x00073560
		[RequiredParameter]
		public Target WrappedTarget { get; set; }

		// Token: 0x0600150B RID: 5387 RVA: 0x00075369 File Offset: 0x00073569
		public override string ToString()
		{
			string text = base.ToString();
			string text2 = "(";
			Target wrappedTarget = this.WrappedTarget;
			return text + text2 + ((wrappedTarget != null) ? wrappedTarget.ToString() : null) + ")";
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00075392 File Offset: 0x00073592
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			this.WrappedTarget.Flush(asyncContinuation);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000753A0 File Offset: 0x000735A0
		protected sealed override void Write(LogEventInfo logEvent)
		{
			throw new NotSupportedException("This target must not be invoked in a synchronous way.");
		}
	}
}
