using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012BC RID: 4796
	[Serializable]
	public class AnimatedMaterialExtradimensional : IPart
	{
		// Token: 0x0600BD79 RID: 48505 RVA: 0x00455E00 File Offset: 0x00454000
		public override bool Render(RenderEvent E)
		{
			Render render = this.ParentObject.Render;
			if (this.ColorString == null)
			{
				this.ColorString = this.ParentObject.Render.ColorString;
			}
			if (this.TileColor == null)
			{
				this.TileColor = this.ParentObject.Render.TileColor;
			}
			if (this.DetailColor == null)
			{
				this.DetailColor = this.ParentObject.Render.DetailColor;
			}
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 200;
			if (num < 2)
			{
				render.TileColor = "&o";
				render.ColorString = "&o";
				render.DetailColor = "o";
			}
			else if (num < 4)
			{
				render.TileColor = "&O";
				render.ColorString = "&O";
				render.DetailColor = "o";
			}
			else if (num < 6)
			{
				render.TileColor = "&O";
				render.ColorString = "&O";
				render.DetailColor = "O";
			}
			else if (num < 8)
			{
				render.TileColor = "&k";
				render.ColorString = "&k";
				render.DetailColor = "k";
			}
			else if (num < 10)
			{
				render.TileColor = "&o";
				render.ColorString = "&o";
				render.DetailColor = "O";
			}
			else
			{
				render.ColorString = this.ColorString;
				render.DetailColor = this.DetailColor;
				render.TileColor = this.TileColor;
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.Random(1, 3);
			}
			if (2.in100() && !Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.RandomCosmetic(0, 100);
			}
			return true;
		}

		// Token: 0x04004BF1 RID: 19441
		public string ColorString;

		// Token: 0x04004BF2 RID: 19442
		public string TileColor;

		// Token: 0x04004BF3 RID: 19443
		public string DetailColor;

		// Token: 0x04004BF4 RID: 19444
		public int FrameOffset;
	}
}
