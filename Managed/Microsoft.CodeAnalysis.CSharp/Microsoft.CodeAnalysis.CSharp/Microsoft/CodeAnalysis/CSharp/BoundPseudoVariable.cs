using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001A6 RID: 422
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPseudoVariable : BoundExpression
	{
		// Token: 0x06001F14 RID: 7956 RVA: 0x000A1D66 File Offset: 0x0009FF66
		public BoundPseudoVariable(SyntaxNode syntax, LocalSymbol localSymbol, PseudoVariableExpressions emitExpressions, TypeSymbol type, bool hasErrors)
			: base(BoundKind.PseudoVariable, syntax, type, hasErrors)
		{
			this.LocalSymbol = localSymbol;
			this.EmitExpressions = emitExpressions;
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x000A1D83 File Offset: 0x0009FF83
		public BoundPseudoVariable(SyntaxNode syntax, LocalSymbol localSymbol, PseudoVariableExpressions emitExpressions, TypeSymbol type)
			: base(BoundKind.PseudoVariable, syntax, type)
		{
			this.LocalSymbol = localSymbol;
			this.EmitExpressions = emitExpressions;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x000A1D9E File Offset: 0x0009FF9E
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000A1DA6 File Offset: 0x0009FFA6
		public LocalSymbol LocalSymbol { get; }

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x000A1DAE File Offset: 0x0009FFAE
		public PseudoVariableExpressions EmitExpressions { get; }

		// Token: 0x06001F19 RID: 7961 RVA: 0x000A1DB6 File Offset: 0x0009FFB6
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPseudoVariable(this);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000A1DC0 File Offset: 0x0009FFC0
		public BoundPseudoVariable Update(LocalSymbol localSymbol, PseudoVariableExpressions emitExpressions, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(localSymbol, this.LocalSymbol) || emitExpressions != this.EmitExpressions || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundPseudoVariable boundPseudoVariable = new BoundPseudoVariable(this.Syntax, localSymbol, emitExpressions, type, base.HasErrors);
				boundPseudoVariable.CopyAttributes(this);
				return boundPseudoVariable;
			}
			return this;
		}
	}
}
