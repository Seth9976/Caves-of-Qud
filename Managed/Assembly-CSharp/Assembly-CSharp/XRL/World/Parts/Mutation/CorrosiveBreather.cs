using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200140A RID: 5130
	[Serializable]
	public class CorrosiveBreather : BreatherBase
	{
		// Token: 0x0600C749 RID: 51017 RVA: 0x0048A183 File Offset: 0x00488383
		public override string GetCommandDisplayName()
		{
			return "Breathe Corrosive Gas";
		}

		// Token: 0x0600C74A RID: 51018 RVA: 0x0048A18A File Offset: 0x0048838A
		public override string GetDescription()
		{
			return "You breathe corrosive gas.";
		}

		// Token: 0x0600C74B RID: 51019 RVA: 0x0048A194 File Offset: 0x00488394
		public override string GetLevelText(int Level)
		{
			return "Breathes corrosive gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C74C RID: 51020 RVA: 0x0048A1EC File Offset: 0x004883EC
		public override bool ChangeLevel(int NewLevel)
		{
			base.StatShifter.SetStatShift("AcidResistance", base.Level * 2, false);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C74D RID: 51021 RVA: 0x0048A20F File Offset: 0x0048840F
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts();
			return base.Unmutate(GO);
		}

		// Token: 0x0600C74E RID: 51022 RVA: 0x0048A223 File Offset: 0x00488423
		public override string GetGasBlueprint()
		{
			return "AcidGas80";
		}

		// Token: 0x0600C74F RID: 51023 RVA: 0x0048A22A File Offset: 0x0048842A
		public override string GetBreathName()
		{
			return "acid gas";
		}

		// Token: 0x0600C750 RID: 51024 RVA: 0x0048A231 File Offset: 0x00488431
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "G", "g", "w");
		}
	}
}
