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
	// Token: 0x02000555 RID: 1365
	[IsLiquid]
	[Serializable]
	public class LiquidGoo : BaseLiquid
	{
		// Token: 0x06003AAE RID: 15022 RVA: 0x00120958 File Offset: 0x0011EB58
		public LiquidGoo()
			: base("goo")
		{
			this.FlameTemperature = 400;
			this.VaporTemperature = 110;
			this.VaporObject = "PoisonGas";
			this.Combustibility = 20;
			this.ThermalConductivity = 40;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 1;
			this.CirculatoryLossTerm = "oozing";
			this.CirculatoryLossNoun = "ooze";
		}

		// Token: 0x06003AAF RID: 15023 RVA: 0x001209CA File Offset: 0x0011EBCA
		public override List<string> GetColors()
		{
			return LiquidGoo.Colors;
		}

		// Token: 0x06003AB0 RID: 15024 RVA: 0x001209D1 File Offset: 0x0011EBD1
		public override string GetColor()
		{
			return "G";
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x001209D8 File Offset: 0x0011EBD8
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{G|green goo}}";
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x001209DF File Offset: 0x0011EBDF
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{G|gooey}}";
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x001209E6 File Offset: 0x0011EBE6
		public override string GetWaterRitualName()
		{
			return "goo";
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x001209ED File Offset: 0x0011EBED
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{G|gooey}}";
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x001209F4 File Offset: 0x0011EBF4
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{G|gooey}}";
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x001209FB File Offset: 0x0011EBFB
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{G|goo}}";
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x00120A02 File Offset: 0x0011EC02
		public override string GetPreparedCookingIngredient()
		{
			return "selfPoison";
		}

		// Token: 0x06003AB8 RID: 15032 RVA: 0x00120A0C File Offset: 0x0011EC0C
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's awful!");
			if (Target.ApplyEffect(new Poisoned(Stat.Roll("1d4+4", null), Stat.Roll("1d2+2", null).ToString() + "d2", 10, null, false, false), null))
			{
				Message.Compound("You feel poison course through your veins!");
				ExitInterface = true;
			}
			return true;
		}

		// Token: 0x06003AB9 RID: 15033 RVA: 0x00120A74 File Offset: 0x0011EC74
		public override void ObjectGoingProne(LiquidVolume Liquid, GameObject GO, bool UsePopups)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (GO.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(GO, "Poisonous goo burns your eyes.", ' ', false, UsePopups, false, null, GO);
			}
			GO.Splatter("&w.");
			if (!GO.MakeSave("Toughness", 30, null, null, "Goo Poison", false, false, false, false, Liquid.ParentObject))
			{
				GO.ApplyEffect(new Poisoned(Stat.Roll("1d4+4", null), Stat.Roll("1d2+2", null).ToString() + "d2", 10, null, UsePopups, false), null);
			}
		}

		// Token: 0x06003ABA RID: 15034 RVA: 0x00120B0C File Offset: 0x0011ED0C
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&G";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003ABB RID: 15035 RVA: 0x00120B47 File Offset: 0x0011ED47
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^G" + eRender.ColorString;
			}
		}

		// Token: 0x06003ABC RID: 15036 RVA: 0x00120B68 File Offset: 0x0011ED68
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&Y^G";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "G";
		}

		// Token: 0x06003ABD RID: 15037 RVA: 0x00120BB4 File Offset: 0x0011EDB4
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&G";
		}

		// Token: 0x06003ABE RID: 15038 RVA: 0x00120BD8 File Offset: 0x0011EDD8
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&Y^G", "&Y", "G");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&Y^G", "&Y", "G");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^G";
				render.TileColor = "&Y";
				render.DetailColor = "G";
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

		// Token: 0x06003ABF RID: 15039 RVA: 0x00120CD0 File Offset: 0x0011EED0
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&G";
			}
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x00120CF0 File Offset: 0x0011EEF0
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x00120D07 File Offset: 0x0011EF07
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			if (FilthAffinity)
			{
				return 0;
			}
			if (!Smart)
			{
				return 2;
			}
			return 3;
		}

		// Token: 0x04001D62 RID: 7522
		public new const string ID = "goo";

		// Token: 0x04001D63 RID: 7523
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "G", "Y" };
	}
}
