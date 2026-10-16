using System;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000BB RID: 187
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal struct BoundPatternBinding
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00056CC1 File Offset: 0x00054EC1
		public BoundPatternBinding(BoundExpression variableAccess, BoundDagTemp tempContainingValue)
		{
			this.VariableAccess = variableAccess;
			this.TempContainingValue = tempContainingValue;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00056CD1 File Offset: 0x00054ED1
		public override string ToString()
		{
			return this.GetDebuggerDisplay();
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00056CDC File Offset: 0x00054EDC
		internal string GetDebuggerDisplay()
		{
			return string.Concat(new string[]
			{
				"(",
				this.VariableAccess.GetDebuggerDisplay(),
				" = ",
				this.TempContainingValue.GetDebuggerDisplay(),
				")"
			});
		}

		// Token: 0x0400055D RID: 1373
		public readonly BoundExpression VariableAccess;

		// Token: 0x0400055E RID: 1374
		public readonly BoundDagTemp TempContainingValue;
	}
}
