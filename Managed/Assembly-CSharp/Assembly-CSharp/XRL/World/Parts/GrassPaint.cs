using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001265 RID: 4709
	[Serializable]
	public class GrassPaint : IPart
	{
		// Token: 0x0600BAB8 RID: 47800 RVA: 0x004429CD File Offset: 0x00440BCD
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BAB9 RID: 47801 RVA: 0x004429D0 File Offset: 0x00440BD0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BABA RID: 47802 RVA: 0x004429E6 File Offset: 0x00440BE6
		public override bool HandleEvent(EnteredCellEvent E)
		{
			GrassPaint.PaintCell(E.Cell);
			this.ParentObject.Obliterate(null, false, null);
			return false;
		}

		// Token: 0x0600BABB RID: 47803 RVA: 0x00442A04 File Offset: 0x00440C04
		public static void PaintCell(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 3)
			{
				C.PaintColorString = "&g";
				C.PaintTile = DirtPicker.GetRandomGrassTile();
			}
			else if (num <= 6)
			{
				C.PaintColorString = "&G";
				C.PaintTile = DirtPicker.GetRandomGrassTile();
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
