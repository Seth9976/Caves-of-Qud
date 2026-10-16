using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000DD RID: 221
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundStackAllocArrayCreationBase : BoundExpression
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x0005E74C File Offset: 0x0005C94C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<BoundExpression> GetChildInitializers(BoundArrayInitialization arrayInitializer)
		{
			if (arrayInitializer == null)
			{
				return ImmutableArray<BoundExpression>.Empty;
			}
			return arrayInitializer.Initializers;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0005E75D File Offset: 0x0005C95D
		protected BoundStackAllocArrayCreationBase(BoundKind kind, SyntaxNode syntax, TypeSymbol elementType, BoundExpression count, [global::System.Runtime.CompilerServices.Nullable(2)] BoundArrayInitialization initializerOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.ElementType = elementType;
			this.Count = count;
			this.InitializerOpt = initializerOpt;
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x0005E782 File Offset: 0x0005C982
		public TypeSymbol ElementType { get; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0005E78A File Offset: 0x0005C98A
		public BoundExpression Count { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x0005E792 File Offset: 0x0005C992
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundArrayInitialization InitializerOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}
	}
}
