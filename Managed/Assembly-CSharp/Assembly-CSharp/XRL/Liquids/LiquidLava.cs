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
	// Token: 0x02000558 RID: 1368
	[IsLiquid]
	[Serializable]
	public class LiquidLava : BaseLiquid
	{
		// Token: 0x06003AEA RID: 15082 RVA: 0x0012138C File Offset: 0x0011F58C
		public LiquidLava()
			: base("lava")
		{
			this.VaporTemperature = 10000;
			this.Temperature = 1000;
			this.Fluidity = 15;
			this.Cleansing = 20;
			this.Weight = 0.5;
			this.PureElectricalConductivity = 90;
			this.MixedElectricalConductivity = 90;
			this.InterruptAutowalk = true;
			this.ConsiderDangerousToContact = true;
			this.ConsiderDangerousToDrink = true;
			this.Glows = true;
			this.FreezeObject1 = "SmallBoulder";
			this.FreezeObjectThreshold1 = 1;
			this.FreezeObjectVerb1 = "solidify";
			this.FreezeObject2 = "MediumBoulder";
			this.FreezeObjectThreshold2 = 100;
			this.FreezeObjectVerb2 = "solidify";
			this.FreezeObject3 = "Shale";
			this.FreezeObjectThreshold3 = 400;
			this.FreezeObjectVerb3 = "solidify";
		}

		// Token: 0x06003AEB RID: 15083 RVA: 0x00121461 File Offset: 0x0011F661
		public override List<string> GetColors()
		{
			return LiquidLava.Colors;
		}

		// Token: 0x06003AEC RID: 15084 RVA: 0x00121468 File Offset: 0x0011F668
		public override string GetColor()
		{
			return "R";
		}

		// Token: 0x06003AED RID: 15085 RVA: 0x0012146F File Offset: 0x0011F66F
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{lava|lava}}";
		}

		// Token: 0x06003AEE RID: 15086 RVA: 0x00121476 File Offset: 0x0011F676
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{lava|magmatic}}";
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x0012147D File Offset: 0x0011F67D
		public override string GetWaterRitualName()
		{
			return "lava";
		}

		// Token: 0x06003AF0 RID: 15088 RVA: 0x00121484 File Offset: 0x0011F684
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{lava|magmatic}}";
		}

		// Token: 0x06003AF1 RID: 15089 RVA: 0x0012148B File Offset: 0x0011F68B
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{lava|lava-covered}}";
		}

		// Token: 0x06003AF2 RID: 15090 RVA: 0x00121492 File Offset: 0x0011F692
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{lava|lava}}";
		}

		// Token: 0x06003AF3 RID: 15091 RVA: 0x00121499 File Offset: 0x0011F699
		public override string GetPreparedCookingIngredient()
		{
			return "heatMinor";
		}

		// Token: 0x06003AF4 RID: 15092 RVA: 0x001214A0 File Offset: 0x0011F6A0
		public override bool SafeContainer(GameObject GO)
		{
			return GO.Physics == null || GO.Physics.FlameTemperature > this.Temperature;
		}

		// Token: 0x06003AF5 RID: 15093 RVA: 0x001214BF File Offset: 0x0011F6BF
		public override float GetValuePerDram()
		{
			return 50f;
		}

		// Token: 0x06003AF6 RID: 15094 RVA: 0x001214C6 File Offset: 0x0011F6C6
		public override bool Vaporized(LiquidVolume Liquid, GameObject gameObject)
		{
			return false;
		}

		// Token: 0x06003AF7 RID: 15095 RVA: 0x001214CC File Offset: 0x0011F6CC
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Achievement.DRINK_LAVA.Unlock();
			JournalAPI.AddAccomplishment("Inexplicably, you drank lava.", "For reasons kept secret by the sphinx of subjective experience, =name= drank the forbidden tea.", string.Concat(new string[]
			{
				"While traveling through ",
				The.Player.CurrentZone.GetTerrainDisplayName(true),
				", =name= stopped at a tavern in ",
				JournalAPI.GetLandmarkNearestPlayer().Text,
				". There ",
				The.Player.GetPronounProvider(false).Subjective,
				" lost a bet and bashfully drank a dram of lava. Miraculously, ",
				The.Player.GetPronounProvider(false).Subjective,
				" left the tavern unharmed."
			}), null, "general", MuralCategory.BodyExperienceNeutral, MuralWeight.Medium, null, -1L, true);
			Message.Compound("{{lava|IT BURNS!}}");
			Target.TemperatureChange(this.Temperature, Target, false, false, false, false, 0, null, null);
			string text = (Liquid.Proportion("lava") / 100 + 1).ToString() + "d100";
			Target.TakeDamage(text.Roll(), "from {{lava|drinking lava}}!", "Heat", null, null, Target, Liquid.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
			ExitInterface = true;
			return true;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x00121604 File Offset: 0x0011F804
		public override void BeforeRender(LiquidVolume Liquid)
		{
			if ((!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed) && (Liquid.Primary == "lava" || Liquid.Secondary == "lava" || Liquid.IsPureLiquid("lava", false)))
			{
				Liquid.AddLight(0, LightLevel.Light, false);
			}
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x00121660 File Offset: 0x0011F860
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^R" + eRender.ColorString;
			}
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x00121680 File Offset: 0x0011F880
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&W^R";
			Liquid.ParentObject.Render.TileColor = "&W";
			Liquid.ParentObject.Render.DetailColor = "R";
		}

		// Token: 0x06003AFB RID: 15099 RVA: 0x001216CC File Offset: 0x0011F8CC
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&R";
		}

		// Token: 0x06003AFC RID: 15100 RVA: 0x001216F0 File Offset: 0x0011F8F0
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&W^R", "&W", "R");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&W^R", "&W", "R");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				if (num < 15)
				{
					render.RenderString = "÷";
					render.ColorString = "&W^R";
					render.TileColor = "&W";
					render.DetailColor = "R";
					return;
				}
				if (num < 30)
				{
					render.RenderString = "~";
					render.ColorString = "&W^r";
					render.TileColor = "&W";
					render.DetailColor = "r";
					return;
				}
				if (num < 45)
				{
					render.RenderString = "\t";
					render.ColorString = "&W^R";
					render.TileColor = "&W";
					render.DetailColor = "R";
					return;
				}
				render.RenderString = "~";
				render.ColorString = "&W^R";
				render.TileColor = "&W";
				render.DetailColor = "R";
			}
		}

		// Token: 0x06003AFD RID: 15101 RVA: 0x0012184E File Offset: 0x0011FA4E
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&r";
			}
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x0012186E File Offset: 0x0011FA6E
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003AFF RID: 15103 RVA: 0x00121888 File Offset: 0x0011FA88
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			if (Smart && GO != null)
			{
				Uncacheable = true;
				int num = GO.Stat("HeatResistance", 0);
				if (num > 0)
				{
					bool flag;
					if (Liquid.IsSwimmingDepth())
					{
						flag = !HasFlammableEquipmentOrInventoryEvent.Check(GO, this.Temperature);
					}
					else
					{
						flag = !HasFlammableEquipmentEvent.Check(GO, this.Temperature);
					}
					if (flag)
					{
						if (num >= 100)
						{
							return 0;
						}
						return Math.Min(Math.Max(40 + 59 * (100 - num) / 100, 0), 99);
					}
				}
			}
			return 99;
		}

		// Token: 0x06003B00 RID: 15104 RVA: 0x00121901 File Offset: 0x0011FB01
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("might", 1);
		}

		// Token: 0x04001D68 RID: 7528
		public new const string ID = "lava";

		// Token: 0x04001D69 RID: 7529
		[NonSerialized]
		public static List<string> Colors = new List<string>(3) { "R", "r", "W" };
	}
}
