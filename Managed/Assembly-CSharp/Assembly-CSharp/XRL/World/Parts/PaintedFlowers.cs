using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001254 RID: 4692
	[Serializable]
	public class PaintedFlowers : IPart
	{
		// Token: 0x0600BA5D RID: 47709 RVA: 0x00441256 File Offset: 0x0043F456
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA5E RID: 47710 RVA: 0x00441259 File Offset: 0x0043F459
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA5F RID: 47711 RVA: 0x0044126F File Offset: 0x0043F46F
		public override bool HandleEvent(EnteredCellEvent E)
		{
			PaintedFlowers.Paint(E.Cell);
			this.ParentObject.Obliterate(null, false, null);
			return false;
		}

		// Token: 0x0600BA60 RID: 47712 RVA: 0x0044128C File Offset: 0x0043F48C
		public static void Paint(Cell C)
		{
			C.PaintTile = "terrain/tile_flowers" + Stat.Random(1, 2).ToString() + ".bmp";
			int num = Stat.Random(1, 7);
			if (num == 1)
			{
				C.PaintColorString = "&R";
			}
			else if (num == 2)
			{
				C.PaintColorString = "&M";
			}
			else if (num == 3)
			{
				C.PaintColorString = "&B";
			}
			else if (num == 4)
			{
				C.PaintColorString = "&C";
			}
			else if (num == 5)
			{
				C.PaintColorString = "&Y";
			}
			else if (num == 6)
			{
				C.PaintColorString = "&G";
			}
			else if (num == 7)
			{
				C.PaintColorString = "&W";
			}
			if (Stat.Random(0, 1) == 0)
			{
				C.PaintColorString = C.PaintColorString.ToLower();
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
