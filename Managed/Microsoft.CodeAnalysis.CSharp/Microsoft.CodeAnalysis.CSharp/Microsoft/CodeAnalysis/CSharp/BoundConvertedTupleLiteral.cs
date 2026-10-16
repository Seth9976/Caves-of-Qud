using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C3 RID: 451
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConvertedTupleLiteral : BoundTupleExpression
	{
		// Token: 0x06001FB0 RID: 8112 RVA: 0x000A2DCE File Offset: 0x000A0FCE
		public BoundConvertedTupleLiteral([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, BoundTupleLiteral sourceTuple, bool wasTargetTyped, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<bool> inferredNamesOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ConvertedTupleLiteral, syntax, arguments, argumentNamesOpt, inferredNamesOpt, type, hasErrors || sourceTuple.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.SourceTuple = sourceTuple;
			this.WasTargetTyped = wasTargetTyped;
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x000A2E08 File Offset: 0x000A1008
		public BoundTupleLiteral SourceTuple { get; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x000A2E10 File Offset: 0x000A1010
		public bool WasTargetTyped { get; }

		// Token: 0x06001FB3 RID: 8115 RVA: 0x000A2E18 File Offset: 0x000A1018
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConvertedTupleLiteral(this);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x000A2E24 File Offset: 0x000A1024
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundConvertedTupleLiteral Update(BoundTupleLiteral sourceTuple, bool wasTargetTyped, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<bool> inferredNamesOpt, TypeSymbol type)
		{
			if (sourceTuple != this.SourceTuple || wasTargetTyped != this.WasTargetTyped || arguments != base.Arguments || argumentNamesOpt != base.ArgumentNamesOpt || inferredNamesOpt != base.InferredNamesOpt || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundConvertedTupleLiteral boundConvertedTupleLiteral = new BoundConvertedTupleLiteral(this.Syntax, sourceTuple, wasTargetTyped, arguments, argumentNamesOpt, inferredNamesOpt, type, base.HasErrors);
				boundConvertedTupleLiteral.CopyAttributes(this);
				return boundConvertedTupleLiteral;
			}
			return this;
		}
	}
}
