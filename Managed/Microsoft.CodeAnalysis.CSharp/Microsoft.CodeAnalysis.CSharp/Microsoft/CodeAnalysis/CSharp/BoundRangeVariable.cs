using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A7 RID: 167
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRangeVariable : BoundExpression
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0005560C File Offset: 0x0005380C
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.RangeVariableSymbol;
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x00055614 File Offset: 0x00053814
		public BoundRangeVariable(SyntaxNode syntax, RangeVariableSymbol rangeVariableSymbol, BoundExpression value, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.RangeVariable, syntax, type, hasErrors || value.HasErrors())
		{
			this.RangeVariableSymbol = rangeVariableSymbol;
			this.Value = value;
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0005563C File Offset: 0x0005383C
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00055644 File Offset: 0x00053844
		public RangeVariableSymbol RangeVariableSymbol { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0005564C File Offset: 0x0005384C
		public BoundExpression Value { get; }

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00055654 File Offset: 0x00053854
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRangeVariable(this);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x00055660 File Offset: 0x00053860
		public BoundRangeVariable Update(RangeVariableSymbol rangeVariableSymbol, BoundExpression value, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(rangeVariableSymbol, this.RangeVariableSymbol) || value != this.Value || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundRangeVariable boundRangeVariable = new BoundRangeVariable(this.Syntax, rangeVariableSymbol, value, type, base.HasErrors);
				boundRangeVariable.CopyAttributes(this);
				return boundRangeVariable;
			}
			return this;
		}
	}
}
