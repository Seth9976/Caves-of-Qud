using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000D6 RID: 214
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundPointerElementAccess : BoundExpression
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0005E199 File Offset: 0x0005C399
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Expression, this.Index);
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0005E1AC File Offset: 0x0005C3AC
		public BoundPointerElementAccess(SyntaxNode syntax, BoundExpression expression, BoundExpression index, bool @checked, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.PointerElementAccess, syntax, type, hasErrors || expression.HasErrors() || index.HasErrors())
		{
			this.Expression = expression;
			this.Index = index;
			this.Checked = @checked;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x0005E1E4 File Offset: 0x0005C3E4
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0005E1EC File Offset: 0x0005C3EC
		public BoundExpression Expression { get; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x0005E1F4 File Offset: 0x0005C3F4
		public BoundExpression Index { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0005E1FC File Offset: 0x0005C3FC
		public bool Checked { get; }

		// Token: 0x0600115D RID: 4445 RVA: 0x0005E204 File Offset: 0x0005C404
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitPointerElementAccess(this);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0005E210 File Offset: 0x0005C410
		public BoundPointerElementAccess Update(BoundExpression expression, BoundExpression index, bool @checked, TypeSymbol type)
		{
			if (expression != this.Expression || index != this.Index || @checked != this.Checked || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundPointerElementAccess boundPointerElementAccess = new BoundPointerElementAccess(this.Syntax, expression, index, @checked, type, base.HasErrors);
				boundPointerElementAccess.CopyAttributes(this);
				return boundPointerElementAccess;
			}
			return this;
		}
	}
}
