using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001257 RID: 4695
	[Serializable]
	public class ExtradimensionallyKelpy : IPart
	{
		// Token: 0x0600BA6C RID: 47724 RVA: 0x0044163E File Offset: 0x0043F83E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA6D RID: 47725 RVA: 0x00441641 File Offset: 0x0043F841
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA6E RID: 47726 RVA: 0x00441658 File Offset: 0x0043F858
		public override bool HandleEvent(EnteredCellEvent E)
		{
			try
			{
				if (!Options.DisableFloorTextureObjects)
				{
					Zone parentZone = E.Cell.ParentZone;
					for (int i = 0; i < parentZone.Height; i++)
					{
						for (int j = 0; j < parentZone.Width; j++)
						{
							ExtradimensionallyKelpy.Paint(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA6F RID: 47727 RVA: 0x004416C4 File Offset: 0x0043F8C4
		public static void Paint(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 4)
			{
				C.PaintTile = DirtPicker.GetRandomGrassTile();
				if (num <= 2 && Stat.Random(1, 100) <= 8)
				{
					C.AddObject("ClamGrass", null, null, null, null);
				}
			}
			else if (num <= 5)
			{
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
			else
			{
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
			num = Stat.Random(1, 4);
			if (num <= 2)
			{
				C.PaintColorString = "&o";
			}
			else if (num <= 4)
			{
				C.PaintColorString = "&O";
			}
			int num2 = Stat.Random(1, 5);
			if (num2 == 1)
			{
				C.PaintRenderString = ",";
			}
			if (num2 == 2)
			{
				C.PaintRenderString = ".";
			}
			if (num2 == 3)
			{
				C.PaintRenderString = "ù";
			}
			if (num2 == 4)
			{
				C.PaintRenderString = "ú";
			}
		}
	}
}
