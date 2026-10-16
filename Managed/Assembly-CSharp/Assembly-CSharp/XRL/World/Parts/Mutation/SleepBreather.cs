using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001410 RID: 5136
	[Serializable]
	public class SleepBreather : BreatherBase
	{
		// Token: 0x0600C782 RID: 51074 RVA: 0x0048AC0E File Offset: 0x00488E0E
		public override string GetCommandDisplayName()
		{
			return "Breathe Sleep Gas";
		}

		// Token: 0x0600C783 RID: 51075 RVA: 0x0048AC15 File Offset: 0x00488E15
		public override string GetDescription()
		{
			return "You breathe sleep gas.";
		}

		// Token: 0x0600C784 RID: 51076 RVA: 0x0048AC1C File Offset: 0x00488E1C
		public override string GetLevelText(int Level)
		{
			return "Breathes sleep gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C785 RID: 51077 RVA: 0x0048AC74 File Offset: 0x00488E74
		public override string GetGasBlueprint()
		{
			return "SleepGas80";
		}

		// Token: 0x0600C786 RID: 51078 RVA: 0x0048AC7B File Offset: 0x00488E7B
		public override string GetBreathName()
		{
			return "sleep gas";
		}

		// Token: 0x0600C787 RID: 51079 RVA: 0x0048AC82 File Offset: 0x00488E82
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "w", "Y", "y");
		}
	}
}
