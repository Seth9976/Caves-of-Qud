using System;
using System.Text;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000248 RID: 584
	[LayoutRenderer("event-properties", false)]
	[Preserve]
	public class EventPropertiesLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00076475 File Offset: 0x00074675
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x0007647D File Offset: 0x0007467D
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x06001572 RID: 5490 RVA: 0x00076488 File Offset: 0x00074688
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.Properties.TryGetValue(this.Item, out obj))
			{
				builder.Append(Convert.ToString(obj));
			}
		}
	}
}
