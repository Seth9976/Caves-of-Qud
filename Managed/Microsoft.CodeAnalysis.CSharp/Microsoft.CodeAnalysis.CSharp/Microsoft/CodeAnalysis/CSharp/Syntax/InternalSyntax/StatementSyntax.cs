using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003E8 RID: 1000
	internal abstract class StatementSyntax : CSharpSyntaxNode
	{
		// Token: 0x060056F3 RID: 22259 RVA: 0x001565B5 File Offset: 0x001547B5
		internal StatementSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060056F4 RID: 22260 RVA: 0x001565C0 File Offset: 0x001547C0
		internal StatementSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060056F5 RID: 22261 RVA: 0x001565C9 File Offset: 0x001547C9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected StatementSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060056F6 RID: 22262
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
