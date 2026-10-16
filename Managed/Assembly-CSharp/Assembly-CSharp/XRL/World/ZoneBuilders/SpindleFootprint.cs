using System;
using Genkit;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000B04 RID: 2820
	public class SpindleFootprint : ZoneBuilderSandbox
	{
		// Token: 0x060070A2 RID: 28834 RVA: 0x002884C0 File Offset: 0x002866C0
		public bool BuildZone(Zone Z, SpindleFootprint.FootprintMode mode = SpindleFootprint.FootprintMode.Spindle)
		{
			new FastNoise(1337);
			if (mode == SpindleFootprint.FootprintMode.Spindle)
			{
				this.wallMaterial = "Burnished Azzurum";
			}
			if (mode == SpindleFootprint.FootprintMode.Foundation)
			{
				this.wallMaterial = "EbonFulcrete";
			}
			Action<Cell> action = delegate(Cell c)
			{
				c.AddObject("EnsureVoidBlocker", null, null, null, null);
			};
			Action<Location2D> <>9__2;
			Action<Rect2D, string> action2 = delegate(Rect2D r, string d)
			{
				if (mode == SpindleFootprint.FootprintMode.Foundation)
				{
					return;
				}
				Action<Location2D> action3;
				if ((action3 = <>9__2) == null)
				{
					action3 = (<>9__2 = delegate(Location2D l)
					{
						Z.GetCell(l).ClearWalls();
					});
				}
				r.ForEachLocation(action3);
				if (d == "N" || d == "S")
				{
					for (int i = r.x1; i <= r.x2; i++)
					{
						Z.GetCell(i, (d == "N") ? r.y1 : r.y2).AddObject("SpindleGlassWall", null, null, null, null);
						Z.GetCell(i, (d == "N") ? r.y2 : r.y1).AddObject("SpindleLight", null, null, null, null);
					}
					return;
				}
				if (d == "E" || d == "W")
				{
					for (int j = r.y1; j <= r.y2; j++)
					{
						Z.GetCell((d == "W") ? r.x1 : r.x2, j).AddObject("SpindleLight", null, null, null, null);
						Z.GetCell((d == "E") ? r.x2 : r.x1, j).AddObject("SpindleGlassWall", null, null, null, null);
					}
				}
			};
			if (Z.X == 0 && Z.Y == 0)
			{
				Z.ClearBoxWith(new Rect2D(76, 22, 79, 24), this.wallMaterial, action);
				Z.GetCell(76, 22).ClearWalls();
			}
			if (Z.X == 1 && Z.Y == 0)
			{
				if (Z.Z != 9)
				{
					Z.ClearBoxWith(new Rect2D(26, 21, 53, 21), this.wallMaterial, action);
				}
				Z.ClearBoxWith(new Rect2D(0, 22, 79, 24), this.wallMaterial, action);
				action2(new Rect2D(73, 22, 74, 24), "N");
				action2(new Rect2D(7, 22, 8, 24), "N");
				action2(new Rect2D(17, 22, 18, 24), "N");
				if (Z.Z != 9)
				{
					action2(new Rect2D(27, 21, 28, 24), "N");
				}
				if (Z.Z != 9)
				{
					action2(new Rect2D(51, 21, 52, 24), "N");
				}
				action2(new Rect2D(63, 22, 64, 24), "N");
			}
			if (Z.X == 2 && Z.Y == 0)
			{
				Z.ClearBoxWith(new Rect2D(0, 22, 3, 24), this.wallMaterial, action);
				Z.GetCell(3, 22).ClearWalls();
			}
			if (Z.X == 0 && Z.Y == 1)
			{
				Z.ClearBoxWith(new Rect2D(76, 0, 79, 24), this.wallMaterial, action);
				Z.ClearBoxWith(new Rect2D(75, 7, 75, 18), this.wallMaterial, action);
				action2(new Rect2D(76, 3, 79, 4), "W");
				action2(new Rect2D(75, 9, 79, 10), "W");
				action2(new Rect2D(75, 15, 79, 16), "W");
				action2(new Rect2D(76, 20, 79, 21), "W");
			}
			if (Z.X == 2 && Z.Y == 1)
			{
				Z.ClearBoxWith(new Rect2D(0, 0, 3, 24), this.wallMaterial, action);
				Z.ClearBoxWith(new Rect2D(4, 7, 4, 18), this.wallMaterial, action);
				action2(new Rect2D(0, 3, 3, 4), "E");
				action2(new Rect2D(0, 9, 4, 10), "E");
				action2(new Rect2D(0, 15, 4, 16), "E");
				action2(new Rect2D(0, 20, 3, 21), "E");
			}
			if (Z.X == 0 && Z.Y == 2)
			{
				Z.ClearBoxWith(new Rect2D(76, 0, 79, 2), this.wallMaterial, action);
				Z.GetCell(76, 2).ClearWalls();
			}
			if (Z.X == 1 && Z.Y == 2)
			{
				if (Z.Z != 9)
				{
					Z.ClearBoxWith(new Rect2D(26, 3, 53, 3), this.wallMaterial, action);
				}
				Z.ClearBoxWith(new Rect2D(0, 0, 79, 2), this.wallMaterial, action);
				action2(new Rect2D(73, 0, 74, 1), "S");
				action2(new Rect2D(7, 0, 8, 1), "S");
				action2(new Rect2D(17, 0, 18, 3), "S");
				if (Z.Z != 9)
				{
					action2(new Rect2D(27, 0, 28, 3), "S");
				}
				if (Z.Z != 9)
				{
					action2(new Rect2D(51, 0, 52, 3), "S");
				}
				action2(new Rect2D(63, 0, 64, 3), "S");
			}
			if (Z.X == 2 && Z.Y == 2)
			{
				Z.ClearBoxWith(new Rect2D(0, 0, 3, 2), this.wallMaterial, action);
				Z.GetCell(3, 2).ClearWalls();
			}
			return true;
		}

		// Token: 0x0400341C RID: 13340
		public string wallMaterial = "EbonFulcrete";

		// Token: 0x02001B40 RID: 6976
		public enum FootprintMode
		{
			// Token: 0x04006C7F RID: 27775
			Spindle,
			// Token: 0x04006C80 RID: 27776
			Foundation
		}
	}
}
