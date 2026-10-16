using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001255 RID: 4693
	[Serializable]
	public class Mushroomy : IPart
	{
		// Token: 0x0600BA62 RID: 47714 RVA: 0x0044139E File Offset: 0x0043F59E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA63 RID: 47715 RVA: 0x004413A1 File Offset: 0x0043F5A1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA64 RID: 47716 RVA: 0x004413B8 File Offset: 0x0043F5B8
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
							Mushroomy.Paint(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA65 RID: 47717 RVA: 0x00441424 File Offset: 0x0043F624
		public static void Paint(Cell C)
		{
			int num = Stat.Random(1, 8);
			if (num <= 4)
			{
				C.PaintTile = DirtPicker.GetRandomGrassTile();
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
				C.PaintColorString = "&c";
			}
			else if (num <= 4)
			{
				C.PaintColorString = "&C";
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
