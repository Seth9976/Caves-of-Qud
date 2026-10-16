using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	/// This class is not used in the base game.
	// Token: 0x020013D2 RID: 5074
	[Serializable]
	public class Hemophilia : BaseMutation
	{
		// Token: 0x0600C411 RID: 50193 RVA: 0x004775FA File Offset: 0x004757FA
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C412 RID: 50194 RVA: 0x004775FD File Offset: 0x004757FD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetBandagePerformanceEvent>.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600C413 RID: 50195 RVA: 0x0047761C File Offset: 0x0047581C
		public override bool HandleEvent(GetBandagePerformanceEvent E)
		{
			if (E.Pass == GetBandagePerformanceEvent.PASSES && E.Subject == this.ParentObject && E.Checking == "Subject")
			{
				int performance = E.Performance;
				E.Performance = performance / GlobalConfig.GetIntSetting("HemophiliaBandagePerformanceDivisor", Hemophilia.DEFAULT_BANDAGE_PERFORMANCE_DIVISOR);
				if (performance > 0 && E.Performance == 0)
				{
					E.Performance = 1;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C414 RID: 50196 RVA: 0x0047768E File Offset: 0x0047588E
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Bleeding", E.Vs))
			{
				E.Roll -= E.NaturalRoll / 2;
				E.IgnoreNatural20 = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C415 RID: 50197 RVA: 0x004776C5 File Offset: 0x004758C5
		public override string GetDescription()
		{
			return "Your blood does not clot easily.\n\nIt takes much longer than usual for you to stop bleeding.";
		}

		// Token: 0x0600C416 RID: 50198 RVA: 0x004776CC File Offset: 0x004758CC
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C417 RID: 50199 RVA: 0x004776D3 File Offset: 0x004758D3
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C418 RID: 50200 RVA: 0x004776DC File Offset: 0x004758DC
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C419 RID: 50201 RVA: 0x004776E6 File Offset: 0x004758E6
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004DCC RID: 19916
		public static readonly int DEFAULT_BANDAGE_PERFORMANCE_DIVISOR = 3;
	}
}
