using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200055D RID: 1373
	[IsLiquid]
	[Serializable]
	public class LiquidPutrescence : BaseLiquid
	{
		// Token: 0x06003B5B RID: 15195 RVA: 0x00122AF4 File Offset: 0x00120CF4
		public LiquidPutrescence()
			: base("putrid")
		{
			this.FlameTemperature = 600;
			this.VaporTemperature = 1600;
			this.Combustibility = 5;
			this.ThermalConductivity = 25;
			this.Fluidity = 15;
			this.Evaporativity = 1;
			this.Staining = 2;
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x00122B47 File Offset: 0x00120D47
		public override List<string> GetColors()
		{
			return LiquidPutrescence.Colors;
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x00122B4E File Offset: 0x00120D4E
		public override string GetColor()
		{
			return "K";
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x00122B55 File Offset: 0x00120D55
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{putrid|putrescence}}";
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x00122B5C File Offset: 0x00120D5C
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{putrid|putrid}}";
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x00122B63 File Offset: 0x00120D63
		public override string GetWaterRitualName()
		{
			return "putrescence";
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x00122B6A File Offset: 0x00120D6A
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{putrid|putrid}}";
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x00122B71 File Offset: 0x00120D71
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{putrid|putrid}}";
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x00122B78 File Offset: 0x00120D78
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{putrid|putrescence}}";
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x00122B7F File Offset: 0x00120D7F
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.IsPlayer() && Message != null && !Message.Contains("It's disgusting!"))
			{
				Message.Compound("It's disgusting!");
			}
			InduceVomitingEvent.Send(Target, ref ExitInterface, Message);
			return true;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x00122BB4 File Offset: 0x00120DB4
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&K";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x00122BEF File Offset: 0x00120DEF
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "^K";
			}
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x00122C10 File Offset: 0x00120E10
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&g^K";
			Liquid.ParentObject.Render.TileColor = "&g";
			Liquid.ParentObject.Render.DetailColor = "K";
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x00122C5C File Offset: 0x00120E5C
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&g";
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x00122C80 File Offset: 0x00120E80
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&G^K", "&G", "K");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				if (num < 15)
				{
					render.RenderString = "÷";
					return;
				}
				if (num < 30)
				{
					render.RenderString = "~";
					return;
				}
				if (num < 45)
				{
					render.RenderString = " ";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x00122D29 File Offset: 0x00120F29
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&K";
			}
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x00122D49 File Offset: 0x00120F49
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x00122D60 File Offset: 0x00120F60
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			if (!FilthAffinity && !Smart)
			{
				return 2;
			}
			return 0;
		}

		// Token: 0x04001D76 RID: 7542
		public new const string ID = "putrid";

		// Token: 0x04001D77 RID: 7543
		[NonSerialized]
		public static List<string> Colors = new List<string>(3) { "K", "g", "w" };
	}
}
