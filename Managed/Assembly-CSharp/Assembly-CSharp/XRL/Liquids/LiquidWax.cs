using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000565 RID: 1381
	[IsLiquid]
	[Serializable]
	public class LiquidWax : BaseLiquid
	{
		// Token: 0x06003C0F RID: 15375 RVA: 0x00125E7C File Offset: 0x0012407C
		public LiquidWax()
			: base("wax")
		{
			this.FlameTemperature = 300;
			this.VaporTemperature = 2000;
			this.Temperature = 100;
			this.Combustibility = 65;
			this.Adsorbence = 25;
			this.PureElectricalConductivity = 0;
			this.MixedElectricalConductivity = 0;
			this.ThermalConductivity = 40;
			this.Fluidity = 7;
			this.Cleansing = 3;
			this.Weight = 0.5;
			this.InterruptAutowalk = true;
			this.FreezeObject1 = "Wax Nodule";
			this.FreezeObjectThreshold1 = 1;
			this.FreezeObjectVerb1 = "congeal";
			this.FreezeObject3 = "Wax Block";
			this.FreezeObjectThreshold3 = 500;
			this.FreezeObjectVerb3 = "solidify";
			this.StickyWhenWet = true;
			this.StickySaveTargetBase = 1;
			this.StickySaveTargetScale = 0.1;
			this.StickyDuration = 12;
			this.StickySaveVs = "Wax Stuck Restraint";
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x00125F6A File Offset: 0x0012416A
		public override List<string> GetColors()
		{
			return LiquidWax.Colors;
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00125F71 File Offset: 0x00124171
		public override string GetColor()
		{
			return "Y";
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00125F78 File Offset: 0x00124178
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{Y|molten wax}}";
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00125F7F File Offset: 0x0012417F
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{Y|waxen}}";
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x00125F86 File Offset: 0x00124186
		public override string GetWaterRitualName()
		{
			return "wax";
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x00125F8D File Offset: 0x0012418D
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{Y|waxy}}";
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00125F94 File Offset: 0x00124194
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{Y|waxy}}";
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x00125F9B File Offset: 0x0012419B
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{Y|wax}}";
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x00125FA4 File Offset: 0x001241A4
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Stomach>())
			{
				Message.Compound("It's hot and disgusting.");
				Target.TemperatureChange(100, Target, false, false, false, false, 0, null, null);
			}
			return true;
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00125FE8 File Offset: 0x001241E8
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^y" + eRender.ColorString;
			}
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x00126008 File Offset: 0x00124208
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&y^Y";
			Liquid.ParentObject.Render.TileColor = "&y";
			Liquid.ParentObject.Render.DetailColor = "Y";
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x00126054 File Offset: 0x00124254
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&y";
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x00126078 File Offset: 0x00124278
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^y", "&Y", "y");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&Y^y", "&Y", "y");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^y";
				render.TileColor = "&Y";
				render.DetailColor = "y";
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

		// Token: 0x06003C1D RID: 15389 RVA: 0x00126170 File Offset: 0x00124370
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&y";
			}
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x00126190 File Offset: 0x00124390
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&y";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x001261CB File Offset: 0x001243CB
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x001261E2 File Offset: 0x001243E2
		public override float GetValuePerDram()
		{
			return 0f;
		}

		// Token: 0x04001D8C RID: 7564
		public new const string ID = "wax";

		// Token: 0x04001D8D RID: 7565
		[NonSerialized]
		public static List<string> Colors = new List<string>(1) { "Y" };
	}
}
