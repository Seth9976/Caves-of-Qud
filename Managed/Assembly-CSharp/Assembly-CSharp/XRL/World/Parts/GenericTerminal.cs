using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001248 RID: 4680
	[Serializable]
	public class GenericTerminal : IPoweredPart
	{
		// Token: 0x0600BA14 RID: 47636 RVA: 0x0043F578 File Offset: 0x0043D778
		public GenericTerminal()
		{
			this.ChargeUse = 100;
			this.WorksOnSelf = true;
		}

		// Token: 0x0600BA15 RID: 47637 RVA: 0x0043F5B0 File Offset: 0x0043D7B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600BA16 RID: 47638 RVA: 0x0043F5D0 File Offset: 0x0043D7D0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AddAction("Interface", "interface", "InterfaceWithTerminal", null, 'i', false, 100, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA17 RID: 47639 RVA: 0x0043F630 File Offset: 0x0043D830
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "InterfaceWithTerminal" && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				GameObject actor = E.Actor;
				if ((actor == null || actor.IsPlayer()) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					if (actor != null && actor.IsTrueKin() && !string.IsNullOrEmpty(this.StartingScreenTrueKin))
					{
						GenericTerminal.ShowTerminal(this.ParentObject, actor, ModManager.CreateInstance<GenericTerminalScreen>(this.StartingScreenTrueKin));
					}
					else if (actor != null && !actor.IsTrueKin() && !string.IsNullOrEmpty(this.StartingScreenMutant))
					{
						GenericTerminal.ShowTerminal(this.ParentObject, actor, ModManager.CreateInstance<GenericTerminalScreen>(this.StartingScreenTrueKin));
					}
					else
					{
						GenericTerminal.ShowTerminal(this.ParentObject, actor, ModManager.CreateInstance<GenericTerminalScreen>(this.StartingScreen));
					}
				}
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA18 RID: 47640 RVA: 0x0043F740 File Offset: 0x0043D940
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BA19 RID: 47641 RVA: 0x0043F743 File Offset: 0x0043D943
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BA1A RID: 47642 RVA: 0x0043F764 File Offset: 0x0043D964
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
					if (gameObjectParameter.IsTrueKin() && !string.IsNullOrEmpty(this.StartingScreenTrueKin))
					{
						GenericTerminal.ShowTerminal(this.ParentObject, gameObjectParameter, ModManager.CreateInstance<GenericTerminalScreen>(this.StartingScreenTrueKin));
					}
					else if (!gameObjectParameter.IsTrueKin() && !string.IsNullOrEmpty(this.StartingScreenMutant))
					{
						GenericTerminal.ShowTerminal(this.ParentObject, gameObjectParameter, ModManager.CreateInstance<GenericTerminalScreen>(this.StartingScreenTrueKin));
					}
					else
					{
						GenericTerminal.ShowTerminal(this.ParentObject, gameObjectParameter, ModManager.CreateInstance<GenericTerminalScreen>(this.StartingScreen));
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004AC7 RID: 19143
		public string StartingScreen = "";

		// Token: 0x04004AC8 RID: 19144
		public string StartingScreenTrueKin = "";

		// Token: 0x04004AC9 RID: 19145
		public string StartingScreenMutant = "";
	}
}
