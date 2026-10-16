using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000378 RID: 888
	internal abstract class NameSyntax : TypeSyntax
	{
		// Token: 0x060050A3 RID: 20643 RVA: 0x00146102 File Offset: 0x00144302
		internal NameSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x0014610D File Offset: 0x0014430D
		internal NameSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x00146116 File Offset: 0x00144316
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected NameSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
