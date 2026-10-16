using System;
using System.Text;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x0200024F RID: 591
	[LayoutRenderer("literal", false)]
	[ThreadAgnostic]
	[AppDomainFixedOutput]
	[Preserve]
	public class LiteralLayoutRenderer : LayoutRenderer
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00076EA0 File Offset: 0x000750A0
		public LiteralLayoutRenderer()
		{
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00076EA8 File Offset: 0x000750A8
		public LiteralLayoutRenderer(string text)
		{
			this.Text = text;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00076EB7 File Offset: 0x000750B7
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x00076EBF File Offset: 0x000750BF
		public string Text { get; set; }

		// Token: 0x060015B2 RID: 5554 RVA: 0x00076EC8 File Offset: 0x000750C8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.Text);
		}
	}
}
