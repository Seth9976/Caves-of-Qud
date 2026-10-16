using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Filters
{
	// Token: 0x02000287 RID: 647
	[Preserve]
	public abstract class LayoutBasedFilter : Filter
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0007A09A File Offset: 0x0007829A
		// (set) Token: 0x06001713 RID: 5907 RVA: 0x0007A0A2 File Offset: 0x000782A2
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
