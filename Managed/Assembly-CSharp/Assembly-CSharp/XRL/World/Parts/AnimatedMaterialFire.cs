using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012BD RID: 4797
	[Serializable]
	public class AnimatedMaterialFire : IPart
	{
		// Token: 0x0600BD7B RID: 48507 RVA: 0x00455FBC File Offset: 0x004541BC
		public AnimatedMaterialFire()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BD7C RID: 48508 RVA: 0x00455FD4 File Offset: 0x004541D4
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
				if (num < 15)
				{
					text = "&R";
				}
				else if (num < 30)
				{
					text = "&W";
				}
				else if (num < 45)
				{
					text = "&r";
				}
				else
				{
					text = "&W";
				}
				E.ApplyColors(text, AnimatedMaterialFire.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x04004BF5 RID: 19445
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004BF6 RID: 19446
		public int FrameOffset;
	}
}
