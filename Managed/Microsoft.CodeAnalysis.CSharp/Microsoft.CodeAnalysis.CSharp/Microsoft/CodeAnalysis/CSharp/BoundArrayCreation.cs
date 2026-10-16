using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001CD RID: 461
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundArrayCreation : BoundExpression
	{
		// Token: 0x06001FE2 RID: 8162 RVA: 0x000A3369 File Offset: 0x000A1569
		public BoundArrayCreation(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> bounds, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ArrayCreation, syntax, type, hasErrors || bounds.HasErrors<BoundExpression>() || initializerOpt.HasErrors())
		{
			this.Bounds = bounds;
			this.InitializerOpt = initializerOpt;
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x000A339C File Offset: 0x000A159C
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x000A33A4 File Offset: 0x000A15A4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Bounds
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x000A33AC File Offset: 0x000A15AC
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundArrayInitialization InitializerOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000A33B4 File Offset: 0x000A15B4
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitArrayCreation(this);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000A33C0 File Offset: 0x000A15C0
		public BoundArrayCreation Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> bounds, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, TypeSymbol type)
		{
			if (bounds != this.Bounds || initializerOpt != this.InitializerOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundArrayCreation boundArrayCreation = new BoundArrayCreation(this.Syntax, bounds, initializerOpt, type, base.HasErrors);
				boundArrayCreation.CopyAttributes(this);
				return boundArrayCreation;
			}
			return this;
		}
	}
}
