using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012CF RID: 4815
	[Serializable]
	public class AnimatedMaterialWhiteFire : IPart
	{
		// Token: 0x0600BDCD RID: 48589 RVA: 0x004594F2 File Offset: 0x004576F2
		public AnimatedMaterialWhiteFire()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BDCE RID: 48590 RVA: 0x00459508 File Offset: 0x00457708
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDCF RID: 48591 RVA: 0x0045950C File Offset: 0x0045770C
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
					text = "Y";
					text2 = "&Y";
				}
				else if (num < 30)
				{
					text = "W";
					text2 = "&W";
				}
				else if (num < 45)
				{
					text = "Y";
					text2 = "&Y";
				}
				else
				{
					text = "W";
					text2 = "&W";
				}
				if (Options.UseTiles)
				{
					E.ApplyDetailColor(text, AnimatedMaterialWhiteFire.ICON_COLOR_PRIORITY);
				}
				else
				{
					E.ApplyColors(text2, AnimatedMaterialWhiteFire.ICON_COLOR_PRIORITY);
				}
			}
			return true;
		}

		// Token: 0x04004C75 RID: 19573
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004C76 RID: 19574
		public int FrameOffset;
	}
}
