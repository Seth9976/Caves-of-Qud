using System;
using System.IO;
using System.Text.RegularExpressions;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200007A RID: 122
	public static class StackFilter
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		public static string Filter(string rawTrace)
		{
			if (rawTrace == null)
			{
				return null;
			}
			StringReader stringReader = new StringReader(rawTrace);
			StringWriter stringWriter = new StringWriter();
			try
			{
				string text;
				while ((text = stringReader.ReadLine()) != null)
				{
					if (!StackFilter.assertOrAssumeRegex.IsMatch(text))
					{
						break;
					}
				}
				while (text != null && text.IndexOf(" System.Reflection.") < 0)
				{
					stringWriter.WriteLine(text.Trim());
					text = stringReader.ReadLine();
				}
			}
			catch (Exception)
			{
				return rawTrace;
			}
			return stringWriter.ToString();
		}

		// Token: 0x040000D5 RID: 213
		private static readonly Regex assertOrAssumeRegex = new Regex(" NUnit\\.Framework\\.Ass(ert|ume)\\.");
	}
}
