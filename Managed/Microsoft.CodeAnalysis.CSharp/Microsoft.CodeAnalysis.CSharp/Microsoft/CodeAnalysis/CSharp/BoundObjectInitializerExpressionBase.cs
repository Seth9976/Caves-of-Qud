using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C5 RID: 453
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundObjectInitializerExpressionBase : BoundExpression
	{
		// Token: 0x06001FBB RID: 8123 RVA: 0x000A2F40 File Offset: 0x000A1140
		protected BoundObjectInitializerExpressionBase(BoundKind kind, SyntaxNode syntax, BoundObjectOrCollectionValuePlaceholder placeholder, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> initializers, TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.Placeholder = placeholder;
			this.Initializers = initializers;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x000A2F5D File Offset: 0x000A115D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x000A2F65 File Offset: 0x000A1165
		public BoundObjectOrCollectionValuePlaceholder Placeholder { get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001FBE RID: 8126 RVA: 0x000A2F6D File Offset: 0x000A116D
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Initializers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
