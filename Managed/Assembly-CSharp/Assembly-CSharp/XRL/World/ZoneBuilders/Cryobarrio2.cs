using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A49 RID: 2633
	public class Cryobarrio2
	{
		// Token: 0x06006DDE RID: 28126 RVA: 0x002535C8 File Offset: 0x002517C8
		public static void MakeCryochamber(int x, int y, Zone Z, string Contents = null, string Plaque = null)
		{
			Z.ClearBox(new Box(x - 2, y - 2, x + 2, y + 2), null);
			Z.GetCell(x + 2, y - 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x + 2, y).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x + 2, y + 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 2, y - 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 2, y).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 2, y + 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 1, y + 2).AddObject("HGlassWall", null, null, null, null);
			Z.GetCell(x + 1, y + 2).AddObject("HGlassWall", null, null, null, null);
			if (y < 12 && Plaque != null)
			{
				if (Contents != null)
				{
					Z.GetCell(x, y + 2).AddObject(Plaque, null, null, null, null);
				}
			}
			else
			{
				Z.GetCell(x, y + 2).AddObject("HGlassWall", null, null, null, null);
			}
			Z.GetCell(x - 1, y - 2).AddObject("HGlassWall", null, null, null, null);
			Z.GetCell(x + 1, y - 2).AddObject("HGlassWall", null, null, null, null);
			if (y >= 12 && Plaque != null)
			{
				if (Contents != null)
				{
					Z.GetCell(x, y - 2).AddObject(Plaque, null, null, null, null);
				}
			}
			else
			{
				Z.GetCell(x, y - 2).AddObject("HGlassWall", null, null, null, null);
			}
			if (Contents == "*Destroyed")
			{
				Z.GetCell(x, y).AddObject("CryoGas", null, null, null, null);
			}
			else
			{
				Z.GetCell(x, y).AddObject("StableCryoGas", null, null, null, null);
			}
			if (!string.IsNullOrEmpty(Contents) && Contents != "*Destroyed")
			{
				GameObject gameObject = null;
				if (Contents == "*Clone")
				{
					if (The.Player != null)
					{
						gameObject = Cloning.GenerateClone(The.Player, null, null, true, false, false, "Cryotube", new Func<GameObject, GameObject>(ItemNaming.GetBaseVersion));
					}
				}
				else
				{
					try
					{
						gameObject = GameObject.Create(Contents);
					}
					catch (Exception ex)
					{
						MetricsManager.LogError(ex);
					}
				}
				if (gameObject != null)
				{
					gameObject.AddPart<NoDamageWhileFrozen>(new NoDamageWhileFrozen(), true, false);
					gameObject.AddPart<OmniphaseWhileFrozen>(new OmniphaseWhileFrozen(), true, false);
					gameObject.SetStringProperty("StartFrozen", "1", false);
					Z.GetCell(x, y).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					if (gameObject.Brain != null)
					{
						gameObject.MakeActive(false);
					}
				}
				else
				{
					MetricsManager.LogError("could not create contents for cryochamber: " + Contents);
				}
			}
			Z.FillHollowBox(new Box(x - 1, y - 1, x + 1, y + 1), "StableCryoGas");
			Z.GetCell(x - 2, y - 2).AddObject("CryochamberWallSE", null, null, null, null);
			Z.GetCell(x + 2, y - 2).AddObject("CryochamberWallSW", null, null, null, null);
			Z.GetCell(x - 2, y + 2).AddObject("CryochamberWallNE", null, null, null, null);
			Z.GetCell(x + 2, y + 2).AddObject("CryochamberWallNW", null, null, null, null);
		}

		// Token: 0x06006DDF RID: 28127 RVA: 0x0025391C File Offset: 0x00251B1C
		public bool BuildZone(Zone Z)
		{
			Z.Fill("Marble");
			Z.FillBox(new Box(0, 0, Z.Width - 1, Z.Height - 1), "Fulcrete", false);
			Z.ClearBox(new Box(10, 2, 70, 21), null);
			Z.FillHollowBox(new Box(9, 2, 71, 22), "Marble");
			List<Point> list = new List<Point>(Algorithms.RandomShuffle<Point>(new List<Point>
			{
				new Point(20, 6),
				new Point(40, 6),
				new Point(60, 6),
				new Point(20, 18),
				new Point(40, 18),
				new Point(60, 18)
			}, Stat.Rand));
			for (int i = 1; i < 7; i++)
			{
				string text = null;
				string text2 = null;
				if (i == 1 || i == 2)
				{
					text = "CryochamberPlaque";
				}
				if (i == 3)
				{
					text = "CryochamberPlaqueSpider";
				}
				if (i == 4)
				{
					text = "CryochamberPlaqueRhinox";
				}
				if (i == 5)
				{
					text = "CryochamberPlaqueSkybear";
				}
				if (i == 6)
				{
					text = "CryochamberPlaqueYempris";
				}
				if (i == 3)
				{
					text2 = "Phase Spider Coward";
				}
				if (i == 4)
				{
					text2 = "Rhinox";
				}
				if (i == 5)
				{
					text2 = "Skybear";
				}
				if (i == 6)
				{
					text2 = "Yempuris";
				}
				Cryobarrio2.MakeCryochamber(list[i - 1].X, list[i - 1].Y, Z, text2, text);
			}
			Z.ClearBox(new Box(2, 11, 9, 13), null);
			Z.ClearBox(new Box(5, 6, 9, 18), null);
			Z.GetCell(2, 12).AddObject("OpenShaft", null, null, null, null);
			Z.GetCell(2, 12).AddObject("Platform", null, null, null, null);
			GameObject gameObject = GameObjectFactory.Factory.CreateObject("ElevatorSwitch");
			ElevatorSwitch part = gameObject.GetPart<ElevatorSwitch>();
			part.TopLevel = Z.Z;
			part.FloorLevel = Z.Z + 1;
			Z.GetCell(2, 11).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			return true;
		}
	}
}
