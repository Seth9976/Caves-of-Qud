using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000177 RID: 375
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAwaitExpression : BoundExpression
	{
		// Token: 0x06001E13 RID: 7699 RVA: 0x000A041D File Offset: 0x0009E61D
		public BoundAwaitExpression(SyntaxNode syntax, BoundExpression expression, BoundAwaitableInfo awaitableInfo, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.AwaitExpression, syntax, type, hasErrors || expression.HasErrors() || awaitableInfo.HasErrors())
		{
			this.Expression = expression;
			this.AwaitableInfo = awaitableInfo;
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x000A044D File Offset: 0x0009E64D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x000A0455 File Offset: 0x0009E655
		public BoundExpression Expression { get; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x000A045D File Offset: 0x0009E65D
		public BoundAwaitableInfo AwaitableInfo { get; }

		// Token: 0x06001E17 RID: 7703 RVA: 0x000A0465 File Offset: 0x0009E665
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAwaitExpression(this);
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x000A046E File Offset: 0x0009E66E
		public BoundAwaitExpression Update(BoundExpression expression, BoundAwaitableInfo awaitableInfo, TypeSymbol type)
		{
			if (expression != this.Expression || awaitableInfo != this.AwaitableInfo || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAwaitExpression boundAwaitExpression = new BoundAwaitExpression(this.Syntax, expression, awaitableInfo, type, base.HasErrors);
				boundAwaitExpression.CopyAttributes(this);
				return boundAwaitExpression;
			}
			return this;
		}
	}
}
