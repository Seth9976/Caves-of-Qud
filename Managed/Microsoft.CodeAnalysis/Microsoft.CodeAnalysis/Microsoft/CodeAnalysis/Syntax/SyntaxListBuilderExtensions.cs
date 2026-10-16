using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A5 RID: 677
	internal static class SyntaxListBuilderExtensions
	{
		// Token: 0x06001CAA RID: 7338 RVA: 0x000543F4 File Offset: 0x000525F4
		[NullableContext(2)]
		public static SyntaxTokenList ToTokenList(this SyntaxListBuilder builder)
		{
			if (builder == null || builder.Count == 0)
			{
				return default(SyntaxTokenList);
			}
			return new SyntaxTokenList(null, builder.ToListNode(), 0, 0);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00054424 File Offset: 0x00052624
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<SyntaxNode> ToList(this SyntaxListBuilder builder)
		{
			GreenNode greenNode = ((builder != null) ? builder.ToListNode() : null);
			if (greenNode == null)
			{
				return default(SyntaxList<SyntaxNode>);
			}
			return new SyntaxList<SyntaxNode>(greenNode.CreateRed());
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00054458 File Offset: 0x00052658
		[return: Nullable(new byte[] { 0, 1 })]
		public static SyntaxList<TNode> ToList<TNode>([Nullable(2)] this SyntaxListBuilder builder) where TNode : SyntaxNode
		{
			GreenNode greenNode = ((builder != null) ? builder.ToListNode() : null);
			if (greenNode == null)
			{
				return default(SyntaxList<TNode>);
			}
			return new SyntaxList<TNode>(greenNode.CreateRed());
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0005448C File Offset: 0x0005268C
		[return: Nullable(new byte[] { 0, 1 })]
		public static SeparatedSyntaxList<TNode> ToSeparatedList<TNode>([Nullable(2)] this SyntaxListBuilder builder) where TNode : SyntaxNode
		{
			GreenNode greenNode = ((builder != null) ? builder.ToListNode() : null);
			if (greenNode == null)
			{
				return default(SeparatedSyntaxList<TNode>);
			}
			return new SeparatedSyntaxList<TNode>(new SyntaxNodeOrTokenList(greenNode.CreateRed(), 0));
		}
	}
}
