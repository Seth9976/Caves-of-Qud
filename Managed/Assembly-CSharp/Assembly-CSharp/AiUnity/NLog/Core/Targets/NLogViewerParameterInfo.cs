using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000222 RID: 546
	[NLogConfigurationItem]
	[Preserve]
	public class NLogViewerParameterInfo
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00072CBC File Offset: 0x00070EBC
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x00072CC4 File Offset: 0x00070EC4
		[RequiredParameter]
		public string Name { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00072CCD File Offset: 0x00070ECD
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x00072CD5 File Offset: 0x00070ED5
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
