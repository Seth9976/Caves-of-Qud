using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B7 RID: 951
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BaseObjectCreationExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x06005428 RID: 21544 RVA: 0x0014F1E2 File Offset: 0x0014D3E2
		internal BaseObjectCreationExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x0014F1ED File Offset: 0x0014D3ED
		internal BaseObjectCreationExpressionSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x0014F1F6 File Offset: 0x0014D3F6
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseObjectCreationExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x0600542B RID: 21547
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract SyntaxToken NewKeyword
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x0600542C RID: 21548
		public abstract ArgumentListSyntax ArgumentList { get; }

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x0600542D RID: 21549
		public abstract InitializerExpressionSyntax Initializer { get; }
	}
}
