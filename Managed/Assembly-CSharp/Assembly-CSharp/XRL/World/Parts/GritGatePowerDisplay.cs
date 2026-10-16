using System;
using ConsoleLib.Console;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x020012FC RID: 4860
	[Serializable]
	public class GritGatePowerDisplay : IPart
	{
		// Token: 0x0600BE89 RID: 48777 RVA: 0x0045C450 File Offset: 0x0045A650
		public override void Attach()
		{
			this.ParentObject.Flags |= 128;
		}

		// Token: 0x0600BE8A RID: 48778 RVA: 0x0045C469 File Offset: 0x0045A669
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.CurrentCell != null)
			{
				E.WantsToPaint = true;
			}
			return true;
		}

		// Token: 0x0600BE8B RID: 48779 RVA: 0x0045C480 File Offset: 0x0045A680
		public override void OnPaint(ScreenBuffer Buffer)
		{
			if (Globals.RenderMode == RenderModeType.Text)
			{
				Buffer.WriteAt(this.ParentObject.CurrentCell, GritGateAmperageImposter.display, true);
			}
		}

		// Token: 0x04004CB7 RID: 19639
		public int Radius = 3;
	}
}
