using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200039C RID: 924
	internal abstract class InstanceExpressionSyntax : ExpressionSyntax
	{
		// Token: 0x0600529F RID: 21151 RVA: 0x0014AE49 File Offset: 0x00149049
		internal InstanceExpressionSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x0014AE54 File Offset: 0x00149054
		internal InstanceExpressionSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x0014AE5D File Offset: 0x0014905D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected InstanceExpressionSyntax(ObjectReader reader)
			: base(reader)
		{
		}
	}
}
