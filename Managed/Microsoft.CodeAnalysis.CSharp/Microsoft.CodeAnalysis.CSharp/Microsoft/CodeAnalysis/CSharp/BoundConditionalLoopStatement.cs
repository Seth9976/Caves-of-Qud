using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200019A RID: 410
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundConditionalLoopStatement : BoundLoopStatement
	{
		// Token: 0x06001EC7 RID: 7879 RVA: 0x000A1434 File Offset: 0x0009F634
		protected BoundConditionalLoopStatement(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression condition, BoundStatement body, GeneratedLabelSymbol breakLabel, GeneratedLabelSymbol continueLabel, bool hasErrors = false)
			: base(kind, syntax, breakLabel, continueLabel, hasErrors)
		{
			this.Locals = locals;
			this.Condition = condition;
			this.Body = body;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x000A145B File Offset: 0x0009F65B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001EC9 RID: 7881 RVA: 0x000A1463 File Offset: 0x0009F663
		public BoundExpression Condition { get; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x000A146B File Offset: 0x0009F66B
		public BoundStatement Body { get; }
	}
}
