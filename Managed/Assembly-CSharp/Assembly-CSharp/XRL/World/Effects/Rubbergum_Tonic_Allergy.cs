using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D7A RID: 3450
	[Serializable]
	public class Rubbergum_Tonic_Allergy : Effect, ITierInitialized
	{
		// Token: 0x060088E3 RID: 35043 RVA: 0x00310786 File Offset: 0x0030E986
		public Rubbergum_Tonic_Allergy()
		{
			this.DisplayName = "{{W|floppy}}";
		}

		// Token: 0x060088E4 RID: 35044 RVA: 0x00310799 File Offset: 0x0030E999
		public Rubbergum_Tonic_Allergy(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060088E5 RID: 35045 RVA: 0x003107A8 File Offset: 0x0030E9A8
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Roll(41, 100);
		}

		// Token: 0x060088E6 RID: 35046 RVA: 0x003107B9 File Offset: 0x0030E9B9
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060088E7 RID: 35047 RVA: 0x003107BC File Offset: 0x0030E9BC
		public override string GetDescription()
		{
			return "{{W|floppy}}";
		}

		// Token: 0x060088E8 RID: 35048 RVA: 0x003107C3 File Offset: 0x0030E9C3
		public override string GetDetails()
		{
			return "35% chance to fall prone when moving.";
		}

		// Token: 0x060088E9 RID: 35049 RVA: 0x003107CA File Offset: 0x0030E9CA
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x060088EA RID: 35050 RVA: 0x003107D4 File Offset: 0x0030E9D4
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_negativeVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return base.Apply(Object);
		}

		// Token: 0x060088EB RID: 35051 RVA: 0x00310815 File Offset: 0x0030EA15
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060088EC RID: 35052 RVA: 0x0031082B File Offset: 0x0030EA2B
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!base.Object.IsFlying && 35.in100())
			{
				base.Object.ApplyEffect(new Prone(), null);
			}
			return base.HandleEvent(E);
		}
	}
}
