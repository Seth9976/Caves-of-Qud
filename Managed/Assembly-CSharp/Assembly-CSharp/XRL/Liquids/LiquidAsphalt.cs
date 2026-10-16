using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200054D RID: 1357
	[IsLiquid]
	[Serializable]
	public class LiquidAsphalt : BaseLiquid
	{
		// Token: 0x060039DC RID: 14812 RVA: 0x0011E584 File Offset: 0x0011C784
		public LiquidAsphalt()
			: base("asphalt")
		{
			this.FlameTemperature = 240;
			this.VaporTemperature = 1240;
			this.Combustibility = 75;
			this.Adsorbence = 25;
			this.PureElectricalConductivity = 10;
			this.MixedElectricalConductivity = 10;
			this.ThermalConductivity = 35;
			this.Fluidity = 1;
			this.Staining = 1;
			this.Weight = 0.5;
			this.InterruptAutowalk = true;
			this.StickyWhenWet = true;
			this.StickySaveTargetBase = 4;
			this.StickySaveTargetScale = 0.1;
			this.StickyDuration = 12;
			this.StickySaveVs = "Asphalt Stuck Restraint";
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x0011E62E File Offset: 0x0011C82E
		public override List<string> GetColors()
		{
			return LiquidAsphalt.Colors;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x0011E635 File Offset: 0x0011C835
		public override string GetColor()
		{
			return "K";
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x0011E63C File Offset: 0x0011C83C
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{K|asphalt}}";
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x0011E643 File Offset: 0x0011C843
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{K|tarry}}";
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x0011E64A File Offset: 0x0011C84A
		public override string GetWaterRitualName()
		{
			return "tar";
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x0011E651 File Offset: 0x0011C851
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{K|tarry}}";
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x0011E658 File Offset: 0x0011C858
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{K|tarred}}";
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x0011E65F File Offset: 0x0011C85F
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{K|tar}}";
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x0011E666 File Offset: 0x0011C866
		public override string GetPreparedCookingIngredient()
		{
			return "stabilityMinor";
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x0011E670 File Offset: 0x0011C870
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("{{K|It burns!}}");
			Target.TemperatureChange(500, Target, false, false, false, false, 0, null, null);
			Damage damage = new Damage(Stat.Roll((Liquid.Proportion("asphalt") / 100 + 1).ToString() + "d6", null));
			Event @event = Event.New("TakeDamage", 0, 0, 0);
			@event.AddParameter("Damage", damage);
			@event.AddParameter("Owner", Liquid);
			@event.AddParameter("Attacker", Liquid);
			@event.AddParameter("Message", "from {{K|drinking asphalt}}!");
			Target.FireEvent(@event);
			ExitInterface = true;
			return true;
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x0011E72F File Offset: 0x0011C92F
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^K" + eRender.ColorString;
			}
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x0011E750 File Offset: 0x0011C950
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&k^K";
			Liquid.ParentObject.Render.TileColor = "&k";
			Liquid.ParentObject.Render.DetailColor = "K";
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x0011E79C File Offset: 0x0011C99C
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&K";
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x0011E7C0 File Offset: 0x0011C9C0
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&k^K", "&k", "K");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&k^K", "&k", "K");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&k^K";
				render.TileColor = "&k";
				render.DetailColor = "K";
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

		// Token: 0x060039EB RID: 14827 RVA: 0x0011E8B8 File Offset: 0x0011CAB8
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&K";
			}
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x0011E8D8 File Offset: 0x0011CAD8
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

		// Token: 0x060039ED RID: 14829 RVA: 0x0011E913 File Offset: 0x0011CB13
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Speckle/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x04001D17 RID: 7447
		public new const string ID = "asphalt";

		// Token: 0x04001D18 RID: 7448
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "K", "y" };
	}
}
