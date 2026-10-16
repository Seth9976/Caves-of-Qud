using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000160 RID: 352
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundEqualsValue : BoundInitializer
	{
		// Token: 0x06001D99 RID: 7577 RVA: 0x0009F8D5 File Offset: 0x0009DAD5
		protected BoundEqualsValue(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundExpression value, bool hasErrors = false)
			: base(kind, syntax, hasErrors)
		{
			this.Locals = locals;
			this.Value = value;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001D9A RID: 7578 RVA: 0x0009F8F0 File Offset: 0x0009DAF0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x0009F8F8 File Offset: 0x0009DAF8
		public BoundExpression Value { get; }
	}
}
