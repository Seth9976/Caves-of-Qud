using System;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000254 RID: 596
	[LayoutRenderer("message", false)]
	[ThreadAgnostic]
	[Preserve]
	public class MessageLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060015DA RID: 5594 RVA: 0x0007776B File Offset: 0x0007596B
		public MessageLayoutRenderer()
		{
			this.ExceptionSeparator = Environment.NewLine;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x0007777E File Offset: 0x0007597E
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x00077786 File Offset: 0x00075986
		public bool WithException { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0007778F File Offset: 0x0007598F
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x00077797 File Offset: 0x00075997
		public string ExceptionSeparator { get; set; }

		// Token: 0x060015DF RID: 5599 RVA: 0x000777A0 File Offset: 0x000759A0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(logEvent.FormattedMessage);
			if (this.WithException && logEvent.Exception != null)
			{
				builder.Append(this.ExceptionSeparator);
				builder.Append(logEvent.Exception.ToString());
			}
		}
	}
}
