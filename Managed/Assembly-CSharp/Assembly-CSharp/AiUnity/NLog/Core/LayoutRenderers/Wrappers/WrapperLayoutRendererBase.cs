using System;
using System.Text;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x02000261 RID: 609
	public abstract class WrapperLayoutRendererBase : LayoutRenderer
	{
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x00077F71 File Offset: 0x00076171
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x00077F79 File Offset: 0x00076179
		[DefaultParameter]
		[Preserve]
		public Layout Inner { get; set; }

		// Token: 0x06001629 RID: 5673 RVA: 0x00077F84 File Offset: 0x00076184
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = this.RenderInner(logEvent);
			builder.Append(this.Transform(text));
		}

		// Token: 0x0600162A RID: 5674
		protected abstract string Transform(string text);

		// Token: 0x0600162B RID: 5675 RVA: 0x00077FA7 File Offset: 0x000761A7
		protected virtual string RenderInner(LogEventInfo logEvent)
		{
			return this.Inner.Render(logEvent);
		}
	}
}
