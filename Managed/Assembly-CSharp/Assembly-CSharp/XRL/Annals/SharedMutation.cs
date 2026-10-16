using System;
using HistoryKit;
using XRL.Language;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts.Mutation;

namespace XRL.Annals
{
	// Token: 0x0200062A RID: 1578
	[Serializable]
	public class SharedMutation : HistoricEvent
	{
		// Token: 0x060041F3 RID: 16883 RVA: 0x00153DD7 File Offset: 0x00151FD7
		public SharedMutation()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x00153DE6 File Offset: 0x00151FE6
		public SharedMutation(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x00153DF8 File Offset: 0x00151FF8
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string text = "";
			string randomProperty = QudHistoryHelpers.GetRandomProperty(snapshotAtYear, snapshotAtYear.GetProperty("defaultSacredThing", "unknown"), new string[] { "sacredThings" });
			base.SetEntityProperty("villageGatherReason", "commune");
			int num;
			do
			{
				num = Stat.Random(1, 1000);
				if (num <= 905)
				{
					int num2 = Stat.Random(1, 1000);
					BaseMutation baseMutation;
					if (num2 <= 600)
					{
						baseMutation = MutationFactory.GetRandomMutation("Physical");
						base.AddEntityListItem("sharedMutations", baseMutation.Name, false);
					}
					else if (num2 <= 900)
					{
						baseMutation = MutationFactory.GetRandomMutation("Mental");
						base.AddEntityListItem("sharedMutations", baseMutation.Name, false);
					}
					else if (num2 <= 960)
					{
						baseMutation = MutationFactory.GetRandomMutation("PhysicalDefects");
						base.AddEntityListItem("sharedMutations", baseMutation.Name, false);
					}
					else if (num2 <= 1000)
					{
						baseMutation = MutationFactory.GetRandomMutation("MentalDefects");
						base.AddEntityListItem("sharedMutations", baseMutation.Name, false);
					}
					else
					{
						baseMutation = MutationFactory.GetRandomMutation("Physical");
						base.AddEntityListItem("sharedMutations", baseMutation.Name, false);
					}
					if (baseMutation.Name == "Confusion" || baseMutation.Name == "Disintegration")
					{
						base.SetEntityProperty("highlyEntropicBeingWorshipAttitude", "50");
					}
					text = baseMutation.GetBearerDescription();
					try
					{
						Faction ifExists = Factions.GetIfExists("Entropic");
						int num3;
						if (ifExists != null && ifExists.PartReputation.TryGetValue(baseMutation.GetMutationEntry().Class, out num3))
						{
							base.SetEntityProperty("highlyEntropicBeingWorshipAttitude", num3.ToString());
						}
						goto IL_022C;
					}
					catch (Exception)
					{
						goto IL_022C;
					}
				}
				if (num > 995)
				{
					goto IL_020C;
				}
			}
			while (this.bVillageZero);
			if (If.CoinFlip(null, null))
			{
				base.AddEntityListItem("sharedDiseases", "Glotrot", false);
				text = "those afflicted with glotrot";
				goto IL_022C;
			}
			base.AddEntityListItem("sharedDiseases", "Ironshank", false);
			text = "those afflicted with ironshank";
			goto IL_022C;
			IL_020C:
			if (num <= 1000)
			{
				base.AddEntityListItem("sharedTransformations", "Mechanical", false);
				text = "automatons";
			}
			IL_022C:
			string text2;
			switch (Stat.Random(1, 3))
			{
			case 1:
				text2 = string.Format("Since the time of the {0} {1}, {2} have {3} inside the {4} walls of {5}.|{6}", new object[]
				{
					base.ExpandString("<spice.adjectives.!random>"),
					base.ExpandString("<spice.personNouns.!random>"),
					text,
					base.ExpandString("<spice.commonPhrases.congregated.!random>"),
					base.ExpandString("<spice.commonPhrases.sacred.!random>"),
					snapshotAtYear.GetProperty("name", "unknown"),
					this.id
				});
				break;
			case 2:
				text2 = string.Format("{0} are welcome in {1}. By the grace of {2}, {3}.|{4}", new object[]
				{
					Grammar.InitCap(text),
					snapshotAtYear.GetProperty("name", "unknown"),
					randomProperty,
					base.ExpandString("<spice.instancesOf.letItAlwaysBeSo.!random>"),
					this.id
				});
				break;
			case 3:
				text2 = string.Format("{4}! {0} {1} here. Be {2} to them, or may the {3} {5} take you.|{6}", new object[]
				{
					Grammar.InitCap(text),
					base.ExpandString("<spice.commonPhrases.gather.!random>"),
					base.ExpandString("<spice.commonPhrases.kind.!random>"),
					base.ExpandString("<spice.adjectives.!random>"),
					Grammar.InitCap(base.ExpandString("<spice.commonPhrases.hark.!random>")),
					base.ExpandString("<spice.personNouns.!random>"),
					this.id
				});
				break;
			default:
				text2 = string.Format("Since the time of the {0} {1}, {2} have {3} inside the {4} walls of {5}.|{6}", new object[]
				{
					base.ExpandString("<spice.adjectives.!random>"),
					base.ExpandString("<spice.personNouns.!random>"),
					text,
					base.ExpandString("<spice.commonPhrases.congregated.!random>"),
					base.ExpandString("<spice.commonPhrases.sacred.!random>"),
					snapshotAtYear.GetProperty("name", "unknown"),
					this.id
				});
				break;
			}
			base.AddEntityListItem("sacredThings", text, false);
			base.AddEntityListItem("Gospels", text2, false);
		}

		// Token: 0x04002063 RID: 8291
		public bool bVillageZero;
	}
}
