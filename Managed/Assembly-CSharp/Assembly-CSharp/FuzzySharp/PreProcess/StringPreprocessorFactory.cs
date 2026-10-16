using System;
using System.Text.RegularExpressions;

namespace FuzzySharp.PreProcess
{
	// Token: 0x020001CD RID: 461
	internal class StringPreprocessorFactory
	{
		// Token: 0x06001089 RID: 4233 RVA: 0x00065D1E File Offset: 0x00063F1E
		private static string Default(string input)
		{
			input = Regex.Replace(input, StringPreprocessorFactory.pattern, " ");
			input = input.ToLower();
			return input.Trim();
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00065D40 File Offset: 0x00063F40
		public static Func<string, string> GetPreprocessor(PreprocessMode mode)
		{
			if (mode == PreprocessMode.Full)
			{
				return new Func<string, string>(StringPreprocessorFactory.Default);
			}
			if (mode != PreprocessMode.None)
			{
				throw new InvalidOperationException(string.Format("Invalid string preprocessor mode: {0}", mode));
			}
			return (string s) => s;
		}

		// Token: 0x04000B0B RID: 2827
		private static string pattern = "[^ a-zA-Z0-9]";
	}
}
