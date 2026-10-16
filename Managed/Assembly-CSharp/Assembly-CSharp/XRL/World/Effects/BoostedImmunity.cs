using System;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000C3F RID: 3135
	[Serializable]
	public class BoostedImmunity : Effect
	{
		// Token: 0x06007FEB RID: 32747 RVA: 0x002F2144 File Offset: 0x002F0344
		public BoostedImmunity()
		{
			this.DisplayName = "{{G|boosted immunity}}";
			this.Duration = 1;
		}

		// Token: 0x06007FEC RID: 32748 RVA: 0x002F2165 File Offset: 0x002F0365
		public BoostedImmunity(int Modifier)
			: this()
		{
			this.Modifier = Modifier;
		}

		// Token: 0x06007FED RID: 32749 RVA: 0x002F2174 File Offset: 0x002F0374
		public override int GetEffectType()
		{
			return 83886084;
		}

		// Token: 0x06007FEE RID: 32750 RVA: 0x002F217B File Offset: 0x002F037B
		public override bool SameAs(Effect e)
		{
			return (e as BoostedImmunity).Modifier == this.Modifier && base.SameAs(e);
		}

		// Token: 0x06007FEF RID: 32751 RVA: 0x002F2199 File Offset: 0x002F0399
		public override string GetDetails()
		{
			return ((this.Modifier > 0) ? "+" : "") + this.Modifier.ToString() + " to saves vs. disease onset.";
		}

		// Token: 0x06007FF0 RID: 32752 RVA: 0x002F21C8 File Offset: 0x002F03C8
		public override bool Apply(GameObject Object)
		{
			BoostedImmunity effect = Object.GetEffect<BoostedImmunity>();
			if (effect != null)
			{
				if (this.Modifier > effect.Modifier)
				{
					effect.Modifier = this.Modifier;
				}
				return false;
			}
			if (GetDiseaseOnsetEvent.GetFor(Object) == null)
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return true;
		}

		// Token: 0x06007FF1 RID: 32753 RVA: 0x002F2233 File Offset: 0x002F0433
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x06007FF2 RID: 32754 RVA: 0x002F2251 File Offset: 0x002F0451
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0 && GetDiseaseOnsetEvent.GetFor(base.Object) == null)
			{
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007FF3 RID: 32755 RVA: 0x002F2277 File Offset: 0x002F0477
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Disease Onset", E.Vs))
			{
				E.Roll += this.Modifier;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040038E2 RID: 14562
		public int Modifier = 4;
	}
}
