using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200042D RID: 1069
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BaseTypeSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005B77 RID: 23415 RVA: 0x00165C6C File Offset: 0x00163E6C
		internal BaseTypeSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x00165C77 File Offset: 0x00163E77
		internal BaseTypeSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x00165C80 File Offset: 0x00163E80
		protected BaseTypeSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06005B7A RID: 23418
		public abstract TypeSyntax Type { get; }
	}
}
