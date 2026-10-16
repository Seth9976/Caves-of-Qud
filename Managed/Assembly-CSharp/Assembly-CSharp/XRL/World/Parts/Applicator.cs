using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E03 RID: 3587
	[Serializable]
	public class Applicator : IPart
	{
		// Token: 0x060090FB RID: 37115 RVA: 0x00343ED4 File Offset: 0x003420D4
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090FC RID: 37116 RVA: 0x00343ED7 File Offset: 0x003420D7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterInventoryActionEvent.ID || ID == GetInventoryActionsAlwaysEvent.ID;
		}

		// Token: 0x060090FD RID: 37117 RVA: 0x00343EF8 File Offset: 0x003420F8
		public override bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			int num = 0;
			if (E.Object.Equipped == E.Actor || E.Object.InInventory == E.Actor)
			{
				if (E.Object.IsImportant())
				{
					num = -1;
				}
				else
				{
					num = 100;
				}
			}
			E.AddAction("Apply", "apply", "Apply", null, 'a', false, num, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x060090FE RID: 37118 RVA: 0x00343F6C File Offset: 0x0034216C
		public override bool HandleEvent(AfterInventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				GameObject actor = E.Actor;
				if (actor != null)
				{
					actor.UseEnergy(1000, "Item Applicator", null, null, false);
				}
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}
	}
}
