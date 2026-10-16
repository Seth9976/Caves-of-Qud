using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200055F RID: 1375
	[IsLiquid]
	[Serializable]
	public class LiquidSap : BaseLiquid
	{
		// Token: 0x06003B7E RID: 15230 RVA: 0x00122FE0 File Offset: 0x001211E0
		public LiquidSap()
			: base("sap")
		{
			this.FlameTemperature = 250;
			this.VaporTemperature = 1250;
			this.Combustibility = 70;
			this.Adsorbence = 25;
			this.ThermalConductivity = 40;
			this.Fluidity = 3;
			this.Evaporativity = 1;
			this.Staining = 2;
			this.Weight = 0.5;
			this.InterruptAutowalk = true;
			this.StickyWhenWet = true;
			this.StickySaveTargetBase = 1;
			this.StickySaveTargetScale = 0.1;
			this.StickyDuration = 12;
			this.StickySaveVs = "Sap Stuck Restraint";
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x00123081 File Offset: 0x00121281
		public override List<string> GetColors()
		{
			return LiquidSap.Colors;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x00123088 File Offset: 0x00121288
		public override string GetColor()
		{
			return "W";
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x0012308F File Offset: 0x0012128F
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{W|sap}}";
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x00123096 File Offset: 0x00121296
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{Y|sugary}}";
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x0012309D File Offset: 0x0012129D
		public override string GetWaterRitualName()
		{
			return "sap";
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x001230A4 File Offset: 0x001212A4
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{W|sappy}}";
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x001230AB File Offset: 0x001212AB
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{W|sappy}}";
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x001230B2 File Offset: 0x001212B2
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{W|sap}}";
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x001230B9 File Offset: 0x001212B9
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			if (Target.HasPart<Stomach>())
			{
				Target.FireEvent(Event.New("AddFood", "Satiation", "Snack"));
				Message.Compound("It's sweet to the taste.");
			}
			return true;
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x001230EC File Offset: 0x001212EC
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^W" + eRender.ColorString;
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x0012310C File Offset: 0x0012130C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&W^Y";
			Liquid.ParentObject.Render.TileColor = "&W";
			Liquid.ParentObject.Render.DetailColor = "Y";
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x00123158 File Offset: 0x00121358
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&W";
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x0012317C File Offset: 0x0012137C
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid == null || Liquid.ParentObject == null)
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&W^Y", "&W", "Y");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&W^Y", "&W", "Y");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&W^Y";
				render.TileColor = "&W";
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
					render.RenderString = "\t";
					return;
				}
				render.RenderString = "~";
			}
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00123280 File Offset: 0x00121480
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&W";
			}
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x001232A0 File Offset: 0x001214A0
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

		// Token: 0x06003B8E RID: 15246 RVA: 0x001232DB File Offset: 0x001214DB
		public override float GetValuePerDram()
		{
			return 2f;
		}

		// Token: 0x04001D7A RID: 7546
		public new const string ID = "sap";

		// Token: 0x04001D7B RID: 7547
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "W", "Y" };
	}
}
