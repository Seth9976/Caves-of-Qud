using System;

namespace Microsoft.CodeAnalysis.Syntax
{
	// Token: 0x020002A1 RID: 673
	internal static class CommonSyntaxNodeRemover
	{
		// Token: 0x06001C75 RID: 7285 RVA: 0x00053CB0 File Offset: 0x00051EB0
		public static void GetSeparatorInfo(SyntaxNodeOrTokenList nodesAndSeparators, int nodeIndex, int endOfLineKind, out bool nextTokenIsSeparator, out bool nextSeparatorBelongsToNode)
		{
			SyntaxNode syntaxNode = nodesAndSeparators[nodeIndex].AsNode();
			nextTokenIsSeparator = nodeIndex + 1 < nodesAndSeparators.Count && nodesAndSeparators[nodeIndex + 1].IsToken;
			bool flag;
			if (nextTokenIsSeparator)
			{
				SyntaxToken syntaxToken = nodesAndSeparators[nodeIndex + 1].AsToken();
				if (!syntaxToken.HasLeadingTrivia && !CommonSyntaxNodeRemover.ContainsEndOfLine(syntaxNode.GetTrailingTrivia(), endOfLineKind))
				{
					flag = CommonSyntaxNodeRemover.ContainsEndOfLine(syntaxToken.TrailingTrivia, endOfLineKind);
					goto IL_0074;
				}
			}
			flag = false;
			IL_0074:
			nextSeparatorBelongsToNode = flag;
		}

		// Token: 0x06001C76 RID: 7286 RVA: 0x00053D32 File Offset: 0x00051F32
		private static bool ContainsEndOfLine(SyntaxTriviaList triviaList, int endOfLineKind)
		{
			return triviaList.IndexOf(endOfLineKind) >= 0;
		}
	}
}
