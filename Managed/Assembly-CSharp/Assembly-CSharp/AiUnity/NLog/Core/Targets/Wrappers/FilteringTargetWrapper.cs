using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Conditions;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets.Wrappers
{
	// Token: 0x02000236 RID: 566
	[Target("FilteringWrapper", IsWrapper = true)]
	[Preserve]
	public class FilteringTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x00074EEC File Offset: 0x000730EC
		public FilteringTargetWrapper()
		{
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00074EF4 File Offset: 0x000730F4
		public FilteringTargetWrapper(Target wrappedTarget, ConditionExpression condition)
		{
			base.WrappedTarget = wrappedTarget;
			this.Condition = condition;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00074F0A File Offset: 0x0007310A
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00074F12 File Offset: 0x00073112
		[RequiredParameter]
		[Display("Condition", "Condition must be met to forward Log events to wrapped target.", false, 0)]
		public ConditionExpression Condition { get; set; }

		// Token: 0x060014EE RID: 5358 RVA: 0x00074F1C File Offset: 0x0007311C
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			object obj = this.Condition.Evaluate(logEvent.LogEvent);
			if (FilteringTargetWrapper.boxedBooleanTrue.Equals(obj))
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
				return;
			}
			logEvent.Continuation(null);
		}

		// Token: 0x04000D6F RID: 3439
		private static readonly object boxedBooleanTrue = true;
	}
}
