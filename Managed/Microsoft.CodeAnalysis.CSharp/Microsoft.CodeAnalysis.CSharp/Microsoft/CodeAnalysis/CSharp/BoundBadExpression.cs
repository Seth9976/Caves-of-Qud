using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000C9 RID: 201
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBadExpression : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x060010F5 RID: 4341 RVA: 0x0005D853 File Offset: 0x0005BA53
		public BoundBadExpression(SyntaxNode syntax, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Symbol> symbols, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> childBoundNodes, TypeSymbol type)
			: this(syntax, resultKind, symbols, childBoundNodes, type, true)
		{
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0005D863 File Offset: 0x0005BA63
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.ChildBoundNodes);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0005D870 File Offset: 0x0005BA70
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.ChildBoundNodes);
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0005D87D File Offset: 0x0005BA7D
		public BoundBadExpression(SyntaxNode syntax, LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Symbol> symbols, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> childBoundNodes, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.BadExpression, syntax, type, hasErrors || childBoundNodes.HasErrors<BoundExpression>())
		{
			this._ResultKind = resultKind;
			this.Symbols = symbols;
			this.ChildBoundNodes = childBoundNodes;
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0005D8AE File Offset: 0x0005BAAE
		public override LookupResultKind ResultKind
		{
			get
			{
				return this._ResultKind;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0005D8B6 File Offset: 0x0005BAB6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		public ImmutableArray<Symbol> Symbols
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0005D8BE File Offset: 0x0005BABE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> ChildBoundNodes
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0005D8C6 File Offset: 0x0005BAC6
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBadExpression(this);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x0005D8D0 File Offset: 0x0005BAD0
		public BoundBadExpression Update(LookupResultKind resultKind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<Symbol> symbols, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> childBoundNodes, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type)
		{
			if (resultKind != this.ResultKind || symbols != this.Symbols || childBoundNodes != this.ChildBoundNodes || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundBadExpression boundBadExpression = new BoundBadExpression(this.Syntax, resultKind, symbols, childBoundNodes, type, base.HasErrors);
				boundBadExpression.CopyAttributes(this);
				return boundBadExpression;
			}
			return this;
		}

		// Token: 0x0400056F RID: 1391
		private readonly LookupResultKind _ResultKind;
	}
}
