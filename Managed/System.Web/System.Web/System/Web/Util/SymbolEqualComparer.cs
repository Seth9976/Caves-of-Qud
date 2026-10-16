using System;
using System.Collections;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x02000132 RID: 306
	internal class SymbolEqualComparer : IComparer
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x00002050 File Offset: 0x00000250
		internal SymbolEqualComparer()
		{
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000267C8 File Offset: 0x000249C8
		int IComparer.Compare(object keyLeft, object keyRight)
		{
			string text = keyLeft as string;
			string text2 = keyRight as string;
			if (text == null)
			{
				throw new ArgumentNullException("keyLeft");
			}
			if (text2 == null)
			{
				throw new ArgumentNullException("keyRight");
			}
			int length = text.Length;
			int length2 = text2.Length;
			if (length != length2)
			{
				return 1;
			}
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				char c2 = text2[i];
				if (c != c2)
				{
					UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
					UnicodeCategory unicodeCategory2 = char.GetUnicodeCategory(c2);
					if (unicodeCategory == UnicodeCategory.UppercaseLetter && unicodeCategory2 == UnicodeCategory.LowercaseLetter)
					{
						if (char.ToLower(c, CultureInfo.InvariantCulture) == c2)
						{
							goto IL_00A5;
						}
					}
					else if (unicodeCategory2 == UnicodeCategory.UppercaseLetter && unicodeCategory == UnicodeCategory.LowercaseLetter && char.ToLower(c2, CultureInfo.InvariantCulture) == c)
					{
						goto IL_00A5;
					}
					return 1;
				}
				IL_00A5:;
			}
			return 0;
		}

		// Token: 0x040011DA RID: 4570
		internal static readonly IComparer Default = new SymbolEqualComparer();
	}
}
