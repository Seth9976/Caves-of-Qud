using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E0 RID: 480
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNonConstructorMethodBody : BoundMethodBodyBase
	{
		// Token: 0x06002048 RID: 8264 RVA: 0x000A4001 File Offset: 0x000A2201
		public BoundNonConstructorMethodBody([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, BoundBlock blockBody, BoundBlock expressionBody, bool hasErrors = false)
			: base(BoundKind.NonConstructorMethodBody, syntax, blockBody, expressionBody, hasErrors || blockBody.HasErrors() || expressionBody.HasErrors())
		{
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000A4026 File Offset: 0x000A2226
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNonConstructorMethodBody(this);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000A402F File Offset: 0x000A222F
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundNonConstructorMethodBody Update(BoundBlock blockBody, BoundBlock expressionBody)
		{
			if (blockBody != base.BlockBody || expressionBody != base.ExpressionBody)
			{
				BoundNonConstructorMethodBody boundNonConstructorMethodBody = new BoundNonConstructorMethodBody(this.Syntax, blockBody, expressionBody, base.HasErrors);
				boundNonConstructorMethodBody.CopyAttributes(this);
				return boundNonConstructorMethodBody;
			}
			return this;
		}
	}
}
