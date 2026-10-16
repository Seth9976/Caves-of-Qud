using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001BC RID: 444
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundDynamicInvocableBase : BoundExpression
	{
		// Token: 0x06001F85 RID: 8069 RVA: 0x000A293D File Offset: 0x000A0B3D
		protected BoundDynamicInvocableBase(BoundKind kind, SyntaxNode syntax, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.Expression = expression;
			this.Arguments = arguments;
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x000A295A File Offset: 0x000A0B5A
		public BoundExpression Expression { get; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x000A2962 File Offset: 0x000A0B62
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
