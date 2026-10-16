using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003C2 RID: 962
	internal abstract class SelectOrGroupClauseSyntax : CSharpSyntaxNode
	{
		// Token: 0x060054C5 RID: 21701 RVA: 0x00150D0E File Offset: 0x0014EF0E
		internal SelectOrGroupClauseSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060054C6 RID: 21702 RVA: 0x00150D19 File Offset: 0x0014EF19
		internal SelectOrGroupClauseSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060054C7 RID: 21703 RVA: 0x00150D22 File Offset: 0x0014EF22
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected SelectOrGroupClauseSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
