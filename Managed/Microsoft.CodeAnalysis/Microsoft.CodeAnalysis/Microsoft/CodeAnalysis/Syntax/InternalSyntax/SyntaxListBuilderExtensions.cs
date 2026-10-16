using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002B2 RID: 690
	internal static class SyntaxListBuilderExtensions
	{
		// Token: 0x06001D40 RID: 7488 RVA: 0x000557E7 File Offset: 0x000539E7
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<GreenNode> ToList(this SyntaxListBuilder builder)
		{
			return builder.ToList<GreenNode>();
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000557F0 File Offset: 0x000539F0
		[return: Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<TNode> ToList<TNode>([Nullable(2)] this SyntaxListBuilder builder) where TNode : GreenNode
		{
			if (builder == null)
			{
				return default(SyntaxList<GreenNode>);
			}
			return new SyntaxList<TNode>(builder.ToListNode());
		}
	}
}
