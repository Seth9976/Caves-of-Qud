using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using HistoryKit;
using XRL.Language;
using XRL.Names;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001268 RID: 4712
	[Serializable]
	public class EaterCryptPlaque : IPart
	{
		// Token: 0x0600BACE RID: 47822 RVA: 0x00442E47 File Offset: 0x00441047
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BACF RID: 47823 RVA: 0x00442E4A File Offset: 0x0044104A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600BAD0 RID: 47824 RVA: 0x00442E70 File Offset: 0x00441070
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BAD1 RID: 47825 RVA: 0x00442E88 File Offset: 0x00441088
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					Look.ShowLooker(0, currentCell.X, currentCell.Y);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAD2 RID: 47826 RVA: 0x00442ECC File Offset: 0x004410CC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.NeedsGeneration)
			{
				this.GeneratePlaque();
			}
			E.Prefix.Append(this.Prefix);
			E.Base.Clear();
			E.Base.Append(this.Inscription);
			E.Postfix.Append(this.Postfix);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAD3 RID: 47827 RVA: 0x00442F30 File Offset: 0x00441130
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BAD4 RID: 47828 RVA: 0x00442F34 File Offset: 0x00441134
		public void GeneratePlaque()
		{
			if (this.FamilyName == null)
			{
				if (this.Faction == "Eater")
				{
					this.FamilyName = NameMaker.Eater();
				}
				else
				{
					this.FamilyName = MutantNameMaker.MakeMutantName();
				}
			}
			bool flag = If.Chance(70, null, null);
			string text = ((this.Caste == "royal") ? "kindred" : this.Caste);
			string text2;
			string text3;
			string text4;
			if (flag)
			{
				text2 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptIntroPlural.!random>", null);
				if (If.CoinFlip(null, null))
				{
					text3 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptPlaque.familyTitlePlural.!random>", null);
					if (If.Chance(60, null, null))
					{
						text4 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptPlaque." + this.Caste + ".familyCognomen.!random>", null).Replace("*" + text + "*", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.instancesOf." + text + ".!random>", null))).Replace("*personNouns*", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.personNouns.!random>", null)));
					}
					else
					{
						text4 = "";
					}
				}
				else
				{
					text3 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptPlaque.familyTitleSingular.!random>", null);
					text4 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptPlaque." + this.Caste + ".familyCognomen.!random>", null).Replace("*" + text + "*", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.instancesOf." + text + ".!random>", null))).Replace("*personNouns*", Grammar.Pluralize(HistoricStringExpander.ExpandString("<spice.personNouns.!random>", null)));
				}
			}
			else
			{
				text2 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptIntroSingular.!random>", null);
				text4 = "";
				text3 = HistoricStringExpander.ExpandString("<spice.tombstones.cryptPlaque.familyTitleSingular.!random>", null);
			}
			text2 = Grammar.MakeTitleCase(text2.Replace("*familyCognomen*", text4).TrimEnd(' '));
			text3 = Grammar.MakeTitleCase(text3.Replace("*familyName*", this.FamilyName));
			string text5 = Grammar.InitCap(HistoricStringExpander.ExpandString("<spice.tombstones.cryptPlaque." + this.Caste + ".familyWords.!random>", null));
			string text6 = "LibraryCorpus.json";
			MarkovBook.EnsureCorpusLoaded(text6);
			MarkovChainData markovChainData = MarkovBook.CorpusData[text6];
			Match match = Regex.Match(text5, "\\*markovSeed:(.+?)\\*");
			if (match.Success)
			{
				text5 = text5.Replace(match.Groups[0].Value, MarkovChain.GenerateShortSentence(markovChainData, MarkovChain.GenerateSeedFromWord(markovChainData, match.Groups[1].Value.Split(',', StringSplitOptions.None).GetRandomElement<string>(null)), 12).TrimEnd(' '));
			}
			text5 = text5.Replace("*shortMarkov*", MarkovChain.GenerateShortSentence(markovChainData, null, 12)).TrimEnd(' ');
			string text7 = "\"" + text5 + "\"";
			text7 = Markup.Wrap(text7);
			List<string> list = new List<string>(16);
			int num = 26;
			list.Add("");
			if (this.IsEmpty)
			{
				foreach (string text8 in StringFormat.ClipText(this.EmptyText, num, false, true, false).Split('\n', StringSplitOptions.None))
				{
					list.Add(text8);
				}
				list.Add("");
			}
			else
			{
				foreach (string text9 in StringFormat.ClipText(text2, num, false, true, false).Split('\n', StringSplitOptions.None))
				{
					list.Add(text9);
				}
				list.Add("");
				list.Add("");
				foreach (string text10 in StringFormat.ClipText(text3, num, false, true, false).Split('\n', StringSplitOptions.None))
				{
					list.Add(text10);
				}
				list.Add("");
				list.Add("");
				foreach (string text11 in StringFormat.ClipText(text7, num, false, true, false).Split('\n', StringSplitOptions.None))
				{
					list.Add(text11);
				}
				list.Add("");
			}
			string text12 = "^k&w";
			string text13 = "&W^w";
			string text14 = "w";
			if (this.Caste == "warrior")
			{
				text12 = "^k&r";
				text13 = "&R^r";
				text14 = "r";
			}
			if (this.Caste == "tutor")
			{
				text12 = "^k&c";
				text13 = "&C^c";
				text14 = "c";
			}
			if (this.Caste == "royal")
			{
				text12 = "^k&m";
				text13 = "&M^m";
				text14 = "m";
			}
			this.Inscription = Event.NewStringBuilder(null).Append('ÿ').Append('ÿ')
				.Append('ÿ')
				.Append(text13 + "\a" + text12)
				.Append('Ä', 31)
				.Append(text13 + "\a^k&y")
				.ToString();
			for (int j = 0; j < list.Count; j++)
			{
				this.Inscription += "\nÿÿÿ";
				this.Inscription = this.Inscription + text12 + "³^k&y";
				int num2 = ColorUtility.LengthExceptFormatting(list[j]);
				this.Inscription += list[j].PadLeft(16 + num2 / 2, 'ÿ').PadRight(31, 'ÿ');
				this.Inscription = this.Inscription + text12 + "³^k&y";
			}
			this.Inscription += Event.NewStringBuilder(null).Append('\n').Append('ÿ')
				.Append('ÿ')
				.Append('ÿ')
				.Append(text13 + "\a" + text12)
				.Append("&" + text14 + "^k")
				.Append('Ä', 31)
				.Append(text13 + "\a^k&y")
				.ToString();
			this.NeedsGeneration = false;
		}

		// Token: 0x04004AEE RID: 19182
		public string Inscription = "";

		// Token: 0x04004AEF RID: 19183
		public string Prefix = "";

		// Token: 0x04004AF0 RID: 19184
		public string Postfix = "";

		// Token: 0x04004AF1 RID: 19185
		public bool NeedsGeneration = true;

		// Token: 0x04004AF2 RID: 19186
		public bool IsEmpty;

		// Token: 0x04004AF3 RID: 19187
		public string Caste = "tutor";

		// Token: 0x04004AF4 RID: 19188
		public string Faction = "Eater";

		// Token: 0x04004AF5 RID: 19189
		public string FamilyName;

		// Token: 0x04004AF6 RID: 19190
		public string EmptyText = "<The plaque has no inscription.>";

		// Token: 0x04004AF7 RID: 19191
		private const int CHANCE_FOR_PLURAL_FAMILY_TITLE = 70;

		// Token: 0x04004AF8 RID: 19192
		private const int CHANCE_USE_FAMILY_COGNOMEN = 60;
	}
}
