using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000431 RID: 1073
	internal abstract class TypeParameterConstraintSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005BA9 RID: 23465 RVA: 0x00166384 File Offset: 0x00164584
		internal TypeParameterConstraintSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x0016638F File Offset: 0x0016458F
		internal TypeParameterConstraintSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x00166398 File Offset: 0x00164598
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected TypeParameterConstraintSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
