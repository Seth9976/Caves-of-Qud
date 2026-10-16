using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012BF RID: 4799
	[Serializable]
	public class AnimatedMaterialForcefieldColors : IPart
	{
		// Token: 0x0600BD83 RID: 48515 RVA: 0x00456545 File Offset: 0x00454745
		public AnimatedMaterialForcefieldColors()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
			this.Rushing = true;
		}

		// Token: 0x0600BD84 RID: 48516 RVA: 0x00456570 File Offset: 0x00454770
		public override bool SameAs(IPart p)
		{
			AnimatedMaterialForcefieldColors animatedMaterialForcefieldColors = p as AnimatedMaterialForcefieldColors;
			return animatedMaterialForcefieldColors.Rushing == this.Rushing && !(animatedMaterialForcefieldColors.Color != this.Color) && base.SameAs(p);
		}

		// Token: 0x0600BD85 RID: 48517 RVA: 0x004565B0 File Offset: 0x004547B0
		public override bool Render(RenderEvent E)
		{
			if (!base.Visible() || !E.ColorsVisible)
			{
				return true;
			}
			string text = null;
			string text4;
			string text5;
			if (E.Tile == null)
			{
				string text2 = "^c";
				string text3 = "^C";
				if (this.Color == "Red")
				{
					text2 = "^r";
					text3 = "^R";
				}
				if (this.Color == "Blue")
				{
					text2 = "^b";
					text3 = "^B";
				}
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
						text4 = "&r";
						text5 = text2;
					}
					else
					{
						text4 = "&R";
						text5 = text3;
					}
				}
				else if ((XRLCore.CurrentFrame + this.FrameOffset) % 60 < 45)
				{
					text4 = "&r";
					text5 = text2;
				}
				else
				{
					text4 = "&R";
					text5 = text3;
				}
			}
			else
			{
				string text6 = "^k";
				string text7 = "^K";
				string text8 = "^c";
				string text9 = "^C";
				string text10 = "&c";
				string text11 = "&C";
				if (this.Color == "Red")
				{
					text6 = "^k";
					text7 = "^r";
					text8 = "^r";
					text9 = "^r";
					text10 = "&r";
					text11 = "&R";
				}
				if (this.Color == "Blue")
				{
					text6 = "^k";
					text7 = "^K";
					text8 = "^b";
					text9 = "^B";
					text10 = "&b";
					text11 = "&B";
				}
				if (this.Rushing)
				{
					int num2 = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
					if (!Options.DisableTextAnimationEffects)
					{
						this.FrameOffset += 3;
					}
					if (Stat.RandomCosmetic(1, 120) == 1)
					{
						this.Rushing = false;
					}
					if (num2 < 15)
					{
						text4 = text11;
						text5 = text6;
						text = text10[1].ToString();
					}
					else if (num2 < 30)
					{
						text4 = text10;
						text5 = text6;
						text = text7[1].ToString();
					}
					else if (num2 < 45)
					{
						text4 = text11;
						text5 = text6;
						text = text8[1].ToString();
					}
					else
					{
						text4 = text10;
						text5 = text6;
						text = text9[1].ToString();
					}
				}
				else
				{
					int num3 = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
					if (num3 < 15)
					{
						text4 = text11;
						text5 = text6;
						text = text10[1].ToString();
					}
					else if (num3 < 30)
					{
						text4 = text10;
						text5 = text6;
						text = text11[1].ToString();
					}
					else if (num3 < 45)
					{
						text4 = text11;
						text5 = text6;
						text = text8[1].ToString();
					}
					else
					{
						text4 = text10;
						text5 = text6;
						text = text9[1].ToString();
					}
				}
			}
			if (!text4.IsNullOrEmpty() || !text5.IsNullOrEmpty() || !text.IsNullOrEmpty())
			{
				E.ApplyColors(text4, text5, text, AnimatedMaterialForcefieldColors.ICON_COLOR_PRIORITY, AnimatedMaterialForcefieldColors.ICON_COLOR_PRIORITY, AnimatedMaterialForcefieldColors.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x04004BFE RID: 19454
		public static readonly int ICON_COLOR_PRIORITY = 50;

		// Token: 0x04004BFF RID: 19455
		public int FrameOffset;

		// Token: 0x04004C00 RID: 19456
		public bool Rushing;

		// Token: 0x04004C01 RID: 19457
		public string Color = "Normal";
	}
}
