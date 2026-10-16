using System;
using ConsoleLib.Console;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200140F RID: 5135
	[Serializable]
	public class ShameBreather : BreatherBase
	{
		// Token: 0x0600C779 RID: 51065 RVA: 0x0048AADF File Offset: 0x00488CDF
		public override string GetCommandDisplayName()
		{
			return "Breathe Shame Gas";
		}

		// Token: 0x0600C77A RID: 51066 RVA: 0x0048AAE6 File Offset: 0x00488CE6
		public override string GetDescription()
		{
			return "You breathe shame gas.";
		}

		// Token: 0x0600C77B RID: 51067 RVA: 0x0048AAF0 File Offset: 0x00488CF0
		public override string GetLevelText(int Level)
		{
			return "Breathes shame gas in a cone.\n" + "Cone length: " + this.GetConeLength(-1).ToString() + " tiles\n" + "Cone angle: " + this.GetConeAngle(-1).ToString() + " degrees\n" + "Cooldown: 15 rounds\n";
		}

		// Token: 0x0600C77C RID: 51068 RVA: 0x0048AB48 File Offset: 0x00488D48
		public override string GetGasBlueprint()
		{
			return "ShameGas80";
		}

		// Token: 0x0600C77D RID: 51069 RVA: 0x0048AB4F File Offset: 0x00488D4F
		public override string GetBreathName()
		{
			return "shame gas";
		}

		// Token: 0x0600C77E RID: 51070 RVA: 0x0048AB56 File Offset: 0x00488D56
		public override void BreatheInCell(Cell C, ScreenBuffer Buffer, bool doEffect = true)
		{
			base.DrawBreathInCell(C, Buffer, "B", "b", "C");
		}

		// Token: 0x0600C77F RID: 51071 RVA: 0x0048AB6F File Offset: 0x00488D6F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyShamed");
			Registrar.Register("ApplyShameGas");
			Registrar.Register("CanApplyShamed");
			Registrar.Register("CanApplyShameGas");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C780 RID: 51072 RVA: 0x0048ABA8 File Offset: 0x00488DA8
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "CanApplyShamed") && !(E.ID == "CanApplyShameGas") && !(E.ID == "ApplyShamed") && !(E.ID == "ApplyShameGas") && base.FireEvent(E);
		}
	}
}
