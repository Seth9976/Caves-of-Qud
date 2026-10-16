using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200016F RID: 367
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFromEndIndexExpression : BoundExpression
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x0009FECC File Offset: 0x0009E0CC
		public BoundFromEndIndexExpression(SyntaxNode syntax, BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.FromEndIndexExpression, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
			this.MethodOpt = methodOpt;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0009FEF4 File Offset: 0x0009E0F4
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0009FEFC File Offset: 0x0009E0FC
		public BoundExpression Operand { get; }

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0009FF04 File Offset: 0x0009E104
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol MethodOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0009FF0C File Offset: 0x0009E10C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFromEndIndexExpression(this);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0009FF18 File Offset: 0x0009E118
		public BoundFromEndIndexExpression Update(BoundExpression operand, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, TypeSymbol type)
		{
			if (operand != this.Operand || !SymbolEqualityComparer.ConsiderEverything.Equals(methodOpt, this.MethodOpt) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundFromEndIndexExpression boundFromEndIndexExpression = new BoundFromEndIndexExpression(this.Syntax, operand, methodOpt, type, base.HasErrors);
				boundFromEndIndexExpression.CopyAttributes(this);
				return boundFromEndIndexExpression;
			}
			return this;
		}
	}
}
