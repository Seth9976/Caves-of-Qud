using System;

namespace XRL.World.Parts
{
	// Token: 0x020010AC RID: 4268
	[Serializable]
	public class Teleporter : ITeleporter
	{
		// Token: 0x0600A9C7 RID: 43463 RVA: 0x003E13FB File Offset: 0x003DF5FB
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A9C8 RID: 43464 RVA: 0x003E141C File Offset: 0x003DF61C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Actor))
			{
				E.AddAction("Activate", "activate", "ActivateTeleporter", null, 'a', false, 100, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9C9 RID: 43465 RVA: 0x003E1464 File Offset: 0x003DF664
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateTeleporter" && base.AttemptTeleport(E.Actor, E))
			{
				E.Actor.UseEnergy(1000, "Item Recoiler", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}
	}
}
