using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003EF RID: 1007
	internal abstract class VariableDesignationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005760 RID: 22368 RVA: 0x00157C41 File Offset: 0x00155E41
		internal VariableDesignationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x00157C4C File Offset: 0x00155E4C
		internal VariableDesignationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x00157C55 File Offset: 0x00155E55
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected VariableDesignationSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
