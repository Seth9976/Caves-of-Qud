using System;
using System.Text.RegularExpressions;

namespace System.Web.Util
{
	// Token: 0x0200012D RID: 301
	internal class RegexUtil
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x00026038 File Offset: 0x00024238
		public static bool IsMatch(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return Regex.IsMatch(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return Regex.IsMatch(stringToMatch, pattern, regOption);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00026074 File Offset: 0x00024274
		public static Match Match(string stringToMatch, string pattern, RegexOptions regOption, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return Regex.Match(stringToMatch, pattern, regOption, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return Regex.Match(stringToMatch, pattern, regOption);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x000260B0 File Offset: 0x000242B0
		public static Regex CreateRegex(string pattern, RegexOptions option, int? timeoutInMillsec)
		{
			int regexTimeout = RegexUtil.GetRegexTimeout(timeoutInMillsec);
			if (regexTimeout > 0 || timeoutInMillsec != null)
			{
				return new Regex(pattern, option, TimeSpan.FromMilliseconds((double)regexTimeout));
			}
			return new Regex(pattern, option);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000260E8 File Offset: 0x000242E8
		internal static Regex CreateRegex(string pattern, RegexOptions option)
		{
			return RegexUtil.CreateRegex(pattern, option, null);
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00026108 File Offset: 0x00024308
		private static bool IsRegexTimeoutSetInAppDomain
		{
			get
			{
				if (RegexUtil._isRegexTimeoutSetInAppDomain == null)
				{
					bool flag = false;
					try
					{
						flag = AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") != null;
					}
					catch
					{
					}
					RegexUtil._isRegexTimeoutSetInAppDomain = new bool?(flag);
				}
				return RegexUtil._isRegexTimeoutSetInAppDomain.Value;
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00026160 File Offset: 0x00024360
		private static int GetRegexTimeout(int? timeoutInMillsec)
		{
			int num = -1;
			if (timeoutInMillsec != null)
			{
				num = timeoutInMillsec.Value;
			}
			else if (!RegexUtil.IsRegexTimeoutSetInAppDomain && BinaryCompatibility.Current.TargetsAtLeastFramework461)
			{
				num = 2000;
			}
			return num;
		}

		// Token: 0x040011C7 RID: 4551
		private static bool? _isRegexTimeoutSetInAppDomain;
	}
}
