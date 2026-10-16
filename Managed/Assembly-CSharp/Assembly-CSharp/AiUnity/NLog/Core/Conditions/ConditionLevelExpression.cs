using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A2 RID: 674
	internal sealed class ConditionLevelExpression : ConditionExpression
	{
		// Token: 0x060017DF RID: 6111 RVA: 0x0007D26F File Offset: 0x0007B46F
		public override string ToString()
		{
			return "level";
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0007D276 File Offset: 0x0007B476
		protected override object EvaluateNode(LogEventInfo context)
		{
			return context.Level;
		}
	}
}
