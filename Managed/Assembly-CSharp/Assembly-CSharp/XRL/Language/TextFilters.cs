using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HistoryKit;
using XRL.Rules;
using XRL.World.Encounters;

namespace XRL.Language
{
	// Token: 0x020005E7 RID: 1511
	public static class TextFilters
	{
		// Token: 0x06004111 RID: 16657 RVA: 0x001460F0 File Offset: 0x001442F0
		public static string Filter(string Phrase, string Filter, string Extras = null, bool FormattingProtect = true)
		{
			if (Filter == "Angry")
			{
				return TextFilters.Angry(Phrase);
			}
			if (Filter == "Corvid")
			{
				return TextFilters.Corvid(Phrase);
			}
			if (Filter == "WaterBird")
			{
				return TextFilters.WaterBird(Phrase);
			}
			if (Filter == "Fish")
			{
				return TextFilters.Fish(Phrase);
			}
			if (Filter == "Frog")
			{
				return TextFilters.Frog(Phrase);
			}
			if (Filter == "Leet")
			{
				return TextFilters.Leet(Phrase, FormattingProtect);
			}
			if (Filter == "Lallated")
			{
				return TextFilters.Lallated(Phrase, Extras);
			}
			if (Filter == "Weird")
			{
				return TextFilters.Weird(Phrase, Extras);
			}
			if (Filter == "Cryptic Machine")
			{
				return TextFilters.CrypticMachine(Phrase);
			}
			return Phrase;
		}

		// Token: 0x06004112 RID: 16658 RVA: 0x001461B8 File Offset: 0x001443B8
		public static string Corvid(string Text)
		{
			TextFilters.SB.Clear();
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			int i = 0;
			int length = Text.Length;
			while (i < length)
			{
				char c2 = Text[i];
				if (c2 == '=')
				{
					flag = !flag;
				}
				else if (c2 == '*')
				{
					flag2 = !flag2;
				}
				else if (!flag && !flag2 && c2 == ' ' && c != ' ' && 17.in100())
				{
					TextFilters.SB.Append(' ').Append(TextFilters.CORVID_WORDS.GetRandomElement<string>(null));
				}
				TextFilters.SB.Append(c2);
				if (!flag && !flag2)
				{
					c = c2;
				}
				i++;
			}
			return TextFilters.SB.ToString();
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x00146268 File Offset: 0x00144468
		public static string WaterBird(string Text)
		{
			TextFilters.SB.Clear();
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			int i = 0;
			int length = Text.Length;
			while (i < length)
			{
				char c2 = Text[i];
				if (c2 == '=')
				{
					flag = !flag;
				}
				else if (c2 == '*')
				{
					flag2 = !flag2;
				}
				else if (c2 == ' ' && !flag && !flag2 && c != ' ' && 17.in100())
				{
					TextFilters.SB.Append(' ').Append(TextFilters.WATERBIRD_WORDS.GetRandomElement<string>(null));
				}
				TextFilters.SB.Append(c2);
				if (!flag && !flag2)
				{
					c = c2;
				}
				i++;
			}
			return TextFilters.SB.ToString();
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x00146318 File Offset: 0x00144518
		public static string Fish(string Text)
		{
			TextFilters.SB.Clear();
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			int i = 0;
			int length = Text.Length;
			while (i < length)
			{
				char c2 = Text[i];
				if (c2 == '=')
				{
					flag = !flag;
				}
				else if (c2 == '*')
				{
					flag2 = !flag2;
				}
				else if (c2 == ' ' && !flag && !flag2 && c != ' ' && 17.in100())
				{
					TextFilters.SB.Append(' ').Append(TextFilters.FISH_WORDS.GetRandomElement<string>(null));
				}
				TextFilters.SB.Append(c2);
				if (!flag && !flag2)
				{
					c = c2;
				}
				i++;
			}
			return TextFilters.SB.ToString();
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x001463C8 File Offset: 0x001445C8
		public static string Frog(string Text)
		{
			TextFilters.SB.Clear();
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			int i = 0;
			int length = Text.Length;
			while (i < length)
			{
				char c2 = Text[i];
				if (c2 == '=')
				{
					flag = !flag;
				}
				else if (c2 == '*')
				{
					flag2 = !flag2;
				}
				else if (c2 == ' ' && !flag && !flag2 && c != ' ' && 9.in100())
				{
					TextFilters.SB.Append(' ').Append(TextFilters.FROG_WORDS.GetRandomElement<string>(null));
				}
				TextFilters.SB.Append(c2);
				if (!flag && !flag2)
				{
					c = c2;
				}
				i++;
			}
			return TextFilters.SB.ToString();
		}

		// Token: 0x06004116 RID: 16662 RVA: 0x00146478 File Offset: 0x00144678
		public static string Angry(string Phrase)
		{
			string[] array = Phrase.Split(new string[] { ". " }, StringSplitOptions.None);
			int i = 0;
			int num = array.Length;
			while (i < num)
			{
				if (50.in100())
				{
					array[i] = Grammar.Stutterize(array[i], HistoricStringExpander.ExpandString("<spice.textFilters.angry.!random>", null));
				}
				i++;
			}
			return string.Join(". ", array);
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x001464D8 File Offset: 0x001446D8
		public static string Lallated(string Text, string Noise)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{ "*Text*", Text },
				{
					"*Noise*",
					Noise.Split(',', StringSplitOptions.None).GetRandomElement<string>(null)
				}
			};
			return HistoricStringExpander.ExpandString("<spice.textFilters.lallated.!random>", null, null, dictionary, null);
		}

		// Token: 0x06004118 RID: 16664 RVA: 0x00146520 File Offset: 0x00144720
		public static string Leet(string Text, bool FormattingProtect = true)
		{
			if (Text == null)
			{
				return null;
			}
			Text = Regex.Replace(Text, "atable\\b", "8able", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "anned\\b", FormattingProtect ? "&&" : "&", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "and\\b", FormattingProtect ? "&&" : "&", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "ude\\b", "00|)", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "too\\b", "2", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bto\\b", "2", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bone\\b", "1", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bwon\\b", "1", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\btwo\\b", "1", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bthree\\b", "3", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bfou?r\\b", "4", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bfive\\b", "5", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bsix\\b", "6", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bseven\\b", "7", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\beight\\b", "8", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "ate\\b", "8", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\bare\\b", "R", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "nine\\b", "9", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "\\byou\\b", "U", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "at\\b", "@", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "S\\b", "Z");
			Text = Regex.Replace(Text, "s\\b", "z");
			Text = Regex.Replace(Text, "a(?=\\w)", "4", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "(?<=\\w)a", "4", RegexOptions.IgnoreCase);
			Text = Text.Replace("EW", "00").Replace("ew", "00");
			Text = Regex.Replace(Text, "b(?=\\w)", "8", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "(?<=\\w)b", "8", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "c(?=\\w)", "(", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "(?<=\\w)c", "(", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "o(?=\\w)", "0", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "(?<=\\w)o", "0", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "w(?=\\w)", "\\/\\/", RegexOptions.IgnoreCase);
			Text = Regex.Replace(Text, "(?<=\\w)w", "\\/\\/", RegexOptions.IgnoreCase);
			Text = Text.Replace("D", "|)").Replace("d", "|)").Replace("E", "3")
				.Replace("e", "3")
				.Replace("H", "#")
				.Replace("h", "#")
				.Replace("I", "1")
				.Replace("i", "1")
				.Replace("S", "5")
				.Replace("s", "5")
				.Replace("T", "7")
				.Replace("t", "7")
				.Replace("V", "\\/")
				.Replace("v", "\\/");
			return Text;
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x0014689C File Offset: 0x00144A9C
		public static string Weird(string Text, string PatternSpec = null)
		{
			int num = 0;
			if (!PatternSpec.IsNullOrEmpty())
			{
				DimensionManager dimensionManager = The.Game.GetObjectGameState("DimensionManager") as DimensionManager;
				if (dimensionManager != null)
				{
					foreach (PsychicFaction psychicFaction in dimensionManager.PsychicFactions)
					{
						if (psychicFaction.factionName == PatternSpec)
						{
							return psychicFaction.Weirdify(Text);
						}
					}
					foreach (ExtraDimension extraDimension in dimensionManager.ExtraDimensions)
					{
						if (extraDimension.Name == PatternSpec)
						{
							return extraDimension.Weirdify(Text);
						}
					}
				}
				int.TryParse(PatternSpec, out num);
			}
			TextFilters.SB.Clear();
			bool flag = false;
			bool flag2 = false;
			int i = 0;
			int length = Text.Length;
			while (i < length)
			{
				char c = Text[i];
				if (c == '=')
				{
					flag = !flag;
				}
				else if (c == '*')
				{
					flag2 = !flag2;
				}
				else if (!flag && !flag2)
				{
					char c2;
					if (c <= 'i')
					{
						if (c <= 'R')
						{
							switch (c)
							{
							case 'A':
								c2 = Grammar.weirdUpperAs.GetCyclicElement<char>(num);
								break;
							case 'B':
								c2 = Grammar.weirdUpperBs.GetCyclicElement<char>(num);
								break;
							case 'C':
								c2 = Grammar.weirdUpperCs.GetCyclicElement<char>(num);
								break;
							case 'D':
							case 'F':
							case 'G':
							case 'H':
							case 'J':
							case 'K':
							case 'M':
								goto IL_0368;
							case 'E':
								c2 = Grammar.weirdUpperEs.GetCyclicElement<char>(num);
								break;
							case 'I':
								c2 = Grammar.weirdUpperIs.GetCyclicElement<char>(num);
								break;
							case 'L':
								c2 = Grammar.weirdUpperLs.GetCyclicElement<char>(num);
								break;
							case 'N':
								c2 = Grammar.weirdUpperNs.GetCyclicElement<char>(num);
								break;
							case 'O':
								c2 = Grammar.weirdUpperOs.GetCyclicElement<char>(num);
								break;
							default:
								if (c != 'R')
								{
									goto IL_0368;
								}
								c2 = Grammar.weirdUpperRs.GetCyclicElement<char>(num);
								break;
							}
						}
						else if (c != 'U')
						{
							if (c != 'Y')
							{
								switch (c)
								{
								case 'a':
									c2 = Grammar.weirdLowerAs.GetCyclicElement<char>(num);
									break;
								case 'b':
								case 'd':
								case 'g':
								case 'h':
									goto IL_0368;
								case 'c':
									c2 = Grammar.weirdLowerCs.GetCyclicElement<char>(num);
									break;
								case 'e':
									c2 = Grammar.weirdLowerEs.GetCyclicElement<char>(num);
									break;
								case 'f':
									c2 = Grammar.weirdLowerFs.GetCyclicElement<char>(num);
									break;
								case 'i':
									c2 = Grammar.weirdLowerIs.GetCyclicElement<char>(num);
									break;
								default:
									goto IL_0368;
								}
							}
							else
							{
								c2 = Grammar.weirdUpperYs.GetCyclicElement<char>(num);
							}
						}
						else
						{
							c2 = Grammar.weirdUpperUs.GetCyclicElement<char>(num);
						}
					}
					else if (c <= 'o')
					{
						if (c != 'n')
						{
							if (c != 'o')
							{
								goto IL_0368;
							}
							c2 = Grammar.weirdLowerOs.GetCyclicElement<char>(num);
						}
						else
						{
							c2 = Grammar.weirdLowerNs.GetCyclicElement<char>(num);
						}
					}
					else if (c != 't')
					{
						if (c != 'u')
						{
							if (c != 'y')
							{
								goto IL_0368;
							}
							c2 = Grammar.weirdLowerYs.GetCyclicElement<char>(num);
						}
						else
						{
							c2 = Grammar.weirdLowerUs.GetCyclicElement<char>(num);
						}
					}
					else
					{
						c2 = Grammar.weirdLowerTs.GetCyclicElement<char>(num);
					}
					IL_036C:
					c = c2;
					goto IL_0370;
					IL_0368:
					c2 = c;
					goto IL_036C;
				}
				IL_0370:
				TextFilters.SB.Append(c);
				i++;
			}
			return TextFilters.SB.ToString();
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x00146C60 File Offset: 0x00144E60
		public static void GenerateCrypticWord(StringBuilder SB)
		{
			int i = 0;
			int num = Stat.Random(TextFilters.CRYPTIC_WORD_LENGTH_LOWER_BOUND, TextFilters.CRYPTIC_WORD_LENGTH_UPPER_BOUND);
			while (i < num)
			{
				SB.Append(TextFilters.CRYPTIC_MACHINE_CHARS.GetRandomElement<char>(null));
				i++;
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x00146C9B File Offset: 0x00144E9B
		public static string GenerateCrypticWord()
		{
			TextFilters.CrypticWordSB.Clear();
			TextFilters.GenerateCrypticWord(TextFilters.CrypticWordSB);
			return TextFilters.CrypticWordSB.ToString();
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x00146CBC File Offset: 0x00144EBC
		public static string CrypticMachine(string Text)
		{
			if (Text.Contains("*READOUT*"))
			{
				TextFilters.CrypticSentenceSB.Clear();
				TextFilters.CrypticSentenceSB.Append("{{c|");
				int i = 0;
				int num = Stat.Random(TextFilters.CRYPTIC_SENTENCE_LENGTH_LOWER_BOUND, TextFilters.CRYPTIC_SENTENCE_LENGTH_UPPER_BOUND);
				while (i < num)
				{
					if (i > 0)
					{
						TextFilters.CrypticSentenceSB.Append(' ');
					}
					TextFilters.GenerateCrypticWord(TextFilters.CrypticSentenceSB);
					i++;
				}
				TextFilters.CrypticSentenceSB.Append("}}");
				return TextFilters.CrypticSentenceSB.ToString();
			}
			TextFilters.CrypticSentenceSB.Clear();
			bool flag = false;
			bool flag2 = false;
			char c = '\0';
			int j = 0;
			int length = Text.Length;
			while (j < length)
			{
				char c2 = Text[j];
				if (c2 == '=')
				{
					flag = !flag;
				}
				else if (c2 == '*')
				{
					flag2 = !flag2;
				}
				else if (c2 == ' ' && !flag && !flag2 && c != ' ' && 9.in100())
				{
					TextFilters.CrypticSentenceSB.Append(" {{c|");
					TextFilters.GenerateCrypticWord(TextFilters.CrypticSentenceSB);
					TextFilters.CrypticSentenceSB.Append("}} ");
				}
				TextFilters.CrypticSentenceSB.Append(c2);
				if (!flag && !flag2)
				{
					c = c2;
				}
				j++;
			}
			return TextFilters.CrypticSentenceSB.ToString();
		}

		// Token: 0x04001FF6 RID: 8182
		private static StringBuilder SB = new StringBuilder();

		// Token: 0x04001FF7 RID: 8183
		private static readonly string[] CORVID_WORDS = new string[] { "{{emote|*CAAW*}}", "{{emote|*CAAAAW*}}", "{{emote|*CAAAAW*}}", "{{emote|*CAAW*}}", "{{emote|*CAAAAAAAAAAW*}}" };

		// Token: 0x04001FF8 RID: 8184
		private static readonly string[] WATERBIRD_WORDS = new string[] { "{{emote|*HONK*}}", "{{emote|*HONK*}}", "{{emote|*HONK*}}", "{{emote|*HOONK*}}", "{{emote|*HOOOONK*}}", "{{emote|*HOOOOOOOOOONK*}}" };

		// Token: 0x04001FF9 RID: 8185
		private static readonly string[] FISH_WORDS = new string[] { "{{emote|*blub*}}", "{{emote|*blub blub*}}", "{{emote|*blub blub*}}", "{{emote|*blub*}}", "{{emote|*blub*}}", "{{emote|*blub*}}" };

		// Token: 0x04001FFA RID: 8186
		private static readonly string[] FROG_WORDS = new string[]
		{
			"{{emote|*crk*}}", "{{emote|*crrk*}}", "{{emote|*crrk*}}", "{{emote|*crrrrrk*}}", "{{emote|*crrrrrrrrrrrk*}}", "{{emote|*CRRK*}}", "{{emote|*reep*}}", "{{emote|*reep*}}", "{{emote|*reep*}}", "{{emote|*reeeeep*}}",
			"{{emote|*reeeeeeeeeeep*}}", "{{emote|*bep*}}", "{{emote|*rff*}}"
		};

		// Token: 0x04001FFB RID: 8187
		private static readonly char[] CRYPTIC_MACHINE_CHARS = new char[]
		{
			'³', '\u00b4', 'µ', '¶', '·', '\u00b8', '¹', 'º', '»', '¼',
			'½', '¾', '¿', 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ',
			'Ç', 'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï', 'Ð',
			'Ñ', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', '×', 'Ø', 'Ù', 'Ú'
		};

		// Token: 0x04001FFC RID: 8188
		private static readonly int CRYPTIC_WORD_LENGTH_LOWER_BOUND = 3;

		// Token: 0x04001FFD RID: 8189
		private static readonly int CRYPTIC_WORD_LENGTH_UPPER_BOUND = 10;

		// Token: 0x04001FFE RID: 8190
		private static StringBuilder CrypticWordSB = new StringBuilder();

		// Token: 0x04001FFF RID: 8191
		private static readonly int CRYPTIC_SENTENCE_LENGTH_LOWER_BOUND = 3;

		// Token: 0x04002000 RID: 8192
		private static readonly int CRYPTIC_SENTENCE_LENGTH_UPPER_BOUND = 40;

		// Token: 0x04002001 RID: 8193
		private static StringBuilder CrypticSentenceSB = new StringBuilder();
	}
}
