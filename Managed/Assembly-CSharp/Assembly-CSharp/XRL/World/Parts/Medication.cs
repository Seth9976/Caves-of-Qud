using System;

namespace XRL.World.Parts
{
	// Token: 0x020010C7 RID: 4295
	[Serializable]
	public class Medication : IPart
	{
		// Token: 0x0600AAF3 RID: 43763 RVA: 0x003E8887 File Offset: 0x003E6A87
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600AAF4 RID: 43764 RVA: 0x003E888A File Offset: 0x003E6A8A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == AfterInventoryActionEvent.ID;
		}

		// Token: 0x0600AAF5 RID: 43765 RVA: 0x003E88A8 File Offset: 0x003E6AA8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			int num = 0;
			if (E.Object.HasPart<Empty_Tonic_Applicator>())
			{
				num = -100;
			}
			else if (E.Object.Equipped == E.Actor || E.Object.InInventory == E.Actor)
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

		// Token: 0x0600AAF6 RID: 43766 RVA: 0x003E8930 File Offset: 0x003E6B30
		public override bool HandleEvent(AfterInventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				GameObject actor = E.Actor;
				if (actor != null)
				{
					actor.UseEnergy(1000, "Item Medication", null, null, false);
				}
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}
	}
}
