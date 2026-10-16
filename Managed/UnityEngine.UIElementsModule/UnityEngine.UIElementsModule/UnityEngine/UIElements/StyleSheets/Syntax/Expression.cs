using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020006F5 RID: 1781
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class Expression
	{
		// Token: 0x06003375 RID: 13173 RVA: 0x000C8B79 File Offset: 0x000C6D79
		public Expression(ExpressionType type)
		{
			this.type = type;
			this.combinator = ExpressionCombinator.None;
			this.multiplier = new ExpressionMultiplier(ExpressionMultiplierType.None);
			this.subExpressions = null;
			this.keyword = null;
		}

		// Token: 0x04001A41 RID: 6721
		public ExpressionType type;

		// Token: 0x04001A42 RID: 6722
		public ExpressionMultiplier multiplier;

		// Token: 0x04001A43 RID: 6723
		public DataType dataType;

		// Token: 0x04001A44 RID: 6724
		public ExpressionCombinator combinator;

		// Token: 0x04001A45 RID: 6725
		public Expression[] subExpressions;

		// Token: 0x04001A46 RID: 6726
		public string keyword;
	}
}
