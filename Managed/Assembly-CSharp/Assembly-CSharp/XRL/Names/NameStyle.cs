using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Names
{
	// Token: 0x02000581 RID: 1409
	public class NameStyle
	{
		// Token: 0x06003C7A RID: 15482 RVA: 0x00127E1C File Offset: 0x0012601C
		public string Generate(GameObject For = null, string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, Dictionary<string, string> NamingContext = null, bool FailureOkay = false, bool SpecialFaildown = false, NameStyle Skip = null, List<NameStyle> SkipList = null, int? HyphenationChance = null, int? TwoNameChance = null, bool? HasHonorific = null, bool? HasEpithet = null)
		{
			int num = 0;
			string text2;
			for (;;)
			{
				if (!this.Base.IsNullOrEmpty())
				{
					if (SkipList != null)
					{
						SkipList = new List<NameStyle>(SkipList);
						SkipList.Add(this);
					}
					else if (Skip != null)
					{
						SkipList = new List<NameStyle> { Skip, this };
						Skip = null;
					}
					else
					{
						Skip = this;
					}
					if (this.Base == "*")
					{
						string text = null;
						bool flag = false;
						NameStyle nameStyle = Skip;
						List<NameStyle> list = SkipList;
						int? num2 = HyphenationChance;
						int? num3 = ((num2 != null) ? num2 : (this.HyphenationChanceSet ? new int?(this.HyphenationChance) : null));
						num2 = TwoNameChance;
						text2 = NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, text, Type, NamingContext, FailureOkay, flag, nameStyle, list, num3, (num2 != null) ? num2 : (this.TwoNameChanceSet ? new int?(this.TwoNameChance) : null), HasHonorific, HasEpithet, true);
					}
					else
					{
						NameStyle nameStyle2;
						if (!NameStyles.NameStyleTable.TryGetValue(this.Base, out nameStyle2))
						{
							break;
						}
						NameStyle nameStyle3 = nameStyle2;
						NameStyle nameStyle4 = Skip;
						List<NameStyle> list2 = SkipList;
						int? num2 = HyphenationChance;
						int? num4 = ((num2 != null) ? num2 : (this.HyphenationChanceSet ? new int?(this.HyphenationChance) : null));
						num2 = TwoNameChance;
						text2 = nameStyle3.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, NamingContext, FailureOkay, SpecialFaildown, nameStyle4, list2, num4, (num2 != null) ? num2 : (this.TwoNameChanceSet ? new int?(this.TwoNameChance) : null), HasHonorific, HasEpithet);
					}
					if (text2 == null)
					{
						goto Block_14;
					}
				}
				else
				{
					NameStyle.SB.Clear();
					int num5 = HyphenationChance ?? this.HyphenationChance;
					int num6 = ((TwoNameChance ?? this.TwoNameChance).in100() ? 2 : 1);
					for (int i = 0; i < num6; i++)
					{
						int num7 = this.PrefixAmount.RollCached();
						int num8 = this.InfixAmount.RollCached();
						int num9 = this.PostfixAmount.RollCached();
						for (int j = 0; j < num7; j++)
						{
							NameStyle.SB.Append(this.Prefixes.GetRandomNameElement<NamePrefix>(null));
							if (num5.in100() && (num8 > 0 || num9 > 0 || j < num7 - 1))
							{
								NameStyle.SB.Append('-');
							}
						}
						for (int k = 0; k < num8; k++)
						{
							NameStyle.SB.Append(this.Infixes.GetRandomNameElement<NameInfix>(null));
							if (num5.in100() && (num9 > 0 || k < num8 - 1))
							{
								NameStyle.SB.Append('-');
							}
						}
						for (int l = 0; l < num9; l++)
						{
							NameStyle.SB.Append(this.Postfixes.GetRandomNameElement<NamePostfix>(null));
							if (num5.in100() && l < num9 - 1)
							{
								NameStyle.SB.Append('-');
							}
						}
						if (i < num6 - 1)
						{
							NameStyle.SB.Append(' ');
						}
					}
					text2 = NameStyle.SB.ToString().Trim();
				}
				string randomNameElement = this.Templates.GetRandomNameElement<NameTemplate>(null);
				if (!randomNameElement.IsNullOrEmpty())
				{
					string text3 = randomNameElement;
					if (text3.Contains(";"))
					{
						text3 = text3.Split(';', StringSplitOptions.None).GetRandomElement<string>(null);
					}
					Dictionary<string, string> dictionary = new Dictionary<string, string>(8);
					if (NamingContext != null)
					{
						foreach (KeyValuePair<string, string> keyValuePair in NamingContext)
						{
							dictionary[keyValuePair.Key] = keyValuePair.Value.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
						}
					}
					if (!dictionary.ContainsKey("*Name*"))
					{
						dictionary["*Name*"] = text2;
					}
					if (text3.Contains("*AltName*") && !dictionary.ContainsKey("*AltName*"))
					{
						Dictionary<string, string> dictionary2 = dictionary;
						string text4 = "*AltName*";
						string text5 = null;
						string text6 = null;
						bool flag2 = false;
						bool flag3 = false;
						NameStyle nameStyle5 = Skip;
						List<NameStyle> list3 = SkipList;
						int? num2 = HyphenationChance;
						int? num10 = ((num2 != null) ? num2 : (this.HyphenationChanceSet ? new int?(this.HyphenationChance) : null));
						num2 = TwoNameChance;
						dictionary2[text4] = NameStyles.Generate(For, Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, text5, text6, NamingContext, flag2, flag3, nameStyle5, list3, num10, (num2 != null) ? num2 : (this.TwoNameChanceSet ? new int?(this.TwoNameChance) : null), HasHonorific, HasEpithet, true);
					}
					if (For != null)
					{
						if (text3.Contains("*CreatureType*") && !dictionary.ContainsKey("*CreatureType*"))
						{
							dictionary["*CreatureType*"] = For.GetCreatureType(false, false);
						}
						if (text3.Contains("*CreatureTypeCap*") && !dictionary.ContainsKey("*CreatureTypeCap*"))
						{
							dictionary["*CreatureTypeCap*"] = For.GetCreatureType(true, false);
						}
						foreach (object obj in Regex.Matches(text3, "(\\*([^*]+)\\*)"))
						{
							Match match = (Match)obj;
							if (!dictionary.ContainsKey(match.Groups[1].Value))
							{
								string text7 = For.GetPropertyOrTag("HeroNameTitle" + match.Groups[2].Value, null);
								if (text7 != null)
								{
									if (text7.Contains(","))
									{
										text7 = text7.Split(',', StringSplitOptions.None).GetRandomElement<string>(null);
									}
									dictionary[match.Groups[1].Value] = text7;
									if (text7.Contains("spice."))
									{
										dictionary[match.Groups[1].Value] = Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<" + text7 + ">", null));
									}
								}
							}
						}
					}
					if (this.TemplateVars != null && this.TemplateVars.Count > 0)
					{
						foreach (KeyValuePair<string, List<NameValue>> keyValuePair2 in this.TemplateVars)
						{
							if (text3.Contains(keyValuePair2.Key))
							{
								string text8 = "*" + keyValuePair2.Key + "*";
								if (!dictionary.ContainsKey(text8))
								{
									dictionary[text8] = keyValuePair2.Value.GetRandomNameElement<NameValue>(null);
									if (dictionary[text8].Contains("spice."))
									{
										dictionary[text8] = Grammar.MakeTitleCase(HistoricStringExpander.ExpandString("<" + dictionary[text8] + ">", null));
									}
								}
							}
						}
					}
					foreach (KeyValuePair<string, List<NameValue>> keyValuePair3 in NameStyles.DefaultTemplateVars)
					{
						if (text3.Contains(keyValuePair3.Key))
						{
							string text9 = "*" + keyValuePair3.Key + "*";
							if (!dictionary.ContainsKey(text9))
							{
								dictionary[text9] = keyValuePair3.Value.GetRandomNameElement<NameValue>(null);
							}
						}
					}
					text2 = HistoricStringExpander.ExpandString(text3, null, null, dictionary, null);
					if (text2.Contains("*"))
					{
						text2 = Regex.Replace(text2, " *\\*[^*]+\\*", "");
					}
					text2 = text2.Trim();
				}
				if (!Grammar.ContainsBadWords(text2))
				{
					goto IL_07C6;
				}
				if (++num > 1000)
				{
					goto Block_53;
				}
			}
			return "InvalidBase:" + this.Base;
			Block_14:
			return null;
			Block_53:
			string text10 = "NameGenBadWordsFail";
			int num11 = ++NameStyle.NameGenerationBadWordsFailures;
			return text10 + num11.ToString();
			IL_07C6:
			return this.ApplyFormats(text2);
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x0012862C File Offset: 0x0012682C
		public string ApplyFormats(string Text)
		{
			return NameStyle.ApplyFormats(this.Format, Text);
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x0012863C File Offset: 0x0012683C
		public static string ApplyFormats(string Format, string Text)
		{
			if (Format.Contains(","))
			{
				foreach (string text in Format.CachedCommaExpansion())
				{
					Text = NameStyle.ApplyFormat(text, Text);
				}
				return Text;
			}
			return NameStyle.ApplyFormat(Format, Text);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x001286A8 File Offset: 0x001268A8
		public static string ApplyFormat(string Format, string Text)
		{
			if (!(Format == "TitleCase"))
			{
				if (!(Format == "AllCaps"))
				{
					if (!(Format == "LowerCase"))
					{
						if (!(Format == "Capitalized"))
						{
							if (!(Format == "SpacesToHyphens"))
							{
								if (!(Format == "AsIs"))
								{
								}
							}
							else
							{
								Text = ColorUtility.ReplaceExceptFormatting(Text, ' ', '-');
							}
						}
						else
						{
							Text = ColorUtility.CapitalizeExceptFormatting(Text);
						}
					}
					else
					{
						Text = ColorUtility.ToLowerExceptFormatting(Text);
					}
				}
				else
				{
					Text = ColorUtility.ToUpperExceptFormatting(Text);
				}
			}
			else
			{
				Text = Grammar.MakeTitleCase(Text);
			}
			return Text;
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x0012873C File Offset: 0x0012693C
		public NameScope CheckApply(string Genotype = null, string Subtype = null, string Species = null, string Culture = null, string Faction = null, string Region = null, string Gender = null, List<string> Mutations = null, string Tag = null, string Special = null, string Type = null, bool HasHonorific = false, bool HasEpithet = false)
		{
			NameScope nameScope = null;
			foreach (NameScope nameScope2 in this.Scopes)
			{
				if (nameScope2.ApplyTo(Genotype, Subtype, Species, Culture, Faction, Region, Gender, Mutations, Tag, Special, Type, HasHonorific, HasEpithet) && (nameScope == null || nameScope2.Priority > nameScope.Priority))
				{
					nameScope = nameScope2;
				}
			}
			return nameScope;
		}

		// Token: 0x04001E39 RID: 7737
		public string Name;

		// Token: 0x04001E3A RID: 7738
		public int HyphenationChance;

		// Token: 0x04001E3B RID: 7739
		public bool HyphenationChanceSet;

		// Token: 0x04001E3C RID: 7740
		public int TwoNameChance;

		// Token: 0x04001E3D RID: 7741
		public bool TwoNameChanceSet;

		// Token: 0x04001E3E RID: 7742
		public string Base;

		// Token: 0x04001E3F RID: 7743
		public string Format = "AsIs";

		// Token: 0x04001E40 RID: 7744
		public List<NamePrefix> Prefixes = new List<NamePrefix>();

		// Token: 0x04001E41 RID: 7745
		public string PrefixAmount = "0";

		// Token: 0x04001E42 RID: 7746
		public List<NameInfix> Infixes = new List<NameInfix>();

		// Token: 0x04001E43 RID: 7747
		public string InfixAmount = "0";

		// Token: 0x04001E44 RID: 7748
		public List<NamePostfix> Postfixes = new List<NamePostfix>();

		// Token: 0x04001E45 RID: 7749
		public string PostfixAmount = "0";

		// Token: 0x04001E46 RID: 7750
		public List<NameScope> Scopes = new List<NameScope>();

		// Token: 0x04001E47 RID: 7751
		public List<NameTemplate> Templates = new List<NameTemplate>();

		// Token: 0x04001E48 RID: 7752
		public Dictionary<string, List<NameValue>> TemplateVars = new Dictionary<string, List<NameValue>>();

		// Token: 0x04001E49 RID: 7753
		private static StringBuilder SB = new StringBuilder();

		// Token: 0x04001E4A RID: 7754
		private static int NameGenerationBadWordsFailures;
	}
}
