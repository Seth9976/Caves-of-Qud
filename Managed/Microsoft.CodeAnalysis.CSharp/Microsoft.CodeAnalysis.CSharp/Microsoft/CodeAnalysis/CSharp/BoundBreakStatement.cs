using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000195 RID: 405
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBreakStatement : BoundStatement
	{
		// Token: 0x06001EAC RID: 7852 RVA: 0x000A11BF File Offset: 0x0009F3BF
		public BoundBreakStatement(SyntaxNode syntax, GeneratedLabelSymbol label, bool hasErrors)
			: base(BoundKind.BreakStatement, syntax, hasErrors)
		{
			this.Label = label;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000A11D2 File Offset: 0x0009F3D2
		public BoundBreakStatement(SyntaxNode syntax, GeneratedLabelSymbol label)
			: base(BoundKind.BreakStatement, syntax)
		{
			this.Label = label;
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x000A11E4 File Offset: 0x0009F3E4
		public GeneratedLabelSymbol Label { get; }

		// Token: 0x06001EAF RID: 7855 RVA: 0x000A11EC File Offset: 0x0009F3EC
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBreakStatement(this);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000A11F5 File Offset: 0x0009F3F5
		public BoundBreakStatement Update(GeneratedLabelSymbol label)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(label, this.Label))
			{
				BoundBreakStatement boundBreakStatement = new BoundBreakStatement(this.Syntax, label, base.HasErrors);
				boundBreakStatement.CopyAttributes(this);
				return boundBreakStatement;
			}
			return this;
		}
	}
}
