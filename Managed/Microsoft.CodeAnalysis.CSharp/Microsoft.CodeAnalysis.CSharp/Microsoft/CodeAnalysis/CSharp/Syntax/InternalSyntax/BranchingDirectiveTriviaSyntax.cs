using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200046C RID: 1132
	internal abstract class BranchingDirectiveTriviaSyntax : DirectiveTriviaSyntax
	{
		// Token: 0x06005F0A RID: 24330 RVA: 0x00170602 File Offset: 0x0016E802
		internal BranchingDirectiveTriviaSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x0017060D File Offset: 0x0016E80D
		internal BranchingDirectiveTriviaSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x00170616 File Offset: 0x0016E816
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BranchingDirectiveTriviaSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06005F0D RID: 24333
		public abstract bool BranchTaken { get; }
	}
}
