using System;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200036B RID: 875
	internal static class CSharpSyntaxNodeCache
	{
		// Token: 0x06004C56 RID: 19542 RVA: 0x0013BD4C File Offset: 0x00139F4C
		internal static GreenNode TryGetNode(int kind, GreenNode child1, SyntaxFactoryContext context, out int hash)
		{
			return SyntaxNodeCache.TryGetNode(kind, child1, CSharpSyntaxNodeCache.GetNodeFlags(context), out hash);
		}

		// Token: 0x06004C57 RID: 19543 RVA: 0x0013BD5C File Offset: 0x00139F5C
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode child2, SyntaxFactoryContext context, out int hash)
		{
			return SyntaxNodeCache.TryGetNode(kind, child1, child2, CSharpSyntaxNodeCache.GetNodeFlags(context), out hash);
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x0013BD6E File Offset: 0x00139F6E
		internal static GreenNode TryGetNode(int kind, GreenNode child1, GreenNode child2, GreenNode child3, SyntaxFactoryContext context, out int hash)
		{
			return SyntaxNodeCache.TryGetNode(kind, child1, child2, child3, CSharpSyntaxNodeCache.GetNodeFlags(context), out hash);
		}

		// Token: 0x06004C59 RID: 19545 RVA: 0x0013BD84 File Offset: 0x00139F84
		private static GreenNode.NodeFlags GetNodeFlags(SyntaxFactoryContext context)
		{
			GreenNode.NodeFlags nodeFlags = SyntaxNodeCache.GetDefaultNodeFlags();
			if (context.IsInAsync)
			{
				nodeFlags |= GreenNode.NodeFlags.FactoryContextIsInAsync;
			}
			if (context.IsInQuery)
			{
				nodeFlags |= GreenNode.NodeFlags.FactoryContextIsInQuery;
			}
			return nodeFlags;
		}
	}
}
