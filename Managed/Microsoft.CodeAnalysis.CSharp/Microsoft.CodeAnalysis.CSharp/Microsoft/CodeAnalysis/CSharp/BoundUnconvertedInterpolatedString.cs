using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D1 RID: 465
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUnconvertedInterpolatedString : BoundInterpolatedStringBase
	{
		// Token: 0x06001FF6 RID: 8182 RVA: 0x000A354C File Offset: 0x000A174C
		public BoundUnconvertedInterpolatedString([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> parts, ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.UnconvertedInterpolatedString, syntax, parts, constantValueOpt, type, hasErrors || parts.HasErrors<BoundExpression>())
		{
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000A356B File Offset: 0x000A176B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnconvertedInterpolatedString(this);
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000A3574 File Offset: 0x000A1774
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundUnconvertedInterpolatedString Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> parts, ConstantValue constantValueOpt, TypeSymbol type)
		{
			if (parts != base.Parts || constantValueOpt != base.ConstantValueOpt || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundUnconvertedInterpolatedString boundUnconvertedInterpolatedString = new BoundUnconvertedInterpolatedString(this.Syntax, parts, constantValueOpt, type, base.HasErrors);
				boundUnconvertedInterpolatedString.CopyAttributes(this);
				return boundUnconvertedInterpolatedString;
			}
			return this;
		}
	}
}
