using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001DF RID: 479
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundMethodBodyBase : BoundNode
	{
		// Token: 0x06002045 RID: 8261 RVA: 0x000A3FD6 File Offset: 0x000A21D6
		protected BoundMethodBodyBase(BoundKind kind, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, BoundBlock blockBody, BoundBlock expressionBody, bool hasErrors = false)
			: base(kind, syntax, hasErrors)
		{
			this.BlockBody = blockBody;
			this.ExpressionBody = expressionBody;
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x000A3FF1 File Offset: 0x000A21F1
		public BoundBlock BlockBody { get; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x000A3FF9 File Offset: 0x000A21F9
		public BoundBlock ExpressionBody { get; }
	}
}
