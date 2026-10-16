using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001263 RID: 4707
	[Serializable]
	public class Grassy : IPart
	{
		// Token: 0x0600BAAE RID: 47790 RVA: 0x004426EE File Offset: 0x004408EE
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BAAF RID: 47791 RVA: 0x004426F1 File Offset: 0x004408F1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BAB0 RID: 47792 RVA: 0x00442708 File Offset: 0x00440908
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
							Grassy.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAB1 RID: 47793 RVA: 0x00442774 File Offset: 0x00440974
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
