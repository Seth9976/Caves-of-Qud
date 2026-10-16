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
	// Token: 0x02000550 RID: 1360
	[IsLiquid]
	[Serializable]
	public class LiquidBrainBrine : BaseLiquid
	{
		// Token: 0x06003A48 RID: 14920 RVA: 0x0011F900 File Offset: 0x0011DB00
		public LiquidBrainBrine()
			: base("brainbrine")
		{
			this.VaporTemperature = 2000;
			this.VaporObject = "ConfusionGas";
			this.Combustibility = 30;
			this.ThermalConductivity = 40;
			this.Fluidity = 10;
			this.Evaporativity = 1;
			this.Staining = 3;
			this.ConversionProduct = "algae";
		}

		// Token: 0x06003A49 RID: 14921 RVA: 0x0011F95F File Offset: 0x0011DB5F
		public override List<string> GetColors()
		{
			return LiquidBrainBrine.Colors;
		}

		// Token: 0x06003A4A RID: 14922 RVA: 0x0011F966 File Offset: 0x0011DB66
		public override string GetColor()
		{
			return "g";
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x0011F96D File Offset: 0x0011DB6D
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{brainbrine|brain brine}}";
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x0011F974 File Offset: 0x0011DB74
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{nervous|nervous}}";
		}

		// Token: 0x06003A4D RID: 14925 RVA: 0x0011F97B File Offset: 0x0011DB7B
		public override string GetWaterRitualName()
		{
			return "brain brine";
		}

		// Token: 0x06003A4E RID: 14926 RVA: 0x0011F982 File Offset: 0x0011DB82
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{nervous|nervous}}";
		}

		// Token: 0x06003A4F RID: 14927 RVA: 0x0011F989 File Offset: 0x0011DB89
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{nervous|nervous}}";
		}

		// Token: 0x06003A50 RID: 14928 RVA: 0x0011F990 File Offset: 0x0011DB90
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{brainbrine|brain-brine}}";
		}

		// Token: 0x06003A51 RID: 14929 RVA: 0x0011F998 File Offset: 0x0011DB98
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Liquid.IsPure())
			{
				Message.Compound("It tastes salty. Your mind starts to swim.");
				int num = Stat.Random(20, 30);
				if (Target != null)
				{
					Target.ApplyEffect(new Confused(num, 1, 3, "Confusion"), null);
					Target.ApplyEffect(new BrainBrineCurse(), null);
					if (Target.IsPlayer())
					{
						Achievement.DRAMS_BRAIN_BRINE_20.Progress.Increment(Volume);
					}
				}
			}
			return true;
		}

		// Token: 0x06003A52 RID: 14930 RVA: 0x0011FA04 File Offset: 0x0011DC04
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&W";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003A53 RID: 14931 RVA: 0x0011FA3F File Offset: 0x0011DC3F
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^W" + eRender.ColorString;
			}
		}

		// Token: 0x06003A54 RID: 14932 RVA: 0x0011FA60 File Offset: 0x0011DC60
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&g^W";
			Liquid.ParentObject.Render.TileColor = "&g";
			Liquid.ParentObject.Render.DetailColor = "W";
		}

		// Token: 0x06003A55 RID: 14933 RVA: 0x0011FAAC File Offset: 0x0011DCAC
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&W";
		}

		// Token: 0x06003A56 RID: 14934 RVA: 0x0011FAD0 File Offset: 0x0011DCD0
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&g^W", "&g", "W");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&g^W", "&g", "W");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&g^W";
				render.TileColor = "&g";
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

		// Token: 0x06003A57 RID: 14935 RVA: 0x0011FBC8 File Offset: 0x0011DDC8
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&W";
			}
		}

		// Token: 0x06003A58 RID: 14936 RVA: 0x0011FBE8 File Offset: 0x0011DDE8
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Dot/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003A59 RID: 14937 RVA: 0x0011FBFF File Offset: 0x0011DDFF
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("scholarship", 1);
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x0011FC0D File Offset: 0x0011DE0D
		public override float GetValuePerDram()
		{
			return 1233f;
		}

		// Token: 0x04001D57 RID: 7511
		public new const string ID = "brainbrine";

		// Token: 0x04001D58 RID: 7512
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "g", "W" };
	}
}
