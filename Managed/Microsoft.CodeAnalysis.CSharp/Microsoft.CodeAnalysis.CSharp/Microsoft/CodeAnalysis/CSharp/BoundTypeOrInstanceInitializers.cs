using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D0 RID: 464
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTypeOrInstanceInitializers : BoundStatementList
	{
		// Token: 0x06001FF3 RID: 8179 RVA: 0x000A34FD File Offset: 0x000A16FD
		public BoundTypeOrInstanceInitializers(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: base(BoundKind.TypeOrInstanceInitializers, syntax, statements, hasErrors || statements.HasErrors<BoundStatement>())
		{
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000A3518 File Offset: 0x000A1718
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTypeOrInstanceInitializers(this);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000A3521 File Offset: 0x000A1721
		public new BoundTypeOrInstanceInitializers Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			if (statements != base.Statements)
			{
				BoundTypeOrInstanceInitializers boundTypeOrInstanceInitializers = new BoundTypeOrInstanceInitializers(this.Syntax, statements, base.HasErrors);
				boundTypeOrInstanceInitializers.CopyAttributes(this);
				return boundTypeOrInstanceInitializers;
			}
			return this;
		}
	}
}
