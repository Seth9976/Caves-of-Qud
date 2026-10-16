using System;
using System.Collections.Generic;
using System.Text;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.Liquids
{
	// Token: 0x0200055E RID: 1374
	[IsLiquid]
	[Serializable]
	public class LiquidSalt : BaseLiquid
	{
		// Token: 0x06003B6E RID: 15214 RVA: 0x00122D9C File Offset: 0x00120F9C
		public LiquidSalt()
			: base("salt")
		{
			this.VaporTemperature = 200;
			this.PureElectricalConductivity = 0;
			this.MixedElectricalConductivity = 100;
			this.ThermalConductivity = 20;
			this.Fluidity = 35;
			this.Staining = 1;
			this.StainOnlyWhenPure = true;
			this.Cleansing = 1;
			this.FreezeObject3 = "Halite";
			this.FreezeObjectThreshold3 = 500;
			this.FreezeObjectVerb3 = "solidify";
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x00122E14 File Offset: 0x00121014
		public override List<string> GetColors()
		{
			return LiquidSalt.Colors;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x00122E1B File Offset: 0x0012101B
		public override string GetColor()
		{
			return "y";
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x00122E22 File Offset: 0x00121022
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{Y|salt}}";
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x00122E2C File Offset: 0x0012102C
		public override string GetAdjective(LiquidVolume Liquid)
		{
			if (Liquid == null)
			{
				return "{{Y|salty}}";
			}
			int num = Liquid.ComponentLiquids["salt"];
			if (num >= 400)
			{
				return "{{Y|salty}}";
			}
			if (num > 0)
			{
				return "{{w|brackish}}";
			}
			return null;
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x00122E6C File Offset: 0x0012106C
		public override string GetWaterRitualName()
		{
			return "salt";
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x00122E73 File Offset: 0x00121073
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			if (Liquid.IsMixed() && Liquid.Proportion("water") >= Liquid.Proportion("salt"))
			{
				return null;
			}
			return "{{Y|salty}}";
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x00122E9C File Offset: 0x0012109C
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{Y|salt-encrusted}}";
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x00122EA3 File Offset: 0x001210A3
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{Y|salt}}";
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x00122EAC File Offset: 0x001210AC
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Stomach>() && !Target.FireEvent(Event.New("AddWater", "Amount", -100 * Liquid.ComponentLiquids["salt"])))
			{
				return false;
			}
			if (!Target.HasPart<Amphibious>())
			{
				Message.Compound("Blech, it's salty!");
			}
			return true;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x00122F03 File Offset: 0x00121103
		public override string GetPreparedCookingIngredient()
		{
			return "tastyMinor";
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x00122F0A File Offset: 0x0012110A
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^Y" + eRender.ColorString;
			}
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x00122F2C File Offset: 0x0012112C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				Liquid.ParentObject.Render.RenderString = "±";
			}
			Liquid.ParentObject.Render.ColorString = "&Y^y";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "y";
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x00122F95 File Offset: 0x00121195
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Speckle/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00122FAC File Offset: 0x001211AC
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("salt", 1);
		}

		// Token: 0x04001D78 RID: 7544
		public new const string ID = "salt";

		// Token: 0x04001D79 RID: 7545
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "y", "Y" };
	}
}
