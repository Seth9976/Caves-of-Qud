using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D2 RID: 466
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundInterpolatedString : BoundInterpolatedStringBase
	{
		// Token: 0x06001FF9 RID: 8185 RVA: 0x000A35C9 File Offset: 0x000A17C9
		public BoundInterpolatedString([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> parts, ConstantValue constantValueOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.InterpolatedString, syntax, parts, constantValueOpt, type, hasErrors || parts.HasErrors<BoundExpression>())
		{
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x000A35E8 File Offset: 0x000A17E8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitInterpolatedString(this);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x000A35F4 File Offset: 0x000A17F4
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundInterpolatedString Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> parts, ConstantValue constantValueOpt, TypeSymbol type)
		{
			if (parts != base.Parts || constantValueOpt != base.ConstantValueOpt || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundInterpolatedString boundInterpolatedString = new BoundInterpolatedString(this.Syntax, parts, constantValueOpt, type, base.HasErrors);
				boundInterpolatedString.CopyAttributes(this);
				return boundInterpolatedString;
			}
			return this;
		}
	}
}
