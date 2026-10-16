using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A48 RID: 2632
	public class Cryobarrio1
	{
		// Token: 0x06006DDA RID: 28122 RVA: 0x00253034 File Offset: 0x00251234
		public static void Websplat(int x, int y, Zone Z, string Object)
		{
			for (double num = (double)Stat.Random(1, 30); num < 360.0; num += (double)Stat.Random(30, 60))
			{
				int num2 = Stat.Random(3, 9);
				for (int i = 0; i < num2; i++)
				{
					int num3 = x + (int)((float)Math.Sin(num.toRadians()) * (float)i);
					int num4 = y + (int)((float)Math.Cos(num.toRadians()) * (float)i);
					Cell cell = Z.GetCell(num3, num4);
					if (cell != null && !cell.HasObjectWithBlueprint(Object))
					{
						cell.AddObject(Object, null, null, null, null);
					}
				}
			}
		}

		// Token: 0x06006DDB RID: 28123 RVA: 0x002530C8 File Offset: 0x002512C8
		public void MakeCryochamber(int x, int y, Zone Z, int n)
		{
			Z.ClearBox(new Box(x - 2, y - 2, x + 2, y + 2), null);
			Z.GetCell(x + 2, y - 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x + 2, y).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x + 2, y + 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 2, y - 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 2, y).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 2, y + 1).AddObject("VGlassWall", null, null, null, null);
			Z.GetCell(x - 1, y + 2).AddObject("HGlassWall", null, null, null, null);
			if (n == 2)
			{
				Z.GetCell(x, y + 2).AddObject("CryochamberPlaqueSpider", null, null, null, null);
			}
			else if (n <= 3)
			{
				Z.GetCell(x, y + 2).AddObject("CryochamberPlaque", null, null, null, null);
			}
			else
			{
				Z.GetCell(x, y + 2).AddObject("HGlassWall", null, null, null, null);
			}
			Z.GetCell(x + 1, y + 2).AddObject("HGlassWall", null, null, null, null);
			Z.GetCell(x - 1, y - 2).AddObject("HGlassWall", null, null, null, null);
			if (n > 3)
			{
				Z.GetCell(x, y - 2).AddObject("CryochamberPlaque", null, null, null, null);
			}
			else
			{
				Z.GetCell(x, y - 2).AddObject("HGlassWall", null, null, null, null);
			}
			Z.GetCell(x + 1, y - 2).AddObject("HGlassWall", null, null, null, null);
			if (n != 2 && !50.in100())
			{
				Z.FillHollowBox(new Box(x - 1, y - 1, x + 1, y + 1), "StableCryoGas");
				Z.GetCell(x - 2, y - 2).AddObject("CryochamberWallSE", null, null, null, null);
				Z.GetCell(x + 2, y - 2).AddObject("CryochamberWallSW", null, null, null, null);
				Z.GetCell(x - 2, y + 2).AddObject("CryochamberWallNE", null, null, null, null);
				Z.GetCell(x + 2, y + 2).AddObject("CryochamberWallNW", null, null, null, null);
				return;
			}
			Physics.LegacyApplyExplosion(Z.GetCell(x, y), new List<Cell>(), new List<GameObject>(), 35000, true, false, null, null, 1, 0.05f);
			if (50.in100())
			{
				for (int i = x - 3; i <= x + 3; i++)
				{
					for (int j = y - 3; j <= y + 3; j++)
					{
						if (Stat.Random(1, 100) <= 60)
						{
							Z.GetCell(i, j).AddObject("ConvalessencePuddle", null, null, null, null);
						}
					}
				}
				Z.GetCell(x - 2, y - 2).AddObject("CryochamberWallBroken", null, null, null, null);
				Z.GetCell(x + 2, y - 2).AddObject("CryochamberWallBroken", null, null, null, null);
				Z.GetCell(x - 2, y + 2).AddObject("CryochamberWallBroken", null, null, null, null);
				Z.GetCell(x + 2, y + 2).AddObject("CryochamberWallBroken", null, null, null, null);
				return;
			}
			Z.GetCell(x - 2, y - 2).AddObject("CryochamberWallSE", null, null, null, null);
			Z.GetCell(x + 2, y - 2).AddObject("CryochamberWallSW", null, null, null, null);
			Z.GetCell(x - 2, y + 2).AddObject("CryochamberWallNE", null, null, null, null);
			Z.GetCell(x + 2, y + 2).AddObject("CryochamberWallNW", null, null, null, null);
		}

		// Token: 0x06006DDC RID: 28124 RVA: 0x00253474 File Offset: 0x00251674
		public bool BuildZone(Zone Z)
		{
			Z.Fill("Marble");
			Z.FillBox(new Box(0, 0, Z.Width - 1, Z.Height - 1), "Fulcrete", false);
			Z.ClearBox(new Box(10, 2, 70, 21), null);
			Z.FillHollowBox(new Box(9, 2, 71, 22), "Marble");
			this.MakeCryochamber(20, 6, Z, 1);
			this.MakeCryochamber(40, 6, Z, 2);
			this.MakeCryochamber(60, 6, Z, 3);
			this.MakeCryochamber(20, 18, Z, 4);
			this.MakeCryochamber(40, 18, Z, 5);
			this.MakeCryochamber(60, 18, Z, 6);
			Cryobarrio1.Websplat(65, 8, Z, "PhaseWeb");
			for (int i = 0; i < 3; i++)
			{
				Cryobarrio1.Websplat(Stat.Random(0, Z.Width - 1), Stat.Random(0, Z.Height - 1), Z, "PhaseWeb");
			}
			Z.FillRoundHollowBox(new Box(65, 8, 79, 16), "Marble");
			Z.GetCell(69, 12).AddObject("Phase Spider", null, null, null, null);
			Z.GetCell(65, 12).Clear(null, false, false, null);
			Z.GetCell(65, 12).AddObject("Fused Security Door", null, null, null, null);
			return true;
		}
	}
}
