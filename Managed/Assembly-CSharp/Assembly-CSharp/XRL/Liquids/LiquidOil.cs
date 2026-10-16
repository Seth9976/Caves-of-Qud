using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200055A RID: 1370
	[IsLiquid]
	[Serializable]
	public class LiquidOil : BaseLiquid
	{
		// Token: 0x06003B1E RID: 15134 RVA: 0x00121E14 File Offset: 0x00120014
		public LiquidOil()
			: base("oil")
		{
			this.FlameTemperature = 250;
			this.VaporTemperature = 2000;
			this.Combustibility = 90;
			this.PureElectricalConductivity = 0;
			this.MixedElectricalConductivity = 0;
			this.ThermalConductivity = 40;
			this.Fluidity = 25;
			this.Staining = 1;
			this.Cleansing = 1;
			this.SlipperyWhenWet = true;
			this.SlipperyWhenFrozen = true;
			this.SlipperySaveTargetBase = 5;
			this.SlipperySaveTargetScale = 0.3;
			this.SlipperySaveVs = "Oil Slip Move";
			this.SlipperyMessage = "{{C|=subject.T= =verb:slip= on the oil!}}";
			this.SlipperyParticle = "&C\u001a";
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x00121EBB File Offset: 0x001200BB
		public override List<string> GetColors()
		{
			return LiquidOil.Colors;
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x00121EC2 File Offset: 0x001200C2
		public override string GetColor()
		{
			return "K";
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x00121EC9 File Offset: 0x001200C9
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{K|oil}}";
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x00121ED0 File Offset: 0x001200D0
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{K|oily}}";
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x00121ED7 File Offset: 0x001200D7
		public override string GetWaterRitualName()
		{
			return "oil";
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x00121EDE File Offset: 0x001200DE
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{K|oily}}";
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x00121EE5 File Offset: 0x001200E5
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{K|oily}}";
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x00121EEC File Offset: 0x001200EC
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{K|oil}}";
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x00121EF3 File Offset: 0x001200F3
		public override float GetValuePerDram()
		{
			return 3f;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x00121EFA File Offset: 0x001200FA
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("{{K|Disgusting!}}");
			return true;
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x00121F0A File Offset: 0x0012010A
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^K" + eRender.ColorString;
			}
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x00121F2C File Offset: 0x0012012C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&Y^k";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "k";
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x00121F78 File Offset: 0x00120178
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&k";
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x00121F9C File Offset: 0x0012019C
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^k", "&Y", "k");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^k";
				render.TileColor = "&Y";
				render.DetailColor = "k";
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
					render.RenderString = "÷";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x00122066 File Offset: 0x00120266
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&K";
			}
		}

		// Token: 0x06003B2E RID: 15150 RVA: 0x00122088 File Offset: 0x00120288
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

		// Token: 0x04001D6C RID: 7532
		public new const string ID = "oil";

		// Token: 0x04001D6D RID: 7533
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "K", "Y" };
	}
}
