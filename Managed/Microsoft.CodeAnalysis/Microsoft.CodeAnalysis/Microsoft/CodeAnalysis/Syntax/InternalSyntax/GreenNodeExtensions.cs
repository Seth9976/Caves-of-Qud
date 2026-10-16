using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Syntax.InternalSyntax
{
	// Token: 0x020002AC RID: 684
	internal static class GreenNodeExtensions
	{
		// Token: 0x06001CF3 RID: 7411 RVA: 0x00054EF4 File Offset: 0x000530F4
		[return: Nullable(new byte[] { 0, 1 })]
		internal static SyntaxList<T> ToGreenList<T>([Nullable(2)] this SyntaxNode node) where T : GreenNode
		{
			if (node == null)
			{
				return default(SyntaxList<T>);
			}
			return node.Green.ToGreenList<T>();
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00054F1C File Offset: 0x0005311C
		[return: Nullable(new byte[] { 0, 1 })]
		internal static SeparatedSyntaxList<T> ToGreenSeparatedList<T>([Nullable(2)] this SyntaxNode node) where T : GreenNode
		{
			if (node == null)
			{
				return default(SeparatedSyntaxList<T>);
			}
			return new SeparatedSyntaxList<T>(node.Green.ToGreenList<T>());
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00054F4B File Offset: 0x0005314B
		[return: Nullable(new byte[] { 0, 1 })]
		internal static SyntaxList<T> ToGreenList<T>([Nullable(2)] this GreenNode node) where T : GreenNode
		{
			return new SyntaxList<T>(node);
		}
	}
}
