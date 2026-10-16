using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x0200125C RID: 4700
	[Serializable]
	public class ConcreteFloor : IPart
	{
		// Token: 0x0600BA8B RID: 47755 RVA: 0x00441F69 File Offset: 0x00440169
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA8C RID: 47756 RVA: 0x00441F6C File Offset: 0x0044016C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BA8D RID: 47757 RVA: 0x00441F84 File Offset: 0x00440184
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
							ConcreteFloor.PaintCell(parentZone.GetCell(j, i));
						}
					}
				}
			}
			catch
			{
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA8E RID: 47758 RVA: 0x00441FF0 File Offset: 0x004401F0
		public static void PaintCell(Cell C)
		{
			if (string.IsNullOrEmpty(C.PaintTile))
			{
				C.PaintColorString = "&y";
				C.PaintTile = "Tiles/tile-dirt1.png";
				C.PaintDetailColor = "k";
				C.PaintColorString = "&y";
				C.PaintRenderString = "ú";
			}
		}
	}
}
