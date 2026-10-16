using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Filters
{
	// Token: 0x02000284 RID: 644
	[NLogConfigurationItem]
	[Preserve]
	public abstract class Filter
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x0007A060 File Offset: 0x00078260
		protected Filter()
		{
			this.Action = FilterResult.Neutral;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0007A06F File Offset: 0x0007826F
		// (set) Token: 0x0600170D RID: 5901 RVA: 0x0007A077 File Offset: 0x00078277
		[RequiredParameter]
		public FilterResult Action { get; set; }

		// Token: 0x0600170E RID: 5902 RVA: 0x0007A080 File Offset: 0x00078280
		internal FilterResult GetFilterResult(LogEventInfo logEvent)
		{
			return this.Check(logEvent);
		}

		// Token: 0x0600170F RID: 5903
		protected abstract FilterResult Check(LogEventInfo logEvent);
	}
}
