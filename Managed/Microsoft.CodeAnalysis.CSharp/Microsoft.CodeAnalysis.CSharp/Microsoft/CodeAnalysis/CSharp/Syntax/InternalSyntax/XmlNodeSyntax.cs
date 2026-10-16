using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200045D RID: 1117
	internal abstract class XmlNodeSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005E2B RID: 24107 RVA: 0x0016DF54 File Offset: 0x0016C154
		internal XmlNodeSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x0016DF5F File Offset: 0x0016C15F
		internal XmlNodeSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005E2D RID: 24109 RVA: 0x0016DF68 File Offset: 0x0016C168
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected XmlNodeSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
