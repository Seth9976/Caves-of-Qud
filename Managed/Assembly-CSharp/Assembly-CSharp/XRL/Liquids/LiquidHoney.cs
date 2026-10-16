using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000556 RID: 1366
	[IsLiquid]
	[Serializable]
	public class LiquidHoney : BaseLiquid
	{
		// Token: 0x06003AC3 RID: 15043 RVA: 0x00120D38 File Offset: 0x0011EF38
		public LiquidHoney()
			: base("honey")
		{
			this.FlameTemperature = 300;
			this.VaporTemperature = 1300;
			this.Combustibility = 60;
			this.Adsorbence = 25;
			this.PureElectricalConductivity = 40;
			this.MixedElectricalConductivity = 40;
			this.ThermalConductivity = 40;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Weight = 0.5;
			this.InterruptAutowalk = true;
			this.StickyWhenWet = true;
			this.StickySaveTargetBase = 1;
			this.StickySaveTargetScale = 0.1;
			this.StickyDuration = 12;
			this.StickySaveVs = "Honey Stuck Restraint";
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x00120DE3 File Offset: 0x0011EFE3
		public override List<string> GetColors()
		{
			return LiquidHoney.Colors;
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x00120DEA File Offset: 0x0011EFEA
		public override string GetColor()
		{
			return "w";
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x00120DF1 File Offset: 0x0011EFF1
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{w|honey}}";
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x00120DF8 File Offset: 0x0011EFF8
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{w|honeyed}}";
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x00120DFF File Offset: 0x0011EFFF
		public override string GetWaterRitualName()
		{
			return "honey";
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x00120E06 File Offset: 0x0011F006
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{w|sticky}}";
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x00120E0D File Offset: 0x0011F00D
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{w|sticky}}";
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x00120E14 File Offset: 0x0011F014
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{w|honey}}";
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x00120E1B File Offset: 0x0011F01B
		public override string GetPreparedCookingIngredient()
		{
			return "medicinalMinor";
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x00120E22 File Offset: 0x0011F022
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Stomach>())
			{
				Target.FireEvent(Event.New("AddFood", "Satiation", "Snack"));
				Message.Compound("Delicious!");
			}
			return true;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x00120E55 File Offset: 0x0011F055
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^w" + eRender.ColorString;
			}
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x00120E78 File Offset: 0x0011F078
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&w^W";
			Liquid.ParentObject.Render.TileColor = "&w";
			Liquid.ParentObject.Render.DetailColor = "W";
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x00120EC4 File Offset: 0x0011F0C4
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&W";
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x00120EE8 File Offset: 0x0011F0E8
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&W^w", "&W", "w");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&W^w", "&W", "w");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&W^w";
				render.TileColor = "&W";
				render.DetailColor = "w";
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
					render.RenderString = "\t";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x00120FE0 File Offset: 0x0011F1E0
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&w";
			}
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x00121000 File Offset: 0x0011F200
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&w";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x0012103B File Offset: 0x0011F23B
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Gunk/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x00121052 File Offset: 0x0011F252
		public override float GetValuePerDram()
		{
			return 2f;
		}

		// Token: 0x04001D64 RID: 7524
		public new const string ID = "honey";

		// Token: 0x04001D65 RID: 7525
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "w", "W" };
	}
}
