using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C8 RID: 456
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundCollectionInitializerExpression : BoundObjectInitializerExpressionBase
	{
		// Token: 0x06001FC9 RID: 8137 RVA: 0x000A30AE File Offset: 0x000A12AE
		public BoundCollectionInitializerExpression(SyntaxNode syntax, BoundObjectOrCollectionValuePlaceholder placeholder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.CollectionInitializerExpression, syntax, placeholder, initializers, type, hasErrors || placeholder.HasErrors() || initializers.HasErrors<BoundExpression>())
		{
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000A30D5 File Offset: 0x000A12D5
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitCollectionInitializerExpression(this);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000A30E0 File Offset: 0x000A12E0
		public BoundCollectionInitializerExpression Update(BoundObjectOrCollectionValuePlaceholder placeholder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers, TypeSymbol type)
		{
			if (placeholder != base.Placeholder || initializers != base.Initializers || !TypeSymbol.Equals(type, base.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundCollectionInitializerExpression boundCollectionInitializerExpression = new BoundCollectionInitializerExpression(this.Syntax, placeholder, initializers, type, base.HasErrors);
				boundCollectionInitializerExpression.CopyAttributes(this);
				return boundCollectionInitializerExpression;
			}
			return this;
		}
	}
}
