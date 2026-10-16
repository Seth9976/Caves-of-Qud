using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002AA RID: 682
	internal sealed class ConditionNotExpression : ConditionExpression
	{
		// Token: 0x060017FD RID: 6141 RVA: 0x0007D639 File Offset: 0x0007B839
		public ConditionNotExpression(ConditionExpression expression)
		{
			this.Expression = expression;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0007D648 File Offset: 0x0007B848
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x0007D650 File Offset: 0x0007B850
		public ConditionExpression Expression { get; private set; }

		// Token: 0x06001800 RID: 6144 RVA: 0x0007D659 File Offset: 0x0007B859
		public override string ToString()
		{
			string text = "(not ";
			ConditionExpression expression = this.Expression;
			return text + ((expression != null) ? expression.ToString() : null) + ")";
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0007D67C File Offset: 0x0007B87C
		protected override object EvaluateNode(LogEventInfo context)
		{
			return !(bool)this.Expression.Evaluate(context);
		}
	}
}
