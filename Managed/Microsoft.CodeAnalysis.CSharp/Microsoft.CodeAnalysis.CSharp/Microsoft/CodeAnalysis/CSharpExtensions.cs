using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000008 RID: 8
	public static class CSharpExtensions
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000022C8 File Offset: 0x000004C8
		public static bool IsKind(this SyntaxToken token, SyntaxKind kind)
		{
			return token.RawKind == (int)kind;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022D4 File Offset: 0x000004D4
		public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind)
		{
			return trivia.RawKind == (int)kind;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E0 File Offset: 0x000004E0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool IsKind([NotNullWhen(true)] this SyntaxNode node, SyntaxKind kind)
		{
			int? num = ((node != null) ? new int?(node.RawKind) : default(int?));
			return (num.GetValueOrDefault() == (int)kind) & (num != null);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000231B File Offset: 0x0000051B
		public static bool IsKind(this SyntaxNodeOrToken nodeOrToken, SyntaxKind kind)
		{
			return nodeOrToken.RawKind == (int)kind;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002327 File Offset: 0x00000527
		internal static SyntaxKind ContextualKind(this SyntaxToken token)
		{
			if (token.Language != "C#")
			{
				return SyntaxKind.None;
			}
			return (SyntaxKind)token.RawContextualKind;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002341 File Offset: 0x00000541
		internal static bool IsUnderscoreToken(this SyntaxToken identifier)
		{
			return identifier.ContextualKind() == SyntaxKind.UnderscoreToken;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002350 File Offset: 0x00000550
		public static int IndexOf<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode
		{
			return list.IndexOf((int)kind);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000235A File Offset: 0x0000055A
		public static bool Any<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode
		{
			return list.IndexOf<TNode>(kind) >= 0;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002369 File Offset: 0x00000569
		public static int IndexOf<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode
		{
			return list.IndexOf((int)kind);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002373 File Offset: 0x00000573
		public static bool Any<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode
		{
			return list.IndexOf<TNode>(kind) >= 0;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002382 File Offset: 0x00000582
		public static int IndexOf(this SyntaxTriviaList list, SyntaxKind kind)
		{
			return list.IndexOf((int)kind);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000238C File Offset: 0x0000058C
		public static bool Any(this SyntaxTriviaList list, SyntaxKind kind)
		{
			return list.IndexOf(kind) >= 0;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000239B File Offset: 0x0000059B
		public static int IndexOf(this SyntaxTokenList list, SyntaxKind kind)
		{
			return list.IndexOf((int)kind);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A5 File Offset: 0x000005A5
		public static bool Any(this SyntaxTokenList list, SyntaxKind kind)
		{
			return list.IndexOf(kind) >= 0;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023B4 File Offset: 0x000005B4
		internal static SyntaxToken FirstOrDefault(this SyntaxTokenList list, SyntaxKind kind)
		{
			int num = list.IndexOf(kind);
			if (num < 0)
			{
				return default(SyntaxToken);
			}
			return list[num];
		}
	}
}
