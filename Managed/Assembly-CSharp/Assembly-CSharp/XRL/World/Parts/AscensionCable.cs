using System;
using XRL.UI;
using XRL.World.Conversations.Parts;

namespace XRL.World.Parts
{
	// Token: 0x02000F95 RID: 3989
	public class AscensionCable : IPart
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06009E45 RID: 40517 RVA: 0x00394D8C File Offset: 0x00392F8C
		public bool InSheva
		{
			get
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				return ((currentZone != null) ? currentZone.ZoneWorld : null) == "NorthSheva";
			}
		}

		// Token: 0x06009E46 RID: 40518 RVA: 0x00394DAF File Offset: 0x00392FAF
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009E47 RID: 40519 RVA: 0x00394DDD File Offset: 0x00392FDD
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009E48 RID: 40520 RVA: 0x00394DF8 File Offset: 0x00392FF8
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				if (this.InSheva && this.TryDescend(E.Actor, false))
				{
					return false;
				}
				if (!this.InSheva && this.TryAscend(E.Actor, false))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E49 RID: 40521 RVA: 0x00394E4C File Offset: 0x0039304C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.InSheva)
			{
				E.AddAction("Descend", "descend", "DescendCable", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			}
			else
			{
				E.AddAction("Ascend", "ascend", "AscendCable", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E4A RID: 40522 RVA: 0x00394EB0 File Offset: 0x003930B0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "AscendCable" && this.TryAscend(E.Actor, true))
			{
				E.RequestInterfaceExit();
			}
			else if (E.Command == "DescendCable" && this.TryDescend(E.Actor, true))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009E4B RID: 40523 RVA: 0x00394F14 File Offset: 0x00393114
		public bool TryAscend(GameObject Actor, bool FromDialog = false)
		{
			if (!Actor.IsPlayer())
			{
				return false;
			}
			Interior part = Actor.GetPart<Interior>();
			if (!Actor.HasTag("Golem") || Actor.IsTemporary || !Actor.HasPart<Vehicle>() || ((part != null) ? part.Zone : null) == null)
			{
				Popup.Show("You don't have the capacity to ascend " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (!The.Game.HasQuest("We Are Starfreight") || !The.Game.HasFinishedQuest("Reclamation"))
			{
				Popup.Show("You can't safely ascend " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " right now.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (!Popup.AskString("Are you sure you want to ascend the Spindle? You know of no means to descend again.\n\nType 'ASCEND' to confirm.", "", "Sounds/UI/ui_notification", null, "ASCEND", 6, 0, false, true, new bool?(false)).EqualsNoCase("ASCEND"))
			{
				return false;
			}
			if (!GenericQueryEvent.Check(this.ParentObject, "AscendQueryGolem", Actor, null, 0, true))
			{
				return false;
			}
			GenericNotifyEvent.Send(this.ParentObject, "AscendNotifyGolem", Actor, null, 0);
			return true;
		}

		// Token: 0x06009E4C RID: 40524 RVA: 0x00395074 File Offset: 0x00393274
		public bool TryDescend(GameObject Actor, bool FromDialog = false)
		{
			if (!Actor.IsPlayer())
			{
				return false;
			}
			Interior part = Actor.GetPart<Interior>();
			if (!Actor.HasTag("Golem") || Actor.IsTemporary || !Actor.HasPart<Vehicle>() || ((part != null) ? part.Zone : null) == null)
			{
				Popup.Show("You don't have the capacity to descend " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (!EndGame.IsArkOpened)
			{
				Popup.Show("You can't safely descend " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false) + " right now.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			if (!Popup.AskString("Are you sure you want to return to Qud? \n\nType 'RETURN' to confirm.", "", "Sounds/UI/ui_notification", null, "RETURN", 6, 0, false, true, new bool?(false)).EqualsNoCase("RETURN"))
			{
				return false;
			}
			The.Game.SetStringGameState("EndType", "Return");
			if (NephalProperties.IsFoiled("Ehalcodon"))
			{
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			EndGame.Start();
			return true;
		}

		// Token: 0x04004174 RID: 16756
		public const string ASCEND_COMMAND = "AscendCable";

		// Token: 0x04004175 RID: 16757
		public const string ASCEND_QUERY = "AscendQueryGolem";

		// Token: 0x04004176 RID: 16758
		public const string ASCEND_NOTIFY = "AscendNotifyGolem";

		// Token: 0x04004177 RID: 16759
		public const string DESCEND_COMMAND = "DescendCable";
	}
}
