using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E2 RID: 994
	internal abstract class InterpolatedStringContentSyntax : CSharpSyntaxNode
	{
		// Token: 0x060056A2 RID: 22178 RVA: 0x0015590D File Offset: 0x00153B0D
		internal InterpolatedStringContentSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x00155918 File Offset: 0x00153B18
		internal InterpolatedStringContentSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x00155921 File Offset: 0x00153B21
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected InterpolatedStringContentSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
