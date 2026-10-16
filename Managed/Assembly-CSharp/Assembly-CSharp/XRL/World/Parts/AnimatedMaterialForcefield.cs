using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012BE RID: 4798
	[Serializable]
	public class AnimatedMaterialForcefield : IPart
	{
		// Token: 0x0600BD7E RID: 48510 RVA: 0x0045605F File Offset: 0x0045425F
		public AnimatedMaterialForcefield()
		{
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
			this.Rushing = true;
		}

		// Token: 0x0600BD7F RID: 48511 RVA: 0x00456099 File Offset: 0x00454299
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BD80 RID: 48512 RVA: 0x0045609C File Offset: 0x0045429C
		public override bool FinalRender(RenderEvent E)
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
				if (this.Color == "Magenta")
				{
					text2 = "^m";
					text3 = "^M";
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
				if (this.Color == "Magenta")
				{
					text6 = "^k";
					text7 = "^m";
					text8 = "^m";
					text9 = "^m";
					text10 = "&m";
					text11 = "&M";
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
						text = text6[1].ToString();
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
						text = text6[1].ToString();
					}
					else if (num3 < 30)
					{
						text4 = text10;
						text5 = text6;
						text = text6[1].ToString();
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
				E.ApplyColors(text4, text5, text, AnimatedMaterialForcefield.ICON_COLOR_PRIORITY, AnimatedMaterialForcefield.ICON_COLOR_PRIORITY, AnimatedMaterialForcefield.ICON_COLOR_PRIORITY);
			}
			return true;
		}

		// Token: 0x0600BD81 RID: 48513 RVA: 0x00456424 File Offset: 0x00454624
		public override bool Render(RenderEvent E)
		{
			if (E.Tile != null)
			{
				if (XRLCore.FrameTimer.ElapsedMilliseconds - this.accumulator > 500L)
				{
					string text = E.Tile.Substring(E.Tile.LastIndexOf('_') + 1);
					this.accumulator = XRLCore.FrameTimer.ElapsedMilliseconds;
					int num = this.lastN + 1;
					this.lastN = num;
					if (num > 4)
					{
						this.lastN = 1;
					}
					this.tileBuilder.Length = 0;
					this.tileBuilder.Append("Assets_Content_Textures_Tiles2_force_field_").Append(this.lastN).Append('_')
						.Append(text);
					this.ParentObject.Render.Tile = this.tileBuilder.ToString();
				}
			}
			else
			{
				int num2 = (XRLCore.CurrentFrame + this.FrameOffset) % 60;
				if (!Options.DisableTextAnimationEffects)
				{
					this.FrameOffset += 3;
				}
				if (num2 < 45)
				{
					E.RenderString = "°";
				}
				else
				{
					E.RenderString = this.ParentObject.Render.RenderString;
				}
			}
			return true;
		}

		// Token: 0x04004BF7 RID: 19447
		public static readonly int ICON_COLOR_PRIORITY = 50;

		// Token: 0x04004BF8 RID: 19448
		public int FrameOffset;

		// Token: 0x04004BF9 RID: 19449
		public bool Rushing;

		// Token: 0x04004BFA RID: 19450
		public string Color = "Normal";

		// Token: 0x04004BFB RID: 19451
		[NonSerialized]
		private StringBuilder tileBuilder = new StringBuilder();

		// Token: 0x04004BFC RID: 19452
		[NonSerialized]
		private int lastN = 1;

		// Token: 0x04004BFD RID: 19453
		[NonSerialized]
		private long accumulator;
	}
}
