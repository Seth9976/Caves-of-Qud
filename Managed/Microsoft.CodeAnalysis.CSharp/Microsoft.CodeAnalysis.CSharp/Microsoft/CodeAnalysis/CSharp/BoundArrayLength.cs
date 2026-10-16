using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000175 RID: 373
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundArrayLength : BoundExpression
	{
		// Token: 0x06001E06 RID: 7686 RVA: 0x000A02B8 File Offset: 0x0009E4B8
		public BoundArrayLength(SyntaxNode syntax, BoundExpression expression, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ArrayLength, syntax, type, hasErrors || expression.HasErrors())
		{
			this.Expression = expression;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x000A02D8 File Offset: 0x0009E4D8
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x000A02E0 File Offset: 0x0009E4E0
		public BoundExpression Expression { get; }

		// Token: 0x06001E09 RID: 7689 RVA: 0x000A02E8 File Offset: 0x0009E4E8
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitArrayLength(this);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000A02F1 File Offset: 0x0009E4F1
		public BoundArrayLength Update(BoundExpression expression, TypeSymbol type)
		{
			if (expression != this.Expression || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundArrayLength boundArrayLength = new BoundArrayLength(this.Syntax, expression, type, base.HasErrors);
				boundArrayLength.CopyAttributes(this);
				return boundArrayLength;
			}
			return this;
		}
	}
}
