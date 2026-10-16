using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012BB RID: 4795
	[Serializable]
	public class AnimatedMaterialElectric : IPart
	{
		// Token: 0x0600BD76 RID: 48502 RVA: 0x00455D8E File Offset: 0x00453F8E
		public AnimatedMaterialElectric()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BD77 RID: 48503 RVA: 0x00455DA4 File Offset: 0x00453FA4
		public override bool Render(RenderEvent E)
		{
			if (E.ColorsVisible)
			{
				if ((XRLCore.CurrentFrame + this.FrameOffset) % 60 % 3 == 0)
				{
					E.ApplyColors("&Y", AnimatedMaterialElectric.ICON_COLOR_PRIORITY);
				}
				else
				{
					E.ApplyColors("&W", AnimatedMaterialElectric.ICON_COLOR_PRIORITY);
				}
			}
			return base.Render(E);
		}

		// Token: 0x04004BEF RID: 19439
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004BF0 RID: 19440
		public int FrameOffset;
	}
}
