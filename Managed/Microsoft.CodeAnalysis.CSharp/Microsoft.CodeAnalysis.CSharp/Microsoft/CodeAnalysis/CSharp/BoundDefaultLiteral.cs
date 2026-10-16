using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A3 RID: 163
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDefaultLiteral : BoundExpression
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0005527D File Offset: 0x0005347D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override ConstantValue ConstantValue
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x00055280 File Offset: 0x00053480
		public override object Display
		{
			get
			{
				return this.Type ?? "default";
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00055291 File Offset: 0x00053491
		public BoundDefaultLiteral(SyntaxNode syntax, bool hasErrors)
			: base(BoundKind.DefaultLiteral, syntax, null, hasErrors)
		{
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0005529E File Offset: 0x0005349E
		public BoundDefaultLiteral(SyntaxNode syntax)
			: base(BoundKind.DefaultLiteral, syntax, null)
		{
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x000552AA File Offset: 0x000534AA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000552B2 File Offset: 0x000534B2
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDefaultLiteral(this);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000552BB File Offset: 0x000534BB
		public BoundDefaultLiteral Update()
		{
			return this;
		}
	}
}
