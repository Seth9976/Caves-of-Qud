using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x0200006A RID: 106
	internal static class UnicodeCharacterUtilities
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000906F File Offset: 0x0000726F
		public static bool IsIdentifierStartCharacter(char ch)
		{
			if (ch < 'a')
			{
				return ch >= 'A' && (ch <= 'Z' || ch == '_');
			}
			return ch <= 'z' || (ch > '\u007f' && UnicodeCharacterUtilities.IsLetterChar(CharUnicodeInfo.GetUnicodeCategory(ch)));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000090A4 File Offset: 0x000072A4
		public static bool IsIdentifierPartCharacter(char ch)
		{
			if (ch < 'a')
			{
				if (ch < 'A')
				{
					return ch >= '0' && ch <= '9';
				}
				return ch <= 'Z' || ch == '_';
			}
			else
			{
				if (ch <= 'z')
				{
					return true;
				}
				if (ch <= '\u007f')
				{
					return false;
				}
				UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(ch);
				return UnicodeCharacterUtilities.IsLetterChar(unicodeCategory) || UnicodeCharacterUtilities.IsDecimalDigitChar(unicodeCategory) || UnicodeCharacterUtilities.IsConnectingChar(unicodeCategory) || UnicodeCharacterUtilities.IsCombiningChar(unicodeCategory) || UnicodeCharacterUtilities.IsFormattingChar(unicodeCategory);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009118 File Offset: 0x00007318
		[NullableContext(2)]
		public static bool IsValidIdentifier([NotNullWhen(true)] string name)
		{
			if (RoslynString.IsNullOrEmpty(name))
			{
				return false;
			}
			if (!UnicodeCharacterUtilities.IsIdentifierStartCharacter(name.get_Chars(0)))
			{
				return false;
			}
			int length = name.Length;
			for (int i = 1; i < length; i++)
			{
				if (!UnicodeCharacterUtilities.IsIdentifierPartCharacter(name.get_Chars(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00009163 File Offset: 0x00007363
		private static bool IsLetterChar(UnicodeCategory cat)
		{
			return cat <= 4 || cat == 9;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009171 File Offset: 0x00007371
		private static bool IsCombiningChar(UnicodeCategory cat)
		{
			return cat - 5 <= 1;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000917C File Offset: 0x0000737C
		private static bool IsDecimalDigitChar(UnicodeCategory cat)
		{
			return cat == 8;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009182 File Offset: 0x00007382
		private static bool IsConnectingChar(UnicodeCategory cat)
		{
			return cat == 18;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009189 File Offset: 0x00007389
		internal static bool IsFormattingChar(char ch)
		{
			return ch > '\u007f' && UnicodeCharacterUtilities.IsFormattingChar(CharUnicodeInfo.GetUnicodeCategory(ch));
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000919D File Offset: 0x0000739D
		private static bool IsFormattingChar(UnicodeCategory cat)
		{
			return cat == 15;
		}
	}
}
