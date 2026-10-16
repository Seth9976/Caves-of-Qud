using System;
using System.Linq.Expressions;

namespace Sheeter
{
	// Token: 0x0200030C RID: 780
	public class BlueprintValueElement : BlueprintElement
	{
		// Token: 0x04000F54 RID: 3924
		public object Value;

		// Token: 0x04000F55 RID: 3925
		public bool Output;

		// Token: 0x04000F56 RID: 3926
		public ExpressionType Operator = ExpressionType.Equal;
	}
}
