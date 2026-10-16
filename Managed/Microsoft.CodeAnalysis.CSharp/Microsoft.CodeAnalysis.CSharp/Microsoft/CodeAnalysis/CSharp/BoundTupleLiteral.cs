using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C2 RID: 450
	internal sealed class BoundTupleLiteral : BoundTupleExpression
	{
		// Token: 0x06001FAC RID: 8108 RVA: 0x000A2D35 File Offset: 0x000A0F35
		public BoundTupleLiteral([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> argumentNamesOpt, ImmutableArray<bool> inferredNamesOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.TupleLiteral, syntax, arguments, argumentNamesOpt, inferredNamesOpt, type, hasErrors || arguments.HasErrors<BoundExpression>())
		{
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x000A2D56 File Offset: 0x000A0F56
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x000A2D5E File Offset: 0x000A0F5E
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTupleLiteral(this);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x000A2D68 File Offset: 0x000A0F68
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundTupleLiteral Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> argumentNamesOpt, ImmutableArray<bool> inferredNamesOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (arguments != base.Arguments || argumentNamesOpt != base.ArgumentNamesOpt || inferredNamesOpt != base.InferredNamesOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundTupleLiteral boundTupleLiteral = new BoundTupleLiteral(this.Syntax, arguments, argumentNamesOpt, inferredNamesOpt, type, base.HasErrors);
				boundTupleLiteral.CopyAttributes(this);
				return boundTupleLiteral;
			}
			return this;
		}
	}
}
