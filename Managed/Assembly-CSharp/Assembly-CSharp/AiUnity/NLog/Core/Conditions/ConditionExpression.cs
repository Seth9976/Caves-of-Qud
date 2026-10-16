using System;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;

namespace AiUnity.NLog.Core.Conditions
{
	// Token: 0x020002A0 RID: 672
	[NLogConfigurationItem]
	[ThreadAgnostic]
	public abstract class ConditionExpression
	{
		// Token: 0x060017D5 RID: 6101 RVA: 0x0007D1E2 File Offset: 0x0007B3E2
		public static implicit operator ConditionExpression(string conditionExpressionText)
		{
			return ConditionParser.ParseExpression(conditionExpressionText);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0007D1EC File Offset: 0x0007B3EC
		public object Evaluate(LogEventInfo context)
		{
			object obj;
			try
			{
				obj = this.EvaluateNode(context);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				throw new ConditionEvaluationException("Exception occurred when evaluating condition", ex);
			}
			return obj;
		}

		// Token: 0x060017D7 RID: 6103
		public abstract override string ToString();

		// Token: 0x060017D8 RID: 6104
		protected abstract object EvaluateNode(LogEventInfo context);
	}
}
