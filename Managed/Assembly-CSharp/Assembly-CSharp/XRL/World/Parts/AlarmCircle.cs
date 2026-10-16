using System;
using ConsoleLib.Console;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020012F6 RID: 4854
	[Serializable]
	public class AlarmCircle : IPart
	{
		// Token: 0x0600BE72 RID: 48754 RVA: 0x0045BF36 File Offset: 0x0045A136
		public override void Attach()
		{
			this.ParentObject.Flags |= 128;
		}

		// Token: 0x0600BE73 RID: 48755 RVA: 0x0045BF4F File Offset: 0x0045A14F
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.CurrentCell != null)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600BE74 RID: 48756 RVA: 0x0045BF68 File Offset: 0x0045A168
		public override void OnPaint(ScreenBuffer buffer)
		{
			if (Globals.RenderMode == RenderModeType.Text && XRLCore.GetCurrentFrameAtFPS(60, 1f) % 120 < 60)
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.GetLocalAdjacentCells(this.Radius, false))
				{
					if (cell.Location.Distance(this.ParentObject.CurrentCell.Location) == this.Radius)
					{
						buffer.Buffer[cell.X, cell.Y].SetBackground('R');
						buffer.Buffer[cell.X, cell.Y].Detail = The.Color.Red;
					}
				}
			}
		}

		// Token: 0x04004CB3 RID: 19635
		public int Radius = 3;
	}
}
