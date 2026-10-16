using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000178 RID: 376
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundTypeOf : BoundExpression
	{
		// Token: 0x06001E19 RID: 7705 RVA: 0x000A04AE File Offset: 0x0009E6AE
		protected BoundTypeOf(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getTypeFromHandle, TypeSymbol type, bool hasErrors)
			: base(kind, syntax, type, hasErrors)
		{
			this.GetTypeFromHandle = getTypeFromHandle;
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000A04C3 File Offset: 0x0009E6C3
		protected BoundTypeOf(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getTypeFromHandle, TypeSymbol type)
			: base(kind, syntax, type)
		{
			this.GetTypeFromHandle = getTypeFromHandle;
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x000A04D6 File Offset: 0x0009E6D6
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x000A04DE File Offset: 0x0009E6DE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol GetTypeFromHandle
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}
	}
}
