using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A3 RID: 675
	internal sealed class ConditionLiteralExpression : ConditionExpression
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x0007D28B File Offset: 0x0007B48B
		public ConditionLiteralExpression(object literalValue)
		{
			this.LiteralValue = literalValue;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0007D29A File Offset: 0x0007B49A
		// (set) Token: 0x060017E4 RID: 6116 RVA: 0x0007D2A2 File Offset: 0x0007B4A2
		public object LiteralValue { get; private set; }

		// Token: 0x060017E5 RID: 6117 RVA: 0x0007D2AB File Offset: 0x0007B4AB
		public override string ToString()
		{
			if (this.LiteralValue == null)
			{
				return "null";
			}
			return Convert.ToString(this.LiteralValue);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0007D2C6 File Offset: 0x0007B4C6
		protected override object EvaluateNode(LogEventInfo context)
		{
			return this.LiteralValue;
		}
	}
}
