using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F73 RID: 3955
	[Serializable]
	public class InteriorContainer : IPart
	{
		// Token: 0x06009D6E RID: 40302 RVA: 0x00390A6D File Offset: 0x0038EC6D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009D6F RID: 40303 RVA: 0x00390A9B File Offset: 0x0038EC9B
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009D70 RID: 40304 RVA: 0x00390AB3 File Offset: 0x0038ECB3
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || !this.FireEvent(Event.New("Open", "Opener", E.Actor))) && base.HandleEvent(E);
		}

		// Token: 0x06009D71 RID: 40305 RVA: 0x00390AE8 File Offset: 0x0038ECE8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Open", "open", "Open", null, 'o', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009D72 RID: 40306 RVA: 0x00390B1F File Offset: 0x0038ED1F
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Open")
			{
				this.ParentObject.FireEvent(Event.New("Open", "Opener", E.Actor), E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D73 RID: 40307 RVA: 0x00390B5C File Offset: 0x0038ED5C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009D74 RID: 40308 RVA: 0x00390B5F File Offset: 0x0038ED5F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Open");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009D75 RID: 40309 RVA: 0x00390B74 File Offset: 0x0038ED74
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Open" && E.GetGameObjectParameter("Opener") == The.Player)
			{
				InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
				if (interiorZone != null)
				{
					TradeUI.ShowTradeScreen(interiorZone.ParentObject, 0f, TradeUI.TradeScreenMode.Trade);
					The.Player.UseEnergy(1000, "Companion Trade", null, null, false);
					E.RequestInterfaceExit();
				}
			}
			return base.FireEvent(E);
		}
	}
}
