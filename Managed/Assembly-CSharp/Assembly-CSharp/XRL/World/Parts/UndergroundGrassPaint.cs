using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001266 RID: 4710
	[Serializable]
	public class UndergroundGrassPaint : IPart
	{
		// Token: 0x0600BABD RID: 47805 RVA: 0x00442AE3 File Offset: 0x00440CE3
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BABE RID: 47806 RVA: 0x00442AE6 File Offset: 0x00440CE6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BABF RID: 47807 RVA: 0x00442AFC File Offset: 0x00440CFC
		public override bool HandleEvent(EnteredCellEvent E)
		{
			UndergroundGrassPaint.PaintCell(E.Cell);
			this.ParentObject.Obliterate(null, false, null);
			return false;
		}

		// Token: 0x0600BAC0 RID: 47808 RVA: 0x00442B1C File Offset: 0x00440D1C
		public static void PaintCell(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 3)
			{
				C.PaintColorString = "&g";
				C.PaintDetailColor = "w";
				C.PaintTile = "Creatures/sw_plant3.bmp";
			}
			else if (num <= 6)
			{
				C.PaintColorString = "&G";
				C.PaintDetailColor = "w";
				C.PaintTile = "Creatures/sw_plant3.bmp";
			}
			else if (num == 7)
			{
				C.PaintColorString = "&w";
				C.PaintDetailColor = "k";
				C.PaintTile = "Tiles/tile-dirt1.png";
			}
			else
			{
				C.PaintColorString = "&y";
				C.PaintDetailColor = "k";
				C.PaintTile = "Tiles/tile-dirt1.png";
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
