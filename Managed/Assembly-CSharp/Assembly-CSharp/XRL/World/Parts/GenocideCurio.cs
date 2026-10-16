using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200103D RID: 4157
	[Serializable]
	public class GenocideCurio : IPart
	{
		// Token: 0x0600A589 RID: 42377 RVA: 0x003C8AAE File Offset: 0x003C6CAE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A58A RID: 42378 RVA: 0x003C8AB1 File Offset: 0x003C6CB1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A58B RID: 42379 RVA: 0x003C8AD7 File Offset: 0x003C6CD7
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("On activation, this item obliterates all nearby members of a faction chosen from among those that existed during the time of the sultanate.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A58C RID: 42380 RVA: 0x003C8AF4 File Offset: 0x003C6CF4
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivateGenocideCurio", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A58D RID: 42381 RVA: 0x003C8B2C File Offset: 0x003C6D2C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateGenocideCurio")
			{
				List<Faction> list = new List<Faction>();
				foreach (Faction faction3 in Factions.Loop())
				{
					if (faction3.Visible && faction3.Old)
					{
						list.Add(faction3);
					}
				}
				list.Sort((Faction a, Faction b) => a.DisplayName.CompareTo(b.DisplayName));
				List<string> list2 = new List<string>(list.Count);
				foreach (Faction faction2 in list)
				{
					list2.Add(faction2.DisplayName);
				}
				if (E.Actor != null && E.Actor.IsPlayer())
				{
					int num = Popup.PickOption("Obliterate all nearby members of what faction?", null, "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
					if (num != -1)
					{
						string faction = list[num].Name;
						base.PlayWorldSound("Sounds/Interact/sfx_interact_curio_creaturekill_activate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						if (E.Actor != null && E.Actor.IsPlayer())
						{
							Popup.Show("You activate " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " and toss it into the air.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						foreach (GameObject gameObject in E.Actor.CurrentZone.GetObjects((GameObject o) => !o.IsPlayer() && o.BelongsToFaction(faction)))
						{
							gameObject.Sparksplatter();
							gameObject.Die(E.Actor, "critical existence failure", "You experienced a lack of existential support.", gameObject.It + " @@experienced a lack of existential support.", false, null, null, false, false, null, null, null);
						}
						this.ParentObject.Destroy(null, false, false, null);
						E.RequestInterfaceExit();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004467 RID: 17511
		public string Display;

		// Token: 0x04004468 RID: 17512
		public string Creature;

		// Token: 0x04004469 RID: 17513
		public string Template;
	}
}
