using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000179 RID: 377
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTypeOfOperator : BoundTypeOf
	{
		// Token: 0x06001E1D RID: 7709 RVA: 0x000A04E6 File Offset: 0x0009E6E6
		public BoundTypeOfOperator(SyntaxNode syntax, BoundTypeExpression sourceType, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getTypeFromHandle, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.TypeOfOperator, syntax, getTypeFromHandle, type, hasErrors || sourceType.HasErrors())
		{
			this.SourceType = sourceType;
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x000A0508 File Offset: 0x0009E708
		public BoundTypeExpression SourceType { get; }

		// Token: 0x06001E1F RID: 7711 RVA: 0x000A0510 File Offset: 0x0009E710
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTypeOfOperator(this);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000A051C File Offset: 0x0009E71C
		public BoundTypeOfOperator Update(BoundTypeExpression sourceType, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getTypeFromHandle, TypeSymbol type)
		{
			if (sourceType != this.SourceType || !SymbolEqualityComparer.ConsiderEverything.Equals(getTypeFromHandle, base.GetTypeFromHandle) || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundTypeOfOperator boundTypeOfOperator = new BoundTypeOfOperator(this.Syntax, sourceType, getTypeFromHandle, type, base.HasErrors);
				boundTypeOfOperator.CopyAttributes(this);
				return boundTypeOfOperator;
			}
			return this;
		}
	}
}
