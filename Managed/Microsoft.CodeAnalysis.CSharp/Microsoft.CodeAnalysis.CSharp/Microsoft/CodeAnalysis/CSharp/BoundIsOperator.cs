using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000182 RID: 386
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundIsOperator : BoundExpression
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x000A0953 File Offset: 0x0009EB53
		public BoundIsOperator(SyntaxNode syntax, BoundExpression operand, BoundTypeExpression targetType, Conversion conversion, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.IsOperator, syntax, type, hasErrors || operand.HasErrors() || targetType.HasErrors())
		{
			this.Operand = operand;
			this.TargetType = targetType;
			this.Conversion = conversion;
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000A098B File Offset: 0x0009EB8B
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x000A0993 File Offset: 0x0009EB93
		public BoundExpression Operand { get; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000A099B File Offset: 0x0009EB9B
		public BoundTypeExpression TargetType { get; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000A09A3 File Offset: 0x0009EBA3
		public Conversion Conversion { get; }

		// Token: 0x06001E55 RID: 7765 RVA: 0x000A09AB File Offset: 0x0009EBAB
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitIsOperator(this);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x000A09B4 File Offset: 0x0009EBB4
		public BoundIsOperator Update(BoundExpression operand, BoundTypeExpression targetType, Conversion conversion, TypeSymbol type)
		{
			if (operand != this.Operand || targetType != this.TargetType || conversion != this.Conversion || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundIsOperator boundIsOperator = new BoundIsOperator(this.Syntax, operand, targetType, conversion, type, base.HasErrors);
				boundIsOperator.CopyAttributes(this);
				return boundIsOperator;
			}
			return this;
		}
	}
}
