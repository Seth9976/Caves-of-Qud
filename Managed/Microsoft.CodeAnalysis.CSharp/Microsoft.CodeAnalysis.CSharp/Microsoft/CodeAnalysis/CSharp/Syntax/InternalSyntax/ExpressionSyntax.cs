using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200038D RID: 909
	internal abstract class ExpressionSyntax : ExpressionOrPatternSyntax
	{
		// Token: 0x060051C2 RID: 20930 RVA: 0x00148C03 File Offset: 0x00146E03
		internal ExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x00148C0E File Offset: 0x00146E0E
		internal ExpressionSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x00148C17 File Offset: 0x00146E17
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected ExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
