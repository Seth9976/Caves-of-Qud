using System;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x020010A5 RID: 4261
	[Serializable]
	public class SummoningCurio : IPart
	{
		// Token: 0x0600A999 RID: 43417 RVA: 0x003DFFBE File Offset: 0x003DE1BE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A99A RID: 43418 RVA: 0x003DFFC1 File Offset: 0x003DE1C1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A99B RID: 43419 RVA: 0x003DFFE8 File Offset: 0x003DE1E8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivateSummoningCurio", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A99C RID: 43420 RVA: 0x003E0020 File Offset: 0x003DE220
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateSummoningCurio")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (E.Actor.OnWorldMap())
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show("You cannot do that on the world map.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					return false;
				}
				Cell cell = null;
				if (E.Actor != null && E.Actor.IsPlayer())
				{
					cell = E.Actor.Physics.PickDestinationCell(8, AllowVis.OnlyVisible, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Fabricate " + this.Display, false, null);
				}
				if (cell == null)
				{
					return true;
				}
				base.PlayWorldSound("sfx_interact_curio_creaturesummon_activate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
				GameObject gameObject = GameObject.Create(this.Creature);
				HeroMaker.MakeHero(gameObject, null, null, -1, "Hero");
				gameObject.RemovePart<GivesRep>();
				gameObject.FireEvent("CurioInit");
				gameObject.SetAlliedLeader<AllyCurio>(E.Actor, 0, false);
				gameObject.MakeActive(false);
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				cell.PlayWorldSound("sfx_creature_appear_summon", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				gameObject.PlayWorldSoundTag("AmbientIdleSound", null, null, 0.5f);
				this.ParentObject.Destroy(null, false, false, null);
				if (E.Actor != null && E.Actor.IsPlayer())
				{
					Popup.Show("You activate the curio and toss it on the ground. It erupts into a throng of tiny polygons, which amalgamate into a fully formed " + gameObject.DisplayName + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A99D RID: 43421 RVA: 0x003E01E0 File Offset: 0x003DE3E0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Display == null && this.Creature != null)
			{
				GameObject gameObject = GameObjectFactory.Factory.CreateSampleObject(this.Creature, null);
				this.Display = ColorUtility.StripFormatting(gameObject.Render.DisplayName);
				gameObject.Obliterate(null, false, null);
			}
			if (this.Display != null)
			{
				E.Postfix.AppendRules("Fabricates one " + this.Display + ", cognitively altered to like you.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040045F0 RID: 17904
		public string Display;

		// Token: 0x040045F1 RID: 17905
		public string Creature;

		// Token: 0x040045F2 RID: 17906
		public string Template;
	}
}
