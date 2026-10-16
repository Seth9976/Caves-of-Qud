using System;
using System.ComponentModel;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000251 RID: 593
	[LayoutRenderer("logger", false)]
	[ThreadAgnostic]
	[Preserve]
	public class LoggerNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0007750E File Offset: 0x0007570E
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00077516 File Offset: 0x00075716
		[DefaultValue(false)]
		public bool ShortName { get; set; }

		// Token: 0x060015CC RID: 5580 RVA: 0x00077520 File Offset: 0x00075720
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (!this.ShortName)
			{
				builder.Append(logEvent.LoggerName);
				return;
			}
			int num = logEvent.LoggerName.LastIndexOf('.');
			if (num < 0)
			{
				builder.Append(logEvent.LoggerName);
				return;
			}
			builder.Append(logEvent.LoggerName.Substring(num + 1));
		}
	}
}
