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
	// Token: 0x0200055B RID: 1371
	[IsLiquid]
	[Serializable]
	public class LiquidOoze : BaseLiquid
	{
		// Token: 0x06003B30 RID: 15152 RVA: 0x001220E8 File Offset: 0x001202E8
		public LiquidOoze()
			: base("ooze")
		{
			this.FlameTemperature = 500;
			this.VaporTemperature = 150;
			this.VaporObject = "Miasma";
			this.Combustibility = 15;
			this.ThermalConductivity = 30;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 1;
			this.CirculatoryLossTerm = "oozing";
			this.CirculatoryLossNoun = "ooze";
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x0012215D File Offset: 0x0012035D
		public override List<string> GetColors()
		{
			return LiquidOoze.Colors;
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x00122164 File Offset: 0x00120364
		public override string GetColor()
		{
			return "K";
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x0012216B File Offset: 0x0012036B
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{K|black ooze}}";
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x00122172 File Offset: 0x00120372
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{K|oozing}}";
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x00122179 File Offset: 0x00120379
		public override string GetWaterRitualName()
		{
			return "ooze";
		}

		// Token: 0x06003B36 RID: 15158 RVA: 0x00122180 File Offset: 0x00120380
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{K|oozing}}";
		}

		// Token: 0x06003B37 RID: 15159 RVA: 0x00122187 File Offset: 0x00120387
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{K|oozing}}";
		}

		// Token: 0x06003B38 RID: 15160 RVA: 0x0012218E File Offset: 0x0012038E
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{K|ooze}}";
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x00122195 File Offset: 0x00120395
		public override string GetPreparedCookingIngredient()
		{
			return "selfGlotrot";
		}

		// Token: 0x06003B3A RID: 15162 RVA: 0x0012219C File Offset: 0x0012039C
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's repulsive!");
			bool flag;
			if (Target.CurrentZone.Z % 2 == 0)
			{
				flag = Target.ApplyEffect(new IronshankOnset(), null);
			}
			else
			{
				flag = Target.ApplyEffect(new GlotrotOnset(), null);
			}
			if (flag)
			{
				Message.Compound("You feel sick!");
			}
			return true;
		}

		// Token: 0x06003B3B RID: 15163 RVA: 0x001221F4 File Offset: 0x001203F4
		public override void ObjectGoingProne(LiquidVolume Liquid, GameObject GO, bool UsePopups)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (GO.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(GO, "Putrid ooze splashes into your mouth. You gag at the awful taste.", ' ', false, UsePopups, false, null, GO);
			}
			GO.Splatter("&w.");
			if (!GO.MakeSave("Toughness", 13, null, null, "Ooze Disease", false, false, false, false, Liquid.ParentObject))
			{
				if (GO.Physics.CurrentCell.ParentZone.Z % 2 == 0)
				{
					GO.ApplyEffect(new IronshankOnset(), null);
					return;
				}
				GO.ApplyEffect(new GlotrotOnset(), null);
			}
		}

		// Token: 0x06003B3C RID: 15164 RVA: 0x00122288 File Offset: 0x00120488
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

		// Token: 0x06003B3D RID: 15165 RVA: 0x001222C3 File Offset: 0x001204C3
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^k" + eRender.ColorString;
			}
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x001222E4 File Offset: 0x001204E4
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&y^k";
			Liquid.ParentObject.Render.TileColor = "&y";
			Liquid.ParentObject.Render.DetailColor = "k";
		}

		// Token: 0x06003B3F RID: 15167 RVA: 0x00122330 File Offset: 0x00120530
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&k";
		}

		// Token: 0x06003B40 RID: 15168 RVA: 0x00122354 File Offset: 0x00120554
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&y^k", "&y", "k");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&y^k", "&y", "k");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&y^k";
				render.TileColor = "&y";
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
					render.RenderString = "\t";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003B41 RID: 15169 RVA: 0x0012244C File Offset: 0x0012064C
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&k";
			}
		}

		// Token: 0x06003B42 RID: 15170 RVA: 0x0012246C File Offset: 0x0012066C
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Splotchy/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x00122483 File Offset: 0x00120683
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

		// Token: 0x04001D6E RID: 7534
		public new const string ID = "ooze";

		// Token: 0x04001D6F RID: 7535
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "K", "y" };
	}
}
