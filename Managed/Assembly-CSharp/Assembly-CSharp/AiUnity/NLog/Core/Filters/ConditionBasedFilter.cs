using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Conditions;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Filters
{
	// Token: 0x02000283 RID: 643
	[Filter("when")]
	[Preserve]
	public class ConditionBasedFilter : Filter
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0007A00B File Offset: 0x0007820B
		// (set) Token: 0x06001707 RID: 5895 RVA: 0x0007A013 File Offset: 0x00078213
		[RequiredParameter]
		public ConditionExpression Condition { get; set; }

		// Token: 0x06001708 RID: 5896 RVA: 0x0007A01C File Offset: 0x0007821C
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			object obj = this.Condition.Evaluate(logEvent);
			if (ConditionBasedFilter.boxedTrue.Equals(obj))
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}

		// Token: 0x04000DFF RID: 3583
		private static readonly object boxedTrue = true;
	}
}
