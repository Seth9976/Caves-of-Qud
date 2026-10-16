using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001264 RID: 4708
	[Serializable]
	public class UndergroundGrassy : IPart
	{
		// Token: 0x0600BAB3 RID: 47795 RVA: 0x00442853 File Offset: 0x00440A53
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BAB4 RID: 47796 RVA: 0x00442856 File Offset: 0x00440A56
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BAB5 RID: 47797 RVA: 0x0044286C File Offset: 0x00440A6C
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
							UndergroundGrassy.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAB6 RID: 47798 RVA: 0x004428D8 File Offset: 0x00440AD8
		public static void PaintCell(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 2)
			{
				C.PaintColorString = "&g";
				C.PaintDetailColor = "w";
				C.PaintTile = "Creatures/sw_plant3.bmp";
			}
			else if (num <= 4)
			{
				C.PaintColorString = "&G";
				C.PaintDetailColor = "w";
				C.PaintTile = "Creatures/sw_plant3.bmp";
			}
			else if (num <= 6)
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
