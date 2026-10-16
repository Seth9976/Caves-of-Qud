using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000459 RID: 1113
	internal abstract class BaseCrefParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005DF8 RID: 24056 RVA: 0x0016D768 File Offset: 0x0016B968
		internal BaseCrefParameterListSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005DF9 RID: 24057 RVA: 0x0016D773 File Offset: 0x0016B973
		internal BaseCrefParameterListSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005DFA RID: 24058 RVA: 0x0016D77C File Offset: 0x0016B97C
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseCrefParameterListSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06005DFB RID: 24059
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SeparatedSyntaxList<CrefParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
