using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000171 RID: 369
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundBinaryOperatorBase : BoundExpression
	{
		// Token: 0x06001DEC RID: 7660 RVA: 0x000A0031 File Offset: 0x0009E231
		protected BoundBinaryOperatorBase(BoundKind kind, SyntaxNode syntax, BoundExpression left, BoundExpression right, TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x000A004E File Offset: 0x0009E24E
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x000A0056 File Offset: 0x0009E256
		public BoundExpression Left { get; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x000A005E File Offset: 0x0009E25E
		public BoundExpression Right { get; }
	}
}
