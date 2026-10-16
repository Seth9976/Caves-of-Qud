using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000451 RID: 1105
	internal abstract class CrefSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005D95 RID: 23957 RVA: 0x0016C8BC File Offset: 0x0016AABC
		internal CrefSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005D96 RID: 23958 RVA: 0x0016C8C7 File Offset: 0x0016AAC7
		internal CrefSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005D97 RID: 23959 RVA: 0x0016C8D0 File Offset: 0x0016AAD0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected CrefSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
