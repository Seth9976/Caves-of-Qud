using System;
using System.Linq.Expressions;
using XRL.World;

namespace Sheeter
{
	// Token: 0x02000311 RID: 785
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class StringProperty : BlueprintValueElement
	{
		// Token: 0x06001CE8 RID: 7400 RVA: 0x0008DF1C File Offset: 0x0008C11C
		public override string GetFrom(GameObjectBlueprint Blueprint)
		{
			string text;
			if (!Blueprint.Props.TryGetValue(this.Key, out text))
			{
				return null;
			}
			string text2 = this.Value as string;
			if (text2 != null && ((this.Operator == ExpressionType.Equal && text != text2) || (this.Operator == ExpressionType.NotEqual && text == text2)))
			{
				return null;
			}
			if (this.Output)
			{
				return text;
			}
			return "true";
		}
	}
}
