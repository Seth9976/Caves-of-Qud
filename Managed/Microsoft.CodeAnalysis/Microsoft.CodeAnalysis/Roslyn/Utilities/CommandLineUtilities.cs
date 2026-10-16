using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Roslyn.Utilities
{
	// Token: 0x02000032 RID: 50
	[NullableContext(1)]
	[Nullable(0)]
	internal static class CommandLineUtilities
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00004688 File Offset: 0x00002888
		public static IEnumerable<string> SplitCommandLineIntoArguments(string commandLine, bool removeHashComments)
		{
			char? c;
			return CommandLineUtilities.SplitCommandLineIntoArguments(commandLine, removeHashComments, out c);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000046A0 File Offset: 0x000028A0
		public static IEnumerable<string> SplitCommandLineIntoArguments(string commandLine, bool removeHashComments, out char? illegalChar)
		{
			StringBuilder stringBuilder = new StringBuilder(commandLine.Length);
			List<string> list = new List<string>();
			int i = 0;
			illegalChar = default(char?);
			while (i < commandLine.Length)
			{
				while (i < commandLine.Length && char.IsWhiteSpace(commandLine.get_Chars(i)))
				{
					i++;
				}
				if (i == commandLine.Length || (commandLine.get_Chars(i) == '#' && removeHashComments))
				{
					break;
				}
				int num = 0;
				stringBuilder.Length = 0;
				while (i < commandLine.Length && (!char.IsWhiteSpace(commandLine.get_Chars(i)) || num % 2 != 0))
				{
					char c = commandLine.get_Chars(i);
					if (c != '"')
					{
						if (c == '\\')
						{
							int num2 = 0;
							do
							{
								stringBuilder.Append(commandLine.get_Chars(i));
								i++;
								num2++;
							}
							while (i < commandLine.Length && commandLine.get_Chars(i) == '\\');
							if (i < commandLine.Length && commandLine.get_Chars(i) == '"')
							{
								if (num2 % 2 == 0)
								{
									num++;
								}
								stringBuilder.Append('"');
								i++;
							}
						}
						else
						{
							if ((c >= '\u0001' && c <= '\u001f') || c == '|')
							{
								if (illegalChar == null)
								{
									illegalChar = new char?(c);
								}
							}
							else
							{
								stringBuilder.Append(c);
							}
							i++;
						}
					}
					else
					{
						stringBuilder.Append(c);
						num++;
						i++;
					}
				}
				if (num == 2 && stringBuilder.get_Chars(0) == '"' && stringBuilder.get_Chars(stringBuilder.Length - 1) == '"')
				{
					stringBuilder.Remove(0, 1);
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
				}
				if (stringBuilder.Length > 0)
				{
					list.Add(stringBuilder.ToString());
				}
			}
			return list;
		}
	}
}
