using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using HistoryKit;
using XRL.Rules;
using XRL.World;

namespace XRL.Language
{
	// Token: 0x020005E5 RID: 1509
	public static class Grammar
	{
		// Token: 0x060040C6 RID: 16582 RVA: 0x00140A18 File Offset: 0x0013EC18
		public static string Pluralize(string word)
		{
			if (string.IsNullOrEmpty(word))
			{
				return word;
			}
			if (word[0] == '=')
			{
				return "=pluralize=" + word;
			}
			string text;
			if (Grammar.singularToPlural.TryGetValue(word, out text))
			{
				return text;
			}
			if (Grammar.irregularPluralization.TryGetValue(word, out text))
			{
				return Grammar.FoundPlural(word, text);
			}
			Match match = Regex.Match(word, "^{{(.*?)\\|(.*)}}$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, string.Concat(new string[]
				{
					"{{",
					match.Groups[1].Value,
					"|",
					Grammar.Pluralize(match.Groups[2].Value),
					"}}"
				}));
			}
			match = Regex.Match(word, "(.*?)(&.(?:\\^.)?)$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, Grammar.Pluralize(match.Groups[1].Value) + match.Groups[2].Value);
			}
			match = Regex.Match(word, "^(&.(?:\\^.)?)(.*?)$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, match.Groups[1].Value + Grammar.Pluralize(match.Groups[2].Value));
			}
			match = Regex.Match(word, "^([*\\-_~'\"/])(.*)(\\1)$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, match.Groups[1].Value + Grammar.Pluralize(match.Groups[2].Value) + match.Groups[3].Value);
			}
			match = Regex.Match(word, "(.*?)( +)$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, Grammar.Pluralize(match.Groups[1].Value) + match.Groups[2].Value);
			}
			match = Regex.Match(word, "^( +)(.*?)$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, match.Groups[1].Value + Grammar.Pluralize(match.Groups[2].Value));
			}
			match = Regex.Match(word, "^(.*)( \\(.*\\))$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, Grammar.Pluralize(match.Groups[1].Value) + match.Groups[2].Value);
			}
			match = Regex.Match(word, "^(.*)( \\[.*\\])$");
			if (match.Success)
			{
				return Grammar.FoundPlural(word, Grammar.Pluralize(match.Groups[1].Value) + match.Groups[2].Value);
			}
			match = Regex.Match(word, "^(.*)( (?:of|in a|in an|in the|into|for|from|o'|to|with) .*)$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				return Grammar.FoundPlural(word, Grammar.Pluralize(match.Groups[1].Value) + match.Groups[2].Value);
			}
			match = Regex.Match(word, "^(.*)( (?:mk\\.?|mark) *(?:[ivx]+))$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				return Grammar.FoundPlural(word, Grammar.Pluralize(match.Groups[1].Value) + match.Groups[2].Value);
			}
			if (!word.Contains(" "))
			{
				match = Regex.Match(word, "^(.*)-(.*)$");
				if (match.Success)
				{
					return Grammar.FoundPlural(word, match.Groups[1].Value + "-" + Grammar.Pluralize(match.Groups[2].Value));
				}
			}
			if (Grammar.identicalPluralization.Contains(word))
			{
				return Grammar.FoundPlural(word, word);
			}
			if (word.EndsWith("folk"))
			{
				return Grammar.FoundPlural(word, word);
			}
			if (Grammar.latinPluralization.Contains(word))
			{
				if (word.EndsWith("us"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "i");
				}
				if (word.EndsWith("a"))
				{
					return Grammar.FoundPlural(word, word + "e");
				}
				if (word.EndsWith("num"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "i");
				}
				if (word.EndsWith("um") || word.EndsWith("on"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "a");
				}
				if (word.EndsWith("en"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "ina");
				}
				if (word.EndsWith("is"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "es");
				}
				if (word.EndsWith("es"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "ites");
				}
				if (word.EndsWith("ex") || word.EndsWith("ix"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "ices");
				}
				return Grammar.FoundPlural(word, word);
			}
			else if (Grammar.greekPluralization1.Contains(word))
			{
				if (word.EndsWith("os") || word.EndsWith("on"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 1) + "i");
				}
				if (word.EndsWith("is") || word.EndsWith("ix") || word.EndsWith("as"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "des");
				}
				if (word.EndsWith("ys"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "daes");
				}
				if (word.EndsWith("ma"))
				{
					return Grammar.FoundPlural(word, word + "ta");
				}
				if (word.EndsWith("a"))
				{
					return Grammar.FoundPlural(word, word + "e");
				}
				if (word.EndsWith("x"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "ges");
				}
				if (word.EndsWith("or"))
				{
					return Grammar.FoundPlural(word, word + "es");
				}
				if (word.EndsWith("r"))
				{
					return Grammar.FoundPlural(word, word + "oi");
				}
				return Grammar.FoundPlural(word, word + "a");
			}
			else if (Grammar.greekPluralization2.Contains(word))
			{
				if (word.EndsWith("on"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "a");
				}
				if (word.EndsWith("is"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "es");
				}
				return Grammar.FoundPlural(word, word);
			}
			else if (Grammar.hebrewPluralization.Contains(word))
			{
				if (word.EndsWith("ah"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "ot");
				}
				if (word.EndsWith("da"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 1) + "ot");
				}
				if (word.EndsWith("esh"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 3) + "ashot");
				}
				if (word.EndsWith("ch") || word.EndsWith("kh"))
				{
					return Grammar.FoundPlural(word, word + "ot");
				}
				if (word.EndsWith("a"))
				{
					return Grammar.FoundPlural(word, word.Substring(0, word.Length - 1) + "im");
				}
				return Grammar.FoundPlural(word, word + "im");
			}
			else
			{
				if (word.Contains(" "))
				{
					string[] array = word.Split(' ', StringSplitOptions.None);
					StringBuilder stringBuilder = Event.NewStringBuilder(null);
					if (Grammar.dualPluralization.Contains(array[array.Length - 1]))
					{
						for (int i = 0; i < array.Length - 2; i++)
						{
							stringBuilder.Append(array[i]);
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(Grammar.Pluralize(array[array.Length - 2]));
						stringBuilder.Append(" ");
						stringBuilder.Append(Grammar.Pluralize(array[array.Length - 1]));
					}
					else
					{
						for (int j = 0; j < array.Length - 1; j++)
						{
							stringBuilder.Append(array[j]);
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(Grammar.Pluralize(array[array.Length - 1]));
					}
					return Grammar.FoundPlural(word, stringBuilder.ToString().Trim());
				}
				if (ColorUtility.HasUpperExceptFormatting(word))
				{
					if (ColorUtility.IsAllUpperExceptFormatting(word))
					{
						return Grammar.FoundPlural(word, ColorUtility.ToUpperExceptFormatting(Grammar.Pluralize(ColorUtility.ToLowerExceptFormatting(word))));
					}
					if (ColorUtility.IsFirstUpperExceptFormatting(word))
					{
						return Grammar.FoundPlural(word, ColorUtility.CapitalizeExceptFormatting(Grammar.Pluralize(ColorUtility.ToLowerExceptFormatting(word))));
					}
					return Grammar.FoundPlural(word, Grammar.Pluralize(ColorUtility.ToLowerExceptFormatting(word)));
				}
				else
				{
					if (word.EndsWith("elf") || word.EndsWith("olf") || word.EndsWith("arf") || word.EndsWith("alf"))
					{
						return Grammar.FoundPlural(word, word.Substring(0, word.Length - 1) + "ves");
					}
					if (word.EndsWith("man") && !string.Equals(word, "human"))
					{
						return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "en");
					}
					if (word.EndsWith("ife"))
					{
						return Grammar.FoundPlural(word, word.Substring(0, word.Length - 2) + "ves");
					}
					if (word.EndsWith("mensch"))
					{
						return Grammar.FoundPlural(word, word + "en");
					}
					if (word.Length == 1)
					{
						return Grammar.FoundPlural(word, word + "s");
					}
					char c = word[word.Length - 1];
					char c2 = word[word.Length - 2];
					string text2 = word.Substring(word.Length - 2, 2);
					text = word;
					if (c == 'z' && (c2 == 'a' || c2 == 'e' || c2 == 'i' || c2 == 'o' || c2 == 'u'))
					{
						text += "z";
					}
					if (c == 's' || c == 'x' || c == 'z' || text2 == "sh" || text2 == "ss" || text2 == "ch" || (c == 'o' && c2 != 'o' && c2 != 'b'))
					{
						text += "es";
					}
					else if (c == 'y' && c2 != 'a' && c2 != 'e' && c2 != 'i' && c2 != 'o' && c2 != 'u')
					{
						text = text.Substring(0, text.Length - 1) + "ies";
					}
					else
					{
						text += "s";
					}
					return Grammar.FoundPlural(word, text);
				}
			}
		}

		/// support method for Pluralize()
		// Token: 0x060040C7 RID: 16583 RVA: 0x001415A9 File Offset: 0x0013F7A9
		private static string FoundPlural(string word, string plural)
		{
			if (!Grammar.singularToPlural.ContainsKey(word))
			{
				Grammar.singularToPlural.Add(word, plural);
			}
			if (!Grammar.pluralToSingular.ContainsKey(plural))
			{
				Grammar.pluralToSingular.Add(plural, word);
			}
			return plural;
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x001415E0 File Offset: 0x0013F7E0
		public static string ThirdPerson(string word, bool PrependSpace = false)
		{
			if (string.IsNullOrEmpty(word))
			{
				return word;
			}
			string text = "";
			if (PrependSpace)
			{
				if (Grammar.firstPersonToThirdPersonWithSpace.TryGetValue(word, out text))
				{
					return text;
				}
			}
			else if (Grammar.firstPersonToThirdPerson.TryGetValue(word, out text))
			{
				return text;
			}
			if (Grammar.irregularThirdPerson.TryGetValue(word, out text))
			{
				return Grammar.FoundThirdPerson(word, text, PrependSpace);
			}
			Match match = Regex.Match(word, "(.*?)(&.(?:\\^.)?)$");
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, Grammar.ThirdPerson(match.Groups[1].Value, false) + match.Groups[2].Value, PrependSpace);
			}
			match = Regex.Match(word, "^(&.(?:\\^.)?)(.*?)$");
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, match.Groups[1].Value + Grammar.ThirdPerson(match.Groups[2].Value, false), PrependSpace);
			}
			match = Regex.Match(word, "^([*\\-_~'\"/])(.*)(\\1)$");
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, match.Groups[1].Value + Grammar.ThirdPerson(match.Groups[2].Value, false) + match.Groups[3].Value, PrependSpace);
			}
			match = Regex.Match(word, "(.*?)( +)$");
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, Grammar.ThirdPerson(match.Groups[1].Value, false) + match.Groups[2].Value, PrependSpace);
			}
			match = Regex.Match(word, "^( +)(.*?)$");
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, match.Groups[1].Value + Grammar.ThirdPerson(match.Groups[2].Value, false), PrependSpace);
			}
			match = Regex.Match(word, "^(.+)( (?:and|or) )(.+)$", RegexOptions.IgnoreCase);
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, Grammar.ThirdPerson(match.Groups[1].Value, false) + match.Groups[2].Value + Grammar.ThirdPerson(match.Groups[3].Value, false), PrependSpace);
			}
			if (word.Contains(" "))
			{
				string[] array = word.Split(' ', StringSplitOptions.None);
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				for (int i = 0; i < array.Length - 1; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(Grammar.ThirdPerson(array[array.Length - 1], false));
				return Grammar.FoundThirdPerson(word, stringBuilder.ToString(), PrependSpace);
			}
			match = Regex.Match(word, "^(.*)-(.*)$");
			if (match.Success)
			{
				return Grammar.FoundThirdPerson(word, match.Groups[1].Value + "-" + Grammar.ThirdPerson(match.Groups[2].Value, false), PrependSpace);
			}
			if (ColorUtility.HasUpperExceptFormatting(word))
			{
				if (ColorUtility.IsAllUpperExceptFormatting(word))
				{
					return Grammar.FoundThirdPerson(word, ColorUtility.ToUpperExceptFormatting(Grammar.ThirdPerson(ColorUtility.ToLowerExceptFormatting(word), false)), PrependSpace);
				}
				if (ColorUtility.IsFirstUpperExceptFormatting(word))
				{
					return Grammar.FoundThirdPerson(word, ColorUtility.CapitalizeExceptFormatting(Grammar.ThirdPerson(ColorUtility.ToLowerExceptFormatting(word), false)), PrependSpace);
				}
				return Grammar.FoundThirdPerson(word, Grammar.ThirdPerson(ColorUtility.ToLowerExceptFormatting(word), false), PrependSpace);
			}
			else
			{
				if (word.Length == 1)
				{
					return Grammar.FoundThirdPerson(word, word + "s", PrependSpace);
				}
				char c = word[word.Length - 1];
				char c2 = word[word.Length - 2];
				string text2 = word.Substring(word.Length - 2, 2);
				text = word;
				if (c == 'z' && (c2 == 'a' || c2 == 'e' || c2 == 'i' || c2 == 'o' || c2 == 'u'))
				{
					text += "z";
				}
				if (c == 's' || c == 'x' || c == 'z' || text2 == "sh" || text2 == "ss" || text2 == "ch" || (c == 'o' && c2 != 'o' && c2 != 'b'))
				{
					text += "es";
				}
				else if (c == 'y' && c2 != 'a' && c2 != 'e' && c2 != 'i' && c2 != 'o' && c2 != 'u')
				{
					text = text.Substring(0, text.Length - 1) + "ies";
				}
				else
				{
					text += "s";
				}
				return Grammar.FoundThirdPerson(word, text, PrependSpace);
			}
		}

		/// support method for ThirdPerson()
		// Token: 0x060040C9 RID: 16585 RVA: 0x00141A58 File Offset: 0x0013FC58
		private static string FoundThirdPerson(string firstPerson, string thirdPerson, bool PrependSpace)
		{
			if (!Grammar.firstPersonToThirdPerson.ContainsKey(firstPerson))
			{
				Grammar.firstPersonToThirdPerson.Add(firstPerson, thirdPerson);
			}
			if (!Grammar.thirdPersonToFirstPerson.ContainsKey(thirdPerson))
			{
				Grammar.thirdPersonToFirstPerson.Add(thirdPerson, firstPerson);
			}
			if (PrependSpace)
			{
				thirdPerson = " " + thirdPerson;
				if (!Grammar.firstPersonToThirdPersonWithSpace.ContainsKey(firstPerson))
				{
					Grammar.firstPersonToThirdPersonWithSpace.Add(firstPerson, thirdPerson);
				}
			}
			return thirdPerson;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x00141AC4 File Offset: 0x0013FCC4
		public static string PastTenseOf(string verb)
		{
			if (verb == "sleep")
			{
				return "slept";
			}
			if (verb == "sit")
			{
				return "sat";
			}
			if (verb == "drink")
			{
				return "drank";
			}
			if (verb == "put")
			{
				return "put";
			}
			if (verb == "are")
			{
				return "was";
			}
			if (verb == "have")
			{
				return "had";
			}
			if (verb == "eat")
			{
				return "ate";
			}
			if (verb.EndsWith("e"))
			{
				return verb + "d";
			}
			return verb + "ed";
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x00141B7A File Offset: 0x0013FD7A
		public static string CardinalNo(int num)
		{
			if (num != 0)
			{
				return Grammar.Cardinal(num);
			}
			return "no";
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x00141B8B File Offset: 0x0013FD8B
		public static string CardinalNo(long num)
		{
			if (num != 0L)
			{
				return Grammar.Cardinal(num);
			}
			return "no";
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x00141B9C File Offset: 0x0013FD9C
		public static string Cardinal(int num)
		{
			if (num == 0)
			{
				return "zero";
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (num < 0)
			{
				stringBuilder.Append("negative");
				num = -num;
			}
			int num2 = (int)Math.Floor(Math.Log10((double)num));
			Grammar.ProcessMagnitudes(ref num, ref num2, stringBuilder, null);
			if (num >= 20)
			{
				int num3 = num % 10;
				int num4 = (num - num3) / 10;
				num = num3;
				switch (num4)
				{
				case 2:
					stringBuilder.Append("twent");
					break;
				case 3:
					stringBuilder.Append("thirt");
					break;
				case 4:
					stringBuilder.Append("fort");
					break;
				case 5:
					stringBuilder.Append("fift");
					break;
				case 6:
					stringBuilder.Append("sixt");
					break;
				case 7:
					stringBuilder.Append("sevent");
					break;
				case 8:
					stringBuilder.Append("eight");
					break;
				case 9:
					stringBuilder.Append("ninet");
					break;
				}
				if (num == 0)
				{
					stringBuilder.Append("y");
					return stringBuilder.ToString();
				}
				stringBuilder.Append("y-");
			}
			switch (num)
			{
			case 1:
				stringBuilder.Append("one");
				break;
			case 2:
				stringBuilder.Append("two");
				break;
			case 3:
				stringBuilder.Append("three");
				break;
			case 4:
				stringBuilder.Append("four");
				break;
			case 5:
				stringBuilder.Append("five");
				break;
			case 6:
				stringBuilder.Append("six");
				break;
			case 7:
				stringBuilder.Append("seven");
				break;
			case 8:
				stringBuilder.Append("eight");
				break;
			case 9:
				stringBuilder.Append("nine");
				break;
			case 10:
				stringBuilder.Append("ten");
				break;
			case 11:
				stringBuilder.Append("eleven");
				break;
			case 12:
				stringBuilder.Append("twelve");
				break;
			case 13:
				stringBuilder.Append("thirteen");
				break;
			case 14:
				stringBuilder.Append("fourteen");
				break;
			case 15:
				stringBuilder.Append("fifteen");
				break;
			case 16:
				stringBuilder.Append("sixteen");
				break;
			case 17:
				stringBuilder.Append("seventeen");
				break;
			case 18:
				stringBuilder.Append("eighteen");
				break;
			case 19:
				stringBuilder.Append("nineteen");
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x00141E3C File Offset: 0x0014003C
		public static string Cardinal(long num)
		{
			if (num == 0L)
			{
				return "zero";
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (num < 0L)
			{
				stringBuilder.Append("negative");
				num = -num;
			}
			int num2 = (int)Math.Floor(Math.Log10((double)num));
			Grammar.ProcessMagnitudes(ref num, ref num2, stringBuilder, null);
			if (num >= 20L)
			{
				long num3 = num % 10L;
				long num4 = (num - num3) / 10L;
				num = num3;
				long num5 = num4 - 2L;
				if (num5 <= 7L)
				{
					switch ((uint)num5)
					{
					case 0U:
						stringBuilder.Append("twent");
						break;
					case 1U:
						stringBuilder.Append("thirt");
						break;
					case 2U:
						stringBuilder.Append("fort");
						break;
					case 3U:
						stringBuilder.Append("fift");
						break;
					case 4U:
						stringBuilder.Append("sixt");
						break;
					case 5U:
						stringBuilder.Append("sevent");
						break;
					case 6U:
						stringBuilder.Append("eight");
						break;
					case 7U:
						stringBuilder.Append("ninet");
						break;
					}
				}
				if (num == 0L)
				{
					stringBuilder.Append("y");
					return stringBuilder.ToString();
				}
				stringBuilder.Append("y-");
			}
			long num6 = num - 1L;
			if (num6 <= 18L)
			{
				switch ((uint)num6)
				{
				case 0U:
					stringBuilder.Append("one");
					break;
				case 1U:
					stringBuilder.Append("two");
					break;
				case 2U:
					stringBuilder.Append("three");
					break;
				case 3U:
					stringBuilder.Append("four");
					break;
				case 4U:
					stringBuilder.Append("five");
					break;
				case 5U:
					stringBuilder.Append("six");
					break;
				case 6U:
					stringBuilder.Append("seven");
					break;
				case 7U:
					stringBuilder.Append("eight");
					break;
				case 8U:
					stringBuilder.Append("nine");
					break;
				case 9U:
					stringBuilder.Append("ten");
					break;
				case 10U:
					stringBuilder.Append("eleven");
					break;
				case 11U:
					stringBuilder.Append("twelve");
					break;
				case 12U:
					stringBuilder.Append("thirteen");
					break;
				case 13U:
					stringBuilder.Append("fourteen");
					break;
				case 14U:
					stringBuilder.Append("fifteen");
					break;
				case 15U:
					stringBuilder.Append("sixteen");
					break;
				case 16U:
					stringBuilder.Append("seventeen");
					break;
				case 17U:
					stringBuilder.Append("eighteen");
					break;
				case 18U:
					stringBuilder.Append("nineteen");
					break;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x001420FC File Offset: 0x001402FC
		public static string Ordinal(int num)
		{
			if (num == 0)
			{
				return "zeroth";
			}
			Grammar.SB1.Length = 0;
			if (num < 0)
			{
				Grammar.SB1.Append("negative");
				num = -num;
			}
			int num2 = (int)Math.Floor(Math.Log10((double)num));
			Grammar.ProcessMagnitudes(ref num, ref num2, Grammar.SB1, "th");
			if (num >= 20)
			{
				int num3 = num % 10;
				int num4 = (num - num3) / 10;
				num = num3;
				switch (num4)
				{
				case 2:
					Grammar.SB1.Append("twent");
					break;
				case 3:
					Grammar.SB1.Append("thirt");
					break;
				case 4:
					Grammar.SB1.Append("fort");
					break;
				case 5:
					Grammar.SB1.Append("fift");
					break;
				case 6:
					Grammar.SB1.Append("sixt");
					break;
				case 7:
					Grammar.SB1.Append("sevent");
					break;
				case 8:
					Grammar.SB1.Append("eight");
					break;
				case 9:
					Grammar.SB1.Append("ninet");
					break;
				}
				if (num == 0)
				{
					Grammar.SB1.Append("ieth");
					return Grammar.SB1.ToString();
				}
				Grammar.SB1.Append("y-");
			}
			switch (num)
			{
			case 1:
				Grammar.SB1.Append("first");
				break;
			case 2:
				Grammar.SB1.Append("second");
				break;
			case 3:
				Grammar.SB1.Append("third");
				break;
			case 4:
				Grammar.SB1.Append("fourth");
				break;
			case 5:
				Grammar.SB1.Append("fifth");
				break;
			case 6:
				Grammar.SB1.Append("sixth");
				break;
			case 7:
				Grammar.SB1.Append("seventh");
				break;
			case 8:
				Grammar.SB1.Append("eighth");
				break;
			case 9:
				Grammar.SB1.Append("ninth");
				break;
			case 10:
				Grammar.SB1.Append("tenth");
				break;
			case 11:
				Grammar.SB1.Append("eleventh");
				break;
			case 12:
				Grammar.SB1.Append("twelfth");
				break;
			case 13:
				Grammar.SB1.Append("thirteenth");
				break;
			case 14:
				Grammar.SB1.Append("fourteenth");
				break;
			case 15:
				Grammar.SB1.Append("fifteenth");
				break;
			case 16:
				Grammar.SB1.Append("sixteenth");
				break;
			case 17:
				Grammar.SB1.Append("seventeenth");
				break;
			case 18:
				Grammar.SB1.Append("eighteenth");
				break;
			case 19:
				Grammar.SB1.Append("nineteenth");
				break;
			}
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x00142430 File Offset: 0x00140630
		public static string Ordinal(long num)
		{
			if (num == 0L)
			{
				return "zeroth";
			}
			Grammar.SB1.Length = 0;
			if (num < 0L)
			{
				Grammar.SB1.Append("negative");
				num = -num;
			}
			int num2 = (int)Math.Floor(Math.Log10((double)num));
			Grammar.ProcessMagnitudes(ref num, ref num2, Grammar.SB1, "th");
			if (num >= 20L)
			{
				long num3 = num % 10L;
				long num4 = (num - num3) / 10L;
				num = num3;
				long num5 = num4 - 2L;
				if (num5 <= 7L)
				{
					switch ((uint)num5)
					{
					case 0U:
						Grammar.SB1.Append("twent");
						break;
					case 1U:
						Grammar.SB1.Append("thirt");
						break;
					case 2U:
						Grammar.SB1.Append("fort");
						break;
					case 3U:
						Grammar.SB1.Append("fift");
						break;
					case 4U:
						Grammar.SB1.Append("sixt");
						break;
					case 5U:
						Grammar.SB1.Append("sevent");
						break;
					case 6U:
						Grammar.SB1.Append("eight");
						break;
					case 7U:
						Grammar.SB1.Append("ninet");
						break;
					}
				}
				if (num == 0L)
				{
					Grammar.SB1.Append("ieth");
					return Grammar.SB1.ToString();
				}
				Grammar.SB1.Append("y-");
			}
			long num6 = num - 1L;
			if (num6 <= 18L)
			{
				switch ((uint)num6)
				{
				case 0U:
					Grammar.SB1.Append("first");
					break;
				case 1U:
					Grammar.SB1.Append("second");
					break;
				case 2U:
					Grammar.SB1.Append("third");
					break;
				case 3U:
					Grammar.SB1.Append("fourth");
					break;
				case 4U:
					Grammar.SB1.Append("fifth");
					break;
				case 5U:
					Grammar.SB1.Append("sixth");
					break;
				case 6U:
					Grammar.SB1.Append("seventh");
					break;
				case 7U:
					Grammar.SB1.Append("eighth");
					break;
				case 8U:
					Grammar.SB1.Append("ninth");
					break;
				case 9U:
					Grammar.SB1.Append("tenth");
					break;
				case 10U:
					Grammar.SB1.Append("eleventh");
					break;
				case 11U:
					Grammar.SB1.Append("twelfth");
					break;
				case 12U:
					Grammar.SB1.Append("thirteenth");
					break;
				case 13U:
					Grammar.SB1.Append("fourteenth");
					break;
				case 14U:
					Grammar.SB1.Append("fifteenth");
					break;
				case 15U:
					Grammar.SB1.Append("sixteenth");
					break;
				case 16U:
					Grammar.SB1.Append("seventeenth");
					break;
				case 17U:
					Grammar.SB1.Append("eighteenth");
					break;
				case 18U:
					Grammar.SB1.Append("nineteenth");
					break;
				}
			}
			return Grammar.SB1.ToString();
		}

		/// support method for Cardinal() and Ordinal()
		// Token: 0x060040D1 RID: 16593 RVA: 0x00142784 File Offset: 0x00140984
		private static void ProcessMagnitude(ref int num, ref int magnitude, StringBuilder result, string place)
		{
			if (magnitude > 3)
			{
				magnitude -= magnitude % 3;
			}
			int num2 = (int)Math.Floor(Math.Exp((double)magnitude * Math.Log(10.0)));
			int num3 = num % num2;
			int num4 = (num - num3) / num2;
			if (num4 > 0)
			{
				result.Append(Grammar.Cardinal(num4));
				result.Append(" ");
				result.Append(place);
				num = num3;
				if (num > 0)
				{
					result.Append(" ");
				}
			}
			magnitude--;
		}

		/// support method for Cardinal() and Ordinal()
		// Token: 0x060040D2 RID: 16594 RVA: 0x00142808 File Offset: 0x00140A08
		private static void ProcessMagnitude(ref long num, ref int magnitude, StringBuilder result, string place)
		{
			if (magnitude > 3)
			{
				magnitude -= magnitude % 3;
			}
			int num2 = (int)Math.Floor(Math.Exp((double)magnitude * Math.Log(10.0)));
			long num3 = num % (long)num2;
			long num4 = (num - num3) / (long)num2;
			if (num4 > 0L)
			{
				result.Append(Grammar.Cardinal(num4));
				result.Append(" ");
				result.Append(place);
				num = num3;
				if (num > 0L)
				{
					result.Append(" ");
				}
			}
			magnitude--;
		}

		/// support method for Cardinal() and Ordinal()
		// Token: 0x060040D3 RID: 16595 RVA: 0x00142890 File Offset: 0x00140A90
		private static bool ProcessMagnitudes(ref int num, ref int magnitude, StringBuilder result, string suffix = null)
		{
			switch (magnitude)
			{
			case 2:
				goto IL_0110;
			case 3:
			case 4:
			case 5:
				goto IL_00F2;
			case 6:
			case 7:
			case 8:
				goto IL_00D4;
			case 9:
			case 10:
			case 11:
				goto IL_00B6;
			case 12:
			case 13:
			case 14:
				goto IL_0098;
			case 15:
			case 16:
			case 17:
				break;
			case 18:
			case 19:
			case 20:
				Grammar.ProcessMagnitude(ref num, ref magnitude, result, "quintillion");
				if (num == 0)
				{
					if (suffix != null)
					{
						result.Append(suffix);
					}
					return true;
				}
				break;
			default:
				return false;
			}
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "quadrillion");
			if (num == 0)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_0098:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "trillion");
			if (num == 0)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_00B6:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "billion");
			if (num == 0)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_00D4:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "million");
			if (num == 0)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_00F2:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "thousand");
			if (num == 0)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_0110:
			if (magnitude > 1)
			{
				Grammar.ProcessMagnitude(ref num, ref magnitude, result, "hundred");
				if (num == 0)
				{
					if (suffix != null)
					{
						result.Append(suffix);
					}
					return true;
				}
			}
			return false;
		}

		/// support method for Cardinal() and Ordinal()
		// Token: 0x060040D4 RID: 16596 RVA: 0x001429D4 File Offset: 0x00140BD4
		private static bool ProcessMagnitudes(ref long num, ref int magnitude, StringBuilder result, string suffix = null)
		{
			switch (magnitude)
			{
			case 2:
				goto IL_0110;
			case 3:
			case 4:
			case 5:
				goto IL_00F2;
			case 6:
			case 7:
			case 8:
				goto IL_00D4;
			case 9:
			case 10:
			case 11:
				goto IL_00B6;
			case 12:
			case 13:
			case 14:
				goto IL_0098;
			case 15:
			case 16:
			case 17:
				break;
			case 18:
			case 19:
			case 20:
				Grammar.ProcessMagnitude(ref num, ref magnitude, result, "quintillion");
				if (num == 0L)
				{
					if (suffix != null)
					{
						result.Append(suffix);
					}
					return true;
				}
				break;
			default:
				return false;
			}
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "quadrillion");
			if (num == 0L)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_0098:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "trillion");
			if (num == 0L)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_00B6:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "billion");
			if (num == 0L)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_00D4:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "million");
			if (num == 0L)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_00F2:
			Grammar.ProcessMagnitude(ref num, ref magnitude, result, "thousand");
			if (num == 0L)
			{
				if (suffix != null)
				{
					result.Append(suffix);
				}
				return true;
			}
			IL_0110:
			if (magnitude > 1)
			{
				Grammar.ProcessMagnitude(ref num, ref magnitude, result, "hundred");
				if (num == 0L)
				{
					if (suffix != null)
					{
						result.Append(suffix);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x00142B18 File Offset: 0x00140D18
		public static string Multiplicative(int num)
		{
			switch (num)
			{
			case 0:
				return "never";
			case 1:
				return "once";
			case 2:
				return "twice";
			case 3:
				return "thrice";
			default:
				return Grammar.Cardinal(num) + " times";
			}
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x00142B68 File Offset: 0x00140D68
		public static string Multiplicative(long num)
		{
			if (num <= 3L)
			{
				switch ((uint)num)
				{
				case 0U:
					return "never";
				case 1U:
					return "once";
				case 2U:
					return "twice";
				case 3U:
					return "thrice";
				}
			}
			return Grammar.Cardinal(num) + " times";
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00142BC0 File Offset: 0x00140DC0
		public static string MakeOrList(string[] Words, bool Serial = true)
		{
			if (Words.Length == 0)
			{
				return "";
			}
			if (Words.Length == 1)
			{
				return Words[0];
			}
			if (Words.Length == 2)
			{
				return Words[0] + " or " + Words[1];
			}
			string text = ",";
			int i = 0;
			int num = Words.Length;
			while (i < num)
			{
				if (Words[i].Contains(','))
				{
					text = ";";
					break;
				}
				i++;
			}
			Grammar.SB1.Length = 0;
			int j = 0;
			int num2 = Words.Length - 2;
			while (j < num2)
			{
				Grammar.SB1.Append(Words[j]);
				Grammar.SB1.Append((Serial || j < num2 - 1) ? text : ' ');
				j++;
			}
			Grammar.SB1.Append(Words[Words.Length - 1]);
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x00142C8C File Offset: 0x00140E8C
		public static string MakeOrList(List<string> Words, bool Serial = true)
		{
			if (Words.Count == 0)
			{
				return "";
			}
			if (Words.Count == 1)
			{
				return Words[0];
			}
			if (Words.Count == 2)
			{
				return Words[0] + " or " + Words[1];
			}
			string text = ", ";
			int i = 0;
			int count = Words.Count;
			while (i < count)
			{
				if (Words[i].Contains(','))
				{
					text = "; ";
					break;
				}
				i++;
			}
			Grammar.SB1.Length = 0;
			int j = 0;
			int num = Words.Count - 1;
			while (j < num)
			{
				Grammar.SB1.Append(Words[j]);
				Grammar.SB1.Append((Serial || j < num - 1) ? text : ' ');
				j++;
			}
			Grammar.SB1.Append("or ");
			Grammar.SB1.Append(Words[Words.Count - 1]);
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x00142D94 File Offset: 0x00140F94
		public static string MakeOrList(List<GameObject> Objects, bool DefiniteArticles = false, bool Serial = true, bool Reflexive = false, bool AsPossessed = false, bool SecondPerson = true, GameObject AsPossessedBy = null)
		{
			if (Objects == null || Objects.Count == 0)
			{
				return "";
			}
			List<string> list = new List<string>(Objects.Count);
			foreach (GameObject gameObject in Objects)
			{
				List<string> list2 = list;
				GameObject gameObject2 = gameObject;
				int maxValue = int.MaxValue;
				string text = null;
				string text2 = null;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = true;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = true;
				bool flag11 = false;
				bool flag12 = DefiniteArticles && !AsPossessed;
				list2.Add(gameObject2.GetDisplayName(maxValue, text, text2, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, !DefiniteArticles && !AsPossessed, flag12, null, false, false, SecondPerson, Reflexive, null, AsPossessed, AsPossessedBy, false, true));
			}
			return Grammar.MakeOrList(list, Serial);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x00142E58 File Offset: 0x00141058
		public static string MakeAndList(IReadOnlyList<string> Words, bool Serial = true)
		{
			if (Words.Count == 0)
			{
				return "";
			}
			if (Words.Count == 1)
			{
				return Words[0];
			}
			if (Words.Count == 2)
			{
				return Words[0] + " and " + Words[1];
			}
			string text = ", ";
			int i = 0;
			int count = Words.Count;
			while (i < count)
			{
				if (Words[i].Contains(','))
				{
					text = "; ";
					break;
				}
				i++;
			}
			Grammar.SB1.Length = 0;
			int j = 0;
			int num = Words.Count - 1;
			while (j < num)
			{
				Grammar.SB1.Append(Words[j]);
				Grammar.SB1.Append((Serial || j < num - 1) ? text : ' ');
				j++;
			}
			Grammar.SB1.Append("and ");
			Grammar.SB1.Append(Words[Words.Count - 1]);
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x00142F60 File Offset: 0x00141160
		public static string MakeAndList(List<GameObject> Objects, bool DefiniteArticles = false, bool Serial = true, bool Reflexive = false, bool SecondPerson = true, bool AsPossessed = false, GameObject AsPossessedBy = null)
		{
			if (Objects == null || Objects.Count == 0)
			{
				return "";
			}
			List<string> list = new List<string>(Objects.Count);
			foreach (GameObject gameObject in Objects)
			{
				List<string> list2 = list;
				GameObject gameObject2 = gameObject;
				int maxValue = int.MaxValue;
				string text = null;
				string text2 = null;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = true;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = true;
				bool flag11 = false;
				bool flag12 = DefiniteArticles && !AsPossessed;
				list2.Add(gameObject2.GetDisplayName(maxValue, text, text2, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, !DefiniteArticles && !AsPossessed, flag12, null, false, false, SecondPerson, Reflexive, null, AsPossessed, AsPossessedBy, false, true));
			}
			return Grammar.MakeAndList(list, Serial);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00143024 File Offset: 0x00141224
		public static string MakeTheList(IReadOnlyList<string> Words, bool Capitalize = false)
		{
			if (Words.Count == 0)
			{
				return "";
			}
			string text = ", ";
			int i = 0;
			int count = Words.Count;
			while (i < count)
			{
				if (Words[i].Contains(','))
				{
					text = "; ";
					break;
				}
				i++;
			}
			StringBuilder sb = Grammar.SB1;
			sb.Length = 0;
			int j = 0;
			int count2 = Words.Count;
			while (j < count2)
			{
				if (j != 0)
				{
					sb.Append(text);
				}
				sb.Append((Capitalize && j == 0) ? "The " : "the ");
				sb.Append(Words[j]);
				j++;
			}
			return sb.ToString();
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x001430D4 File Offset: 0x001412D4
		public static string MakePossessive(string word)
		{
			int num = 0;
			while (word.EndsWith("}}"))
			{
				num++;
				word = word.Substring(0, word.Length - 2);
			}
			if (word == "you")
			{
				word = "your";
			}
			else if (word == "You")
			{
				word = "Your";
			}
			else if (word == "YOU")
			{
				word = "YOUR";
			}
			else if (word.EndsWith("s"))
			{
				word += "'";
			}
			else
			{
				word += "'s";
			}
			for (int i = 0; i < num; i++)
			{
				word += "}}";
			}
			return word;
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x0014318C File Offset: 0x0014138C
		public static string MakeCompoundWord(string Word1, string Word2, bool UseHyphen = false)
		{
			Grammar.SB1.Clear();
			if (object.Equals(Word1[Word1.Count<char>() - 1], Word2[0]) || UseHyphen)
			{
				Grammar.SB1.Append(Word1);
				Grammar.SB1.Append("-");
				Grammar.SB1.Append(Word2);
			}
			else
			{
				Grammar.SB1.Append(Word1);
				Grammar.SB1.Append(Word2);
			}
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00143218 File Offset: 0x00141418
		public static string MakeTitleCase(string Phrase)
		{
			string[] array = Phrase.Split(' ', StringSplitOptions.None);
			bool flag = true;
			Grammar.SB1.Clear();
			foreach (string text in array)
			{
				if (flag)
				{
					Grammar.SB1.Append(Grammar.InitialCap(text));
					flag = false;
				}
				else if (Grammar.IsLowerCapWord(text))
				{
					Grammar.SB1.Append(text);
				}
				else
				{
					Grammar.SB1.Append(Grammar.InitialCap(text));
				}
				Grammar.SB1.Append(" ");
			}
			return Grammar.SB1.ToString().TrimEnd(' ');
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x001432B0 File Offset: 0x001414B0
		public static string MakeLowerCase(string Phrase)
		{
			string[] array = Phrase.Split(' ', StringSplitOptions.None);
			Grammar.SB1.Clear();
			foreach (string text in array)
			{
				Grammar.SB1.Append(Grammar.InitLower(text)).Append(" ");
			}
			return Grammar.SB1.ToString().TrimEnd(' ');
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x00143310 File Offset: 0x00141510
		public static string InitialCap(string Word)
		{
			if (Word.IsNullOrEmpty())
			{
				return Word;
			}
			if (Word.Contains("-"))
			{
				string[] array = Word.Split('-', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].Length > 1)
					{
						array[i] = array[i].Capitalize();
					}
					else if (i == 0)
					{
						array[i] = array[i].ToUpper();
					}
				}
				return string.Join("-", array);
			}
			return Word.Capitalize();
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00143384 File Offset: 0x00141584
		public static bool IsSupportingWord(string Word)
		{
			string text = (Word.Any<char>(new Func<char, bool>(char.IsUpper)) ? Word.ToLower() : Word);
			return Grammar.prepositions.Contains(text) || Grammar.articles.Contains(text) || Grammar.conjunctions.Contains(text) || Grammar.badEndingWords.Contains(text) || Grammar.badStartingWords.Contains(text);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x001433F0 File Offset: 0x001415F0
		public static bool IsLowerCapWord(string Word)
		{
			string text = (Word.Any<char>(new Func<char, bool>(char.IsUpper)) ? Word.ToLower() : Word);
			return Grammar.shortPrepositions.Contains(text) || Grammar.articles.Contains(text) || Grammar.conjunctions.Contains(text);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00143444 File Offset: 0x00141644
		public static bool IsBadTitleStartingWord(string Word)
		{
			string text = (Word.Any<char>(new Func<char, bool>(char.IsUpper)) ? Word.ToLower() : Word);
			return Grammar.badStartingWords.Contains(text);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0014347C File Offset: 0x0014167C
		private static bool IsArticleException(string Word)
		{
			if (Word == null || Grammar.articleExceptions == null)
			{
				return false;
			}
			for (int i = 0; i < Grammar.articleExceptions.Length; i++)
			{
				if (string.Equals(Grammar.articleExceptions[i], Word, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x001434BC File Offset: 0x001416BC
		public static string RemoveBadTitleEndingWords(string Phrase)
		{
			string[] array = Phrase.Split(' ', StringSplitOptions.None);
			if (Grammar.IsSupportingWord(array[array.Length - 1]))
			{
				array[array.Length - 1] = "";
				return Grammar.RemoveBadTitleEndingWords(string.Join(" ", array).TrimEnd(' '));
			}
			return Phrase;
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00143508 File Offset: 0x00141708
		public static string RemoveBadTitleStartingWords(string Phrase)
		{
			string[] array = Phrase.Split(' ', StringSplitOptions.None);
			if (Grammar.IsBadTitleStartingWord(array[0]))
			{
				array[0] = "";
				return Grammar.RemoveBadTitleStartingWords(string.Join(" ", array).TrimStart(' '));
			}
			return Phrase;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x0014354A File Offset: 0x0014174A
		public static string RandomShePronoun()
		{
			if (!50.in100())
			{
				return "she";
			}
			return "he";
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x00143560 File Offset: 0x00141760
		public static string ObjectPronoun(string subjectPronoun)
		{
			if (subjectPronoun == "he")
			{
				return "him";
			}
			return "her";
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x0014357A File Offset: 0x0014177A
		public static string PossessivePronoun(string subjectPronoun)
		{
			if (subjectPronoun == "he")
			{
				return "his";
			}
			return "her";
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x00143594 File Offset: 0x00141794
		public static string ReflexivePronoun(string subjectPronoun)
		{
			if (subjectPronoun == "he")
			{
				return "himself";
			}
			return "herself";
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x001435B0 File Offset: 0x001417B0
		public unsafe static string InitCap(string word)
		{
			if (word.IsNullOrEmpty())
			{
				return "";
			}
			if (char.IsUpper(word[0]))
			{
				return word;
			}
			int length = word.Length;
			if (length == 1)
			{
				return char.ToUpper(word[0]).ToString();
			}
			checked
			{
				if (length < 64)
				{
					int num = length;
					Span<char> span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num) * 2], num);
					word.AsSpan().CopyTo(span);
					*span[0] = char.ToUpper(*span[0]);
					return new string(span);
				}
				char[] array = word.ToCharArray();
				array[0] = char.ToUpper(array[0]);
				return new string(array);
			}
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x00143660 File Offset: 0x00141860
		public static string InitCapWithFormatting(string word)
		{
			if (string.IsNullOrEmpty(word))
			{
				return "";
			}
			if (word.Length == 1)
			{
				return char.ToUpper(word[0]).ToString();
			}
			char[] array = word.ToCharArray();
			for (int i = 0; i < word.Length; i++)
			{
				if (word[i] != '&' && word[i] != '^')
				{
					array[i] = char.ToUpper(array[i]);
					break;
				}
				i++;
			}
			return new string(array);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x001436E0 File Offset: 0x001418E0
		public static string CapAfterNewlines(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return "";
			}
			Grammar.SB1.Length = 0;
			string[] array = text.Split('\n', StringSplitOptions.None);
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				Grammar.SB1.Append(Grammar.InitCap(array[i]));
				if (i < num - 1)
				{
					Grammar.SB1.Append('\n');
				}
				i++;
			}
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x00143750 File Offset: 0x00141950
		public static string InitLower(string Word)
		{
			if (Word.IsNullOrEmpty())
			{
				return "";
			}
			if (char.IsLower(Word[0]))
			{
				return Word;
			}
			if (Word.Length == 1)
			{
				return char.ToLower(Word[0]).ToString();
			}
			return ColorUtility.UncapitalizeExceptFormatting(Word);
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x0014379F File Offset: 0x0014199F
		public static string InitLowerIfArticle(string Word)
		{
			if (ColorUtility.BeginsWithExceptFormatting(Word, "A ") || ColorUtility.BeginsWithExceptFormatting(Word, "An ") || ColorUtility.BeginsWithExceptFormatting(Word, "The ") || ColorUtility.BeginsWithExceptFormatting(Word, "Some "))
			{
				return ColorUtility.UncapitalizeExceptFormatting(Word);
			}
			return Word;
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x001437E0 File Offset: 0x001419E0
		public static string LowerArticles(string Text)
		{
			Text = Regex.Replace(Text, "\\bA\\b", "a");
			Text = Regex.Replace(Text, "\\bAn\\b", "an");
			Text = Regex.Replace(Text, "\\bThe\\b", "the");
			Text = Regex.Replace(Text, "\\bSome\\b", "some");
			return Text;
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x00143838 File Offset: 0x00141A38
		public static string MakeTitleCaseWithArticle(string phrase)
		{
			if (phrase.StartsWith("a ") || phrase.StartsWith("A ") || phrase.StartsWith("an ") || phrase.StartsWith("An ") || phrase.StartsWith("the ") || phrase.StartsWith("The ") || phrase.StartsWith("some ") || phrase.StartsWith("Some "))
			{
				string text = Grammar.MakeTitleCase(phrase);
				return char.ToLower(text[0]).ToString() + text.Substring(1);
			}
			return Grammar.MakeTitleCase(phrase);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x001438DB File Offset: 0x00141ADB
		private static bool IsNonWord(char Ch)
		{
			return !char.IsLetterOrDigit(Ch) && Ch != '-';
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x001438F0 File Offset: 0x00141AF0
		public static bool IndefiniteArticleShouldBeAn(string Word)
		{
			if (Word.IsNullOrEmpty())
			{
				return false;
			}
			string text = Word;
			if (ColorUtility.HasFormatting(text))
			{
				text = ColorUtility.StripFormatting(text);
			}
			if (text.IndexOf(' ') == 0)
			{
				text = text.TrimStart(' ');
			}
			int num = text.IndexOf(' ');
			if (num != -1)
			{
				text = text.Substring(0, num);
			}
			bool flag = false;
			for (int i = 0; i < text.Length; i++)
			{
				if (Grammar.IsNonWord(text[i]))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				Grammar.SB2.Clear();
				int j = 0;
				int length = text.Length;
				while (j < length)
				{
					if (!Grammar.IsNonWord(text[j]))
					{
						Grammar.SB2.Append(text[j]);
					}
					j++;
				}
				text = Grammar.SB2.ToString();
			}
			if (text.Length > 0 && text[0] == '-')
			{
				text = text.TrimStart('-');
			}
			if (text == "")
			{
				return false;
			}
			char c = text[0];
			return ((c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U') ^ Grammar.IsArticleException(text)) && !text.StartsWith("one-", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x00143A44 File Offset: 0x00141C44
		public static bool IndefiniteArticleShouldBeAn(int Number)
		{
			return Grammar.IndefiniteArticleShouldBeAn(Grammar.Cardinal(Number));
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x00143A51 File Offset: 0x00141C51
		public static string IndefiniteArticle(string Word, bool Capitalize = false)
		{
			if (!Grammar.IndefiniteArticleShouldBeAn(Word))
			{
				if (!Capitalize)
				{
					return "a ";
				}
				return "A ";
			}
			else
			{
				if (!Capitalize)
				{
					return "an ";
				}
				return "An ";
			}
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x00143A78 File Offset: 0x00141C78
		public static string A(string Word, bool Capitalize = false)
		{
			if (Word[0] == '=')
			{
				return "=article=" + Word;
			}
			return (Grammar.IndefiniteArticleShouldBeAn(Word) ? (Capitalize ? "An " : "an ") : (Capitalize ? "A " : "a ")) + Word;
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x00143ACC File Offset: 0x00141CCC
		public static void A(string Word, StringBuilder Result, bool Capitalize = false)
		{
			if (Word[0] == '=')
			{
				Result.Append("=article=").Append(Word);
				return;
			}
			Result.Append(Grammar.IndefiniteArticleShouldBeAn(Word) ? (Capitalize ? "An " : "an ") : (Capitalize ? "A " : "a ")).Append(Word);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x00143B2C File Offset: 0x00141D2C
		public static string A(int Number, bool Capitalize = false)
		{
			return (Grammar.IndefiniteArticleShouldBeAn(Number) ? (Capitalize ? "An " : "an ") : (Capitalize ? "A " : "a ")) + Number.ToString();
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x00143B62 File Offset: 0x00141D62
		public static void A(int Number, StringBuilder Result, bool Capitalize = false)
		{
			Result.Append(Grammar.IndefiniteArticleShouldBeAn(Number) ? (Capitalize ? "An " : "an ") : (Capitalize ? "A " : "a ")).Append(Number);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x00143B9C File Offset: 0x00141D9C
		public static string ConvertAtoAn(string sentence)
		{
			string[] array = sentence.Split(' ', StringSplitOptions.None);
			Grammar.SB1.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				Grammar.SB1.Append(array[i]);
				if (i < array.Length - 1)
				{
					if ((array[i].Equals("a") || array[i].Equals("A")) && !string.IsNullOrEmpty(array[i + 1]) && Grammar.IndefiniteArticleShouldBeAn(array[i + 1]))
					{
						Grammar.SB1.Append("n");
					}
					Grammar.SB1.Append(" ");
				}
			}
			return Grammar.SB1.ToString();
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x00143C42 File Offset: 0x00141E42
		public static string AOrAnBeforeNumber(int number)
		{
			if (number == 11)
			{
				return "an";
			}
			while (number >= 10)
			{
				number /= 10;
			}
			if (number == 8)
			{
				return "an";
			}
			return "a";
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x00143C6C File Offset: 0x00141E6C
		public static string GetWordRoot(string word)
		{
			string randomMeaningfulWord = Grammar.GetRandomMeaningfulWord(word);
			string text = "";
			string[] array = Regex.Split(randomMeaningfulWord, "(?=[aeiouy])");
			for (int i = 0; i <= array.Length - 1; i++)
			{
				if (i == array.Length - 1)
				{
					text += array[i].TrimEnd(new char[] { 'a', 'e', 'i', 'o', 'u', 'y' });
				}
				else
				{
					text += array[i];
				}
			}
			return text;
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x00143CD8 File Offset: 0x00141ED8
		public static string Adjectify(string word)
		{
			string randomElement = new string[] { "ian", "ic", "-like", "ary", "ique" }.GetRandomElement<string>(null);
			word = Grammar.TrimTrailingS(word);
			word = Grammar.RemovePunctuation(word);
			if (randomElement[0] != '-')
			{
				word = Grammar.GetWordRoot(word);
			}
			return word + randomElement;
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x00143D44 File Offset: 0x00141F44
		public static string TrimLeadingThe(string phrase)
		{
			if (phrase.StartsWith("the ") && phrase.Length >= 5)
			{
				return phrase.Substring(4);
			}
			return phrase;
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x00143D65 File Offset: 0x00141F65
		public static string TrimTrailingS(string word)
		{
			if (word[word.Length - 1] == 's')
			{
				return word.Substring(0, word.Length - 1);
			}
			return word;
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x00143D8A File Offset: 0x00141F8A
		public static string TrimTrailingPunctuation(string phrase)
		{
			if (phrase.Length == 0)
			{
				return phrase;
			}
			if (Grammar.punctuation.Contains(phrase[phrase.Length - 1]))
			{
				return Grammar.TrimTrailingPunctuation(phrase.Substring(0, phrase.Length - 1));
			}
			return phrase;
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x00143DC8 File Offset: 0x00141FC8
		public static string RemovePunctuation(string word)
		{
			foreach (char c in Grammar.punctuation)
			{
				if (word.IndexOf(c) != -1)
				{
					word = word.Replace(c.ToString() ?? "", "");
				}
			}
			return word;
		}

		// Token: 0x06004103 RID: 16643 RVA: 0x00143E18 File Offset: 0x00142018
		public static string GetRandomMeaningfulWord(string phrase)
		{
			string[] array = phrase.Split(' ', StringSplitOptions.None);
			string text = array[Stat.Random(0, array.Length - 1)];
			int num = 500;
			while (Grammar.prepositions.Contains(text) || Grammar.articles.Contains(text) || Grammar.conjunctions.Contains(text) || Grammar.ordinalsRoman.Contains(text) || (Grammar.demonstrativePronouns.Contains(text) && num > 0))
			{
				text = array[Stat.Random(0, array.Length - 1)];
				num--;
			}
			return text;
		}

		// Token: 0x06004104 RID: 16644 RVA: 0x00143E9D File Offset: 0x0014209D
		public static string GetWeightedStartingArticle()
		{
			return Grammar.articleStartingWords.GetRandomElement<string>(null);
		}

		// Token: 0x06004105 RID: 16645 RVA: 0x00143EAC File Offset: 0x001420AC
		public static string Stutterize(string Sentence, string Word)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			int i = 0;
			int length = Sentence.Length;
			while (i < length)
			{
				char c2 = Sentence[i];
				if (c2 == '=')
				{
					flag = !flag;
				}
				else if (c2 == '*')
				{
					flag2 = !flag2;
				}
				else if (c2 == ' ' && !flag && !flag2 && c != ' ')
				{
					num++;
				}
				if (!flag && !flag2)
				{
					c = c2;
				}
				i++;
			}
			if (num <= 2)
			{
				return Sentence;
			}
			Grammar.StutterSB.Clear();
			int num2 = Stat.Random(0, num - 3);
			int num3 = ((num2 == 0) ? 4 : 0);
			int num4 = 0;
			flag = false;
			flag2 = false;
			c = '\0';
			int j = 0;
			int length2 = Sentence.Length;
			while (j < length2)
			{
				char c3 = Sentence[j];
				if (c3 == '=')
				{
					flag = !flag;
				}
				else if (c3 == '*')
				{
					flag2 = !flag2;
				}
				else if (c3 == ' ' && !flag && !flag2 && c != ' ')
				{
					num4++;
					if (num4 == num2)
					{
						num3 = 4;
					}
					if (num3 == 1)
					{
						Grammar.StutterSB.Append("... ").Append(Word);
						num3--;
					}
					else if (num3 > 0)
					{
						Grammar.StutterSB.Append("...");
						num3--;
					}
				}
				Grammar.StutterSB.Append(c3);
				if (!flag && !flag2)
				{
					c = c3;
				}
				j++;
			}
			if (num3 == 1)
			{
				Grammar.StutterSB.Append("... ").Append(Word);
			}
			else if (num3 > 0)
			{
				Grammar.StutterSB.Append("...");
			}
			return Grammar.StutterSB.ToString();
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x00144040 File Offset: 0x00142240
		public static string GetProsaicZoneName(Zone Z)
		{
			if (Z.HasProperName)
			{
				return Z.DisplayName;
			}
			if (Z.NameContext != null)
			{
				return "the outskirts of " + Z.NameContext;
			}
			string text;
			if (Z == null)
			{
				text = null;
			}
			else
			{
				GameObject terrainObject = Z.GetTerrainObject();
				text = ((terrainObject != null) ? terrainObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false) : null);
			}
			return text ?? "an unnamed place";
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x001440B4 File Offset: 0x001422B4
		public static string GetRomanNumeral(int val)
		{
			if (val == 0)
			{
				return "N";
			}
			Grammar.SB1.Clear();
			if (val < 0)
			{
				Grammar.SB1.Append("-");
				val = -val;
			}
			if (val >= 1000)
			{
				int i = 0;
				int num = val / 1000;
				while (i < num)
				{
					Grammar.SB1.Append('M');
					i++;
				}
				val %= 1000;
			}
			if (val >= 100)
			{
				int num2 = val / 100;
				switch (num2)
				{
				case 4:
					Grammar.SB1.Append("CD");
					break;
				case 5:
					Grammar.SB1.Append('D');
					break;
				case 6:
				case 7:
				case 8:
				{
					Grammar.SB1.Append('D');
					int j = 0;
					int num3 = num2 - 5;
					while (j < num3)
					{
						Grammar.SB1.Append('C');
						j++;
					}
					break;
				}
				case 9:
					Grammar.SB1.Append("CM");
					break;
				default:
				{
					for (int k = 0; k < num2; k++)
					{
						Grammar.SB1.Append('C');
					}
					break;
				}
				}
				val %= 100;
			}
			if (val >= 10)
			{
				int num4 = val / 10;
				switch (num4)
				{
				case 4:
					Grammar.SB1.Append("XL");
					break;
				case 5:
					Grammar.SB1.Append('L');
					break;
				case 6:
				case 7:
				case 8:
				{
					Grammar.SB1.Append('L');
					int l = 0;
					int num5 = num4 - 5;
					while (l < num5)
					{
						Grammar.SB1.Append('X');
						l++;
					}
					break;
				}
				case 9:
					Grammar.SB1.Append("XC");
					break;
				default:
				{
					for (int m = 0; m < num4; m++)
					{
						Grammar.SB1.Append('X');
					}
					break;
				}
				}
				val %= 10;
			}
			switch (val)
			{
			case 4:
				Grammar.SB1.Append("IV");
				break;
			case 5:
				Grammar.SB1.Append('V');
				break;
			case 6:
			case 7:
			case 8:
			{
				Grammar.SB1.Append('V');
				int n = 0;
				int num6 = val - 5;
				while (n < num6)
				{
					Grammar.SB1.Append('I');
					n++;
				}
				break;
			}
			case 9:
				Grammar.SB1.Append("IX");
				break;
			default:
			{
				for (int num7 = 0; num7 < val; num7++)
				{
					Grammar.SB1.Append('I');
				}
				break;
			}
			}
			return Grammar.SB1.ToString();
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x0014433C File Offset: 0x0014253C
		public static string GetRomanNumeral(long val)
		{
			if (val == 0L)
			{
				return "N";
			}
			Grammar.SB1.Clear();
			if (val < 0L)
			{
				Grammar.SB1.Append("-");
				val = -val;
			}
			if (val >= 1000L)
			{
				long num = 0L;
				long num2 = val / 1000L;
				while (num < num2)
				{
					Grammar.SB1.Append('M');
					num += 1L;
				}
				val %= 1000L;
			}
			if (val >= 100L)
			{
				long num3 = val / 100L;
				long num4 = num3 - 4L;
				if (num4 <= 5L)
				{
					switch ((uint)num4)
					{
					case 0U:
						Grammar.SB1.Append("CD");
						goto IL_0124;
					case 1U:
						Grammar.SB1.Append('D');
						goto IL_0124;
					case 2U:
					case 3U:
					case 4U:
					{
						Grammar.SB1.Append('D');
						long num5 = 0L;
						long num6 = num3 - 5L;
						while (num5 < num6)
						{
							Grammar.SB1.Append('C');
							num5 += 1L;
						}
						goto IL_0124;
					}
					case 5U:
						Grammar.SB1.Append("CM");
						goto IL_0124;
					}
				}
				int num7 = 0;
				while ((long)num7 < num3)
				{
					Grammar.SB1.Append('C');
					num7++;
				}
				IL_0124:
				val %= 100L;
			}
			if (val >= 10L)
			{
				long num8 = val / 10L;
				long num9 = num8 - 4L;
				if (num9 <= 5L)
				{
					switch ((uint)num9)
					{
					case 0U:
						Grammar.SB1.Append("XL");
						goto IL_01F4;
					case 1U:
						Grammar.SB1.Append('L');
						goto IL_01F4;
					case 2U:
					case 3U:
					case 4U:
					{
						Grammar.SB1.Append('L');
						long num10 = 0L;
						long num11 = num8 - 5L;
						while (num10 < num11)
						{
							Grammar.SB1.Append('X');
							num10 += 1L;
						}
						goto IL_01F4;
					}
					case 5U:
						Grammar.SB1.Append("XC");
						goto IL_01F4;
					}
				}
				for (long num12 = 0L; num12 < num8; num12 += 1L)
				{
					Grammar.SB1.Append('X');
				}
				IL_01F4:
				val %= 10L;
			}
			long num13 = val - 4L;
			if (num13 <= 5L)
			{
				switch ((uint)num13)
				{
				case 0U:
					Grammar.SB1.Append("IV");
					goto IL_02B1;
				case 1U:
					Grammar.SB1.Append('V');
					goto IL_02B1;
				case 2U:
				case 3U:
				case 4U:
				{
					Grammar.SB1.Append('V');
					long num14 = 0L;
					long num15 = val - 5L;
					while (num14 < num15)
					{
						Grammar.SB1.Append('I');
						num14 += 1L;
					}
					goto IL_02B1;
				}
				case 5U:
					Grammar.SB1.Append("IX");
					goto IL_02B1;
				}
			}
			for (long num16 = 0L; num16 < val; num16 += 1L)
			{
				Grammar.SB1.Append('I');
			}
			IL_02B1:
			return Grammar.SB1.ToString();
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x00144604 File Offset: 0x00142804
		public static string Weirdify(string word, int Chance = 100)
		{
			char randomElement = Grammar.weirdLowerAs.GetRandomElement<char>(null);
			char randomElement2 = Grammar.weirdUpperAs.GetRandomElement<char>(null);
			char randomElement3 = Grammar.weirdLowerEs.GetRandomElement<char>(null);
			char randomElement4 = Grammar.weirdLowerEs.GetRandomElement<char>(null);
			char randomElement5 = Grammar.weirdLowerIs.GetRandomElement<char>(null);
			char randomElement6 = Grammar.weirdUpperIs.GetRandomElement<char>(null);
			char randomElement7 = Grammar.weirdLowerOs.GetRandomElement<char>(null);
			char randomElement8 = Grammar.weirdUpperOs.GetRandomElement<char>(null);
			char randomElement9 = Grammar.weirdLowerUs.GetRandomElement<char>(null);
			char randomElement10 = Grammar.weirdUpperUs.GetRandomElement<char>(null);
			char randomElement11 = Grammar.weirdLowerCs.GetRandomElement<char>(null);
			char randomElement12 = Grammar.weirdLowerFs.GetRandomElement<char>(null);
			char randomElement13 = Grammar.weirdLowerNs.GetRandomElement<char>(null);
			char randomElement14 = Grammar.weirdLowerTs.GetRandomElement<char>(null);
			char randomElement15 = Grammar.weirdLowerYs.GetRandomElement<char>(null);
			char randomElement16 = Grammar.weirdUpperBs.GetRandomElement<char>(null);
			char randomElement17 = Grammar.weirdUpperCs.GetRandomElement<char>(null);
			char randomElement18 = Grammar.weirdUpperYs.GetRandomElement<char>(null);
			char randomElement19 = Grammar.weirdUpperLs.GetRandomElement<char>(null);
			char randomElement20 = Grammar.weirdUpperRs.GetRandomElement<char>(null);
			char randomElement21 = Grammar.weirdUpperNs.GetRandomElement<char>(null);
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('a', randomElement);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('A', randomElement2);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('e', randomElement3);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('E', randomElement4);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('i', randomElement5);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('I', randomElement6);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('o', randomElement7);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('O', randomElement8);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('u', randomElement9);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('U', randomElement10);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('c', randomElement11);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('f', randomElement12);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('n', randomElement13);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('t', randomElement14);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('y', randomElement15);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('B', randomElement16);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('C', randomElement17);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('Y', randomElement18);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('L', randomElement19);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('R', randomElement20);
			}
			if (If.Chance(Chance, null, null))
			{
				word = word.Replace('N', randomElement21);
			}
			return word;
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x001448EC File Offset: 0x00142AEC
		public static string Obfuscate(string phrase, int noiseValue = 15, int sameObfuscatorChance = 80, int onlyObfuscastorsChance = 5)
		{
			StringBuilder stringBuilder = new StringBuilder();
			char randomElement = Grammar.obfuscators.GetRandomElement<char>(null);
			bool flag = If.Chance(onlyObfuscastorsChance, null, null);
			foreach (char c in phrase)
			{
				if (c == ' ')
				{
					stringBuilder.Append(c);
				}
				else if (If.Chance(noiseValue, null, null))
				{
					if (If.Chance(sameObfuscatorChance, null, null))
					{
						stringBuilder.Append(randomElement);
					}
					else
					{
						stringBuilder.Append(Grammar.obfuscators.GetRandomElement<char>(null));
					}
				}
				else if (!flag)
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x00144990 File Offset: 0x00142B90
		public static int LevenshteinDistance(string s, string t, bool caseInsensitive = true)
		{
			int length = s.Length;
			int length2 = t.Length;
			if (caseInsensitive)
			{
				for (int i = 0; i < length; i++)
				{
					if (char.IsUpper(s[i]))
					{
						s = s.ToLower();
						break;
					}
				}
				for (int j = 0; j < length2; j++)
				{
					if (char.IsUpper(t[j]))
					{
						t = t.ToLower();
						break;
					}
				}
			}
			int[,] array = new int[length + 1, length2 + 1];
			if (length == 0)
			{
				return length2;
			}
			if (length2 == 0)
			{
				return length;
			}
			int k = 0;
			while (k <= length)
			{
				array[k, 0] = k++;
			}
			int l = 0;
			while (l <= length2)
			{
				array[0, l] = l++;
			}
			for (int m = 1; m <= length; m++)
			{
				for (int n = 1; n <= length2; n++)
				{
					int num = ((t[n - 1] == s[m - 1]) ? 0 : 1);
					array[m, n] = Math.Min(Math.Min(array[m - 1, n] + 1, array[m, n - 1] + 1), array[m - 1, n - 1] + num);
				}
			}
			return array[length, length2];
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x00144AD0 File Offset: 0x00142CD0
		public static string ClosestMatch(IList<string> Options, string Text, bool CaseInsensitive = true)
		{
			if (Options.IsNullOrEmpty<string>())
			{
				return null;
			}
			string text = Options[0];
			int num = Grammar.LevenshteinDistance(text, Text, CaseInsensitive);
			int i = 1;
			int count = Options.Count;
			while (i < count)
			{
				int num2 = Grammar.LevenshteinDistance(Options[i], Text, CaseInsensitive);
				if (num > num2)
				{
					text = Options[i];
					num = num2;
				}
				i++;
			}
			return text;
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x00144B2C File Offset: 0x00142D2C
		public static bool ContainsBadWords(string phrase)
		{
			foreach (string text in Grammar.badWords)
			{
				if (phrase.Contains(text, CompareOptions.IgnoreCase))
				{
					return true;
				}
			}
			foreach (string text2 in Grammar.badWordsExact)
			{
				if (phrase.EqualsNoCase(text2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001FC2 RID: 8130
		public static bool AllowSecondPerson = true;

		// Token: 0x04001FC3 RID: 8131
		private static StringBuilder StutterSB = new StringBuilder();

		// Token: 0x04001FC4 RID: 8132
		public static char[] weirdLowerAs = new char[] { '\u0083', '\u0084', '\u0085', '\u0086', '\u00a0', '¦', 'à', 'a' };

		// Token: 0x04001FC5 RID: 8133
		public static char[] weirdUpperAs = new char[] { 'A', '\u008e', '\u008f', '\u0092' };

		// Token: 0x04001FC6 RID: 8134
		public static char[] weirdLowerEs = new char[] { 'e', '\u0082', '\u0088', '\u0089', '\u008a', 'î' };

		// Token: 0x04001FC7 RID: 8135
		public static char[] weirdUpperEs = new char[] { 'E', '\u0090', 'ä' };

		// Token: 0x04001FC8 RID: 8136
		public static char[] weirdLowerIs = new char[] { 'i', '\u008b', '\u008c', '\u008d', '¡', '\u00ad' };

		// Token: 0x04001FC9 RID: 8137
		public static char[] weirdUpperIs = new char[] { 'I', '\u00ad', '³' };

		// Token: 0x04001FCA RID: 8138
		public static char[] weirdLowerOs = new char[] { 'o', '\u0093', '\u0094', '\u0095', '¢', 'å', 'ë', 'ø' };

		// Token: 0x04001FCB RID: 8139
		public static char[] weirdUpperOs = new char[] { 'O', '\u0099', 'è', 'é', 'í' };

		// Token: 0x04001FCC RID: 8140
		public static char[] weirdLowerUs = new char[] { 'u', '\u0096', '\u0097', '£', 'æ' };

		// Token: 0x04001FCD RID: 8141
		public static char[] weirdUpperUs = new char[] { 'U', '\u009a', 'æ' };

		// Token: 0x04001FCE RID: 8142
		public static char[] weirdLowerCs = new char[] { 'c', '\u0087', '\u009b' };

		// Token: 0x04001FCF RID: 8143
		public static char[] weirdLowerFs = new char[] { 'f', '\u009f' };

		// Token: 0x04001FD0 RID: 8144
		public static char[] weirdLowerNs = new char[] { 'n', '¤', 'ã', 'ï' };

		// Token: 0x04001FD1 RID: 8145
		public static char[] weirdLowerTs = new char[] { 't', 'ç' };

		// Token: 0x04001FD2 RID: 8146
		public static char[] weirdLowerYs = new char[] { 'y', '\u0098' };

		// Token: 0x04001FD3 RID: 8147
		public static char[] weirdUpperBs = new char[] { 'B', 'á' };

		// Token: 0x04001FD4 RID: 8148
		public static char[] weirdUpperCs = new char[] { 'C', '\u0080' };

		// Token: 0x04001FD5 RID: 8149
		public static char[] weirdUpperYs = new char[] { 'Y', '\u009d' };

		// Token: 0x04001FD6 RID: 8150
		public static char[] weirdUpperLs = new char[] { 'L', '\u009c' };

		// Token: 0x04001FD7 RID: 8151
		public static char[] weirdUpperRs = new char[] { 'R', '\u009e' };

		// Token: 0x04001FD8 RID: 8152
		public static char[] weirdUpperNs = new char[] { 'N', '¥', 'î' };

		// Token: 0x04001FD9 RID: 8153
		private static char[] obfuscators = new char[]
		{
			'\u0005', '\u000f', '\u0016', '\u001e', '\u001f', '²', 'Û', 'Ü', 'Ý', 'Þ',
			'ß', 'ø', 'þ'
		};

		// Token: 0x04001FDA RID: 8154
		private static string[] shortPrepositions = new string[]
		{
			"at", "by", "in", "of", "on", "to", "up", "from", "with", "into",
			"over", "out"
		};

		// Token: 0x04001FDB RID: 8155
		private static string[] prepositions = new string[]
		{
			"at", "by", "in", "of", "on", "to", "up", "from", "with", "into",
			"out"
		};

		// Token: 0x04001FDC RID: 8156
		private static string[] articles = new string[] { "an", "a", "the" };

		// Token: 0x04001FDD RID: 8157
		private static string[] demonstrativePronouns = new string[] { "this", "that", "those", "these", "such", "none", "neither", "who", "whom", "whose" };

		// Token: 0x04001FDE RID: 8158
		private static string[] conjunctions = new string[] { "and", "but", "or", "nor", "for", "as" };

		// Token: 0x04001FDF RID: 8159
		private static string[] ordinalsRoman = new string[] { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX", "X" };

		// Token: 0x04001FE0 RID: 8160
		private static string[] articleStartingWords = new string[]
		{
			"a", "an", "A", "An", "the", "The", "a", "an", "A", "An",
			"the", "The", "a", "an", "A", "An", "the", "The", "Some", "some"
		};

		// Token: 0x04001FE1 RID: 8161
		private static string[] badEndingWords = new string[]
		{
			"this", "were", "every", "are", "which", "their", "has", "your", "that", "who",
			"our", "additional", "its", "he", "her", "during", "no", "she's", "he's", "than",
			"they"
		};

		// Token: 0x04001FE2 RID: 8162
		private static string[] badStartingWords = new string[] { "were", "though", "them", "him,", "her", "is" };

		// Token: 0x04001FE3 RID: 8163
		private static string[] articleExceptions = new string[]
		{
			"heir", "heiress", "heirloom", "herb", "herbal", "hour", "hourglass", "once", "one", "ubiquitous",
			"ubiquitously", "ubiquity", "unary", "unicorn", "unicorns", "unicycle", "unidirectional", "unidirectionality", "unidirectionally", "unification",
			"unifications", "unified", "unifier", "unifies", "uniform", "uniformed", "uniformity", "uniformly", "uniforms", "unify",
			"unifying", "union", "unionization", "unionize", "unionized", "unionizer", "unionizers", "unionizes", "unionizing", "unions",
			"uniprocessor", "unique", "uniquely", "uniqueness", "unison", "unit", "unitary", "unities", "uniting", "unity",
			"univalve", "univalved", "univalves", "universal", "universal", "universality", "universally", "universe", "universes", "universities",
			"university", "usable", "use", "useless"
		};

		// Token: 0x04001FE4 RID: 8164
		private static string[] badWords = new string[] { "nigg", "fag", "nigr", "niggr", "kike" };

		// Token: 0x04001FE5 RID: 8165
		private static string[] badWordsExact = new string[] { "chikan", "clit", "hamas" };

		// Token: 0x04001FE6 RID: 8166
		private static char[] punctuation = new char[] { '.', '!', ',', ':', ';' };

		// Token: 0x04001FE7 RID: 8167
		private static Dictionary<string, string> singularToPlural = new Dictionary<string, string>();

		// Token: 0x04001FE8 RID: 8168
		private static Dictionary<string, string> pluralToSingular = new Dictionary<string, string>();

		// Token: 0x04001FE9 RID: 8169
		private static Dictionary<string, string> irregularPluralization = new Dictionary<string, string>
		{
			{ "atterkop", "atterkoppen" },
			{ "attorney general", "attorneys general" },
			{ "bergrisi", "bergrisar" },
			{ "child", "children" },
			{ "childe", "childer" },
			{ "commando", "commandos" },
			{ "court martial", "courts martial" },
			{ "die", "dice" },
			{ "djinn", "djinni" },
			{ "dunadan", "dunadain" },
			{ "eldjotun", "eldjotnar" },
			{ "eldthurs", "eldthursar" },
			{ "felljotun", "felljotnar" },
			{ "fife", "fifes" },
			{ "fomor", "fomori" },
			{ "foot", "feet" },
			{ "forefoot", "forefeet" },
			{ "genus", "genera" },
			{ "goose", "geese" },
			{ "hindfoot", "hindfeet" },
			{ "hrimthurs", "hrimthursar" },
			{ "ifrit", "ifriti" },
			{ "jabberwock", "jabberwocky" },
			{ "jerky", "jerkys" },
			{ "jotun", "jotnar" },
			{ "kin", "kin" },
			{ "kindred", "kindred" },
			{ "kinsmen", "kinsmen" },
			{ "kinswomen", "kinswomen" },
			{ "knight templar", "knights templar" },
			{ "lb.", "lbs." },
			{ "leaf", "leaves" },
			{ "loaf", "loaves" },
			{ "longstaff", "longstaves" },
			{ "mosquito", "mosquitos" },
			{ "mouse", "mice" },
			{ "notary public", "notaries public" },
			{ "octopus", "octopodes" },
			{ "opus", "opera" },
			{ "ordo", "ordines" },
			{ "ox", "oxen" },
			{ "pancreas", "pancreata" },
			{ "person", "people" },
			{ "platypus", "platypoda" },
			{ "plus", "plusses" },
			{ "quarterstaff", "quarterstaves" },
			{ "rhinox", "rhinoxen" },
			{ "risi", "risar" },
			{ "secretary general", "secretaries general" },
			{ "shaman", "shamans" },
			{ "staff", "staves" },
			{ "sturmjotun", "sturmjotnar" },
			{ "surgeon general", "surgeons general" },
			{ "talisman", "talismans" },
			{ "thief", "thieves" },
			{ "tooth", "teeth" },
			{ "topaz", "topazes" },
			{ "townsperson", "townspeople" },
			{ "moment in time chosen arbitrarily", "moments in time chosen arbitrarily" }
		};

		// Token: 0x04001FEA RID: 8170
		private static string[] identicalPluralization = new string[]
		{
			"barracks", "bison", "buffalo", "caribou", "chitin", "chosen", "corps", "deer", "einheriar", "fish",
			"fruit", "geisha", "gi", "hellspawn", "katana", "kraken", "lamia", "kris", "means", "moose",
			"naga", "ninja", "nunchaku", "oni", "remains", "sai", "scissors", "series", "sheep", "shrimp",
			"shuriken", "spawn", "species", "sputum", "waterworks", "wakizashi", "yeti", "yoroi", "young", "pentaceps"
		};

		// Token: 0x04001FEB RID: 8171
		private static string[] latinPluralization = new string[]
		{
			"abacus", "adytum", "alkalus", "alumnus", "alumno", "alumna", "anima", "animo", "animus", "antenna",
			"apex", "appendix", "arboretum", "astrum", "automaton", "axis", "bacterium", "ballista", "cacosteum", "cactus",
			"cestus", "cinctus", "cognomen", "corpus", "datum", "desideratum", "dictum", "dominatrix", "drosophilium", "ellipsis",
			"emerita", "emerito", "emeritus", "epona", "eques", "equus", "erratum", "esophagus", "exoculus", "exodus",
			"fascia", "focus", "forum", "fungus", "haruspex", "hippocampus", "hippopotamus", "hypha", "iambus", "illuminata",
			"illuminato", "illuminatus", "imperator", "imperatrix", "incarnus", "larva", "locus", "lorica", "maga", "mago",
			"magus", "manica", "matrix", "medium", "melia", "momentum", "neurosis", "nexus", "nomen", "nucleus",
			"patagium", "pegasus", "penis", "persona", "phenomenon", "phoenix", "pilum", "plexus", "praenomen", "psychosis",
			"quantum", "radius", "rectum", "sanctum", "scintilla", "scriptorium", "scrotum", "scutum", "septum", "simulacrum",
			"stratum", "substratum", "testis", "tympani", "ultimatum", "uterus", "vagina", "vertex", "vomitorium", "vortex",
			"vulva"
		};

		// Token: 0x04001FEC RID: 8172
		private static string[] greekPluralization1 = new string[]
		{
			"archon", "aristos", "astron", "bebelos", "charisma", "chimera", "daimon", "domos", "echthros", "eidolon",
			"ephemeris", "epopis", "hegemon", "horos", "hystrix", "kentaur", "kharisma", "kudos", "laryngis", "larynx",
			"lemma", "logos", "mestor", "minotaur", "mnemon", "mythos", "omphalos", "ouros", "patris", "pharynx",
			"pragma", "rhetor", "rhinoceros", "schema", "stigma", "telos", "topos"
		};

		// Token: 0x04001FED RID: 8173
		private static string[] greekPluralization2 = new string[] { "diokesis", "ganglion", "noumenon", "numenon", "praxis", "therion" };

		// Token: 0x04001FEE RID: 8174
		private static string[] hebrewPluralization = new string[]
		{
			"aswad", "chaya", "cherub", "galgal", "golem", "kabbalah", "keruv", "khaya", "nefesh", "nephil",
			"neshamah", "qabalah", "ruach", "ruakh", "sephirah", "seraph", "yechida", "yekhida"
		};

		// Token: 0x04001FEF RID: 8175
		private static string[] dualPluralization = new string[] { "emerita", "emeritus" };

		// Token: 0x04001FF0 RID: 8176
		private static Dictionary<string, string> firstPersonToThirdPerson = new Dictionary<string, string>();

		// Token: 0x04001FF1 RID: 8177
		private static Dictionary<string, string> firstPersonToThirdPersonWithSpace = new Dictionary<string, string>();

		// Token: 0x04001FF2 RID: 8178
		private static Dictionary<string, string> thirdPersonToFirstPerson = new Dictionary<string, string>();

		// Token: 0x04001FF3 RID: 8179
		private static Dictionary<string, string> irregularThirdPerson = new Dictionary<string, string>
		{
			{ "'re", "'s" },
			{ "'ve", "'s" },
			{ "are", "is" },
			{ "aren't", "isn't" },
			{ "cannot", "cannot" },
			{ "can't", "can't" },
			{ "caught", "caught" },
			{ "could", "could" },
			{ "couldn't", "couldn't" },
			{ "don't", "doesn't" },
			{ "grew", "grew" },
			{ "had", "had" },
			{ "have", "has" },
			{ "may", "may" },
			{ "might", "might" },
			{ "must", "must" },
			{ "shall", "shall" },
			{ "shouldn't", "shouldn't" },
			{ "should", "should" },
			{ "sought", "sought" },
			{ "were", "was" },
			{ "will", "will" },
			{ "won't", "won't" },
			{ "wouldn't", "wouldn't" },
			{ "would", "would" }
		};

		// Token: 0x04001FF4 RID: 8180
		private static StringBuilder SB1 = new StringBuilder(512);

		// Token: 0x04001FF5 RID: 8181
		private static StringBuilder SB2 = new StringBuilder(32);
	}
}
