using System;
using System.Collections.Generic;
using System.Text;
using Qud.API;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000562 RID: 1378
	[IsLiquid]
	[Serializable]
	public class LiquidSunSlag : BaseLiquid
	{
		// Token: 0x06003BB9 RID: 15289 RVA: 0x00123B3C File Offset: 0x00121D3C
		public LiquidSunSlag()
			: base("sunslag")
		{
			this.VaporTemperature = 2000;
			this.VaporObject = "Steam";
			this.Combustibility = 30;
			this.PureElectricalConductivity = 100;
			this.MixedElectricalConductivity = 100;
			this.ThermalConductivity = 40;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 3;
			this.ConversionProduct = "lava";
		}

		// Token: 0x06003BBA RID: 15290 RVA: 0x00123BAB File Offset: 0x00121DAB
		public override List<string> GetColors()
		{
			return LiquidSunSlag.Colors;
		}

		// Token: 0x06003BBB RID: 15291 RVA: 0x00123BB2 File Offset: 0x00121DB2
		public override string GetColor()
		{
			return "Y";
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x00123BB9 File Offset: 0x00121DB9
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{sunslag|sunslag}}";
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00123BC0 File Offset: 0x00121DC0
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{radiant|radiant}}";
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x00123BC7 File Offset: 0x00121DC7
		public override string GetWaterRitualName()
		{
			return "sunslag";
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00123BCE File Offset: 0x00121DCE
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{radiant|radiant}}";
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x00123BD5 File Offset: 0x00121DD5
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{radiant|radiant}}";
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x00123BDC File Offset: 0x00121DDC
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{sunslag|sunslag}}";
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x00123BE4 File Offset: 0x00121DE4
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Liquid.IsPure() && Target != null)
			{
				if (Target.GetIntProperty("SunslagDramsDrank", 0) >= 10)
				{
					Message.Compound("Brightness burns your mouth, but you cannot be roused any higher.");
				}
				else if (!Target.HasStat("Speed"))
				{
					Message.Compound("Brightness burns your mouth.");
				}
				else
				{
					int num = DrinkMagnifier.Magnify(Target, 1);
					Target.Statistics["Speed"].BaseValue += num;
					Message.Compound("Brightness burns your mouth and inspirits you.\n\nYour Quickness is permanently increased by {{rules|").Append(num.ToString()).Append("}}.");
					int num2 = Target.GetIntProperty("SunslagDramsDrank", 0) + 1;
					Target.SetIntProperty("SunslagDramsDrank", num2, false);
					if (Target.IsPlayer())
					{
						if (num2 > Achievement.SWOLLEN_BULB.Progress.Value)
						{
							Achievement.SWOLLEN_BULB.Progress.SetValue(num2);
						}
						JournalAPI.AddAccomplishment("You drank the bright cream of the Palladium Reef and were quickened.", "See =name= crack apart the bones of poleis and drink their marrow! Be quickened, our Saad, by the civic lifeblood!", string.Concat(new string[]
						{
							"While traveling through ",
							The.Player.CurrentZone.GetTerrainDisplayName(true),
							", =name= stopped at a tavern in ",
							JournalAPI.GetLandmarkNearestPlayer().Text,
							". There ",
							The.Player.GetPronounProvider(false).Subjective,
							" won a bet and triumphantly drank a dram of sunslag. ",
							The.Player.GetPronounProvider(false).CapitalizedSubjective,
							" ",
							The.Player.GetVerb("were", true, false, false, true, false),
							" quickened and left the tavern at an untrackable pace."
						}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
					}
				}
			}
			return true;
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x00123D90 File Offset: 0x00121F90
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

		// Token: 0x06003BC4 RID: 15300 RVA: 0x00123DCB File Offset: 0x00121FCB
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^W" + eRender.ColorString;
			}
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x00123DEC File Offset: 0x00121FEC
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&Y^W";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "W";
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x00123E38 File Offset: 0x00122038
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&W";
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00123E5C File Offset: 0x0012205C
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^W", "&Y", "W");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^W", "&Y", "W");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^W";
				render.TileColor = "&Y";
				render.DetailColor = "W";
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

		// Token: 0x06003BC8 RID: 15304 RVA: 0x00123F54 File Offset: 0x00122154
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&W";
			}
		}

		// Token: 0x06003BC9 RID: 15305 RVA: 0x00123F74 File Offset: 0x00122174
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Paisley/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x00123F8B File Offset: 0x0012218B
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("stars", 1);
		}

		// Token: 0x06003BCB RID: 15307 RVA: 0x00123F99 File Offset: 0x00122199
		public override float GetValuePerDram()
		{
			return 1000f;
		}

		// Token: 0x04001D80 RID: 7552
		public new const string ID = "sunslag";

		// Token: 0x04001D81 RID: 7553
		[NonSerialized]
		public static List<string> Colors = new List<string>(3) { "Y", "W", "R" };
	}
}
