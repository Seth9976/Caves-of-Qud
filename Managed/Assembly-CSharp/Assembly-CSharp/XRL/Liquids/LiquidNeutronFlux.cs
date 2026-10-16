using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000559 RID: 1369
	[IsLiquid]
	[Serializable]
	public class LiquidNeutronFlux : BaseLiquid
	{
		// Token: 0x06003B02 RID: 15106 RVA: 0x00121940 File Offset: 0x0011FB40
		public LiquidNeutronFlux()
			: base("neutronflux")
		{
			this.VaporTemperature = 10000;
			this.FreezeTemperature = -100;
			this.BrittleTemperature = -250;
			this.PureElectricalConductivity = 0;
			this.MixedElectricalConductivity = 0;
			this.ThermalConductivity = 0;
			this.Fluidity = 100;
			this.Evaporativity = 100;
			this.Cleansing = 100;
			this.Weight = 2.5;
			this.InterruptAutowalk = true;
			this.ConsiderDangerousToContact = true;
			this.ConsiderDangerousToDrink = true;
			this.Glows = true;
			this.CirculatoryLossTerm = "fluxing";
			this.CirculatoryLossNoun = "fluxation";
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x001219E4 File Offset: 0x0011FBE4
		public override List<string> GetColors()
		{
			return LiquidNeutronFlux.Colors;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x001219EB File Offset: 0x0011FBEB
		public override string GetColor()
		{
			return "y";
		}

		// Token: 0x06003B05 RID: 15109 RVA: 0x001219F2 File Offset: 0x0011FBF2
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{neutronic|neutron}} {{Y|flux}}";
		}

		// Token: 0x06003B06 RID: 15110 RVA: 0x001219F9 File Offset: 0x0011FBF9
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{neutronic|neutronic}}";
		}

		// Token: 0x06003B07 RID: 15111 RVA: 0x00121A00 File Offset: 0x0011FC00
		public override string GetWaterRitualName()
		{
			return "flux";
		}

		// Token: 0x06003B08 RID: 15112 RVA: 0x00121A07 File Offset: 0x0011FC07
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{neutronic|neutronic}}";
		}

		// Token: 0x06003B09 RID: 15113 RVA: 0x00121A0E File Offset: 0x0011FC0E
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{neutronic|neutral}}";
		}

		// Token: 0x06003B0A RID: 15114 RVA: 0x00121A15 File Offset: 0x0011FC15
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{neutronic|flux}}";
		}

		// Token: 0x06003B0B RID: 15115 RVA: 0x00121A1C File Offset: 0x0011FC1C
		public override float GetValuePerDram()
		{
			return 1000f;
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x00121A23 File Offset: 0x0011FC23
		public override string GetPreparedCookingIngredient()
		{
			return "density";
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x00121A2C File Offset: 0x0011FC2C
		public static bool Explode(GameObject WhatExplodes, GameObject Owner = null, int Phase = 0)
		{
			LiquidVolume liquidVolume = WhatExplodes.LiquidVolume;
			if (liquidVolume != null)
			{
				liquidVolume.Volume = 0;
				liquidVolume.ComponentLiquids.Clear();
			}
			return WhatExplodes.Explode(15000, Owner, "10d10+250", 1f, true, false, false, Phase, null);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x00121A74 File Offset: 0x0011FC74
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Cell currentCell = Target.CurrentCell;
			int num = 15000;
			List<Cell> list = null;
			List<GameObject> list2 = null;
			bool flag = true;
			bool flag2 = true;
			int phase = Target.GetPhase();
			Physics.ApplyExplosion(currentCell, num, list, list2, flag, flag2, Target, "10d10+250", phase, true, false, 1f, null);
			return true;
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x00121AAC File Offset: 0x0011FCAC
		public override void SmearOnTick(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
			base.SmearOnTick(Liquid, Target, By, FromCell);
			LiquidNeutronFlux.Explode(Target, By, 0);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x00121AC2 File Offset: 0x0011FCC2
		public override void SmearOn(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
			base.SmearOn(Liquid, Target, By, FromCell);
			LiquidNeutronFlux.Explode(Target, By, 0);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x00121AD8 File Offset: 0x0011FCD8
		public override void BeforeRender(LiquidVolume Liquid)
		{
			if (!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed)
			{
				Liquid.AddLight(0, LightLevel.Light, false);
			}
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x00121AF7 File Offset: 0x0011FCF7
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^Y" + eRender.ColorString;
			}
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x00121B18 File Offset: 0x0011FD18
		public override bool MixingWith(LiquidVolume Liquid, LiquidVolume NewLiquid, int Amount, GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy, ref bool ExitInterface)
		{
			bool flag = base.MixingWith(Liquid, NewLiquid, Amount, PouredFrom, PouredTo, PouredBy, ref ExitInterface);
			if (!flag)
			{
				return false;
			}
			GameObject parentObject = NewLiquid.ParentObject;
			if (((parentObject != null) ? parentObject.GetCurrentCell() : null) != null)
			{
				bool flag2;
				NeutronFluxPourExplodesEvent.Check(out flag2, PouredFrom, PouredTo, PouredBy, Liquid, true);
				if (flag2)
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x00121B68 File Offset: 0x0011FD68
		public override void MixedWith(LiquidVolume Liquid, LiquidVolume NewLiquid, int Amount, GameObject PouredFrom, GameObject PouredTo, GameObject PouredBy, ref bool ExitInterface)
		{
			base.MixedWith(Liquid, NewLiquid, Amount, PouredFrom, PouredTo, PouredBy, ref ExitInterface);
			GameObject parentObject = NewLiquid.ParentObject;
			if (((parentObject != null) ? parentObject.GetCurrentCell() : null) != null && NeutronFluxPourExplodesEvent.Check(PouredFrom, PouredTo, PouredBy, Liquid, false))
			{
				int num;
				if (NewLiquid.ParentObject != null && NewLiquid.ContainsLiquid("neutronflux"))
				{
					num = (NewLiquid.ParentObject.GetObjectContext() ?? NewLiquid.ParentObject).GetPhase();
				}
				else
				{
					num = Liquid.ParentObject.GetPhase();
				}
				if (LiquidNeutronFlux.Explode(NewLiquid.ParentObject, PouredBy, num))
				{
					ExitInterface = true;
				}
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x00121C00 File Offset: 0x0011FE00
		public override bool EnteredCell(LiquidVolume Liquid, EnteredCellEvent E)
		{
			bool flag = base.EnteredCell(Liquid, E);
			if (flag && Liquid.IsOpenVolume() && LiquidNeutronFlux.Explode(Liquid.ParentObject, E.Actor, 0))
			{
				E.RequestInterfaceExit();
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x00121C40 File Offset: 0x0011FE40
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&Y^y";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "y";
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x00121C8C File Offset: 0x0011FE8C
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&Y";
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00121CB0 File Offset: 0x0011FEB0
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&Y^y", "&Y", "y");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&Y^y", "&Y", "y");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&Y^y";
				render.TileColor = "&Y";
				render.DetailColor = "y";
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

		// Token: 0x06003B19 RID: 15129 RVA: 0x00121DA8 File Offset: 0x0011FFA8
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&Y";
			}
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x00121DC8 File Offset: 0x0011FFC8
		public override string GetPaintAtlas(LiquidVolume Liquid)
		{
			if (Liquid.IsWadingDepth())
			{
				return "Liquids/Paisley/";
			}
			return base.GetPaintAtlas(Liquid);
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x00121DDF File Offset: 0x0011FFDF
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			return 99;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x00121DE3 File Offset: 0x0011FFE3
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("stars", 1);
		}

		// Token: 0x04001D6A RID: 7530
		public new const string ID = "neutronflux";

		// Token: 0x04001D6B RID: 7531
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "y", "Y" };
	}
}
