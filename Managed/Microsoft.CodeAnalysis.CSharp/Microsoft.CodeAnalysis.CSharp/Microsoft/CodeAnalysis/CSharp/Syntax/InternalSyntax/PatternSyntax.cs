using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003D4 RID: 980
	internal abstract class PatternSyntax : ExpressionOrPatternSyntax
	{
		// Token: 0x060055D9 RID: 21977 RVA: 0x00153B85 File Offset: 0x00151D85
		internal PatternSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060055DA RID: 21978 RVA: 0x00153B90 File Offset: 0x00151D90
		internal PatternSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060055DB RID: 21979 RVA: 0x00153B99 File Offset: 0x00151D99
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected PatternSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
