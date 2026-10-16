using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001CE RID: 462
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundArrayInitialization : BoundExpression
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x000A3410 File Offset: 0x000A1610
		public BoundArrayInitialization(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers, bool hasErrors = false)
			: base(BoundKind.ArrayInitialization, syntax, null, hasErrors || initializers.HasErrors<BoundExpression>())
		{
			this.Initializers = initializers;
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000A3432 File Offset: 0x000A1632
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001FEA RID: 8170 RVA: 0x000A343A File Offset: 0x000A163A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Initializers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000A3442 File Offset: 0x000A1642
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitArrayInitialization(this);
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000A344B File Offset: 0x000A164B
		public BoundArrayInitialization Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers)
		{
			if (initializers != this.Initializers)
			{
				BoundArrayInitialization boundArrayInitialization = new BoundArrayInitialization(this.Syntax, initializers, base.HasErrors);
				boundArrayInitialization.CopyAttributes(this);
				return boundArrayInitialization;
			}
			return this;
		}
	}
}
