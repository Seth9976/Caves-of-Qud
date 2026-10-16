using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F9 RID: 761
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TextUtilities
	{
		// Token: 0x06002099 RID: 8345 RVA: 0x000645C0 File Offset: 0x000627C0
		internal static int GetLengthOfLineBreak(SourceText text, int index)
		{
			char c = text[index];
			if (c - '\u000e' <= 'q')
			{
				return 0;
			}
			return TextUtilities.GetLengthOfLineBreakSlow(text, index, c);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000645E8 File Offset: 0x000627E8
		private static int GetLengthOfLineBreakSlow(SourceText text, int index, char c)
		{
			if (c == '\r')
			{
				int num = index + 1;
				if (num >= text.Length || '\n' != text[num])
				{
					return 1;
				}
				return 2;
			}
			else
			{
				if (TextUtilities.IsAnyLineBreakCharacter(c))
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00064624 File Offset: 0x00062824
		public static void GetStartAndLengthOfLineBreakEndingAt(SourceText text, int index, out int startLinebreak, out int lengthLinebreak)
		{
			char c = text[index];
			if (c == '\n')
			{
				if (index > 0 && text[index - 1] == '\r')
				{
					startLinebreak = index - 1;
					lengthLinebreak = 2;
					return;
				}
				startLinebreak = index;
				lengthLinebreak = 1;
				return;
			}
			else
			{
				if (TextUtilities.IsAnyLineBreakCharacter(c))
				{
					startLinebreak = index;
					lengthLinebreak = 1;
					return;
				}
				startLinebreak = index + 1;
				lengthLinebreak = 0;
				return;
			}
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00064676 File Offset: 0x00062876
		internal static bool IsAnyLineBreakCharacter(char c)
		{
			return c == '\n' || c == '\r' || c == '\u0085' || c == '\u2028' || c == '\u2029';
		}
	}
}
