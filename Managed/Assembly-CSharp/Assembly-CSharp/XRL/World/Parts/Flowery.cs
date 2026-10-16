using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001253 RID: 4691
	[Serializable]
	public class Flowery : IPart
	{
		// Token: 0x0600BA58 RID: 47704 RVA: 0x004410BC File Offset: 0x0043F2BC
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA59 RID: 47705 RVA: 0x004410BF File Offset: 0x0043F2BF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA5A RID: 47706 RVA: 0x004410D8 File Offset: 0x0043F2D8
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
							Flowery.Paint(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA5B RID: 47707 RVA: 0x00441144 File Offset: 0x0043F344
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
