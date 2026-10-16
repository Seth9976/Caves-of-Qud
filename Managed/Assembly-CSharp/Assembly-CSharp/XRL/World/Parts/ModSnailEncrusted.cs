using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x0200116D RID: 4461
	[Serializable]
	public class ModSnailEncrusted : IModification
	{
		// Token: 0x0600B177 RID: 45431 RVA: 0x00411B57 File Offset: 0x0040FD57
		public ModSnailEncrusted()
		{
		}

		// Token: 0x0600B178 RID: 45432 RVA: 0x00411B6A File Offset: 0x0040FD6A
		public ModSnailEncrusted(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B179 RID: 45433 RVA: 0x00411B7E File Offset: 0x0040FD7E
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B17A RID: 45434 RVA: 0x00411B87 File Offset: 0x0040FD87
		public override void ApplyModification(GameObject Object)
		{
			AddsRep.AddModifier(Object, "Mollusks:" + this.Amount.ToString() + ":hidden");
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B17B RID: 45435 RVA: 0x00411BB2 File Offset: 0x0040FDB2
		public override bool SameAs(IPart p)
		{
			return (p as ModSnailEncrusted).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600B17C RID: 45436 RVA: 0x00411BD0 File Offset: 0x0040FDD0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B17D RID: 45437 RVA: 0x00411BF6 File Offset: 0x0040FDF6
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction += 30;
			return base.HandleEvent(E);
		}

		// Token: 0x0600B17E RID: 45438 RVA: 0x00411C0E File Offset: 0x0040FE0E
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Understood() || !E.Object.HasProperName)
			{
				E.AddAdjective("{{snail-encrusted|snail-encrusted}}", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B17F RID: 45439 RVA: 0x00411C38 File Offset: 0x0040FE38
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(this.GetInstanceDescription());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B180 RID: 45440 RVA: 0x00411C53 File Offset: 0x0040FE53
		public static string GetDescription(int Tier)
		{
			return "Snail-Encrusted: This item is crawling with tiny snails and grants the wearer reputation with mollusks.";
		}

		// Token: 0x0600B181 RID: 45441 RVA: 0x00411C5C File Offset: 0x0040FE5C
		public string GetInstanceDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Snail-Encrusted: This item is crawling with tiny snails and grants the wearer ");
			if (this.Amount > 0)
			{
				stringBuilder.Append('+');
			}
			stringBuilder.Append(this.Amount).Append(" reputation with mollusks.");
			return stringBuilder.ToString();
		}

		// Token: 0x04004825 RID: 18469
		public int Amount = 250;
	}
}
