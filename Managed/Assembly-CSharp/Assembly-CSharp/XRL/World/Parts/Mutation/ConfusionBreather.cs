using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001409 RID: 5129
	[Serializable]
	public class ConfusionBreather : BreatherBase
	{
		// Token: 0x0600C742 RID: 51010 RVA: 0x0048A0EB File Offset: 0x004882EB
		public override string GetCommandDisplayName()
		{
			return "Breathe Confusion Gas";
		}

		// Token: 0x0600C743 RID: 51011 RVA: 0x0048A0F2 File Offset: 0x004882F2
		public override string GetDescription()
		{
			return "You breathe confusion gas.";
		}

		// Token: 0x0600C744 RID: 51012 RVA: 0x0048A0FC File Offset: 0x004882FC
		public override string GetLevelText(int Level)
		{
			return "Breathes confusion gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C745 RID: 51013 RVA: 0x0048A154 File Offset: 0x00488354
		public override string GetGasBlueprint()
		{
			return "ConfusionGas80";
		}

		// Token: 0x0600C746 RID: 51014 RVA: 0x0048A15B File Offset: 0x0048835B
		public override string GetBreathName()
		{
			return "confusion gas";
		}

		// Token: 0x0600C747 RID: 51015 RVA: 0x0048A162 File Offset: 0x00488362
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "B", "b", "y");
		}
	}
}
