using System;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x0200024E RID: 590
	[LayoutRenderer("level", false)]
	[ThreadAgnostic]
	[Preserve]
	public class LevelLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060015AC RID: 5548 RVA: 0x00076E70 File Offset: 0x00075070
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(logEvent.Level.ToString());
		}
	}
}
