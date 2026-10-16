using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F1 RID: 241
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class PseudoVariableExpressions
	{
		// Token: 0x060012B9 RID: 4793
		internal abstract BoundExpression GetValue(BoundPseudoVariable variable, DiagnosticBag diagnostics);

		// Token: 0x060012BA RID: 4794
		internal abstract BoundExpression GetAddress(BoundPseudoVariable variable);
	}
}
