using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CompilerOptionParseUtilities
	{
		// Token: 0x060000EA RID: 234 RVA: 0x0000484B File Offset: 0x00002A4B
		public static IList<string> ParseFeatureFromMSBuild([Nullable(2)] string features)
		{
			if (RoslynString.IsNullOrEmpty(features))
			{
				return new List<string>(0);
			}
			return features.Split(new char[] { ';', ',', ' ' }, 1);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004874 File Offset: 0x00002A74
		public static void ParseFeatures(IDictionary<string, string> builder, List<string> values)
		{
			foreach (string text in values)
			{
				foreach (string text2 in text.Split(new char[] { ',' }, 1))
				{
					CompilerOptionParseUtilities.ParseFeatureCore(builder, text2);
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000048E8 File Offset: 0x00002AE8
		private static void ParseFeatureCore(IDictionary<string, string> builder, string feature)
		{
			int num = feature.IndexOf('=');
			if (num > 0)
			{
				string text = feature.Substring(0, num);
				string text2 = feature.Substring(num + 1);
				builder[text] = text2;
				return;
			}
			builder[feature] = "true";
		}
	}
}
