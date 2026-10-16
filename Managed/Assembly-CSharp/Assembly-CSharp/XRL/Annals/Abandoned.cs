using System;
using HistoryKit;
using XRL.Language;
using XRL.World;

namespace XRL.Annals
{
	// Token: 0x02000620 RID: 1568
	[Serializable]
	public class Abandoned : HistoricEvent
	{
		// Token: 0x060041D0 RID: 16848 RVA: 0x00150CF6 File Offset: 0x0014EEF6
		public Abandoned()
		{
			this.bVillageZero = false;
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x00150D05 File Offset: 0x0014EF05
		public Abandoned(bool VillageZero)
		{
			this.bVillageZero = VillageZero;
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x00150D14 File Offset: 0x0014EF14
		public override void Generate()
		{
			this.duration = 1L;
			HistoricEntitySnapshot snapshotAtYear = this.entity.GetSnapshotAtYear(this.entity.lastYear);
			string randomElement = Abandoned.Branches.GetRandomElement<string>(null);
			string text;
			if (!(randomElement == "sacked"))
			{
				if (!(randomElement == "curse"))
				{
					if (!(randomElement == "lose interest"))
					{
						if (!(randomElement == "embrance profanity"))
						{
							text = Grammar.InitCap(string.Format("{0}, the villagers of {1} {2} {3}, and the village was {4}.|{5}", new object[]
							{
								Grammar.InitCap(base.ExpandString("<spice.instancesOf.overTime.!random>")),
								snapshotAtYear.GetProperty("name", "unknown"),
								base.ExpandString("<spice.instancesOf.lostFaithIn.!random>"),
								snapshotAtYear.GetProperty("governor", "unknown"),
								base.ExpandString("<spice.commonPhrases.abandoned.!random>"),
								this.id
							}));
						}
						else
						{
							text = Grammar.InitCap(string.Format("{0}, the {1} of {2} was {3} by the villagers of {4}. Having lost the cohesion of its communal principles, the village was {5} in %{6}%.|{7}", new object[]
							{
								Grammar.InitCap(base.ExpandString("<spice.instancesOf.overTime.!random>")),
								base.ExpandString("<spice.commonPhrases.profanity.!random>"),
								snapshotAtYear.profaneThing,
								base.ExpandString("<spice.commonPhrases.embraced.!random>"),
								snapshotAtYear.GetProperty("name", "unknown"),
								base.ExpandString("<spice.commonPhrases.abandoned.!random>"),
								this.year,
								this.id
							}));
						}
					}
					else
					{
						text = Grammar.InitCap(string.Format("{0}, the villagers of {1} {2} {3}, and the village was {4}.|{5}", new object[]
						{
							Grammar.InitCap(base.ExpandString("<spice.instancesOf.overTime.!random>")),
							snapshotAtYear.GetProperty("name", "unknown"),
							base.ExpandString("<spice.instancesOf.lostInterestIn.!random>"),
							snapshotAtYear.sacredThing,
							base.ExpandString("<spice.commonPhrases.abandoned.!random>"),
							this.id
						}));
					}
				}
				else
				{
					text = Grammar.InitCap(string.Format("{0} {1} and the {2} village of {3}! Let no {4} {5} here {6}.|{7}", new object[]
					{
						base.ExpandString("<spice.instancesOf.aCurseUpon.!random>"),
						snapshotAtYear.sacredThing,
						base.ExpandString("<spice.commonPhrases.despicable.!random>"),
						snapshotAtYear.GetProperty("name", "unknown"),
						base.ExpandString("<spice.personNouns.!random>"),
						base.ExpandString("<spice.commonPhrases.settle.!random>"),
						base.ExpandString("<spice.instancesOf.forAllTime.!random>"),
						this.id
					}));
				}
			}
			else
			{
				text = Grammar.InitCap(string.Format("In %{0}%, the village of {1} was {2} by {3}.|{4}", new object[]
				{
					this.year,
					snapshotAtYear.GetProperty("name", "unknown"),
					base.ExpandString("<spice.commonPhrases.sacked.!random>"),
					Faction.GetFormattedName(Factions.GetRandomFaction().Name),
					this.id
				}));
			}
			int num = int.Parse(this.history.GetEntitiesWithProperty("Resheph").GetRandomElement(null).GetCurrentSnapshot()
				.GetProperty("flipYear", "unknown"));
			base.AddEntityListItem("Gospels", text, false);
			base.SetEntityProperty("abandoned", "true");
			base.SetEntityProperty("ruinScale", Math.Max(Math.Min(((long)(1000 + num) - this.year) / 100L, 4L), 1L).ToString());
		}

		// Token: 0x0400204B RID: 8267
		public bool bVillageZero;

		// Token: 0x0400204C RID: 8268
		public static readonly string[] Branches = new string[] { "sacked", "sacked", "curse", "curse", "curse", "lose interest", "embrance profanity", "lose faith" };
	}
}
