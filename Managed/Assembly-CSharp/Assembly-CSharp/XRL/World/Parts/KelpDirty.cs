using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001258 RID: 4696
	[Serializable]
	public class KelpDirty : IPart
	{
		// Token: 0x0600BA71 RID: 47729 RVA: 0x004417AD File Offset: 0x0043F9AD
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA72 RID: 47730 RVA: 0x004417B0 File Offset: 0x0043F9B0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA73 RID: 47731 RVA: 0x004417C8 File Offset: 0x0043F9C8
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
							KelpDirty.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA74 RID: 47732 RVA: 0x00441834 File Offset: 0x0043FA34
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
				C.PaintColorString = "&w";
				C.PaintTile = DirtPicker.GetRandomGrassTile();
			}
			else if (num == 7)
			{
				C.PaintColorString = "&W";
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
