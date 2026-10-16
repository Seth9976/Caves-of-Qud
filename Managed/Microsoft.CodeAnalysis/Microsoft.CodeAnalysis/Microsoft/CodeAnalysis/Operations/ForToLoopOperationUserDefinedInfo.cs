using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Operations
{
	// Token: 0x02000465 RID: 1125
	[NullableContext(1)]
	[Nullable(0)]
	internal class ForToLoopOperationUserDefinedInfo
	{
		// Token: 0x06002C26 RID: 11302 RVA: 0x0007CDCC File Offset: 0x0007AFCC
		public ForToLoopOperationUserDefinedInfo(IBinaryOperation addition, IBinaryOperation subtraction, IOperation lessThanOrEqual, IOperation greaterThanOrEqual)
		{
			this.Addition = addition;
			this.Subtraction = subtraction;
			this.LessThanOrEqual = lessThanOrEqual;
			this.GreaterThanOrEqual = greaterThanOrEqual;
		}

		// Token: 0x04001183 RID: 4483
		public readonly IBinaryOperation Addition;

		// Token: 0x04001184 RID: 4484
		public readonly IBinaryOperation Subtraction;

		// Token: 0x04001185 RID: 4485
		public readonly IOperation LessThanOrEqual;

		// Token: 0x04001186 RID: 4486
		public readonly IOperation GreaterThanOrEqual;
	}
}
