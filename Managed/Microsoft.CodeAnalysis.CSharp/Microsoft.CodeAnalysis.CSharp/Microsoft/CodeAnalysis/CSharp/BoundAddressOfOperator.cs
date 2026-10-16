using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D1 RID: 209
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAddressOfOperator : BoundExpression
	{
		// Token: 0x06001133 RID: 4403 RVA: 0x0005DE18 File Offset: 0x0005C018
		public BoundAddressOfOperator(SyntaxNode syntax, BoundExpression operand, TypeSymbol type, bool hasErrors = false)
			: this(syntax, operand, false, type, hasErrors)
		{
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0005DE26 File Offset: 0x0005C026
		public BoundAddressOfOperator(SyntaxNode syntax, BoundExpression operand, bool isManaged, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.AddressOfOperator, syntax, type, hasErrors || operand.HasErrors())
		{
			this.Operand = operand;
			this.IsManaged = isManaged;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0005DE4E File Offset: 0x0005C04E
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0005DE56 File Offset: 0x0005C056
		public BoundExpression Operand { get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0005DE5E File Offset: 0x0005C05E
		public bool IsManaged { get; }

		// Token: 0x06001138 RID: 4408 RVA: 0x0005DE66 File Offset: 0x0005C066
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAddressOfOperator(this);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0005DE6F File Offset: 0x0005C06F
		public BoundAddressOfOperator Update(BoundExpression operand, bool isManaged, TypeSymbol type)
		{
			if (operand != this.Operand || isManaged != this.IsManaged || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAddressOfOperator boundAddressOfOperator = new BoundAddressOfOperator(this.Syntax, operand, isManaged, type, base.HasErrors);
				boundAddressOfOperator.CopyAttributes(this);
				return boundAddressOfOperator;
			}
			return this;
		}
	}
}
