using System;
using XRL.UI;
using XRL.World.ZoneParts;

namespace XRL.World.Parts
{
	// Token: 0x02001062 RID: 4194
	[Serializable]
	public class MapReveal : IPart
	{
		// Token: 0x0600A6EC RID: 42732 RVA: 0x003D0736 File Offset: 0x003CE936
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A6ED RID: 42733 RVA: 0x003D075C File Offset: 0x003CE95C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "ActivateMapReveal", null, 'r', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6EE RID: 42734 RVA: 0x003D0794 File Offset: 0x003CE994
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateMapReveal")
			{
				if (!E.Actor.IsPlayer())
				{
					return false;
				}
				if (!this.ParentObject.Owner.IsNullOrEmpty())
				{
					if (Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						this.ParentObject.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" not owned by you, and using ",
						this.ParentObject.them,
						" will consume ",
						this.ParentObject.them,
						". Are you sure you want to do so?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
				}
				else
				{
					GameObject inInventory = this.ParentObject.InInventory;
					if (!((inInventory != null) ? inInventory.Owner : null).IsNullOrEmpty() && Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						this.ParentObject.InInventory.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" not owned by you, and using ",
						this.ParentObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" will consume ",
						this.ParentObject.them,
						". Are you sure you want to do so?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
				}
				if (E.Item.IsTemporary || !IComponent<GameObject>.CheckRealityDistortionUsability(E.Actor, null, E.Actor, this.ParentObject, null, null, 0))
				{
					Popup.Show(this.ParentObject.Does("seem", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to be behaving as nothing more than an ordinary piece of paper.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					E.Actor.UseEnergy(1000, "Item Failure", null, null, false);
					E.RequestInterfaceExit();
					return true;
				}
				Popup.Show(this.ParentObject.Itis + " a map of your surroundings!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				int num = this.Duration.RollCached();
				AmbientOmniscience ambientOmniscience = E.Actor.CurrentZone.RequirePart<AmbientOmniscience>(false);
				ambientOmniscience.IsRealityDistortionBased = true;
				if (num > 0)
				{
					ambientOmniscience.Duration = num;
				}
				this.ParentObject.Destroy(null, false, false, null);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6EF RID: 42735 RVA: 0x003D0A40 File Offset: 0x003CEC40
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 10);
				E.Add("scholarship", 4);
				E.Add("time", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040044F7 RID: 17655
		public string Duration = "50";
	}
}
