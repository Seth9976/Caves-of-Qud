using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Conditions;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x02000260 RID: 608
	[LayoutRenderer("when", true)]
	[AmbientProperty("When")]
	[ThreadAgnostic]
	[Preserve]
	public sealed class WhenLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x00077F23 File Offset: 0x00076123
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x00077F2B File Offset: 0x0007612B
		[RequiredParameter]
		public ConditionExpression When { get; set; }

		// Token: 0x06001624 RID: 5668 RVA: 0x00077F34 File Offset: 0x00076134
		protected override string Transform(string text)
		{
			return text;
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00077F38 File Offset: 0x00076138
		protected override string RenderInner(LogEventInfo logEvent)
		{
			if (true.Equals(this.When.Evaluate(logEvent)))
			{
				return base.RenderInner(logEvent);
			}
			return string.Empty;
		}
	}
}
