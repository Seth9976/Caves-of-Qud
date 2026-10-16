using System;
using HistoryKit;
using XRL.Language;
using XRL.Names;

namespace XRL.Annals
{
	// Token: 0x020005ED RID: 1517
	[Serializable]
	public class InitializeRegion : HistoricEvent
	{
		// Token: 0x06004151 RID: 16721 RVA: 0x00148B7E File Offset: 0x00146D7E
		public InitializeRegion(int _period)
		{
			this.period = _period;
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x00148B90 File Offset: 0x00146D90
		public override void Generate()
		{
			base.SetEntityProperty("type", "region");
			base.SetEntityProperty("period", this.period.ToString());
			this.SnapRegionParams = QudHistoryHelpers.GetRegionalizationParametersSnapshot(this.history);
			this.OrganizingPrincipleType = base.ExpandString("<spice.history.regions.organizingPrinciple." + this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown") + ".types.!random>");
			this.GovernmentType = base.ExpandString("<spice.history.regions.government." + this.SnapRegionParams.GetProperty("government", "unknown") + ".name.!random>");
			base.SetEntityProperty("organizingPrincipleType", this.OrganizingPrincipleType);
			base.SetEntityProperty("governmentType", this.GovernmentType);
			base.AddEntityListItem("parameters", this.OrganizingPrincipleType, false);
			base.AddEntityListItem("parameters", this.GovernmentType, false);
			this.NameRegion();
			for (int i = 1; i <= 6 - this.period; i++)
			{
				int num;
				if (i == 1)
				{
					num = 2;
				}
				else
				{
					num = base.Random(1, 2);
				}
				for (int j = 1; j <= num; j++)
				{
					HistoricEntity historicEntity = this.history.CreateEntity(this.history.currentYear);
					historicEntity.ApplyEvent(new InitializeLocation(this.Name, this.period), long.MinValue);
					base.AddEntityListItem("locations", historicEntity.GetSnapshotAtYear(this.history.currentYear).GetProperty("name", "unknown"), false);
				}
			}
			this.duration = 0L;
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x00148D24 File Offset: 0x00146F24
		public void NameRegion()
		{
			string text;
			do
			{
				text = NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
			}
			while (this.history.GetEntitiesWherePropertyEquals("name", text).Count > 0);
			base.SetEntityProperty("nameRoot", text);
			string text2 = "";
			if (this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown").Equals("religion"))
			{
				if (this.OrganizingPrincipleType.Equals("sultans"))
				{
					text2 = "sultan";
				}
				else if (!this.OrganizingPrincipleType.Equals("godless"))
				{
					text2 = base.ExpandString("<spice.elements." + this.OrganizingPrincipleType + ".nouns.!random>");
				}
				else
				{
					text2 = "nothing";
				}
				base.SetEntityProperty("orgPrincipleStash", Grammar.Pluralize(text2));
			}
			if (15.in100())
			{
				this.Name = text;
				base.SetEntityProperty("name", this.Name);
				base.SetEntityProperty("newName", this.Name);
				return;
			}
			if (this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown").Equals("function"))
			{
				if (25.in100())
				{
					this.Name = text;
					base.SetEntityProperty("name", this.Name);
					base.SetEntityProperty("newName", this.Name);
					return;
				}
				string text3 = base.ExpandString(string.Concat(new string[]
				{
					"<spice.history.regions.organizingPrinciple.",
					this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown"),
					".",
					this.OrganizingPrincipleType,
					".titles.!random>"
				}));
				string text4 = base.ExpandString("<spice.history.regions.government." + this.SnapRegionParams.GetProperty("government", "unknown") + ".nameArticle>");
				string text5 = base.ExpandString("<spice.history.regions.government." + this.SnapRegionParams.GetProperty("government", "unknown") + ".of>");
				this.Name = Grammar.MakeTitleCaseWithArticle(string.Concat(new string[] { text4, text3, " ", this.GovernmentType, " ", text5, text }));
				base.SetEntityProperty("name", this.Name);
				int num = base.Random(1, 100);
				if (num <= 25)
				{
					base.SetEntityProperty("newName", text);
					return;
				}
				if (num <= 65)
				{
					base.SetEntityProperty("newName", Grammar.MakeTitleCaseWithArticle("the " + Grammar.Pluralize(text3) + " of " + text));
					return;
				}
				base.SetEntityProperty("newName", Grammar.MakeTitleCaseWithArticle(text + " " + text3));
				return;
			}
			else if (this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown").Equals("wealth"))
			{
				if (25.in100())
				{
					this.Name = text;
					base.SetEntityProperty("name", this.Name);
					base.SetEntityProperty("newName", this.Name);
					return;
				}
				string text6 = base.ExpandString(string.Concat(new string[]
				{
					"<spice.history.regions.organizingPrinciple.",
					this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown"),
					".",
					this.OrganizingPrincipleType,
					".titles.!random>"
				}));
				this.Name = Grammar.MakeTitleCaseWithArticle(base.ExpandString("<spice.history.regions.organizingPrinciple.wealth." + this.OrganizingPrincipleType + ".titleArticle> ") + text + " " + text6).TrimStart(' ');
				base.SetEntityProperty("name", this.Name);
				if (50.in100())
				{
					base.SetEntityProperty("newName", text);
					return;
				}
				base.SetEntityProperty("newName", this.Name);
				return;
			}
			else
			{
				if (!this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown").Equals("profession"))
				{
					if (this.SnapRegionParams.GetProperty("organizingPrinciple", "unknown").Equals("religion"))
					{
						if (25.in100())
						{
							this.Name = text;
							base.SetEntityProperty("name", this.Name);
							base.SetEntityProperty("newName", this.Name);
							return;
						}
						if (this.OrganizingPrincipleType.Equals("godless"))
						{
							this.Name = "the godless " + Grammar.MakeTitleCaseWithArticle(this.GovernmentType + " of " + text);
							base.SetEntityProperty("newName", text);
						}
						else
						{
							this.Name = "the " + Grammar.GetRandomMeaningfulWord(text2) + "-worshipping " + Grammar.MakeTitleCaseWithArticle(this.GovernmentType + " of " + text);
							if (25.in100())
							{
								base.SetEntityProperty("newName", text);
							}
							else
							{
								base.SetEntityProperty("newName", Grammar.MakeTitleCaseWithArticle(string.Concat(new string[]
								{
									"the ",
									text,
									" ",
									Grammar.GetRandomMeaningfulWord(text2),
									"<spice.commonPhrases.yard.!random>"
								})));
							}
						}
						base.SetEntityProperty("name", this.Name);
					}
					return;
				}
				if (25.in100())
				{
					this.Name = text;
					base.SetEntityProperty("name", this.Name);
					base.SetEntityProperty("newName", this.Name);
					return;
				}
				string text7 = base.ExpandString("<spice.professions." + this.OrganizingPrincipleType + ".plural>");
				this.Name = Grammar.MakeTitleCaseWithArticle(string.Concat(new string[]
				{
					"the ",
					Grammar.MakePossessive(text7),
					" ",
					this.GovernmentType,
					" of ",
					text
				}));
				base.SetEntityProperty("name", this.Name);
				int num2 = base.Random(1, 100);
				if (num2 <= 30)
				{
					base.SetEntityProperty("newName", text);
					return;
				}
				if (num2 <= 70)
				{
					base.SetEntityProperty("newName", Grammar.MakeTitleCaseWithArticle(base.ExpandString(text + ", <spice.professions." + this.OrganizingPrincipleType + ".singular><spice.commonPhrases.shortHearth.!random>")));
					return;
				}
				base.SetEntityProperty("newName", Grammar.MakeTitleCaseWithArticle(base.ExpandString(text + ", <spice.commonPhrases.bygone.!random> <spice.commonPhrases.hearth.!random> of " + text7)));
				return;
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x00149384 File Offset: 0x00147584
		public void NameRegion_Old()
		{
			string text = NameMaker.MakeName(null, null, null, null, "Qudish", null, null, null, null, null, null, "Site", null, false, false, null, null);
			if (25.in100())
			{
				int num = base.Random(0, 14);
				if (num == 0)
				{
					text += " Sector";
				}
				else if (num == 1)
				{
					text = "the " + text + " Perimeter";
				}
				else if (num == 2)
				{
					text = "the " + text + " Periphery";
				}
				else if (num == 3)
				{
					text = "the " + text + " Anterior";
				}
				else if (num == 4)
				{
					text = "the " + text + " Posterier";
				}
				else if (num == 5)
				{
					text = "Inner " + text;
				}
				else if (num == 6)
				{
					text = "Outer " + text;
				}
				else if (num == 7)
				{
					text += " Province";
				}
				else if (num == 8)
				{
					text += " District";
				}
				else if (num == 9)
				{
					text += " Quarter";
				}
				else if (num == 10)
				{
					text += " Precinct";
				}
				else if (num == 11)
				{
					text += " Strata";
				}
				else if (num == 12)
				{
					text = "the " + text + " Torus";
				}
				else if (num == 13)
				{
					text = "the " + text + " Sphere";
				}
				else if (num == 14)
				{
					text = "the " + text + " Ellipsoid";
				}
			}
			if (base.Random(0, 100) < 10)
			{
				int num2 = base.Random(1, 10);
				if (num2 == 1)
				{
					text += " I";
				}
				else if (num2 == 2)
				{
					text += " II";
				}
				else if (num2 == 3)
				{
					text += " III";
				}
				else if (num2 == 4)
				{
					text += " IV";
				}
				else if (num2 == 5)
				{
					text += " V";
				}
				else if (num2 == 6)
				{
					text += " VI";
				}
				else if (num2 == 7)
				{
					text += " VII";
				}
				else if (num2 == 8)
				{
					text += " VIII";
				}
				else if (num2 == 9)
				{
					text += " IX";
				}
				else if (num2 == 10)
				{
					text += " X";
				}
			}
			base.SetEntityProperty("name", text);
		}

		// Token: 0x04002007 RID: 8199
		public HistoricEntitySnapshot SnapRegionParams;

		// Token: 0x04002008 RID: 8200
		public string GovernmentType;

		// Token: 0x04002009 RID: 8201
		public string OrganizingPrincipleType;

		// Token: 0x0400200A RID: 8202
		public string Name;

		// Token: 0x0400200B RID: 8203
		public const int normalNameChance = 25;

		// Token: 0x0400200C RID: 8204
		public const int normalNameChance_GLOBAL = 15;

		// Token: 0x0400200D RID: 8205
		public int period;
	}
}
