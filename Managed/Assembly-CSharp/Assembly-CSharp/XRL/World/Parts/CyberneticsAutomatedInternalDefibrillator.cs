using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F18 RID: 3864
	[Serializable]
	public class CyberneticsAutomatedInternalDefibrillator : IPoweredPart
	{
		// Token: 0x060099F9 RID: 39417 RVA: 0x0037E781 File Offset: 0x0037C981
		public CyberneticsAutomatedInternalDefibrillator()
		{
			this.ChargeUse = 0;
			this.NameForStatus = "AutomatedInternalDefibrillator";
		}

		// Token: 0x060099FA RID: 39418 RVA: 0x0037E7A3 File Offset: 0x0037C9A3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060099FB RID: 39419 RVA: 0x0037E7A6 File Offset: 0x0037C9A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x060099FC RID: 39420 RVA: 0x0037E7BC File Offset: 0x0037C9BC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			return base.HandleEvent(E);
		}

		// Token: 0x060099FD RID: 39421 RVA: 0x0037E7D6 File Offset: 0x0037C9D6
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060099FE RID: 39422 RVA: 0x0037E7DC File Offset: 0x0037C9DC
		public override void TurnTick(long TimeTick, int Amount)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && implantee.HasEffect<CardiacArrest>() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.GetChance(implantee).in100())
			{
				implantee.RemoveEffect<CardiacArrest>();
			}
		}

		// Token: 0x060099FF RID: 39423 RVA: 0x0037E840 File Offset: 0x0037CA40
		public int GetChance(GameObject who = null)
		{
			int num = this.Chance;
			if (who == null)
			{
				who = this.ParentObject.Implantee;
			}
			if (who != null)
			{
				num = GetAvailableComputePowerEvent.AdjustUp(who, num);
			}
			return num;
		}

		// Token: 0x0400402B RID: 16427
		public int Chance = 50;
	}
}
