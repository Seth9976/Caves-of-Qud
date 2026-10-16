using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200018E RID: 398
	internal abstract class BoundMultipleLocalDeclarationsBase : BoundStatement
	{
		// Token: 0x06001E90 RID: 7824 RVA: 0x000A0F40 File Offset: 0x0009F140
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BoundMultipleLocalDeclarationsBase(BoundKind kind, SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundLocalDeclaration> localDeclarations, bool hasErrors = false)
			: base(kind, syntax, hasErrors)
		{
			this.LocalDeclarations = localDeclarations;
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000A0F53 File Offset: 0x0009F153
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundLocalDeclaration> LocalDeclarations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
