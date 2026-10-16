using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02001164 RID: 4452
	[Serializable]
	public class ModScaled : IModification
	{
		// Token: 0x0600B115 RID: 45333 RVA: 0x00410D53 File Offset: 0x0040EF53
		public ModScaled()
		{
		}

		// Token: 0x0600B116 RID: 45334 RVA: 0x00410D66 File Offset: 0x0040EF66
		public ModScaled(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B117 RID: 45335 RVA: 0x00410D7A File Offset: 0x0040EF7A
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B118 RID: 45336 RVA: 0x00410D83 File Offset: 0x0040EF83
		public override void ApplyModification(GameObject Object)
		{
			AddsRep.AddModifier(Object, "Unshelled Reptiles:" + this.Amount.ToString() + ":hidden");
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600B119 RID: 45337 RVA: 0x00410DAF File Offset: 0x0040EFAF
		public override bool SameAs(IPart p)
		{
			return (p as ModScaled).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600B11A RID: 45338 RVA: 0x00410DCD File Offset: 0x0040EFCD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B11B RID: 45339 RVA: 0x00410DF3 File Offset: 0x0040EFF3
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction += 30;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B11C RID: 45340 RVA: 0x00410E0B File Offset: 0x0040F00B
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{scaled|scaled}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B11D RID: 45341 RVA: 0x00410E35 File Offset: 0x0040F035
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B11E RID: 45342 RVA: 0x00410E50 File Offset: 0x0040F050
		public static string GetDescription(int Tier)
		{
			return "Scaled: This item grants the wearer reputation with unshelled reptiles.";
		}

		// Token: 0x0600B11F RID: 45343 RVA: 0x00410E58 File Offset: 0x0040F058
		public string GetInstanceDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Scaled: This item grants the wearer ");
			if (this.Amount > 0)
			{
				stringBuilder.Append('+');
			}
			stringBuilder.Append(this.Amount).Append(" reputation with unshelled reptiles.");
			return stringBuilder.ToString();
		}

		// Token: 0x04004823 RID: 18467
		public int Amount = 250;
	}
}
