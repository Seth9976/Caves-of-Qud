using System;
using System.Linq.Expressions;
using XRL.World;

namespace Sheeter
{
	// Token: 0x0200030F RID: 783
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class Tag : BlueprintValueElement
	{
		// Token: 0x06001CE4 RID: 7396 RVA: 0x0008DE84 File Offset: 0x0008C084
		public override string GetFrom(GameObjectBlueprint Blueprint)
		{
			string text;
			if (!Blueprint.Tags.TryGetValue(this.Key, out text))
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
