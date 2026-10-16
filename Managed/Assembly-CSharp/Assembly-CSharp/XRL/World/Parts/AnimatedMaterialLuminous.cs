using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012C5 RID: 4805
	[Serializable]
	public class AnimatedMaterialLuminous : IPart
	{
		// Token: 0x0600BDA2 RID: 48546 RVA: 0x00457D13 File Offset: 0x00455F13
		public AnimatedMaterialLuminous()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BDA3 RID: 48547 RVA: 0x00457D29 File Offset: 0x00455F29
		public override bool Render(RenderEvent E)
		{
			if (E.ColorsVisible)
			{
				if ((XRLCore.CurrentFrame + this.FrameOffset) % 60 % 3 == 0)
				{
					E.ApplyColors("&C", AnimatedMaterialLuminous.ICON_COLOR_PRIORITY);
				}
				else
				{
					E.ApplyColors("&Y", AnimatedMaterialLuminous.ICON_COLOR_PRIORITY);
				}
			}
			return true;
		}

		// Token: 0x04004C46 RID: 19526
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004C47 RID: 19527
		public int FrameOffset;
	}
}
