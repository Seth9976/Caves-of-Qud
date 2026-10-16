using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200140D RID: 5133
	[Serializable]
	public class NormalityBreather : BreatherBase
	{
		// Token: 0x0600C76B RID: 51051 RVA: 0x0048A9B0 File Offset: 0x00488BB0
		public override string GetCommandDisplayName()
		{
			return "Breathe Normality Gas";
		}

		// Token: 0x0600C76C RID: 51052 RVA: 0x0048A9B7 File Offset: 0x00488BB7
		public override string GetDescription()
		{
			return "You breathe normality gas.";
		}

		// Token: 0x0600C76D RID: 51053 RVA: 0x0048A9C0 File Offset: 0x00488BC0
		public override string GetLevelText(int Level)
		{
			return "Breathes normality gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C76E RID: 51054 RVA: 0x0048AA18 File Offset: 0x00488C18
		public override string GetGasBlueprint()
		{
			return "NormalityGas80";
		}

		// Token: 0x0600C76F RID: 51055 RVA: 0x0048AA1F File Offset: 0x00488C1F
		public override string GetBreathName()
		{
			return "normality gas";
		}

		// Token: 0x0600C770 RID: 51056 RVA: 0x0048AA26 File Offset: 0x00488C26
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "K", "y", "Y");
		}
	}
}
