using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x0200025F RID: 607
	[LayoutRenderer("whenEmpty", true)]
	[AmbientProperty("WhenEmpty")]
	[ThreadAgnostic]
	[Preserve]
	public sealed class WhenEmptyLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00077EDA File Offset: 0x000760DA
		// (set) Token: 0x0600161E RID: 5662 RVA: 0x00077EE2 File Offset: 0x000760E2
		[RequiredParameter]
		public Layout WhenEmpty { get; set; }

		// Token: 0x0600161F RID: 5663 RVA: 0x00077EEB File Offset: 0x000760EB
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00077EF0 File Offset: 0x000760F0
		protected override string RenderInner(LogEventInfo logEvent)
		{
			string text = base.RenderInner(logEvent);
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return this.WhenEmpty.Render(logEvent);
		}
	}
}
