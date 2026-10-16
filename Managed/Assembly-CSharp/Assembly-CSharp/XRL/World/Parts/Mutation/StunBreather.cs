using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001411 RID: 5137
	[Serializable]
	public class StunBreather : BreatherBase
	{
		// Token: 0x0600C789 RID: 51081 RVA: 0x0048ACA3 File Offset: 0x00488EA3
		public override string GetCommandDisplayName()
		{
			return "Breathe Stun Gas";
		}

		// Token: 0x0600C78A RID: 51082 RVA: 0x0048ACAA File Offset: 0x00488EAA
		public override string GetDescription()
		{
			return "You breathe stun gas.";
		}

		// Token: 0x0600C78B RID: 51083 RVA: 0x0048ACB4 File Offset: 0x00488EB4
		public override string GetLevelText(int Level)
		{
			return "Breathes stun gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C78C RID: 51084 RVA: 0x0048AD0C File Offset: 0x00488F0C
		public override string GetGasBlueprint()
		{
			return "StunGas80";
		}

		// Token: 0x0600C78D RID: 51085 RVA: 0x0048AD13 File Offset: 0x00488F13
		public override string GetBreathName()
		{
			return "stun gas";
		}

		// Token: 0x0600C78E RID: 51086 RVA: 0x0048AD1A File Offset: 0x00488F1A
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "C", "c", "y");
		}
	}
}
