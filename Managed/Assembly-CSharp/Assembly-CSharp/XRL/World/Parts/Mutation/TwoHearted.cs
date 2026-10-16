using System;
using System.Text;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001459 RID: 5209
	[Serializable]
	public class TwoHearted : BaseMutation
	{
		// Token: 0x0600CBEE RID: 52206 RVA: 0x004A24B0 File Offset: 0x004A06B0
		public override void Initialize()
		{
			base.Initialize();
			Run.SyncAbility(this.ParentObject, false);
		}

		// Token: 0x0600CBEF RID: 52207 RVA: 0x004A24C4 File Offset: 0x004A06C4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CBF0 RID: 52208 RVA: 0x004A24C7 File Offset: 0x004A06C7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSprintDurationEvent>.ID;
		}

		// Token: 0x0600CBF1 RID: 52209 RVA: 0x004A24E0 File Offset: 0x004A06E0
		public override bool HandleEvent(GetSprintDurationEvent E)
		{
			E.PercentageIncrease += this.GetSprintBonus();
			Templates.StatCollector stats = E.Stats;
			if (stats != null)
			{
				stats.AddPercentageBonusModifier("Duration", this.GetSprintBonus(), base.GetDisplayName(true) + " " + base.GetMutationTerm(null));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CBF2 RID: 52210 RVA: 0x004A253B File Offset: 0x004A073B
		public override string GetDescription()
		{
			return "You have two hearts.";
		}

		// Token: 0x0600CBF3 RID: 52211 RVA: 0x004A2544 File Offset: 0x004A0744
		public override string GetLevelText(int Level)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("+{{rules|").Append(2 + (Level - 1) / 2).Append("}} Toughness\n")
				.Append("You can sprint for {{rules|")
				.Append(TwoHearted.GetSprintBonus(Level))
				.Append("%}} longer.");
			return stringBuilder.ToString();
		}

		// Token: 0x0600CBF4 RID: 52212 RVA: 0x004A259D File Offset: 0x004A079D
		public static int GetSprintBonus(int Level)
		{
			return 20 + Level * 10;
		}

		// Token: 0x0600CBF5 RID: 52213 RVA: 0x004A25A6 File Offset: 0x004A07A6
		public int GetSprintBonus()
		{
			return TwoHearted.GetSprintBonus(base.Level);
		}

		// Token: 0x0600CBF6 RID: 52214 RVA: 0x004A25B3 File Offset: 0x004A07B3
		public static int GetToughnessBonus(int Level)
		{
			return 2 + (Level - 1) / 2;
		}

		// Token: 0x0600CBF7 RID: 52215 RVA: 0x004A25BC File Offset: 0x004A07BC
		public int GetToughnessBonus()
		{
			return TwoHearted.GetToughnessBonus(base.Level);
		}

		// Token: 0x0600CBF8 RID: 52216 RVA: 0x004A25C9 File Offset: 0x004A07C9
		public int GetToughnessBonusAttributableToTemporaryLevels()
		{
			return this.GetToughnessBonus() - TwoHearted.GetToughnessBonus(base.Level - base.GetTemporaryLevels());
		}

		// Token: 0x0600CBF9 RID: 52217 RVA: 0x004A25E4 File Offset: 0x004A07E4
		public override bool ChangeLevel(int NewLevel)
		{
			int toughnessBonusAttributableToTemporaryLevels = this.GetToughnessBonusAttributableToTemporaryLevels();
			int num = TwoHearted.GetToughnessBonus(base.Level) - toughnessBonusAttributableToTemporaryLevels;
			base.StatShifter.SetStatShift(this.ParentObject, "Toughness", num, true);
			base.StatShifter.SetStatShift(this.ParentObject, "Toughness", toughnessBonusAttributableToTemporaryLevels, false);
			Run.SyncAbility(this.ParentObject, false);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CBFA RID: 52218 RVA: 0x004A264B File Offset: 0x004A084B
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CBFB RID: 52219 RVA: 0x004A2655 File Offset: 0x004A0855
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts(GO);
			return base.Unmutate(GO);
		}
	}
}
