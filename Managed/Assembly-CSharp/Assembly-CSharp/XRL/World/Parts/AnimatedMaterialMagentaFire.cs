using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012C6 RID: 4806
	[Serializable]
	public class AnimatedMaterialMagentaFire : IPart
	{
		// Token: 0x0600BDA5 RID: 48549 RVA: 0x00457D72 File Offset: 0x00455F72
		public AnimatedMaterialMagentaFire()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BDA6 RID: 48550 RVA: 0x00457D88 File Offset: 0x00455F88
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDA7 RID: 48551 RVA: 0x00457D8C File Offset: 0x00455F8C
		public override bool Render(RenderEvent E)
		{
			if (E.ColorsVisible)
			{
				int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				if (!Options.DisableTextAnimationEffects)
				{
					this.FrameOffset += Stat.RandomCosmetic(1, 5);
				}
				string text;
				string text2;
				if (num < 15)
				{
					text = "m";
					text2 = "&m";
				}
				else if (num < 30)
				{
					text = "Y";
					text2 = "&Y";
				}
				else if (num < 45)
				{
					text = "M";
					text2 = "&M";
				}
				else
				{
					text = "m";
					text2 = "&m";
				}
				if (Options.UseTiles)
				{
					E.ApplyDetailColor(text, AnimatedMaterialMagentaFire.ICON_COLOR_PRIORITY);
				}
				else
				{
					E.ApplyColors(text2, AnimatedMaterialMagentaFire.ICON_COLOR_PRIORITY);
				}
			}
			return true;
		}

		// Token: 0x04004C48 RID: 19528
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004C49 RID: 19529
		public int FrameOffset;
	}
}
