using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000560 RID: 1376
	[IsLiquid]
	[Serializable]
	public class LiquidSlime : BaseLiquid
	{
		// Token: 0x06003B90 RID: 15248 RVA: 0x00123308 File Offset: 0x00121508
		public LiquidSlime()
			: base("slime")
		{
			this.FlameTemperature = 550;
			this.VaporTemperature = 1550;
			this.Combustibility = 8;
			this.ThermalConductivity = 35;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 1;
			this.ConversionProduct = "gel";
			this.SlipperyWhenWet = true;
			this.SlipperyWhenFrozen = true;
			this.SlipperySaveTargetBase = 5;
			this.SlipperySaveTargetScale = 0.3;
			this.SlipperySaveVs = "Slime Slip Move";
			this.SlipperyMessage = "{{slimy|=subject.T= =verb:slip= on the slime!}}";
			this.SlipperyParticle = "&g\u001a";
			this.CirculatoryLossTerm = "oozing";
			this.CirculatoryLossNoun = "ooze";
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x001233C1 File Offset: 0x001215C1
		public override List<string> GetColors()
		{
			return LiquidSlime.Colors;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x001233C8 File Offset: 0x001215C8
		public override string GetColor()
		{
			return "g";
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x001233CF File Offset: 0x001215CF
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{slimy|slime}}";
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x001233D6 File Offset: 0x001215D6
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{slimy|slimy}}";
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x001233DD File Offset: 0x001215DD
		public override string GetWaterRitualName()
		{
			return "slime";
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x001233E4 File Offset: 0x001215E4
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{slimy|slimy}}";
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x001233EB File Offset: 0x001215EB
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{slimy|slimy}}";
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x001233F2 File Offset: 0x001215F2
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{g|slime}}";
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x001233F9 File Offset: 0x001215F9
		public override string GetPreparedCookingIngredient()
		{
			return "slimeSpitting";
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x00123400 File Offset: 0x00121600
		public override float GetValuePerDram()
		{
			return 0.1f;
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x00123407 File Offset: 0x00121607
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's disgustingly slimy!");
			if (Target.ApplyEffect(new Confused(Stat.Roll("3d6", null), 5, 7, "Confusion"), null))
			{
				ExitInterface = true;
			}
			return true;
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x0012343C File Offset: 0x0012163C
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&g";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x00123477 File Offset: 0x00121677
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^g" + eRender.ColorString;
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x00123498 File Offset: 0x00121698
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&w^g";
			Liquid.ParentObject.Render.TileColor = "&w";
			Liquid.ParentObject.Render.DetailColor = "g";
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x001234E4 File Offset: 0x001216E4
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&g";
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x00123508 File Offset: 0x00121708
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid == null || Liquid.ParentObject == null)
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&g^w", "&g", "w");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&g^w", "&g", "w");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				if (num < 15)
				{
					render.RenderString = "÷";
					render.ColorString = "&w^g";
					render.TileColor = "&w";
					render.DetailColor = "g";
					return;
				}
				if (num < 30)
				{
					render.RenderString = "~";
					render.ColorString = "&g^w";
					render.TileColor = "&g";
					render.DetailColor = "w";
					return;
				}
				if (num < 45)
				{
					render.RenderString = " ";
					render.ColorString = "&w^g";
					render.TileColor = "&w";
					render.DetailColor = "g";
					return;
				}
				render.RenderString = "~";
				render.ColorString = "&w^g";
				render.TileColor = "&w";
				render.DetailColor = "g";
			}
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00123672 File Offset: 0x00121872
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&g";
			}
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x00123692 File Offset: 0x00121892
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x04001D7C RID: 7548
		public new const string ID = "slime";

		// Token: 0x04001D7D RID: 7549
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "g", "w" };
	}
}
