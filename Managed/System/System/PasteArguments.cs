using System;
using System.Text;

namespace System
{
	// Token: 0x02000147 RID: 327
	internal static class PasteArguments
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0001FD24 File Offset: 0x0001DF24
		internal static void AppendArgument(StringBuilder stringBuilder, string argument)
		{
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append(' ');
			}
			if (argument.Length != 0 && PasteArguments.ContainsNoWhitespaceOrQuotes(argument))
			{
				stringBuilder.Append(argument);
				return;
			}
			stringBuilder.Append('"');
			int i = 0;
			while (i < argument.Length)
			{
				char c = argument[i++];
				if (c == '\\')
				{
					int num = 1;
					while (i < argument.Length && argument[i] == '\\')
					{
						i++;
						num++;
					}
					if (i == argument.Length)
					{
						stringBuilder.Append('\\', num * 2);
					}
					else if (argument[i] == '"')
					{
						stringBuilder.Append('\\', num * 2 + 1);
						stringBuilder.Append('"');
						i++;
					}
					else
					{
						stringBuilder.Append('\\', num);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append('\\');
					stringBuilder.Append('"');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			stringBuilder.Append('"');
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001FE20 File Offset: 0x0001E020
		private static bool ContainsNoWhitespaceOrQuotes(string s)
		{
			foreach (char c in s)
			{
				if (char.IsWhiteSpace(c) || c == '"')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000568 RID: 1384
		private const char Quote = '"';

		// Token: 0x04000569 RID: 1385
		private const char Backslash = '\\';
	}
}
