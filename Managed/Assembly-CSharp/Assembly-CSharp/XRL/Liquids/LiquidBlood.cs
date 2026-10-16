using System;
using System.Collections.Generic;
using System.Text;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200054F RID: 1359
	[IsLiquid]
	[Serializable]
	public class LiquidBlood : BaseLiquid
	{
		// Token: 0x06003A35 RID: 14901 RVA: 0x0011F6CC File Offset: 0x0011D8CC
		public LiquidBlood()
			: base("blood")
		{
			this.FlameTemperature = 400;
			this.VaporTemperature = 1200;
			this.Combustibility = 2;
			this.Fluidity = 5;
			this.Evaporativity = 1;
			this.Staining = 2;
			this.ThermalConductivity = 35;
			this.CirculatoryLossTerm = "bleeding";
			this.CirculatoryLossNoun = "bleed";
		}

		// Token: 0x06003A36 RID: 14902 RVA: 0x0011F734 File Offset: 0x0011D934
		public override List<string> GetColors()
		{
			return LiquidBlood.Colors;
		}

		// Token: 0x06003A37 RID: 14903 RVA: 0x0011F73B File Offset: 0x0011D93B
		public override string GetColor()
		{
			return "r";
		}

		// Token: 0x06003A38 RID: 14904 RVA: 0x0011F742 File Offset: 0x0011D942
		public override float GetValuePerDram()
		{
			return 0.25f;
		}

		// Token: 0x06003A39 RID: 14905 RVA: 0x0011F749 File Offset: 0x0011D949
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It has a metallic taste.");
			return true;
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x0011F759 File Offset: 0x0011D959
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{r|bloody}}";
		}

		// Token: 0x06003A3B RID: 14907 RVA: 0x0011F760 File Offset: 0x0011D960
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{r|bloody}}";
		}

		// Token: 0x06003A3C RID: 14908 RVA: 0x0011F767 File Offset: 0x0011D967
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{r|blood}}";
		}

		// Token: 0x06003A3D RID: 14909 RVA: 0x0011F76E File Offset: 0x0011D96E
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{r|blood}}";
		}

		// Token: 0x06003A3E RID: 14910 RVA: 0x0011F775 File Offset: 0x0011D975
		public override string GetWaterRitualName()
		{
			return "blood";
		}

		// Token: 0x06003A3F RID: 14911 RVA: 0x0011F77C File Offset: 0x0011D97C
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{r|bloody}}";
		}

		// Token: 0x06003A40 RID: 14912 RVA: 0x0011F783 File Offset: 0x0011D983
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^r" + eRender.ColorString;
			}
		}

		// Token: 0x06003A41 RID: 14913 RVA: 0x0011F7A4 File Offset: 0x0011D9A4
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&r^k";
			Liquid.ParentObject.Render.TileColor = "&r";
			Liquid.ParentObject.Render.DetailColor = "k";
		}

		// Token: 0x06003A42 RID: 14914 RVA: 0x0011F7F0 File Offset: 0x0011D9F0
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&r";
			if (Liquid.ContainsLiquid("algae"))
			{
				Render render2 = Liquid.ParentObject.Render;
				render2.ColorString += "^C";
			}
		}

		// Token: 0x06003A43 RID: 14915 RVA: 0x0011F84A File Offset: 0x0011DA4A
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "&r";
			}
		}

		// Token: 0x06003A44 RID: 14916 RVA: 0x0011F860 File Offset: 0x0011DA60
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&r";
				if (Liquid.ContainsLiquid("algae"))
				{
					eRender.DetailColor += "C";
				}
			}
		}

		// Token: 0x06003A45 RID: 14917 RVA: 0x0011F8AE File Offset: 0x0011DAAE
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "&r";
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003A46 RID: 14918 RVA: 0x0011F8CC File Offset: 0x0011DACC
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("might", 1);
		}

		// Token: 0x04001D55 RID: 7509
		public new const string ID = "blood";

		// Token: 0x04001D56 RID: 7510
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "r", "K" };
	}
}
