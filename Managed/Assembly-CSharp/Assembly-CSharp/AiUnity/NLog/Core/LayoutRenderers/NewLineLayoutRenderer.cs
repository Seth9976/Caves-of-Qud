using System;
using System.Text;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000255 RID: 597
	[LayoutRenderer("newline", false)]
	[Preserve]
	public class NewLineLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060015E0 RID: 5600 RVA: 0x000777DE File Offset: 0x000759DE
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(Environment.NewLine);
		}
	}
}
