using System;
using System.Collections.Generic;
using System.Text;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200054C RID: 1356
	[IsLiquid]
	[Serializable]
	public class LiquidAlgae : BaseLiquid
	{
		// Token: 0x060039CB RID: 14795 RVA: 0x0011E320 File Offset: 0x0011C520
		public LiquidAlgae()
			: base("algae")
		{
			this.VaporTemperature = 200;
			this.ThermalConductivity = 20;
			this.Fluidity = 35;
			this.Evaporativity = 1;
			this.Staining = 1;
			this.StainOnlyWhenPure = true;
			this.Cleansing = 1;
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x0011E36F File Offset: 0x0011C56F
		public override List<string> GetColors()
		{
			return LiquidAlgae.Colors;
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x0011E376 File Offset: 0x0011C576
		public override string GetColor()
		{
			return "g";
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x0011E37D File Offset: 0x0011C57D
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{g|algae}}";
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x0011E384 File Offset: 0x0011C584
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{g|algal}}";
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x0011E38B File Offset: 0x0011C58B
		public override string GetWaterRitualName()
		{
			return "algae";
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x0011E392 File Offset: 0x0011C592
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			if (Liquid.IsMixed() && Liquid.Proportion("water") >= Liquid.Proportion("algae"))
			{
				return null;
			}
			return "{{g|algal}}";
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x0011E3BB File Offset: 0x0011C5BB
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{g|algae-covered}}";
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x0011E3C2 File Offset: 0x0011C5C2
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{g|algae}}";
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x0011E3CC File Offset: 0x0011C5CC
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Stomach>())
			{
				Target.FireEvent(Event.New("AddFood", "Satiation", "Snack"));
				if (!Target.FireEvent(Event.New("AddWater", "Amount", -50 * Liquid.ComponentLiquids["algae"])))
				{
					return false;
				}
				Message.Compound("The brine stings your mouth and fills your breath with lake air.");
			}
			return true;
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x0011E436 File Offset: 0x0011C636
		public override string GetPreparedCookingIngredient()
		{
			return "plantMinor";
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x0011E43D File Offset: 0x0011C63D
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^g" + eRender.ColorString;
			}
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x0011E45D File Offset: 0x0011C65D
		public override void RenderBackgroundSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "^C";
			}
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x0011E480 File Offset: 0x0011C680
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				Liquid.ParentObject.Render.RenderString = "±";
			}
			Liquid.ParentObject.Render.ColorString = "&g^C";
			Liquid.ParentObject.Render.TileColor = "&g";
			Liquid.ParentObject.Render.DetailColor = "C";
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x0011E4EC File Offset: 0x0011C6EC
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			if (Liquid.GetPrimaryLiquidID() == "water")
			{
				Liquid.ParentObject.Render.ColorString = "&g^C";
				Liquid.ParentObject.Render.TileColor = "&g";
				Liquid.ParentObject.Render.DetailColor = "C";
			}
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x0011E54A File Offset: 0x0011C74A
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Paisley/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x04001D15 RID: 7445
		public new const string ID = "algae";

		// Token: 0x04001D16 RID: 7446
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "g", "C" };
	}
}
