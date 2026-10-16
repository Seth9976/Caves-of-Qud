using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200125F RID: 4703
	[Serializable]
	public class PaleDirty : IPart
	{
		// Token: 0x0600BA9A RID: 47770 RVA: 0x0044228F File Offset: 0x0044048F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA9B RID: 47771 RVA: 0x00442292 File Offset: 0x00440492
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA9C RID: 47772 RVA: 0x004422A8 File Offset: 0x004404A8
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
							PaleDirty.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA9D RID: 47773 RVA: 0x00442314 File Offset: 0x00440514
		public static void PaintCell(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 5)
			{
				C.PaintColorString = "&y";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
			}
			else if (num == 6)
			{
				C.PaintColorString = "&y";
				C.PaintTile = DirtPicker.GetRandomGrassTile();
			}
			else if (num == 7)
			{
				C.PaintColorString = "&Y";
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
