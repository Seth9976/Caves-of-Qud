using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E3 RID: 227
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSequence : BoundExpression
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x0005EBA8 File Offset: 0x0005CDA8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.SideEffects.Add(this.Value));
			}
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0005EBCE File Offset: 0x0005CDCE
		public BoundSequence(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> sideEffects, BoundExpression value, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.Sequence, syntax, type, hasErrors || sideEffects.HasErrors<BoundExpression>() || value.HasErrors())
		{
			this.Locals = locals;
			this.SideEffects = sideEffects;
			this.Value = value;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0005EC0A File Offset: 0x0005CE0A
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x0005EC12 File Offset: 0x0005CE12
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0005EC1A File Offset: 0x0005CE1A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> SideEffects
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0005EC22 File Offset: 0x0005CE22
		public BoundExpression Value { get; }

		// Token: 0x060011B9 RID: 4537 RVA: 0x0005EC2A File Offset: 0x0005CE2A
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSequence(this);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0005EC34 File Offset: 0x0005CE34
		public BoundSequence Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> sideEffects, BoundExpression value, TypeSymbol type)
		{
			if (locals != this.Locals || sideEffects != this.SideEffects || value != this.Value || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundSequence boundSequence = new BoundSequence(this.Syntax, locals, sideEffects, value, type, base.HasErrors);
				boundSequence.CopyAttributes(this);
				return boundSequence;
			}
			return this;
		}
	}
}
