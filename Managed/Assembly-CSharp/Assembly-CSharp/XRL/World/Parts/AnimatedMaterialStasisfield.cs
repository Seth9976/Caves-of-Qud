using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012CA RID: 4810
	[Serializable]
	public class AnimatedMaterialStasisfield : IPart
	{
		// Token: 0x0600BDB8 RID: 48568 RVA: 0x00458537 File Offset: 0x00456737
		public AnimatedMaterialStasisfield()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
			this.Rushing = true;
		}

		// Token: 0x0600BDB9 RID: 48569 RVA: 0x00458554 File Offset: 0x00456754
		public override bool FinalRender(RenderEvent E)
		{
			if (base.Visible() && E.ColorsVisible)
			{
				string text;
				string text2;
				if (this.Rushing)
				{
					int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
					if (!Options.DisableTextAnimationEffects)
					{
						this.FrameOffset += 3;
					}
					if (Stat.RandomCosmetic(1, 120) == 1)
					{
						this.Rushing = false;
					}
					if (num < 45)
					{
						text = "^m";
						text2 = "m";
					}
					else
					{
						text = "^C";
						text2 = "C";
					}
				}
				else if ((XRLCore.CurrentFrame + this.FrameOffset) % 60 < 45)
				{
					text = "^m";
					text2 = "m";
				}
				else
				{
					text = "^C";
					text2 = "C";
				}
				if (!text.IsNullOrEmpty() || !text2.IsNullOrEmpty())
				{
					E.ApplyColors(null, text, text2, AnimatedMaterialStasisfield.ICON_COLOR_PRIORITY, AnimatedMaterialStasisfield.ICON_COLOR_PRIORITY, AnimatedMaterialStasisfield.ICON_COLOR_PRIORITY);
				}
			}
			return base.FinalRender(E);
		}

		// Token: 0x0600BDBA RID: 48570 RVA: 0x00458638 File Offset: 0x00456838
		public override bool Render(RenderEvent E)
		{
			string text;
			string text2;
			if (this.Rushing)
			{
				int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				if (!Options.DisableTextAnimationEffects)
				{
					this.FrameOffset += 3;
				}
				if (Stat.RandomCosmetic(1, 120) == 1)
				{
					this.Rushing = false;
				}
				if (num < 15)
				{
					E.RenderString = "°";
					text = "&C";
					text2 = "k";
				}
				else if (num < 30)
				{
					E.RenderString = "°";
					text = "&c";
					text2 = "m";
				}
				else if (num < 45)
				{
					E.RenderString = "°";
					text = "&M";
					text2 = "c";
				}
				else
				{
					text = "&m";
					text2 = "C";
				}
			}
			else
			{
				int num2 = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				if (num2 < 15)
				{
					E.RenderString = "°";
					text = "&C";
					text2 = "k";
				}
				else if (num2 < 30)
				{
					E.RenderString = "°";
					text = "&c";
					text2 = "M";
				}
				else if (num2 < 45)
				{
					E.RenderString = "°";
					text = "&C";
					text2 = "m";
				}
				else
				{
					text = "&m";
					text2 = "C";
				}
			}
			if (!text.IsNullOrEmpty() || !text2.IsNullOrEmpty())
			{
				E.ApplyColors(text, text2, AnimatedMaterialStasisfield.ICON_COLOR_PRIORITY, AnimatedMaterialStasisfield.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x04004C56 RID: 19542
		public static readonly int ICON_COLOR_PRIORITY = 50;

		// Token: 0x04004C57 RID: 19543
		public int FrameOffset;

		// Token: 0x04004C58 RID: 19544
		public bool Rushing;
	}
}
