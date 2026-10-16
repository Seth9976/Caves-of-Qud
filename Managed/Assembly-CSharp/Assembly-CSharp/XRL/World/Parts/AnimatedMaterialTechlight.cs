using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012CB RID: 4811
	[Serializable]
	public class AnimatedMaterialTechlight : IPart
	{
		// Token: 0x0600BDBC RID: 48572 RVA: 0x004587A4 File Offset: 0x004569A4
		public AnimatedMaterialTechlight()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BDBD RID: 48573 RVA: 0x004587C5 File Offset: 0x004569C5
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDBE RID: 48574 RVA: 0x004587C8 File Offset: 0x004569C8
		public override bool Render(RenderEvent E)
		{
			Render render = this.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 500;
			render.TileColor = this.baseColor;
			if (Stat.Random(1, 200) == 1 || this.FlickerFrame > 0)
			{
				render.ColorString = "&c";
				render.DetailColor = "c";
				if (this.FlickerFrame == 0)
				{
					this.FlickerFrame = 3;
				}
				this.FlickerFrame--;
			}
			if (num < 4)
			{
				render.ColorString = "&C";
				render.DetailColor = "C";
			}
			else if (num < 8)
			{
				render.ColorString = "&B";
				render.DetailColor = "B";
			}
			else if (num < 12)
			{
				render.ColorString = "&b";
				render.DetailColor = "b";
			}
			else
			{
				render.ColorString = "&Y";
				render.DetailColor = "Y";
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.Random(0, 20);
			}
			if (Stat.Random(1, 400) == 1 || this.FlickerFrame > 0)
			{
				render.ColorString = "&b";
				render.DetailColor = "b";
			}
			return true;
		}

		// Token: 0x04004C59 RID: 19545
		public int nFrameOffset;

		// Token: 0x04004C5A RID: 19546
		public int FrameOffset;

		// Token: 0x04004C5B RID: 19547
		public int FlickerFrame;

		// Token: 0x04004C5C RID: 19548
		public string baseColor = "c";
	}
}
