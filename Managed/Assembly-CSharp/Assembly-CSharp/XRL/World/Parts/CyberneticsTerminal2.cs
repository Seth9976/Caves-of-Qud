using System;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F4D RID: 3917
	[Serializable]
	public class CyberneticsTerminal2 : IPoweredPart, IHackingSifrahHandler
	{
		// Token: 0x06009C5A RID: 40026 RVA: 0x0038B2A5 File Offset: 0x003894A5
		public CyberneticsTerminal2()
		{
			this.ChargeUse = 100;
			this.WorksOnSelf = true;
			this.NameForStatus = "BecomingNookInterface";
		}

		// Token: 0x06009C5B RID: 40027 RVA: 0x0038B2C7 File Offset: 0x003894C7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009C5C RID: 40028 RVA: 0x0038B305 File Offset: 0x00389505
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("circuitry", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C5D RID: 40029 RVA: 0x0038B328 File Offset: 0x00389528
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				E.Add(this.ParentObject, this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false), null, null, null, null, null, 1, null, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C5E RID: 40030 RVA: 0x0038B380 File Offset: 0x00389580
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!E.Actor.IsPlayer() || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x06009C5F RID: 40031 RVA: 0x0038B3CB File Offset: 0x003895CB
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			return this.AttemptInterface(E.Actor, E) && base.HandleEvent(E);
		}

		// Token: 0x06009C60 RID: 40032 RVA: 0x0038B3E8 File Offset: 0x003895E8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Interface", "interface", CyberneticsTerminal2.COMMAND_NAME, null, 'i', false, 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C61 RID: 40033 RVA: 0x0038B420 File Offset: 0x00389620
		public override bool HandleEvent(InventoryActionEvent E)
		{
			return (!(E.Command == CyberneticsTerminal2.COMMAND_NAME) || this.AttemptInterface(E.Actor, E)) && base.HandleEvent(E);
		}

		// Token: 0x06009C62 RID: 40034 RVA: 0x0038B44C File Offset: 0x0038964C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C63 RID: 40035 RVA: 0x0038B450 File Offset: 0x00389650
		public void HackingResultSuccess(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			if (Object == this.ParentObject)
			{
				int num = 1;
				while (30.in100())
				{
					num++;
				}
				this.ParentObject.ModIntProperty("HackLevel", num, false);
				if (this.ParentObject.GetIntProperty("SecurityAlertLevel", 0) >= this.ParentObject.GetIntProperty("HackLevel", 0))
				{
					this.ParentObject.SetIntProperty("HackLevel", this.ParentObject.GetIntProperty("SecurityAlertLevel", 0) + 1, false);
				}
				this.AskLowLevelHack(Actor);
			}
		}

		// Token: 0x06009C64 RID: 40036 RVA: 0x0038B4DC File Offset: 0x003896DC
		public void HackingResultExceptionalSuccess(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			if (Object == this.ParentObject)
			{
				int num = 2;
				while (50.in100())
				{
					num++;
				}
				this.ParentObject.ModIntProperty("HackLevel", num, false);
				if (this.ParentObject.GetIntProperty("SecurityAlertLevel", 0) >= this.ParentObject.GetIntProperty("HackLevel", 0))
				{
					this.ParentObject.SetIntProperty("HackLevel", this.ParentObject.GetIntProperty("SecurityAlertLevel", 0) + 1, false);
				}
				int num2 = 1;
				while (30.in100())
				{
					num2++;
				}
				if (Actor.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						"In the course of the hack, you are able to insert instructions into ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						" granting you an extra ",
						(num2 == 1) ? "cybernetics license point" : num2.Things("cybernetics license point", null),
						"!"
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				Actor.ModIntProperty("CyberneticsLicenses", num2, false);
				this.AskLowLevelHack(Actor);
			}
		}

		// Token: 0x06009C65 RID: 40037 RVA: 0x0038B60A File Offset: 0x0038980A
		public void HackingResultPartialSuccess(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			if (Object == this.ParentObject && Actor.IsPlayer())
			{
				Popup.Show("The hack fails, but you manage to cover your tracks before any security measures kick in.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06009C66 RID: 40038 RVA: 0x0038B634 File Offset: 0x00389834
		public void HackingResultFailure(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			if (Object == this.ParentObject)
			{
				int num = 1;
				while (30.in100())
				{
					num++;
				}
				this.ParentObject.ModIntProperty("SecurityAlertLevel", num, false);
				if (Actor.IsPlayer())
				{
					Popup.Show("The hack fails, and alert lights on " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " begin pulsing rhythmically...", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x06009C67 RID: 40039 RVA: 0x0038B6C0 File Offset: 0x003898C0
		public void HackingResultCriticalFailure(GameObject Actor, GameObject Object, HackingSifrah Game)
		{
			if (Object == this.ParentObject)
			{
				if (Actor.HasPart<Dystechnia>())
				{
					FusionReactor part = this.ParentObject.GetPart<FusionReactor>();
					if (part == null || !part.Explode(null, false))
					{
						this.ParentObject.Explode(10000, null, null, 1f, false, false, false, 0, null);
					}
					Game.RequestInterfaceExit();
					return;
				}
				int num = 2;
				while (50.in100())
				{
					num++;
				}
				this.ParentObject.ModIntProperty("SecurityAlertLevel", num, false);
				if (Actor.IsPlayer())
				{
					Popup.Show("The hack fails, and alert lights on " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " begin pulsing urgently...", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x06009C68 RID: 40040 RVA: 0x0038B798 File Offset: 0x00389998
		public bool AttemptInterface(GameObject Actor, IEvent FromEvent = null)
		{
			if (!GameObject.Validate(Actor))
			{
				return false;
			}
			if (!Actor.IsPlayer())
			{
				return false;
			}
			if (!base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + base.GetStatusPhrase() + ".");
			}
			CyberneticsTerminal.ShowTerminal(this.ParentObject, Actor, null, null);
			if (FromEvent != null)
			{
				FromEvent.RequestInterfaceExit();
			}
			return true;
		}

		// Token: 0x06009C69 RID: 40041 RVA: 0x0038B848 File Offset: 0x00389A48
		private void AskLowLevelHack(GameObject Actor)
		{
			if (GameObject.Validate(ref Actor) && Actor.IsPlayer())
			{
				string sifrahHackingLowLevel = Options.SifrahHackingLowLevel;
				if (sifrahHackingLowLevel == "Ask")
				{
					if (Popup.ShowYesNo("Do you want to use a low-level hack? Low-level hacks make it more difficult to read the terminal output but reduce the chance of triggering security alerts.", "Sounds/UI/ui_notification", false, DialogResult.Yes, null) == DialogResult.Yes)
					{
						this.ParentObject.SetIntProperty("LowLevelHack", 1, false);
						return;
					}
					this.ParentObject.RemoveIntProperty("LowLevelHack");
					return;
				}
				else
				{
					if (sifrahHackingLowLevel == "Always")
					{
						this.ParentObject.SetIntProperty("LowLevelHack", 1, false);
						return;
					}
					if (sifrahHackingLowLevel == "Never")
					{
						this.ParentObject.RemoveIntProperty("LowLevelHack");
					}
				}
			}
		}

		// Token: 0x040040AF RID: 16559
		public static readonly string COMMAND_NAME = "InterfaceWithBecomingNook";
	}
}
