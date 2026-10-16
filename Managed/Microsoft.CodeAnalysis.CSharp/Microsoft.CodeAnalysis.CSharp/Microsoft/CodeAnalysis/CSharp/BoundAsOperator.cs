using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000183 RID: 387
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAsOperator : BoundExpression
	{
		// Token: 0x06001E57 RID: 7767 RVA: 0x000A0A10 File Offset: 0x0009EC10
		public BoundAsOperator(SyntaxNode syntax, BoundExpression operand, BoundTypeExpression targetType, Conversion conversion, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.AsOperator, syntax, type, hasErrors || operand.HasErrors() || targetType.HasErrors())
		{
			this.Operand = operand;
			this.TargetType = targetType;
			this.Conversion = conversion;
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000A0A48 File Offset: 0x0009EC48
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x000A0A50 File Offset: 0x0009EC50
		public BoundExpression Operand { get; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000A0A58 File Offset: 0x0009EC58
		public BoundTypeExpression TargetType { get; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x000A0A60 File Offset: 0x0009EC60
		public Conversion Conversion { get; }

		// Token: 0x06001E5C RID: 7772 RVA: 0x000A0A68 File Offset: 0x0009EC68
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAsOperator(this);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000A0A74 File Offset: 0x0009EC74
		public BoundAsOperator Update(BoundExpression operand, BoundTypeExpression targetType, Conversion conversion, TypeSymbol type)
		{
			if (operand != this.Operand || targetType != this.TargetType || conversion != this.Conversion || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAsOperator boundAsOperator = new BoundAsOperator(this.Syntax, operand, targetType, conversion, type, base.HasErrors);
				boundAsOperator.CopyAttributes(this);
				return boundAsOperator;
			}
			return this;
		}
	}
}
