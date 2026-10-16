using System;
using XRL.Liquids;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C48 RID: 3144
	[Serializable]
	public class ContainedAcidEating : Effect, ITierInitialized
	{
		// Token: 0x0600806C RID: 32876 RVA: 0x002F485D File Offset: 0x002F2A5D
		public ContainedAcidEating()
		{
			this.Duration = 1;
			this.DisplayName = "{{Y|sizzling}}";
		}

		// Token: 0x0600806D RID: 32877 RVA: 0x002F4882 File Offset: 0x002F2A82
		public override int GetEffectType()
		{
			return 512;
		}

		// Token: 0x0600806E RID: 32878 RVA: 0x002F4889 File Offset: 0x002F2A89
		public override bool SameAs(Effect e)
		{
			return !((e as ContainedAcidEating).LiquidID != this.LiquidID) && base.SameAs(e);
		}

		// Token: 0x0600806F RID: 32879 RVA: 0x002F48AC File Offset: 0x002F2AAC
		public override string GetDetails()
		{
			LiquidVolume liquidVolume = this.ValidVolume();
			if (liquidVolume != null)
			{
				return string.Concat(new string[]
				{
					"Dissolving from acid.\nTaking ",
					this.MinDamage(liquidVolume).ToString(),
					"-",
					this.MaxDamage(liquidVolume).ToString(),
					" per turn."
				});
			}
			return "No damage.";
		}

		// Token: 0x06008070 RID: 32880 RVA: 0x002F4910 File Offset: 0x002F2B10
		public override bool Apply(GameObject Object)
		{
			return Object.HasPart<LiquidVolume>() && Object.GetPart<LiquidVolume>().MaxVolume > 0 && !new LiquidAcid().SafeContainer(Object) && !Object.HasEffect<ContainedAcidEating>();
		}

		// Token: 0x06008071 RID: 32881 RVA: 0x002F4944 File Offset: 0x002F2B44
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x06008072 RID: 32882 RVA: 0x002F4946 File Offset: 0x002F2B46
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == PooledEvent<CanBeTradedEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06008073 RID: 32883 RVA: 0x002F4974 File Offset: 0x002F2B74
		public override bool HandleEvent(CanBeTradedEvent E)
		{
			return false;
		}

		// Token: 0x06008074 RID: 32884 RVA: 0x002F4977 File Offset: 0x002F2B77
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference && this.CanAffect(base.Object))
			{
				E.AddAdjective(this.DisplayName, 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008075 RID: 32885 RVA: 0x002F49A3 File Offset: 0x002F2BA3
		public override bool HandleEvent(AdjustValueEvent E)
		{
			E.AdjustValue(0.01);
			return base.HandleEvent(E);
		}

		// Token: 0x06008076 RID: 32886 RVA: 0x002F49BC File Offset: 0x002F2BBC
		public override bool HandleEvent(EndTurnEvent E)
		{
			LiquidVolume liquidVolume = this.ValidVolume();
			if (liquidVolume == null || !this.CanAffect(base.Object, liquidVolume))
			{
				base.Object.RemoveEffect(this, true);
			}
			else
			{
				int num = Stat.Random(this.MinDamage(liquidVolume), this.MaxDamage(liquidVolume));
				if (num > 0)
				{
					base.Object.TakeDamage(num, "from {{G|acid}}!", "Acid", null, null, null, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x002F4A3A File Offset: 0x002F2C3A
		private int MinDamage(LiquidVolume V)
		{
			return Math.Max(1, (int)((float)V.Volume * ((float)V.ComponentLiquids[this.LiquidID] / 1000f / 16f)));
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x002F4A69 File Offset: 0x002F2C69
		private int MaxDamage(LiquidVolume V)
		{
			return Math.Max(2, (int)((float)V.Volume * ((float)V.ComponentLiquids[this.LiquidID] / 1000f)));
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x002F4A94 File Offset: 0x002F2C94
		private LiquidVolume ValidVolume()
		{
			LiquidVolume liquidVolume = base.Object.LiquidVolume;
			if (liquidVolume.Volume <= 0)
			{
				return null;
			}
			if (!liquidVolume.ComponentLiquids.ContainsKey(this.LiquidID))
			{
				return null;
			}
			return liquidVolume;
		}

		// Token: 0x0600807A RID: 32890 RVA: 0x002F4AD0 File Offset: 0x002F2CD0
		public int MinDamage()
		{
			LiquidVolume liquidVolume = this.ValidVolume();
			if (liquidVolume == null)
			{
				return 0;
			}
			return this.MinDamage(liquidVolume);
		}

		// Token: 0x0600807B RID: 32891 RVA: 0x002F4AF0 File Offset: 0x002F2CF0
		public int MaxDamage()
		{
			LiquidVolume liquidVolume = this.ValidVolume();
			if (liquidVolume == null)
			{
				return 0;
			}
			return this.MinDamage(liquidVolume);
		}

		// Token: 0x0600807C RID: 32892 RVA: 0x002F4B10 File Offset: 0x002F2D10
		private bool CanAffect(GameObject GO, LiquidVolume V)
		{
			return !LiquidVolume.GetLiquid(this.LiquidID).SafeContainer(GO);
		}

		// Token: 0x0600807D RID: 32893 RVA: 0x002F4B2C File Offset: 0x002F2D2C
		public bool CanAffect(GameObject GO)
		{
			LiquidVolume liquidVolume = this.ValidVolume();
			return liquidVolume != null && this.CanAffect(GO, liquidVolume);
		}

		// Token: 0x040038FC RID: 14588
		private string LiquidID = "acid";
	}
}
