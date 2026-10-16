using System;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	// Token: 0x02000F68 RID: 3944
	[Serializable]
	public class PaintTest : IPart
	{
		// Token: 0x06009D14 RID: 40212 RVA: 0x0038E0C8 File Offset: 0x0038C2C8
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.Physics.CurrentCell != null)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x06009D15 RID: 40213 RVA: 0x0038E0E4 File Offset: 0x0038C2E4
		public override void OnPaint(ScreenBuffer buffer)
		{
			int num = (int)(IComponent<GameObject>.frameTimerMS % 1000L / 200L);
			foreach (Cell cell in this.ParentObject.Physics.CurrentCell.GetLocalAdjacentCells(num, false))
			{
				if (cell.Location.Distance(this.ParentObject.Physics.CurrentCell.Location) == num)
				{
					buffer.Buffer[cell.X, cell.Y].SetBackground('G');
					buffer.Buffer[cell.X, cell.Y].Detail = The.Color.Green;
				}
			}
		}
	}
}
