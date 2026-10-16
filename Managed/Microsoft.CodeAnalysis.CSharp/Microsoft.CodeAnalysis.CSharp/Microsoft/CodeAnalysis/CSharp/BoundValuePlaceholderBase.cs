using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000165 RID: 357
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundValuePlaceholderBase : BoundExpression
	{
		// Token: 0x06001DAC RID: 7596 RVA: 0x0009FAEC File Offset: 0x0009DCEC
		protected BoundValuePlaceholderBase(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors)
			: base(kind, syntax, type, hasErrors)
		{
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0009FAF9 File Offset: 0x0009DCF9
		protected BoundValuePlaceholderBase(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
			: base(kind, syntax, type)
		{
		}
	}
}
