using System;
using AiUnity.NLog.Core.Layouts;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A1 RID: 673
	internal sealed class ConditionLayoutExpression : ConditionExpression
	{
		// Token: 0x060017DA RID: 6106 RVA: 0x0007D234 File Offset: 0x0007B434
		public ConditionLayoutExpression(Layout layout)
		{
			this.Layout = layout;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0007D243 File Offset: 0x0007B443
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x0007D24B File Offset: 0x0007B44B
		public Layout Layout { get; private set; }

		// Token: 0x060017DD RID: 6109 RVA: 0x0007D254 File Offset: 0x0007B454
		public override string ToString()
		{
			return this.Layout.ToString();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0007D261 File Offset: 0x0007B461
		protected override object EvaluateNode(LogEventInfo context)
		{
			return this.Layout.Render(context);
		}
	}
}
