using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200140E RID: 5134
	[Serializable]
	public class PoisonBreather : BreatherBase
	{
		// Token: 0x0600C772 RID: 51058 RVA: 0x0048AA47 File Offset: 0x00488C47
		public override string GetCommandDisplayName()
		{
			return "Breathe Poison Gas";
		}

		// Token: 0x0600C773 RID: 51059 RVA: 0x0048AA4E File Offset: 0x00488C4E
		public override string GetDescription()
		{
			return "You breathe poison gas.";
		}

		// Token: 0x0600C774 RID: 51060 RVA: 0x0048AA58 File Offset: 0x00488C58
		public override string GetLevelText(int Level)
		{
			return "Breathes poison gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C775 RID: 51061 RVA: 0x0048AAB0 File Offset: 0x00488CB0
		public override string GetGasBlueprint()
		{
			return "PoisonGas80";
		}

		// Token: 0x0600C776 RID: 51062 RVA: 0x0048AAB7 File Offset: 0x00488CB7
		public override string GetBreathName()
		{
			return "poison gas";
		}

		// Token: 0x0600C777 RID: 51063 RVA: 0x0048AABE File Offset: 0x00488CBE
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "G", "g", "w");
		}
	}
}
