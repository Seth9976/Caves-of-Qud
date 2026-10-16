using System;
using System.Linq.Expressions;
using XRL.World;

namespace Sheeter
{
	// Token: 0x02000312 RID: 786
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class IntProperty : BlueprintValueElement
	{
		// Token: 0x06001CEA RID: 7402 RVA: 0x0008DF90 File Offset: 0x0008C190
		public override string GetFrom(GameObjectBlueprint Blueprint)
		{
			int num;
			if (!Blueprint.IntProps.TryGetValue(this.Key, out num))
			{
				return null;
			}
			object value = this.Value;
			if (value is int)
			{
				int num2 = (int)value;
				if ((this.Operator == ExpressionType.Equal && num != num2) || (this.Operator == ExpressionType.NotEqual && num == num2) || (this.Operator == ExpressionType.GreaterThan && num <= num2) || (this.Operator == ExpressionType.GreaterThanOrEqual && num < num2) || (this.Operator == ExpressionType.LessThan && num >= num2) || (this.Operator == ExpressionType.LessThanOrEqual && num > num2))
				{
					return null;
				}
			}
			if (this.Output)
			{
				return num.ToString();
			}
			return "true";
		}
	}
}
