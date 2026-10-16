using System;
using System.Collections.Generic;
using System.Text;
using XRL.Collections;
using XRL.Core;
using XRL.Rules;
using XRL.World;
using XRL.World.Effects;
using XRL.World.Parts;

namespace XRL.Liquids
{
	// Token: 0x0200054B RID: 1355
	[IsLiquid]
	[Serializable]
	public class LiquidAcid : BaseLiquid
	{
		// Token: 0x060039B3 RID: 14771 RVA: 0x0011DE04 File Offset: 0x0011C004
		public LiquidAcid()
			: base("acid")
		{
			this.Combustibility = 3;
			this.ThermalConductivity = 45;
			this.Fluidity = 30;
			this.Evaporativity = 1;
			this.Staining = 1;
			this.Cleansing = 30;
			this.VaporObject = "AcidGas";
			this.InterruptAutowalk = true;
			this.ConsiderDangerousToContact = true;
			this.ConsiderDangerousToDrink = true;
		}

		// Token: 0x060039B4 RID: 14772 RVA: 0x0011DE69 File Offset: 0x0011C069
		public override List<string> GetColors()
		{
			return LiquidAcid.Colors;
		}

		// Token: 0x060039B5 RID: 14773 RVA: 0x0011DE70 File Offset: 0x0011C070
		public override string GetColor()
		{
			return "G";
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x0011DE77 File Offset: 0x0011C077
		public override string GetSmearedAdjective(LiquidVolume Liquid)
		{
			return "{{G|acidic}}";
		}

		// Token: 0x060039B7 RID: 14775 RVA: 0x0011DE7E File Offset: 0x0011C07E
		public override string GetSmearedName(LiquidVolume Liquid)
		{
			return "{{G|acid-covered}}";
		}

		// Token: 0x060039B8 RID: 14776 RVA: 0x0011DE85 File Offset: 0x0011C085
		public override string GetName(LiquidVolume Liquid)
		{
			return "{{G|acid}}";
		}

		// Token: 0x060039B9 RID: 14777 RVA: 0x0011DE8C File Offset: 0x0011C08C
		public override string GetWaterRitualName()
		{
			return "acid";
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x0011DE93 File Offset: 0x0011C093
		public override string GetStainedName(LiquidVolume Liquid)
		{
			return "{{G|acid}}";
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x0011DE9A File Offset: 0x0011C09A
		public override string GetAdjective(LiquidVolume Liquid)
		{
			return "{{G|acidic}}";
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x0011DEA1 File Offset: 0x0011C0A1
		public override string GetPreparedCookingIngredient()
		{
			return "acidMinor";
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x0011DEA8 File Offset: 0x0011C0A8
		public override bool SafeContainer(GameObject GO)
		{
			return !GO.IsOrganic;
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x0011DEB3 File Offset: 0x0011C0B3
		public override float GetValuePerDram()
		{
			return 1.5f;
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x0011DEBC File Offset: 0x0011C0BC
		public override bool Drank(LiquidVolume Liquid, int Volume, GameObject Target, StringBuilder Message, ref bool ExitInterface)
		{
			Message.Compound("{{G|IT BURNS!}}");
			string text = (Liquid.Proportion("acid") / 100 + 1).ToString() + "d10";
			Target.TakeDamage(text.Roll(), "from {{G|drinking acid}}!", "Acid", null, null, Target, Liquid.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
			ExitInterface = true;
			return true;
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x0011DF2C File Offset: 0x0011C12C
		public override void FillingContainer(GameObject Container, LiquidVolume Liquid)
		{
			if (!this.SafeContainer(Container))
			{
				Container.ApplyEffect(new ContainedAcidEating(), null);
			}
			base.FillingContainer(Container, Liquid);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x0011DF4C File Offset: 0x0011C14C
		public override void RenderBackgroundPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString = "^g" + eRender.ColorString;
			}
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x0011DF6C File Offset: 0x0011C16C
		public override void BaseRenderPrimary(LiquidVolume Liquid)
		{
			Liquid.ParentObject.Render.ColorString = "&G^g";
			Liquid.ParentObject.Render.TileColor = "&G";
			Liquid.ParentObject.Render.DetailColor = "g";
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x0011DFB8 File Offset: 0x0011C1B8
		public override void BaseRenderSecondary(LiquidVolume Liquid)
		{
			Render render = Liquid.ParentObject.Render;
			render.ColorString += "&g";
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x0011DFDC File Offset: 0x0011C1DC
		public override void RenderPrimary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (!Liquid.IsWadingDepth())
			{
				return;
			}
			if (Liquid.ParentObject.IsFrozen())
			{
				eRender.RenderString = "~";
				eRender.TileVariantColors("&G^g", "&G", "g");
				return;
			}
			Render render = Liquid.ParentObject.Render;
			int num = (XRLCore.CurrentFrame + Liquid.FrameOffset) % 60;
			if (Stat.RandomCosmetic(1, 600) == 1)
			{
				eRender.RenderString = "\u000f";
				eRender.TileVariantColors("&G^g", "&G", "g");
			}
			if (Stat.RandomCosmetic(1, 60) == 1)
			{
				render.ColorString = "&G^g";
				render.TileColor = "&G";
				render.DetailColor = "g";
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

		// Token: 0x060039C5 RID: 14789 RVA: 0x0011E0D4 File Offset: 0x0011C2D4
		public override void RenderSecondary(LiquidVolume Liquid, RenderEvent eRender)
		{
			if (eRender.ColorsVisible)
			{
				eRender.ColorString += "&G";
			}
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x0011E0F4 File Offset: 0x0011C2F4
		public void ApplyAcid(LiquidVolume Liquid, GameObject GO, GameObject By, bool FromCell = false)
		{
			int liquidExposureMillidrams = Liquid.GetLiquidExposureMillidrams(GO, "acid");
			int num = liquidExposureMillidrams / 20000 + Stat.Random(1, liquidExposureMillidrams) / 10000 + ((Stat.Random(0, 10000) < liquidExposureMillidrams) ? 1 : 0) + ((Stat.Random(0, 100000) < liquidExposureMillidrams) ? 4 : 0);
			GO.TakeDamage(num, "from {{G|acid}}!", "Acid", null, null, null, By ?? Liquid.ParentObject, null, null, null, false, FromCell, false, false, false, false, true, false, false, 5, null);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x0011E17B File Offset: 0x0011C37B
		public override void SmearOn(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
			base.SmearOn(Liquid, Target, By, FromCell);
			this.ApplyAcid(Liquid, Target, By, FromCell);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x0011E193 File Offset: 0x0011C393
		public override void SmearOnTick(LiquidVolume Liquid, GameObject Target, GameObject By, bool FromCell)
		{
			base.SmearOnTick(Liquid, Target, By, FromCell);
			this.ApplyAcid(Liquid, Target, By, FromCell);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x0011E1AC File Offset: 0x0011C3AC
		public override int GetNavigationWeight(LiquidVolume Liquid, GameObject GO, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			if (Smart && GO != null)
			{
				Uncacheable = true;
				int num = 100 - GO.Stat("AcidResistance", 0);
				if (num >= 99)
				{
					return 99;
				}
				using (ScopeDisposedList<GameObject> fromPool = ScopeDisposedList<GameObject>.GetFromPool())
				{
					if (Liquid.IsSwimmingDepth())
					{
						GO.GetContents(fromPool);
						using (PooledContainer<GameObject>.Enumerator enumerator = fromPool.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								GameObject gameObject = enumerator.Current;
								if (!gameObject.IsNatural() && !gameObject.HasPart<NoDamage>() && !gameObject.HasPart<NoDamageExcept>())
								{
									num = Math.Max(num, 100 - gameObject.Stat("AcidResistance", 0));
								}
							}
							goto IL_00FC;
						}
					}
					Body body = GO.Body;
					if (body != null)
					{
						body.GetEquippedObjectsExceptNatural(fromPool);
					}
					foreach (GameObject gameObject2 in fromPool)
					{
						if (!gameObject2.HasPart<NoDamage>() && !gameObject2.HasPart<NoDamageExcept>())
						{
							num = Math.Max(num, 100 - gameObject2.Stat("AcidResistance", 0));
						}
					}
					IL_00FC:
					return Math.Clamp(num, 0, 99);
				}
				return 30;
			}
			return 30;
		}

		// Token: 0x04001D13 RID: 7443
		public new const string ID = "acid";

		// Token: 0x04001D14 RID: 7444
		[NonSerialized]
		public static List<string> Colors = new List<string>(2) { "G", "g" };
	}
}
