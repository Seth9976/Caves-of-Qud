using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x020003B2 RID: 946
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class LambdaExpressionSyntax : AnonymousFunctionExpressionSyntax
	{
		// Token: 0x060053DE RID: 21470 RVA: 0x0014E34D File Offset: 0x0014C54D
		internal LambdaExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060053DF RID: 21471 RVA: 0x0014E358 File Offset: 0x0014C558
		internal LambdaExpressionSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060053E0 RID: 21472 RVA: 0x0014E361 File Offset: 0x0014C561
		protected LambdaExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001350 RID: 4944
		// (get) Token: 0x060053E1 RID: 21473
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17001351 RID: 4945
		// (get) Token: 0x060053E2 RID: 21474
		public abstract SyntaxToken ArrowToken { get; }
	}
}
