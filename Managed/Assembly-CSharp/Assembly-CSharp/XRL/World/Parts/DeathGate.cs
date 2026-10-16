using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F9D RID: 3997
	[Serializable]
	public class DeathGate : IPart
	{
		// Token: 0x06009EA7 RID: 40615 RVA: 0x00397746 File Offset: 0x00395946
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009EA8 RID: 40616 RVA: 0x00397749 File Offset: 0x00395949
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EA9 RID: 40617 RVA: 0x0039774C File Offset: 0x0039594C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanAttemptDoorUnlock");
			Registrar.Register("AttemptDoorUnlock");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009EAA RID: 40618 RVA: 0x0039776C File Offset: 0x0039596C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanAttemptDoorUnlock")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Opener");
				E.GetParameter("Door");
				if (this.ParentObject.Brain != null)
				{
					return true;
				}
				if (gameObjectParameter == null)
				{
					return false;
				}
				if (!gameObjectParameter.IsPlayer())
				{
					return false;
				}
				if (!gameObjectParameter.HasMarkOfDeath(null))
				{
					return false;
				}
			}
			else if (E.ID == "AttemptDoorUnlock")
			{
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Opener");
				Door door = E.GetParameter("Door") as Door;
				if (this.ParentObject.Brain != null)
				{
					return true;
				}
				if (gameObjectParameter2 == null)
				{
					return false;
				}
				if (!gameObjectParameter2.IsPlayer())
				{
					return false;
				}
				if (!gameObjectParameter2.HasMarkOfDeath(null))
				{
					Popup.ShowFail("The gates are sealed for eternity.", true, true, true);
					return false;
				}
				if (door.Locked)
				{
					door.ParentObject.SetIntProperty("NoClose", 1, false);
					door.Locked = false;
					Popup.Show("The gates swing wide.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					JournalAPI.RevealObservation("MarkOfDeathSecret", true);
					The.Game.FinishQuestStep("Tomb of the Eaters", "Enter the Tomb of the Eaters", -1, true, null);
					door.PerformOpen(null, null, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009EAB RID: 40619 RVA: 0x003978A3 File Offset: 0x00395AA3
		public static void AddMarkOfDeath()
		{
			ReshephsCrypt.AddMarkOfDeath();
			The.Game.FinishQuestStep("Tomb of the Eaters", "Inscribe the Mark", -1, true, null);
		}
	}
}
