using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HistoryKit;
using XRL.Names;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000FA3 RID: 4003
	[Serializable]
	public class EaterUrn : IPart
	{
		// Token: 0x06009EF9 RID: 40697 RVA: 0x0039A6AA File Offset: 0x003988AA
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009EFA RID: 40698 RVA: 0x0039A6AD File Offset: 0x003988AD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009EFB RID: 40699 RVA: 0x0039A6D3 File Offset: 0x003988D3
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009EFC RID: 40700 RVA: 0x0039A6EC File Offset: 0x003988EC
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

		// Token: 0x06009EFD RID: 40701 RVA: 0x0039A730 File Offset: 0x00398930
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.NeedsGeneration)
			{
				this.GenerateUrn();
			}
			if (!this.Prefix.IsNullOrEmpty())
			{
				E.Prefix.Append(this.Prefix);
			}
			if (!this.Inscription.IsNullOrEmpty())
			{
				E.Base.Append(this.Inscription);
			}
			if (!this.Postfix.IsNullOrEmpty())
			{
				E.Postfix.Append(this.Postfix);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EFE RID: 40702 RVA: 0x0039A7AF File Offset: 0x003989AF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EFF RID: 40703 RVA: 0x0039A7B4 File Offset: 0x003989B4
		public void GenerateUrn()
		{
			this.NeedsGeneration = false;
			if (this.MemorializedName.IsNullOrEmpty())
			{
				if (this.Faction == "Eater")
				{
					this.MemorializedName = NameMaker.Eater();
				}
				else if (this.Faction == "YdFreehold")
				{
					this.MemorializedName = NameMaker.YdFreeholder();
				}
				else if (!this.Faction.IsNullOrEmpty())
				{
					this.MemorializedName = NameMaker.MakeName(null, null, null, null, null, this.Faction, null, null, null, null, null, null, null, false, false, null, null);
				}
				else
				{
					this.MemorializedName = MutantNameMaker.MakeMutantName();
				}
			}
			if (this.Introduction.IsNullOrEmpty())
			{
				if (this.Faction == "Eater")
				{
					this.Introduction = HistoricStringExpander.ExpandString("<spice.tombstones.eaterUrnIntro.!random>", null);
				}
				else if (this.Faction == "YdFreehold")
				{
					this.Introduction = HistoricStringExpander.ExpandString("<spice.tombstones.ydFreeholderUrnIntro.!random>", null);
				}
				else if (this.Faction == "Barathrumites")
				{
					this.Introduction = HistoricStringExpander.ExpandString("<spice.tombstones.barathrumiteUrnIntro.!random>", null);
				}
				else
				{
					this.Introduction = HistoricStringExpander.ExpandString("<spice.tombstones.genericUrnIntro.!random>", null);
				}
			}
			if (this.Eulogy.IsNullOrEmpty())
			{
				string text = "LibraryCorpus.json";
				MarkovBook.EnsureCorpusLoaded(text);
				MarkovChainData markovChainData = MarkovBook.CorpusData[text];
				this.Eulogy = "\"" + MarkovChain.GenerateShortSentence(markovChainData, null, 12).TrimEnd(' ') + "\"";
			}
			List<string> list = new List<string>(16);
			int num = 25;
			list.Add("");
			foreach (string text2 in StringFormat.ClipText(this.Introduction, num, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text2);
			}
			list.Add("");
			list.Add("");
			foreach (string text3 in StringFormat.ClipText(this.MemorializedName, num, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text3);
			}
			list.Add("");
			list.Add("");
			if (!this.Reason.IsNullOrEmpty())
			{
				string text4 = this.Reason.Strip();
				int num2 = text4.IndexOf("@@");
				if (num2 != -1)
				{
					text4 = text4.Substring(num2 + 2).Capitalize();
				}
				if (text4.Contains("##"))
				{
					text4 = (80.in100() ? text4.Replace("##", "") : Regex.Replace(text4, "##.*?##", ""));
				}
				if (text4.Contains("by you ") || text4.Contains("by you."))
				{
					text4 = text4.Replace("by you", "by " + The.Player.GetReferenceDisplayName(int.MaxValue, null, "MemorialKiller", false, false, false, false, false, false, true, false, null, false));
				}
				foreach (string text5 in StringFormat.ClipText(text4, num, false, true, false).Split('\n', StringSplitOptions.None))
				{
					list.Add(text5);
				}
				list.Add("");
				list.Add("");
			}
			foreach (string text6 in StringFormat.ClipText(this.Eulogy, num, false, true, false).Split('\n', StringSplitOptions.None))
			{
				list.Add(text6);
			}
			list.Add("");
			this.Inscription = "";
			for (int j = 0; j < list.Count; j++)
			{
				this.Inscription += "\nÿÿÿ";
				if (j % 2 == 0)
				{
					this.Inscription += " ";
				}
				else
				{
					this.Inscription += " ";
				}
				this.Inscription += list[j].PadLeft(17 + (list[j].Length / 2 - 1), 'ÿ').PadRight(31, 'ÿ');
				if (j % 2 == 0)
				{
					this.Inscription += " ";
				}
				else
				{
					this.Inscription += " ";
				}
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('/');
			stringBuilder.Append('-', 27);
			stringBuilder.Append('\\');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append("\n");
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append(" &K");
			stringBuilder.Append('_', 31);
			stringBuilder.Append(" &y");
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append("\n");
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append(" &K");
			stringBuilder.Append('_', 31);
			stringBuilder.Append(" &y");
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append('ÿ');
			stringBuilder.Append("\n");
			this.Prefix = stringBuilder.ToString();
			StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
			for (int k = 0; k < 2; k++)
			{
				stringBuilder2.Append("\n");
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append(" &K");
				stringBuilder2.Append('_', 21);
				stringBuilder2.Append(" &y");
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
				stringBuilder2.Append('ÿ');
			}
			this.Inscription += "\n";
			this.Inscription += stringBuilder2.ToString();
		}

		// Token: 0x040041C4 RID: 16836
		public string Inscription;

		// Token: 0x040041C5 RID: 16837
		public string Introduction;

		// Token: 0x040041C6 RID: 16838
		public string Eulogy;

		// Token: 0x040041C7 RID: 16839
		public string Prefix;

		// Token: 0x040041C8 RID: 16840
		public string Postfix;

		// Token: 0x040041C9 RID: 16841
		public string Reason;

		// Token: 0x040041CA RID: 16842
		public bool NeedsGeneration = true;

		// Token: 0x040041CB RID: 16843
		public string Faction = "Eater";

		// Token: 0x040041CC RID: 16844
		public string MemorializedName;
	}
}
