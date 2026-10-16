using System;
using Genkit;
using XRL.Core;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200109C RID: 4252
	[Serializable]
	public class SpiralBorerCurio : IPart
	{
		// Token: 0x0600A928 RID: 43304 RVA: 0x003DD711 File Offset: 0x003DB911
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A929 RID: 43305 RVA: 0x003DD714 File Offset: 0x003DB914
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A92A RID: 43306 RVA: 0x003DD734 File Offset: 0x003DB934
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Activate", "activate", "ActivateSpiralBorer", null, 'a', false, 10, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A92B RID: 43307 RVA: 0x003DD76C File Offset: 0x003DB96C
		public bool PlaceStair(Cell c, string stairObject)
		{
			if (c == null)
			{
				return false;
			}
			if (c.HasObjectWithTag("Pit"))
			{
				return false;
			}
			c.ClearWalls();
			c.RequireObject(stairObject, null, null, null, null);
			return true;
		}

		// Token: 0x0600A92C RID: 43308 RVA: 0x003DD798 File Offset: 0x003DB998
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateSpiralBorer")
			{
				if (E.Actor.OnWorldMap())
				{
					E.Actor.Fail("You cannot do that on the world map.");
					return false;
				}
				if (E.Actor.CurrentZone is InteriorZone)
				{
					E.Actor.Fail("You cannot do that inside.");
					return false;
				}
				GameObject parentObject = this.ParentObject;
				GameObject actor = E.Actor;
				if (!GenericQueryEvent.Check(parentObject, E.Command, actor, null, 0, true))
				{
					return false;
				}
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				Point2D pos2D = E.Actor.CurrentCell.Pos2D;
				Point2D point2D;
				if (pos2D.x > 0)
				{
					point2D = E.Actor.CurrentCell.Pos2D.FromDirection("W");
				}
				else
				{
					point2D = E.Actor.CurrentCell.Pos2D.FromDirection("E");
				}
				IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_drill_deep", 1f, false, SoundRequest.SoundEffectType.None);
				Popup.Show("The metal satchel opens, folds itself inside out, and transforms into a contraption studded with pinions and drills. It starts to burrow into the ground.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				int i = 0;
				int num = 20;
				while (i <= num)
				{
					string zoneIDFromDirection = E.Actor.CurrentZone.GetZoneIDFromDirection("D", i);
					if (The.ZoneManager.IsZoneBuilt(zoneIDFromDirection))
					{
						Zone zone = The.ZoneManager.GetZone(zoneIDFromDirection);
						if (i == 0)
						{
							this.PlaceStair(zone.GetCell(pos2D), "StairsDownUnconnected");
						}
						else if (i == num)
						{
							this.PlaceStair(zone.GetCell(point2D), "StairsUpUnconnected");
						}
						else if (i % 2 == 0)
						{
							this.PlaceStair(zone.GetCell(pos2D), "StairsDownUnconnected");
							this.PlaceStair(zone.GetCell(point2D), "StairsUpUnconnected");
						}
						else
						{
							this.PlaceStair(zone.GetCell(pos2D), "StairsUpUnconnected");
							this.PlaceStair(zone.GetCell(point2D), "StairsDownUnconnected");
						}
					}
					else
					{
						ZoneManager zoneManager = XRLCore.Core.Game.ZoneManager;
						if (i == 0)
						{
							zoneManager.AddZonePostBuilder(zoneIDFromDirection, "ClearWallAddObject", "x", pos2D.x.ToString(), "y", pos2D.y.ToString(), "obj", "StairsDownUnconnected");
						}
						else if (i == num)
						{
							zoneManager.AddZonePostBuilder(zoneIDFromDirection, "ClearWallAddObject", "x", point2D.x.ToString(), "y", point2D.y.ToString(), "obj", "StairsUpUnconnected");
						}
						else if (i % 2 == 0)
						{
							zoneManager.AddZonePostBuilder(zoneIDFromDirection, "ClearWallAddObject", "x", pos2D.x.ToString(), "y", pos2D.y.ToString(), "obj", "StairsDownUnconnected");
							zoneManager.AddZonePostBuilder(zoneIDFromDirection, "ClearWallAddObject", "x", point2D.x.ToString(), "y", point2D.y.ToString(), "obj", "StairsUpUnconnected");
						}
						else
						{
							zoneManager.AddZonePostBuilder(zoneIDFromDirection, "ClearWallAddObject", "x", pos2D.x.ToString(), "y", pos2D.y.ToString(), "obj", "StairsUpUnconnected");
							zoneManager.AddZonePostBuilder(zoneIDFromDirection, "ClearWallAddObject", "x", point2D.x.ToString(), "y", point2D.y.ToString(), "obj", "StairsDownUnconnected");
						}
					}
					i++;
				}
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.HandleEvent(E);
		}
	}
}
