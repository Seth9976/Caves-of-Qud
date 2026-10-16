using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200125E RID: 4702
	[Serializable]
	public class Dirty : IPart
	{
		// Token: 0x0600BA95 RID: 47765 RVA: 0x00442129 File Offset: 0x00440329
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA96 RID: 47766 RVA: 0x0044212C File Offset: 0x0044032C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA97 RID: 47767 RVA: 0x00442144 File Offset: 0x00440344
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
							Dirty.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA98 RID: 47768 RVA: 0x004421B0 File Offset: 0x004403B0
		public static void PaintCell(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 5)
			{
				C.PaintColorString = "&w";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
			else if (num == 6)
			{
				C.PaintColorString = "&g";
				C.PaintTile = DirtPicker.GetRandomGrassTile();
			}
			else if (num == 7)
			{
				C.PaintColorString = "&G";
				C.PaintTile = DirtPicker.GetRandomGrassTile();
			}
			else
			{
				C.PaintColorString = "&y";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
			int num2 = Stat.Random(1, 5);
			if (num2 == 1)
			{
				C.PaintRenderString = ".";
			}
			if (num2 == 2)
			{
				C.PaintRenderString = ",";
			}
			if (num2 == 3)
			{
				C.PaintRenderString = "`";
			}
			if (num2 == 4)
			{
				C.PaintRenderString = "'";
			}
		}
	}
}
