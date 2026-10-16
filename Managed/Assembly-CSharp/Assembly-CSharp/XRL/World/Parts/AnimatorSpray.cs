using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E02 RID: 3586
	[Serializable]
	public class AnimatorSpray : IPart
	{
		// Token: 0x060090F7 RID: 37111 RVA: 0x003439DF File Offset: 0x00341BDF
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060090F8 RID: 37112 RVA: 0x003439E2 File Offset: 0x00341BE2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060090F9 RID: 37113 RVA: 0x003439F8 File Offset: 0x00341BF8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (E.Item.IsBroken() || E.Item.IsRusted())
				{
					if (E.Actor.IsPlayer())
					{
						Popup.ShowFail("The sprayer head won't move.", true, true, true);
					}
					return false;
				}
				if (E.Actor.IsPlayer() && !E.Item.Understood())
				{
					E.Item.MakeUnderstood(false);
					Popup.Show(E.Item.Itis + " " + E.Item.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				Cell cell = null;
				if (E.Actor != null && E.Actor.IsPlayer())
				{
					cell = E.Actor.Physics.PickDirection("Animate", null);
				}
				if (cell == null)
				{
					return false;
				}
				List<GameObject> list = new List<GameObject>();
				List<string> list2 = new List<string>();
				char c = 'a';
				List<char> list3 = new List<char>();
				foreach (GameObject gameObject in cell.Objects)
				{
					if (gameObject.HasTagOrProperty("Animatable"))
					{
						list.Add(gameObject);
						list2.Add(gameObject.DisplayNameOnlyStripped);
						list3.Add(c);
						c += '\u0001';
					}
				}
				if (list.Count == 0)
				{
					Popup.ShowFail("There's nothing viable to animate here.", true, true, true);
					return false;
				}
				int num = Popup.PickOption("", "Choose a piece of furniture or other viable object to animate.", "", "Sounds/UI/ui_notification", list2.ToArray(), list3.ToArray(), null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num < 0)
				{
					return false;
				}
				GameObject gameObject2 = list[num];
				if (gameObject2.HasPart<Brain>() && E.Actor.IsPlayer())
				{
					Popup.ShowFail("You can't animate an object that already has a brain.", true, true, true);
					return false;
				}
				gameObject2.PlayWorldSound("Sounds/Interact/sfx_interact_sentience_imbue", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				Popup.Show("You imbue " + gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " with life.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("You imbued " + gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + " with life. Why?", string.Concat(new string[]
				{
					"While traveling in ",
					Grammar.GetProsaicZoneName(The.Player.CurrentZone),
					", =name= performed a sacred ritual with ",
					gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					", imbuing ",
					gameObject2.them,
					" with life and arranging ",
					gameObject2.them,
					" ",
					HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".babeTrait.!random>", null),
					". Many of the local denizens declared it a miracle. Some weren't so sure."
				}), string.Concat(new string[]
				{
					"While traveling in ",
					Grammar.GetProsaicZoneName(The.Player.CurrentZone),
					", =name= performed a sacred ritual with ",
					gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					", imbuing ",
					gameObject2.them,
					" with life and arranging ",
					gameObject2.them,
					" ",
					HistoricStringExpander.ExpandString("<spice.elements." + The.Player.GetMythicDomain(5, null) + ".babeTrait.!random>", null),
					". Many of the local denizens declared it a miracle."
				}), null, "general", MuralCategory.CommitsFolly, MuralWeight.Medium, null, -1L, true);
				AnimateObject.Animate(gameObject2, E.Actor, this.ParentObject);
				CombatJuice.playPrefabAnimation(gameObject2, "Abilities/AbilityVFXAnimated", gameObject2.ID, string.Concat(new string[]
				{
					gameObject2.Render.Tile,
					";",
					gameObject2.Render.GetTileForegroundColor(),
					";",
					gameObject2.Render.getDetailColor().ToString()
				}), null, false);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}
	}
}
