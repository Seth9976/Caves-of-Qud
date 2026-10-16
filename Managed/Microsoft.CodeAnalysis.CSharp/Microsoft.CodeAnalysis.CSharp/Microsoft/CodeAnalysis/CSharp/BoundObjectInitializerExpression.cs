using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C6 RID: 454
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundObjectInitializerExpression : BoundObjectInitializerExpressionBase
	{
		// Token: 0x06001FBF RID: 8127 RVA: 0x000A2F75 File Offset: 0x000A1175
		public BoundObjectInitializerExpression(SyntaxNode syntax, BoundObjectOrCollectionValuePlaceholder placeholder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.ObjectInitializerExpression, syntax, placeholder, initializers, type, hasErrors || placeholder.HasErrors() || initializers.HasErrors<BoundExpression>())
		{
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000A2F9C File Offset: 0x000A119C
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitObjectInitializerExpression(this);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x000A2FA8 File Offset: 0x000A11A8
		public BoundObjectInitializerExpression Update(BoundObjectOrCollectionValuePlaceholder placeholder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers, TypeSymbol type)
		{
			if (placeholder != base.Placeholder || initializers != base.Initializers || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundObjectInitializerExpression boundObjectInitializerExpression = new BoundObjectInitializerExpression(this.Syntax, placeholder, initializers, type, base.HasErrors);
				boundObjectInitializerExpression.CopyAttributes(this);
				return boundObjectInitializerExpression;
			}
			return this;
		}
	}
}
