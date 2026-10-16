using System;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002AE RID: 686
	internal sealed class ConditionRelationalExpression : ConditionExpression
	{
		// Token: 0x06001819 RID: 6169 RVA: 0x0007DD34 File Offset: 0x0007BF34
		public ConditionRelationalExpression(ConditionExpression leftExpression, ConditionExpression rightExpression, ConditionRelationalOperator relationalOperator)
		{
			this.LeftExpression = leftExpression;
			this.RightExpression = rightExpression;
			this.RelationalOperator = relationalOperator;
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0007DD51 File Offset: 0x0007BF51
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x0007DD59 File Offset: 0x0007BF59
		public ConditionExpression LeftExpression { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0007DD62 File Offset: 0x0007BF62
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x0007DD6A File Offset: 0x0007BF6A
		public ConditionExpression RightExpression { get; private set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0007DD73 File Offset: 0x0007BF73
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x0007DD7B File Offset: 0x0007BF7B
		public ConditionRelationalOperator RelationalOperator { get; private set; }

		// Token: 0x06001820 RID: 6176 RVA: 0x0007DD84 File Offset: 0x0007BF84
		public override string ToString()
		{
			string[] array = new string[7];
			array[0] = "(";
			int num = 1;
			ConditionExpression leftExpression = this.LeftExpression;
			array[num] = ((leftExpression != null) ? leftExpression.ToString() : null);
			array[2] = " ";
			array[3] = this.GetOperatorString();
			array[4] = " ";
			int num2 = 5;
			ConditionExpression rightExpression = this.RightExpression;
			array[num2] = ((rightExpression != null) ? rightExpression.ToString() : null);
			array[6] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x0007DDF0 File Offset: 0x0007BFF0
		protected override object EvaluateNode(LogEventInfo context)
		{
			object obj = this.LeftExpression.Evaluate(context);
			object obj2 = this.RightExpression.Evaluate(context);
			return ConditionRelationalExpression.Compare(obj, obj2, this.RelationalOperator);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x0007DE24 File Offset: 0x0007C024
		private static object Compare(object leftValue, object rightValue, ConditionRelationalOperator relationalOperator)
		{
			StringComparer invariantCulture = StringComparer.InvariantCulture;
			ConditionRelationalExpression.PromoteTypes(ref leftValue, ref rightValue);
			switch (relationalOperator)
			{
			case ConditionRelationalOperator.Equal:
				return invariantCulture.Compare(leftValue, rightValue) == 0;
			case ConditionRelationalOperator.NotEqual:
				return invariantCulture.Compare(leftValue, rightValue) != 0;
			case ConditionRelationalOperator.Less:
				return invariantCulture.Compare(leftValue, rightValue) < 0;
			case ConditionRelationalOperator.Greater:
				return invariantCulture.Compare(leftValue, rightValue) > 0;
			case ConditionRelationalOperator.LessOrEqual:
				return invariantCulture.Compare(leftValue, rightValue) <= 0;
			case ConditionRelationalOperator.GreaterOrEqual:
				return invariantCulture.Compare(leftValue, rightValue) >= 0;
			default:
				throw new NotSupportedException("Relational operator " + relationalOperator.ToString() + " is not supported.");
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0007DEF0 File Offset: 0x0007C0F0
		private static void PromoteTypes(ref object val1, ref object val2)
		{
			if (val1 == null || val2 == null)
			{
				return;
			}
			if (val1.GetType() == val2.GetType())
			{
				return;
			}
			if (val1 is DateTime || val2 is DateTime)
			{
				val1 = Convert.ToDateTime(val1);
				val2 = Convert.ToDateTime(val2);
				return;
			}
			if (val1 is string || val2 is string)
			{
				val1 = Convert.ToString(val1);
				val2 = Convert.ToString(val2);
				return;
			}
			if (val1 is double || val2 is double)
			{
				val1 = Convert.ToDouble(val1);
				val2 = Convert.ToDouble(val2);
				return;
			}
			if (val1 is float || val2 is float)
			{
				val1 = Convert.ToSingle(val1);
				val2 = Convert.ToSingle(val2);
				return;
			}
			if (val1 is decimal || val2 is decimal)
			{
				val1 = Convert.ToDecimal(val1);
				val2 = Convert.ToDecimal(val2);
				return;
			}
			if (val1 is long || val2 is long)
			{
				val1 = Convert.ToInt64(val1);
				val2 = Convert.ToInt64(val2);
				return;
			}
			if (val1 is int || val2 is int)
			{
				val1 = Convert.ToInt32(val1);
				val2 = Convert.ToInt32(val2);
				return;
			}
			if (val1 is bool || val2 is bool)
			{
				val1 = Convert.ToBoolean(val1);
				val2 = Convert.ToBoolean(val2);
				return;
			}
			throw new ConditionEvaluationException(string.Concat(new string[]
			{
				"Cannot find common type for '",
				val1.GetType().Name,
				"' and '",
				val2.GetType().Name,
				"'."
			}));
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x0007E0D0 File Offset: 0x0007C2D0
		private string GetOperatorString()
		{
			switch (this.RelationalOperator)
			{
			case ConditionRelationalOperator.Equal:
				return "==";
			case ConditionRelationalOperator.NotEqual:
				return "!=";
			case ConditionRelationalOperator.Less:
				return "<";
			case ConditionRelationalOperator.Greater:
				return ">";
			case ConditionRelationalOperator.LessOrEqual:
				return "<=";
			case ConditionRelationalOperator.GreaterOrEqual:
				return ">=";
			default:
				throw new NotSupportedException("Relational operator " + this.RelationalOperator.ToString() + " is not supported.");
			}
		}
	}
}
