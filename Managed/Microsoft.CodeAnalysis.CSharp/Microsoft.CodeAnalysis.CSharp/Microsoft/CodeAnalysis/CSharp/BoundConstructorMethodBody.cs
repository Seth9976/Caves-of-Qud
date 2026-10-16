using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001E1 RID: 481
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConstructorMethodBody : BoundMethodBodyBase
	{
		// Token: 0x0600204B RID: 8267 RVA: 0x000A405F File Offset: 0x000A225F
		public BoundConstructorMethodBody([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpressionStatement initializer, BoundBlock blockBody, BoundBlock expressionBody, bool hasErrors = false)
			: base(BoundKind.ConstructorMethodBody, syntax, blockBody, expressionBody, hasErrors || initializer.HasErrors() || blockBody.HasErrors() || expressionBody.HasErrors())
		{
			this.Locals = locals;
			this.Initializer = initializer;
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x000A409E File Offset: 0x000A229E
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x000A40A6 File Offset: 0x000A22A6
		public BoundExpressionStatement Initializer { get; }

		// Token: 0x0600204E RID: 8270 RVA: 0x000A40AE File Offset: 0x000A22AE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConstructorMethodBody(this);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000A40B8 File Offset: 0x000A22B8
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundConstructorMethodBody Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpressionStatement initializer, BoundBlock blockBody, BoundBlock expressionBody)
		{
			if (locals != this.Locals || initializer != this.Initializer || blockBody != base.BlockBody || expressionBody != base.ExpressionBody)
			{
				BoundConstructorMethodBody boundConstructorMethodBody = new BoundConstructorMethodBody(this.Syntax, locals, initializer, blockBody, expressionBody, base.HasErrors);
				boundConstructorMethodBody.CopyAttributes(this);
				return boundConstructorMethodBody;
			}
			return this;
		}
	}
}
