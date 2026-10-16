using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200043A RID: 1082
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BaseMethodDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005C1E RID: 23582 RVA: 0x00167511 File Offset: 0x00165711
		internal BaseMethodDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x0016751C File Offset: 0x0016571C
		internal BaseMethodDeclarationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x00167525 File Offset: 0x00165725
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseMethodDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06005C21 RID: 23585
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract ParameterListSyntax ParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06005C22 RID: 23586
		public abstract BlockSyntax Body { get; }

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x06005C23 RID: 23587
		public abstract ArrowExpressionClauseSyntax ExpressionBody { get; }

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x06005C24 RID: 23588
		public abstract SyntaxToken SemicolonToken { get; }
	}
}
