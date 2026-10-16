using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x02000553 RID: 1363
	[IsLiquid]
	[Serializable]
	public class LiquidConvalessence : BaseLiquid
	{
		// Token: 0x06003A85 RID: 14981 RVA: 0x001202EC File Offset: 0x0011E4EC
		public LiquidConvalessence()
			: base("convalessence")
		{
			this.Temperature = 5;
			this.Combustibility = 5;
			this.PureElectricalConductivity = 10;
			this.MixedElectricalConductivity = 10;
			this.ThermalConductivity = 80;
			this.Fluidity = 20;
			this.Evaporativity = 15;
			this.Cleansing = 5;
			this.Glows = true;
			this.FreezeObject3 = "CryoGas";
			this.FreezeObjectThreshold3 = 1;
			this.FreezeObjectVerb3 = "sublimate";
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x00120365 File Offset: 0x0011E565
		public override List<string> GetColors()
		{
			return LiquidConvalessence.Colors;
		}

		// Token: 0x06003A87 RID: 14983 RVA: 0x0012036C File Offset: 0x0011E56C
		public override string GetColor()
		{
			return "C";
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x00120373 File Offset: 0x0011E573
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{C|convalessence}}";
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x0012037A File Offset: 0x0011E57A
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{C|luminous}}";
		}

		// Token: 0x06003A8A RID: 14986 RVA: 0x00120381 File Offset: 0x0011E581
		public override string GetWaterRitualName()
		{
			return "convalessence";
		}

		// Token: 0x06003A8B RID: 14987 RVA: 0x00120388 File Offset: 0x0011E588
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{C|luminous}}";
		}

		// Token: 0x06003A8C RID: 14988 RVA: 0x0012038F File Offset: 0x0011E58F
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{C|luminous}}";
		}

		// Token: 0x06003A8D RID: 14989 RVA: 0x00120396 File Offset: 0x0011E596
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{C|convalessence}}";
		}

		// Token: 0x06003A8E RID: 14990 RVA: 0x0012039D File Offset: 0x0011E59D
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("It's effervescent!");
			return true;
		}

		// Token: 0x06003A8F RID: 14991 RVA: 0x001203AD File Offset: 0x0011E5AD
		public override string GetPreparedCookingIngredient()
		{
			return "coldMinor,regenLowtierMinor";
		}

		// Token: 0x06003A90 RID: 14992 RVA: 0x001203B4 File Offset: 0x0011E5B4
		public override void BeforeRender(LiquidVolume Liquid)
		{
			if (!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed)
			{
				Cell currentCell = Liquid.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					int num = Liquid.Amount("convalessence");
					int num2;
					if (num >= 1000)
					{
						num2 = 3;
					}
					else if (num >= 500)
					{
						num2 = 2;
					}
					else
					{
						num2 = 1;
					}
					Zone parentZone = currentCell.ParentZone;
					if (parentZone == null)
					{
						return;
					}
					parentZone.AddLight(currentCell.X, currentCell.Y, num2, LightLevel.Light, false);
				}
			}
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x00120429 File Offset: 0x0011E629
		public override void BeforeRenderSecondary(LiquidVolume Liquid)
		{
			if (!Liquid.Sealed || Liquid.LiquidVisibleWhenSealed)
			{
				Liquid.AddLight(1, LightLevel.Light, false);
			}
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x00120448 File Offset: 0x0011E648
		public override void RenderSmearPrimary(LiquidVolume Liquid, RenderEvent eRender, GameObject obj)
		{
			if (eRender.ColorsVisible)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					eRender.ColorString = "&C";
				}
			}
			base.RenderSmearPrimary(Liquid, eRender, obj);
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x00120483 File Offset: 0x0011E683
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^C" + eRender.ColorString;
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x001204A4 File Offset: 0x0011E6A4
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&Y^C";
			Liquid.ParentObject.Render.TileColor = "&Y";
			Liquid.ParentObject.Render.DetailColor = "C";
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x001204F0 File Offset: 0x0011E6F0
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&C";
		}

		// Token: 0x06003A96 RID: 14998 RVA: 0x00120514 File Offset: 0x0011E714
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&C^Y", "&C", "Y");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			render.ColorString = "&Y^C";
			render.TileColor = "&Y";
			render.DetailColor = "C";
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
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

		// Token: 0x06003A97 RID: 14999 RVA: 0x001205DE File Offset: 0x0011E7DE
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&C";
			}
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x001205FE File Offset: 0x0011E7FE
		public override void ObjectInCell(LiquidVolume Liquid, GameObject GO)
		{
			base.ObjectInCell(Liquid, GO);
			if (Liquid.IsOpenVolume() && GO.IsAlive)
			{
				GO.Heal(1, false, true, true);
			}
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x00120623 File Offset: 0x0011E823
		public override int GetHealingLocationValue(LiquidVolume Liquid, GameObject Actor)
		{
			if (Liquid.IsOpenVolume() && Actor.IsOrganic)
			{
				return 10;
			}
			return base.GetHealingLocationValue(Liquid, Actor);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x00120640 File Offset: 0x0011E840
		public override void StainElements(LiquidVolume Liquid, GetItemElementsEvent E)
		{
			E.Add("ice", 1);
		}

		// Token: 0x04001D5E RID: 7518
		public new const string ID = "convalessence";

		// Token: 0x04001D5F RID: 7519
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "C", "Y" };
	}
}
