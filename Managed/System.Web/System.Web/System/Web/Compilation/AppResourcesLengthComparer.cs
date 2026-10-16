using System;
using System.Collections.Generic;

namespace System.Web.Compilation
{
	// Token: 0x0200060C RID: 1548
	internal class AppResourcesLengthComparer<T> : IComparer<T>
	{
		// Token: 0x060042A2 RID: 17058 RVA: 0x000AE5F4 File Offset: 0x000AC7F4
		private int CompareStrings(string a, string b)
		{
			if (a == null || b == null)
			{
				return 0;
			}
			return b.Length - a.Length;
		}

		// Token: 0x060042A3 RID: 17059 RVA: 0x000AE60C File Offset: 0x000AC80C
		int IComparer<T>.Compare(T _a, T _b)
		{
			string text;
			string text2;
			if (_a is string && _b is string)
			{
				text = _a as string;
				text2 = _b as string;
			}
			else if (_a is List<string> && _b is List<string>)
			{
				text = (_a as List<string>)[0];
				text2 = (_b as List<string>)[0];
			}
			else
			{
				if (!(_a is AppResourceFileInfo) || !(_b is AppResourceFileInfo))
				{
					return 0;
				}
				text = (_a as AppResourceFileInfo).Info.Name;
				text2 = (_b as AppResourceFileInfo).Info.Name;
			}
			return this.CompareStrings(text, text2);
		}
	}
}
