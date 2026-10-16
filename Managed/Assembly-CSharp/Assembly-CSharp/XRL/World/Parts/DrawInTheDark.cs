using System;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020010C0 RID: 4288
	[Serializable]
	public class DrawInTheDark : IPart
	{
		// Token: 0x0600AABE RID: 43710 RVA: 0x003E7194 File Offset: 0x003E5394
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.Physics.CurrentCell != null && this.ParentObject.Physics.currentCell.IsExplored() && !this.ParentObject.Physics.currentCell.IsVisible())
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600AABF RID: 43711 RVA: 0x003E71EC File Offset: 0x003E53EC
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (this.ParentObject.Physics.currentCell != null && this.ParentObject.Physics.currentCell.IsExplored() && !this.ParentObject.Physics.currentCell.IsVisible())
			{
				int x = this.ParentObject.Physics.currentCell.X;
				int y = this.ParentObject.Physics.currentCell.Y;
				if (Options.UseTiles && !string.IsNullOrEmpty(this.ParentObject.Render.Tile))
				{
					if (!string.IsNullOrEmpty(this.BackgroundTileColor))
					{
						buffer.Buffer[x, y].SetBackground(this.BackgroundTileColor[0]);
					}
					else if (!string.IsNullOrEmpty(this.BackgroundColor))
					{
						buffer.Buffer[x, y].SetBackground(this.BackgroundColor[0]);
					}
					if (!string.IsNullOrEmpty(this.ForegroundTileColor))
					{
						buffer.Buffer[x, y].SetForeground(this.ForegroundTileColor[0]);
					}
					else if (!string.IsNullOrEmpty(this.ForegroundColor))
					{
						buffer.Buffer[x, y].SetForeground(this.ForegroundColor[0]);
					}
					if (!string.IsNullOrEmpty(this.DetailColor))
					{
						buffer.Buffer[x, y].Detail = ColorUtility.ColorMap[this.DetailColor[0]];
						return;
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(this.BackgroundColor))
					{
						buffer.Buffer[x, y].SetBackground(this.BackgroundColor[0]);
					}
					if (!string.IsNullOrEmpty(this.ForegroundColor))
					{
						buffer.Buffer[x, y].SetForeground(this.ForegroundColor[0]);
					}
				}
			}
		}

		// Token: 0x04004660 RID: 18016
		public string ForegroundTileColor = "";

		// Token: 0x04004661 RID: 18017
		public string BackgroundTileColor = "";

		// Token: 0x04004662 RID: 18018
		public string DetailColor = "";

		// Token: 0x04004663 RID: 18019
		public string BackgroundColor = "";

		// Token: 0x04004664 RID: 18020
		public string ForegroundColor = "";
	}
}
