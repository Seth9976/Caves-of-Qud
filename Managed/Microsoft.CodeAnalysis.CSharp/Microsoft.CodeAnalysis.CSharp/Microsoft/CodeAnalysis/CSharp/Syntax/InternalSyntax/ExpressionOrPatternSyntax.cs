using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200038C RID: 908
	internal abstract class ExpressionOrPatternSyntax : CSharpSyntaxNode
	{
		// Token: 0x060051BF RID: 20927 RVA: 0x00148BE6 File Offset: 0x00146DE6
		internal ExpressionOrPatternSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x00148BF1 File Offset: 0x00146DF1
		internal ExpressionOrPatternSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x00148BFA File Offset: 0x00146DFA
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected ExpressionOrPatternSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
