using System;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements
{
	// Token: 0x020005AD RID: 1453
	internal static class LanguageDirectionExtensions
	{
		// Token: 0x06002B55 RID: 11093 RVA: 0x0009CA3C File Offset: 0x0009AC3C
		internal static LanguageDirection toTextCore(this LanguageDirection dir)
		{
			LanguageDirection languageDirection;
			if (dir > LanguageDirection.LTR)
			{
				if (dir != LanguageDirection.RTL)
				{
					throw new ArgumentOutOfRangeException("dir", dir, "impossible to convert value");
				}
				languageDirection = LanguageDirection.RTL;
			}
			else
			{
				languageDirection = LanguageDirection.LTR;
			}
			return languageDirection;
		}
	}
}
