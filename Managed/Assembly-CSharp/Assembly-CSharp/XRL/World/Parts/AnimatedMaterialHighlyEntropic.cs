using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012C4 RID: 4804
	[Serializable]
	public class AnimatedMaterialHighlyEntropic : IPart
	{
		// Token: 0x0600BDA0 RID: 48544 RVA: 0x00457B60 File Offset: 0x00455D60
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
			int num = (XRLCore.CurrentFrame + this.FrameOffset) % 150;
			if (num < 3)
			{
				render.TileColor = "&k";
				render.ColorString = "&k";
			}
			else if (num < 6)
			{
				render.DetailColor = "K";
			}
			else if (num < 9)
			{
				render.TileColor = "&m";
				render.ColorString = "&m";
			}
			else if (num < 12)
			{
				render.DetailColor = "m";
			}
			else if (num < 15)
			{
				render.TileColor = "&K";
				render.ColorString = "&K";
				render.DetailColor = "y";
			}
			else if (num < 18)
			{
				render.TileColor = "&m";
				render.ColorString = "&m";
				render.DetailColor = "K";
			}
			else
			{
				render.ColorString = this.ColorString;
				render.DetailColor = this.DetailColor;
				render.TileColor = this.TileColor;
			}
			if (!Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += (50.in100() ? 1 : Stat.Random(1, 3));
			}
			if (2.in100() && !Options.DisableTextAnimationEffects)
			{
				this.FrameOffset += Stat.RandomCosmetic(0, 100);
			}
			return true;
		}

		// Token: 0x04004C42 RID: 19522
		public string ColorString;

		// Token: 0x04004C43 RID: 19523
		public string TileColor;

		// Token: 0x04004C44 RID: 19524
		public string DetailColor;

		// Token: 0x04004C45 RID: 19525
		public int FrameOffset;
	}
}
