using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000286 RID: 646
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SyntaxTreeExtensions
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x0004E8C4 File Offset: 0x0004CAC4
		[Conditional("DEBUG")]
		internal static void VerifySource(this SyntaxTree tree, [Nullable(2)] IEnumerable<TextChangeRange> changes = null)
		{
			SyntaxNode root = tree.GetRoot(default(CancellationToken));
			SourceText text = tree.GetText(default(CancellationToken));
			TextSpan textSpan = new TextSpan(0, text.Length);
			SyntaxNode syntaxNode = null;
			if (changes != null)
			{
				TextSpan change = TextChangeRange.Collapse(changes).Span;
				if (change != textSpan)
				{
					syntaxNode = Enumerable.LastOrDefault<SyntaxNode>(root.DescendantNodes((SyntaxNode n) => n.FullSpan.Contains(change), false));
				}
			}
			if (syntaxNode == null)
			{
				syntaxNode = root;
			}
			TextSpan fullSpan = syntaxNode.FullSpan;
			TextSpan? textSpan2 = fullSpan.Intersection(textSpan);
			char c = '\0';
			char c2 = '\0';
			int num;
			if (textSpan2 == null)
			{
				num = 0;
			}
			else
			{
				string text2 = text.ToString(textSpan2.Value);
				string text3 = syntaxNode.ToFullString();
				num = SyntaxTreeExtensions.FindFirstDifference(text2, text3);
				if (num >= 0)
				{
					c = text3.get_Chars(num);
					c2 = text2.get_Chars(num);
				}
			}
			if (num >= 0)
			{
				num += fullSpan.Start;
				if (num < text.Length)
				{
					LinePosition linePosition = text.Lines.GetLinePosition(num);
					TextLine textLine = text.Lines[linePosition.Line];
					text.ToString();
					string.Format("Unexpected difference at offset {0}: Line {1}, Column {2} \"{3}\"  (Found: [{4}] Expected: [{5}])", new object[]
					{
						num,
						linePosition.Line + 1,
						linePosition.Character + 1,
						textLine.ToString(),
						c,
						c2
					});
				}
			}
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x0004EA60 File Offset: 0x0004CC60
		private static int FindFirstDifference(string s1, string s2)
		{
			int length = s1.Length;
			int length2 = s2.Length;
			int num = Math.Min(length, length2);
			for (int i = 0; i < num; i++)
			{
				if (s1.get_Chars(i) != s2.get_Chars(i))
				{
					return i;
				}
			}
			if (length != length2)
			{
				return num + 1;
			}
			return -1;
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x0004EAAC File Offset: 0x0004CCAC
		public static bool IsHiddenPosition(this SyntaxTree tree, int position, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!tree.HasHiddenRegions())
			{
				return false;
			}
			LineVisibility lineVisibility = tree.GetLineVisibility(position, cancellationToken);
			return lineVisibility == LineVisibility.Hidden || lineVisibility == LineVisibility.BeforeFirstLineDirective;
		}
	}
}
