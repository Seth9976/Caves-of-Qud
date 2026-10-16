using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002AB RID: 683
	internal sealed class ConditionOrExpression : ConditionExpression
	{
		// Token: 0x06001802 RID: 6146 RVA: 0x0007D697 File Offset: 0x0007B897
		public ConditionOrExpression(ConditionExpression left, ConditionExpression right)
		{
			this.LeftExpression = left;
			this.RightExpression = right;
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0007D6AD File Offset: 0x0007B8AD
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x0007D6B5 File Offset: 0x0007B8B5
		public ConditionExpression LeftExpression { get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0007D6BE File Offset: 0x0007B8BE
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x0007D6C6 File Offset: 0x0007B8C6
		public ConditionExpression RightExpression { get; private set; }

		// Token: 0x06001807 RID: 6151 RVA: 0x0007D6D0 File Offset: 0x0007B8D0
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "(";
			int num = 1;
			ConditionExpression leftExpression = this.LeftExpression;
			array[num] = ((leftExpression != null) ? leftExpression.ToString() : null);
			array[2] = " or ";
			int num2 = 3;
			ConditionExpression rightExpression = this.RightExpression;
			array[num2] = ((rightExpression != null) ? rightExpression.ToString() : null);
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0007D72A File Offset: 0x0007B92A
		protected override object EvaluateNode(LogEventInfo context)
		{
			if ((bool)this.LeftExpression.Evaluate(context))
			{
				return ConditionOrExpression.boxedTrue;
			}
			if ((bool)this.RightExpression.Evaluate(context))
			{
				return ConditionOrExpression.boxedTrue;
			}
			return ConditionOrExpression.boxedFalse;
		}

		// Token: 0x04000E43 RID: 3651
		private static readonly object boxedFalse = false;

		// Token: 0x04000E44 RID: 3652
		private static readonly object boxedTrue = true;
	}
}
