using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000DD6 RID: 3542
	[Serializable]
	public class GritGateMainframeTerminal : IPoweredPart
	{
		// Token: 0x06009023 RID: 36899 RVA: 0x0033FD97 File Offset: 0x0033DF97
		public GritGateMainframeTerminal()
		{
			this.ChargeUse = 100;
			this.WorksOnSelf = true;
			this.NameForStatus = "GritGateMainframeTerminalInterface";
		}

		// Token: 0x06009024 RID: 36900 RVA: 0x0033FDB9 File Offset: 0x0033DFB9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009025 RID: 36901 RVA: 0x0033FDD8 File Offset: 0x0033DFD8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AddAction("Interface", "interface", "InterfaceWithGritGateMainframe", null, 'i', false, 100, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009026 RID: 36902 RVA: 0x0033FE38 File Offset: 0x0033E038
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "InterfaceWithGritGateMainframe")
			{
				if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.AccessTerminal(E.Actor);
					E.RequestInterfaceExit();
				}
				else
				{
					Popup.Show(this.ParentObject.The + this.ParentObject.ShortDisplayName + this.ParentObject.Is + " unresponsive.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009027 RID: 36903 RVA: 0x0033FED5 File Offset: 0x0033E0D5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009028 RID: 36904 RVA: 0x0033FED8 File Offset: 0x0033E0D8
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009029 RID: 36905 RVA: 0x0033FEF8 File Offset: 0x0033E0F8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				if (E.GetGameObjectParameter("User").IsPlayer() && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					return false;
				}
			}
			else if (E.ID == "CommandSmartUse")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("User");
				if (gameObjectParameter.IsPlayer() && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					this.AccessTerminal(gameObjectParameter);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600902A RID: 36906 RVA: 0x0033FFB0 File Offset: 0x0033E1B0
		private void AccessTerminal(GameObject Actor)
		{
			if (The.Game.HasQuest("Grave Thoughts") && !The.Game.HasFinishedQuest("A Call to Arms"))
			{
				CyberneticsTerminal.ShowTerminal(this.ParentObject, Actor, null, new GritGateTerminalScreenRoot());
				return;
			}
			if (The.Game.HasFinishedQuest("Decoding the Signal"))
			{
				CyberneticsTerminal.ShowTerminal(this.ParentObject, Actor, null, new GritGateTerminalScreenBasicAccess());
				return;
			}
			CyberneticsTerminal.ShowTerminal(this.ParentObject, Actor, null, new GritGateTerminalScreenGoodbye());
		}
	}
}
