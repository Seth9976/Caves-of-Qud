using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001028 RID: 4136
	[Serializable]
	public class FactionDeed : IPart
	{
		// Token: 0x0600A4C1 RID: 42177 RVA: 0x003C3C1E File Offset: 0x003C1E1E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A4C2 RID: 42178 RVA: 0x003C3C21 File Offset: 0x003C1E21
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A4C3 RID: 42179 RVA: 0x003C3C58 File Offset: 0x003C1E58
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.Faction.IsNullOrEmpty())
			{
				E.AddTag("{{y|[" + Factions.Get(this.Faction).DisplayName + " chapter]}}", -60);
			}
			else
			{
				E.AddTag("{{y|[chapter unspecified]}}", -60);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4C4 RID: 42180 RVA: 0x003C3CB0 File Offset: 0x003C1EB0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "entangle text", "ActivateFactionDeed", null, 'n', false, 15, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4C5 RID: 42181 RVA: 0x003C3CE8 File Offset: 0x003C1EE8
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("time", 10);
				if (this.Faction.IsNullOrEmpty())
				{
					E.Add("chance", 5);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4C6 RID: 42182 RVA: 0x003C3D28 File Offset: 0x003C1F28
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateFactionDeed")
			{
				if (!E.Actor.IsPlayer())
				{
					return false;
				}
				if (!this.ParentObject.Owner.IsNullOrEmpty())
				{
					if (Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						this.ParentObject.Does("are", 2147483647, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
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
						this.ParentObject.InInventory.Does("are", 2147483647, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
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
					Popup.ShowFail(this.ParentObject.Does("seem", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to be behaving as nothing more than an ordinary piece of paper.", true, true, true);
					E.Actor.UseEnergy(1000, "Item Failure", null, null, false);
					E.RequestInterfaceExit();
					return false;
				}
				string text = this.Faction;
				if (text.IsNullOrEmpty())
				{
					List<Faction> list = new List<Faction>();
					foreach (Faction faction in Factions.Loop())
					{
						if (faction.Visible)
						{
							list.Add(faction);
						}
					}
					list.Sort((Faction a, Faction b) => a.DisplayName.CompareTo(b.DisplayName));
					List<string> list2 = new List<string>(list.Count);
					foreach (Faction faction2 in list)
					{
						list2.Add(faction2.DisplayName);
					}
					int num = Popup.PickOption("Choose a faction's chapter to insert your good deed.", null, "", "Sounds/UI/ui_notification", list2.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
					if (num == -1)
					{
						return false;
					}
					text = list[num].Name;
				}
				Faction ifExists = Factions.GetIfExists(text);
				int num2 = this.Amount.RollCached();
				if (Options.SifrahRealityDistortion && E.Actor.IsPlayer())
				{
					int num3 = 3;
					if (ifExists != null)
					{
						num3 += ifExists.HistoricalSignificance;
					}
					if (num3 < 3)
					{
						num3 = 3;
					}
					RealityDistortionSifrah realityDistortionSifrah = new RealityDistortionSifrah(this.ParentObject, "FactionDeed", "inserting a deed into history", E.Actor.Stat("Ego", 0), num3);
					realityDistortionSifrah.Play(this.ParentObject);
					if (realityDistortionSifrah.Abort || realityDistortionSifrah.InterfaceExitRequested)
					{
						return false;
					}
					num2 = num2 * realityDistortionSifrah.Performance / 100;
					if (num2 == 0)
					{
						Popup.ShowFail("The operation fails.", true, true, true);
						this.ParentObject.Destroy(null, false, false, null);
						return false;
					}
				}
				int num4 = GivesRep.VaryRep(num2);
				int num5 = Stat.Random(0, 438000);
				base.PlayWorldSound("Sounds/Interact/sfx_interact_quantumHistoryPage_activate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				Popup.Show(string.Concat(new string[]
				{
					"You add the following entry into the {{K|Annals of Qud}}.\n\n\"On the ",
					Calendar.GetDay(num5),
					" of ",
					Calendar.GetMonth(num5),
					", {{Y|",
					IComponent<GameObject>.ThePlayer.BaseDisplayName,
					"}} became ",
					(num4 >= 0) ? "admired" : "despised",
					" by ",
					XRL.World.Faction.GetFormattedName(text),
					" for ",
					(num4 >= 0) ? GenerateFriendOrFoe.getLikeReason() : GenerateFriendOrFoe.getHateReason(),
					".\""
				}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				The.Game.PlayerReputation.Modify(text, num4, "FactionDeed", null, null, false, false);
				if (text.StartsWith("villagers of ") || text.Equals("Hindren"))
				{
					string vName;
					if (text.Equals("Hindren"))
					{
						vName = "Bey Lah";
					}
					else
					{
						vName = text.Substring(13);
					}
					List<JournalMapNote> mapNotes = JournalAPI.GetMapNotes((JournalMapNote note) => note.Text == vName);
					if (mapNotes.Count > 0)
					{
						mapNotes.GetRandomElement<JournalMapNote>(null).Reveal(null, false);
					}
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A4C7 RID: 42183 RVA: 0x003C42DC File Offset: 0x003C24DC
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Faction == "[random]")
			{
				List<Faction> list = new List<Faction>(64);
				List<string> list2 = new List<string>();
				foreach (Faction faction in Factions.Loop())
				{
					if (faction.Visible)
					{
						list2.Add(faction.DisplayName);
						list.Add(faction);
					}
				}
				this.Faction = list.GetRandomElement<Faction>(null).Name;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004437 RID: 17463
		public string Faction = "";

		// Token: 0x04004438 RID: 17464
		public string Amount = "200";
	}
}
