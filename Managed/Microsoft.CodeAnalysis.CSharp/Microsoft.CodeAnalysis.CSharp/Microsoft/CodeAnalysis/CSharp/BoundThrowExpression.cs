using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E1 RID: 225
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundThrowExpression : BoundExpression
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x0005EADC File Offset: 0x0005CCDC
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Expression);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x0005EAE9 File Offset: 0x0005CCE9
		public override object Display
		{
			get
			{
				return MessageID.IDS_ThrowExpression.Localize();
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0005EAFA File Offset: 0x0005CCFA
		public BoundThrowExpression(SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ThrowExpression, syntax, type, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060011AB RID: 4523 RVA: 0x0005EB1D File Offset: 0x0005CD1D
		public BoundExpression Expression { get; }

		// Token: 0x060011AC RID: 4524 RVA: 0x0005EB25 File Offset: 0x0005CD25
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitThrowExpression(this);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0005EB2E File Offset: 0x0005CD2E
		public BoundThrowExpression Update(BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (expression != this.Expression || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundThrowExpression boundThrowExpression = new BoundThrowExpression(this.Syntax, expression, type, base.HasErrors);
				boundThrowExpression.CopyAttributes(this);
				return boundThrowExpression;
			}
			return this;
		}
	}
}
