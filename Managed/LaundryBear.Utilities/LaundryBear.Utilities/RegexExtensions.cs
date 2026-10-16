using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LaundryBear
{
	// Token: 0x02000007 RID: 7
	public static class RegexExtensions
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023AC File Offset: 0x000005AC
		public static string ReplaceGroup(this Regex regex, string input, string groupName, string replacement)
		{
			return regex.Replace(input, delegate(Match match)
			{
				Group group = match.Groups[groupName];
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				foreach (Capture capture in group.Captures.Cast<Capture>())
				{
					int num2 = capture.Index + capture.Length - match.Index;
					int num3 = capture.Index - match.Index - num;
					stringBuilder.Append(match.Value.Substring(num, num3));
					stringBuilder.Append(replacement);
					num = num2;
				}
				stringBuilder.Append(match.Value.Substring(num));
				return stringBuilder.ToString();
			});
		}
	}
}
