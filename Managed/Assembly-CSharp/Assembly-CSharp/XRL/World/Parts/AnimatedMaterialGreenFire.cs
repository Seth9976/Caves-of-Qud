using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012C3 RID: 4803
	[Serializable]
	public class AnimatedMaterialGreenFire : IPart
	{
		// Token: 0x0600BD9C RID: 48540 RVA: 0x00457A8E File Offset: 0x00455C8E
		public AnimatedMaterialGreenFire()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BD9D RID: 48541 RVA: 0x00457AA4 File Offset: 0x00455CA4
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BD9E RID: 48542 RVA: 0x00457AA8 File Offset: 0x00455CA8
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
					text = "g";
					text2 = "&g";
				}
				else if (num < 30)
				{
					text = "Y";
					text2 = "&Y";
				}
				else if (num < 45)
				{
					text = "G";
					text2 = "&G";
				}
				else
				{
					text = "g";
					text2 = "&g";
				}
				if (Options.UseTiles)
				{
					E.ApplyDetailColor(text, AnimatedMaterialGreenFire.ICON_COLOR_PRIORITY);
				}
				else
				{
					E.ApplyColors(text2, AnimatedMaterialGreenFire.ICON_COLOR_PRIORITY);
				}
			}
			return true;
		}

		// Token: 0x04004C40 RID: 19520
		public static readonly int ICON_COLOR_PRIORITY = 30;

		// Token: 0x04004C41 RID: 19521
		public int FrameOffset;
	}
}
