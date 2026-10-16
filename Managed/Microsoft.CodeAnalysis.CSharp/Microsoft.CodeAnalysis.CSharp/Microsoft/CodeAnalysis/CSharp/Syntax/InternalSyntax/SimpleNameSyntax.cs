using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000379 RID: 889
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SimpleNameSyntax : NameSyntax
	{
		// Token: 0x060050A6 RID: 20646 RVA: 0x0014611F File Offset: 0x0014431F
		internal SimpleNameSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x0014612A File Offset: 0x0014432A
		internal SimpleNameSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00146133 File Offset: 0x00144333
		protected SimpleNameSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x060050A9 RID: 20649
		public abstract SyntaxToken Identifier { get; }
	}
}
