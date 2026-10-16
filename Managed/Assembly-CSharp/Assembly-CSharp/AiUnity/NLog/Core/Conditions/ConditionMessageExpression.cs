using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A5 RID: 677
	internal sealed class ConditionMessageExpression : ConditionExpression
	{
		// Token: 0x060017EA RID: 6122 RVA: 0x0007D2E5 File Offset: 0x0007B4E5
		public override string ToString()
		{
			return "message";
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0007D2EC File Offset: 0x0007B4EC
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.FormattedMessage;
		}
	}
}
