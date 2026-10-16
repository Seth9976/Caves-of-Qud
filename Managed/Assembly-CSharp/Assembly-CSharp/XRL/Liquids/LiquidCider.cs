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
	// Token: 0x02000551 RID: 1361
	[IsLiquid]
	[Serializable]
	public class LiquidCider : BaseLiquid
	{
		// Token: 0x06003A5C RID: 14940 RVA: 0x0011FC37 File Offset: 0x0011DE37
		public LiquidCider()
			: base("cider")
		{
			this.FlameTemperature = 500;
			this.VaporTemperature = 800;
			this.Combustibility = 5;
			this.ThermalConductivity = 40;
			this.Evaporativity = 5;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x0011FC70 File Offset: 0x0011DE70
		public override List<string> GetColors()
		{
			return LiquidCider.Colors;
		}

		// Token: 0x06003A5E RID: 14942 RVA: 0x0011FC77 File Offset: 0x0011DE77
		public override string GetColor()
		{
			return "w";
		}

		// Token: 0x06003A5F RID: 14943 RVA: 0x0011FC7E File Offset: 0x0011DE7E
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{cider|cider}}";
		}

		// Token: 0x06003A60 RID: 14944 RVA: 0x0011FC85 File Offset: 0x0011DE85
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{cider|spiced}}";
		}

		// Token: 0x06003A61 RID: 14945 RVA: 0x0011FC8C File Offset: 0x0011DE8C
		public override string GetWaterRitualName()
		{
			return "cider";
		}

		// Token: 0x06003A62 RID: 14946 RVA: 0x0011FC93 File Offset: 0x0011DE93
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{cider|fragrant}}";
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x0011FC9A File Offset: 0x0011DE9A
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{cider|fragrant}}";
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x0011FCA1 File Offset: 0x0011DEA1
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{cider|cider}}";
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x0011FCA8 File Offset: 0x0011DEA8
		public override string GetPreparedCookingIngredient()
		{
			return "quicknessMinor";
		}

		// Token: 0x06003A66 RID: 14950 RVA: 0x0011FCB0 File Offset: 0x0011DEB0
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			long turns = XRLCore.Core.Game.Turns;
			if (Target.HasPart<Stomach>())
			{
				Target.FireEvent(Event.New("AddWater", "Amount", Volume * 2, "Forced", 1));
				Target.FireEvent(Event.New("AddFood", "Satiation", "Snack"));
				Message.Compound("It is spicy and delicious.");
			}
			if (!Target.HasProperty("ConfuseOnEatTurnWine"))
			{
				Target.SetLongProperty("ConfuseOnEatTurnWine", XRLCore.Core.Game.Turns);
			}
			long longProperty = Target.GetLongProperty("ConfuseOnEatTurnWine", 0L);
			int num = Target.GetIntProperty("ConfuseOnEatAmountWine", 0);
			if (turns - longProperty > 80L)
			{
				num = 0;
			}
			if (num > Math.Max(1, Target.StatMod("Toughness", 0) * 2) && Target.ApplyEffect(new Confused(Stat.Roll("5d5", null), 1, 3, "Confusion"), null))
			{
				ExitInterface = true;
			}
			Target.SetLongProperty("ConfuseOnEatTurnWine", turns);
			num++;
			Target.SetIntProperty("ConfuseOnEatAmountWine", num, false);
			return true;
		}

		// Token: 0x06003A67 RID: 14951 RVA: 0x0011FDC4 File Offset: 0x0011DFC4
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^w" + eRender.ColorString;
			}
		}

		// Token: 0x06003A68 RID: 14952 RVA: 0x0011FDE4 File Offset: 0x0011DFE4
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&w^r";
			Liquid.ParentObject.Render.TileColor = "&w";
			Liquid.ParentObject.Render.DetailColor = "r";
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x0011FE30 File Offset: 0x0011E030
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&w";
		}

		// Token: 0x06003A6A RID: 14954 RVA: 0x0011FE54 File Offset: 0x0011E054
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&r^w", "&r", "w");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&r^w", "&r", "w");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				if (num < 15)
				{
					render.RenderString = "÷";
				}
				else if (num < 30)
				{
					render.RenderString = "~";
				}
				else if (num < 45)
				{
					render.RenderString = "\t";
				}
				else
				{
					render.RenderString = "~";
				}
				render.ColorString = "&w^r";
				render.TileColor = "&w";
				render.DetailColor = "r";
			}
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x0011FF4F File Offset: 0x0011E14F
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&w";
			}
		}

		// Token: 0x06003A6C RID: 14956 RVA: 0x0011FF70 File Offset: 0x0011E170
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

		// Token: 0x06003A6D RID: 14957 RVA: 0x0011FFAB File Offset: 0x0011E1AB
		public override float GetValuePerDram()
		{
			return 3.8f;
		}

		// Token: 0x04001D59 RID: 7513
		public new const string ID = "cider";

		// Token: 0x04001D5A RID: 7514
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "w", "r" };
	}
}
