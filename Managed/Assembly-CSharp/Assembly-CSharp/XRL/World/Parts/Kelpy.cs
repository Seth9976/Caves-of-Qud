using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001256 RID: 4694
	[Serializable]
	public class Kelpy : IPart
	{
		// Token: 0x0600BA67 RID: 47719 RVA: 0x004414EE File Offset: 0x0043F6EE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA68 RID: 47720 RVA: 0x004414F1 File Offset: 0x0043F6F1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA69 RID: 47721 RVA: 0x00441508 File Offset: 0x0043F708
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
							Kelpy.Paint(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA6A RID: 47722 RVA: 0x00441574 File Offset: 0x0043F774
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
				C.PaintColorString = "&w";
			}
			else if (num <= 4)
			{
				C.PaintColorString = "&W";
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
