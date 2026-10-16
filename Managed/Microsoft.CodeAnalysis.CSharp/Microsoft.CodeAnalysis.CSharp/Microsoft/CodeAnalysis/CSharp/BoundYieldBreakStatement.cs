using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000CC RID: 204
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundYieldBreakStatement : BoundStatement
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x0005DA7F File Offset: 0x0005BC7F
		public static BoundYieldBreakStatement Synthesized(SyntaxNode syntax, bool hasErrors = false)
		{
			return new BoundYieldBreakStatement(syntax, hasErrors)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0005DA8F File Offset: 0x0005BC8F
		public BoundYieldBreakStatement(SyntaxNode syntax, bool hasErrors)
			: base(BoundKind.YieldBreakStatement, syntax, hasErrors)
		{
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0005DA9B File Offset: 0x0005BC9B
		public BoundYieldBreakStatement(SyntaxNode syntax)
			: base(BoundKind.YieldBreakStatement, syntax)
		{
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0005DAA6 File Offset: 0x0005BCA6
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitYieldBreakStatement(this);
		}
	}
}
