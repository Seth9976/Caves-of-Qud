using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x0200029E RID: 670
	internal sealed class ConditionAndExpression : ConditionExpression
	{
		// Token: 0x060017CA RID: 6090 RVA: 0x0007D0E4 File Offset: 0x0007B2E4
		public ConditionAndExpression(ConditionExpression left, ConditionExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0007D0FA File Offset: 0x0007B2FA
		// (set) Token: 0x060017CC RID: 6092 RVA: 0x0007D102 File Offset: 0x0007B302
		public ConditionExpression Left { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0007D10B File Offset: 0x0007B30B
		// (set) Token: 0x060017CE RID: 6094 RVA: 0x0007D113 File Offset: 0x0007B313
		public ConditionExpression Right { get; private set; }

		// Token: 0x060017CF RID: 6095 RVA: 0x0007D11C File Offset: 0x0007B31C
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "(";
			int num = 1;
			ConditionExpression left = this.Left;
			array[num] = ((left != null) ? left.ToString() : null);
			array[2] = " and ";
			int num2 = 3;
			ConditionExpression right = this.Right;
			array[num2] = ((right != null) ? right.ToString() : null);
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0007D176 File Offset: 0x0007B376
		protected override object EvaluateNode(LogEventInfo context)
		{
			if (!(bool)this.Left.Evaluate(context))
			{
				return ConditionAndExpression.boxedFalse;
			}
			if (!(bool)this.Right.Evaluate(context))
			{
				return ConditionAndExpression.boxedFalse;
			}
			return ConditionAndExpression.boxedTrue;
		}

		// Token: 0x04000E38 RID: 3640
		private static readonly object boxedFalse = false;

		// Token: 0x04000E39 RID: 3641
		private static readonly object boxedTrue = true;
	}
}
