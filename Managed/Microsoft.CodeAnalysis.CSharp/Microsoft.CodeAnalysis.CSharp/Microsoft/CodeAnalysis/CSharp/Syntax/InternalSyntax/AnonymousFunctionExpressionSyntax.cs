using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B0 RID: 944
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class AnonymousFunctionExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x060053C6 RID: 21446 RVA: 0x0014DF21 File Offset: 0x0014C121
		internal AnonymousFunctionExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060053C7 RID: 21447 RVA: 0x0014DF2C File Offset: 0x0014C12C
		internal AnonymousFunctionExpressionSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060053C8 RID: 21448 RVA: 0x0014DF35 File Offset: 0x0014C135
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected AnonymousFunctionExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x060053C9 RID: 21449
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x060053CA RID: 21450
		public abstract BlockSyntax Block { get; }

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x060053CB RID: 21451
		public abstract ExpressionSyntax ExpressionBody { get; }
	}
}
