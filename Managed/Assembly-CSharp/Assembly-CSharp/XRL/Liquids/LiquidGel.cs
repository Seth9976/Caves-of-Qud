using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000554 RID: 1364
	[IsLiquid]
	[Serializable]
	public class LiquidGel : BaseLiquid
	{
		// Token: 0x06003A9C RID: 15004 RVA: 0x00120674 File Offset: 0x0011E874
		public LiquidGel()
			: base("gel")
		{
			this.PureElectricalConductivity = 100;
			this.MixedElectricalConductivity = 100;
			this.ThermalConductivity = 70;
			this.Fluidity = 5;
			this.Evaporativity = 1;
			this.Cleansing = 1;
			this.SlipperyWhenWet = true;
			this.SlipperyWhenFrozen = true;
			this.SlipperySaveTargetBase = 5;
			this.SlipperySaveTargetScale = 0.3;
			this.SlipperySaveVs = "Gel Slip Move";
			this.SlipperyMessage = "{{Y|=subject.T= =verb:slip= on the gel!}}";
			this.SlipperyParticle = "&Y\u001a";
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x001206FE File Offset: 0x0011E8FE
		public override List<string> GetColors()
		{
			return LiquidGel.Colors;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x00120705 File Offset: 0x0011E905
		public override string GetColor()
		{
			return "Y";
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x0012070C File Offset: 0x0011E90C
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{Y|gel}}";
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x00120713 File Offset: 0x0011E913
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{Y|unctuous}}";
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x0012071A File Offset: 0x0011E91A
		public override string GetWaterRitualName()
		{
			return "gel";
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x00120721 File Offset: 0x0011E921
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's very greasy.");
			return true;
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x00120731 File Offset: 0x0011E931
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{Y|unctuous}}";
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x00120738 File Offset: 0x0011E938
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{Y|unctuous}}";
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x0012073F File Offset: 0x0011E93F
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{Y|gel}}";
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x00120746 File Offset: 0x0011E946
		public override float GetValuePerDram()
		{
			return 0.5f;
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x00120750 File Offset: 0x0011E950
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&Y";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x0012078B File Offset: 0x0011E98B
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^Y" + eRender.ColorString;
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x001207AC File Offset: 0x0011E9AC
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&y^Y";
			Liquid.ParentObject.Render.TileColor = "&y";
			Liquid.ParentObject.Render.DetailColor = "Y";
		}

		// Token: 0x06003AAA RID: 15018 RVA: 0x001207F8 File Offset: 0x0011E9F8
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&Y";
		}

		// Token: 0x06003AAB RID: 15019 RVA: 0x0012081C File Offset: 0x0011EA1C
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&y^C", "&y", "C");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&y^C", "&y", "C");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&y^Y";
				render.TileColor = "&y";
				render.DetailColor = "Y";
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

		// Token: 0x06003AAC RID: 15020 RVA: 0x00120914 File Offset: 0x0011EB14
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&Y";
			}
		}

		// Token: 0x04001D60 RID: 7520
		public new const string ID = "gel";

		// Token: 0x04001D61 RID: 7521
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "Y", "y" };
	}
}
