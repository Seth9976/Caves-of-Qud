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
	// Token: 0x02000566 RID: 1382
	[IsLiquid]
	[Serializable]
	public class LiquidWine : BaseLiquid
	{
		// Token: 0x06003C22 RID: 15394 RVA: 0x00126204 File Offset: 0x00124404
		public LiquidWine()
			: base("wine")
		{
			this.FlameTemperature = 620;
			this.VaporTemperature = 1620;
			this.Combustibility = 15;
			this.ThermalConductivity = 45;
			this.Fluidity = 30;
			this.Evaporativity = 3;
			this.Staining = 2;
			this.Cleansing = 1;
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x0012625F File Offset: 0x0012445F
		public override List<string> GetColors()
		{
			return LiquidWine.Colors;
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x00126266 File Offset: 0x00124466
		public override string GetColor()
		{
			return "m";
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x0012626D File Offset: 0x0012446D
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{m|wine}}";
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x00126274 File Offset: 0x00124474
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{m|lush}}";
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x0012627B File Offset: 0x0012447B
		public override string GetWaterRitualName()
		{
			return "wine";
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x00126282 File Offset: 0x00124482
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{m|lush}}";
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x00126289 File Offset: 0x00124489
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{m|lush}}";
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x00126290 File Offset: 0x00124490
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{m|wine}}";
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x00126298 File Offset: 0x00124498
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			long turns = XRLCore.Core.Game.Turns;
			if (Target == null)
			{
				return true;
			}
			if (Target.HasPart<Stomach>())
			{
				Target.FireEvent(Event.New("AddWater", "Amount", 2 * Volume, "Forced", 1));
				Message.Compound("You flush with the warming draught!");
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

		// Token: 0x06003C2C RID: 15404 RVA: 0x00126396 File Offset: 0x00124596
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^m" + eRender.ColorString;
			}
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x001263B8 File Offset: 0x001245B8
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&m^r";
			Liquid.ParentObject.Render.TileColor = "&m";
			Liquid.ParentObject.Render.DetailColor = "r";
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x00126404 File Offset: 0x00124604
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&m";
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x00126428 File Offset: 0x00124628
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&M^b", "&M", "b");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&r^m", "&r", "m");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&m^r";
				render.TileColor = "&m";
				render.DetailColor = "r";
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

		// Token: 0x06003C30 RID: 15408 RVA: 0x00126520 File Offset: 0x00124720
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&m";
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x00126540 File Offset: 0x00124740
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&m";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x0012657B File Offset: 0x0012477B
		public override float GetValuePerDram()
		{
			return 4f;
		}

		// Token: 0x04001D8E RID: 7566
		public new const string ID = "wine";

		// Token: 0x04001D8F RID: 7567
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "m", "r" };
	}
}
