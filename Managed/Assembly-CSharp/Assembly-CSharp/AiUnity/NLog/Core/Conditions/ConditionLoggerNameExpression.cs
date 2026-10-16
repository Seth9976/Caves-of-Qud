using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A4 RID: 676
	internal sealed class ConditionLoggerNameExpression : ConditionExpression
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x0007D2CE File Offset: 0x0007B4CE
		public override string ToString()
		{
			return "logger";
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0007D2D5 File Offset: 0x0007B4D5
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.LoggerName;
		}
	}
}
