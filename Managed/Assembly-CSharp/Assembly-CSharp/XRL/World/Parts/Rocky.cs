using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001261 RID: 4705
	[Serializable]
	public class Rocky : IPart
	{
		// Token: 0x0600BAA4 RID: 47780 RVA: 0x004424E1 File Offset: 0x004406E1
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BAA5 RID: 47781 RVA: 0x004424E4 File Offset: 0x004406E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BAA6 RID: 47782 RVA: 0x004424FC File Offset: 0x004406FC
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
							Rocky.Paint(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BAA7 RID: 47783 RVA: 0x00442568 File Offset: 0x00440768
		public static void Paint(Cell C)
		{
			if (string.IsNullOrEmpty(C.PaintTile))
			{
				C.PaintColorString = "&y";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
				int num = Stat.Random(1, 5);
				if (num == 1)
				{
					C.PaintRenderString = ".";
				}
				if (num == 2)
				{
					C.PaintRenderString = ",";
				}
				if (num == 3)
				{
					C.PaintRenderString = "`";
				}
				if (num == 4)
				{
					C.PaintRenderString = "'";
				}
			}
		}
	}
}
