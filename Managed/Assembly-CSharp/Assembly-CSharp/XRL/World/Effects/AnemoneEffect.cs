using System;
using System.Text;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000C31 RID: 3121
	[Serializable]
	public class AnemoneEffect : Effect
	{
		// Token: 0x06007F0D RID: 32525 RVA: 0x002EEAAF File Offset: 0x002ECCAF
		public AnemoneEffect()
		{
			this.DisplayName = "{{r|anticoagulated}}";
		}

		// Token: 0x06007F0E RID: 32526 RVA: 0x002EEAC2 File Offset: 0x002ECCC2
		public AnemoneEffect(string DamageBonus = null, int SavePenalty = 0, int Duration = 1)
			: this()
		{
			this.DamageBonus = DamageBonus;
			this.SavePenalty = SavePenalty;
			this.Duration = Duration;
		}

		// Token: 0x06007F0F RID: 32527 RVA: 0x002EEADF File Offset: 0x002ECCDF
		public override int GetEffectType()
		{
			return 100663824;
		}

		// Token: 0x06007F10 RID: 32528 RVA: 0x002EEAE6 File Offset: 0x002ECCE6
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x06007F11 RID: 32529 RVA: 0x002EEAEC File Offset: 0x002ECCEC
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.DamageBonus != null)
			{
				stringBuilder.Compound("Additional ", "\n").Append(this.DamageBonus).Append(" bleeding damage per turn.");
			}
			if (this.SavePenalty != 0)
			{
				SavingThrows.AppendSaveBonusDescription(stringBuilder, -this.SavePenalty, "Bleeding", false, false, true);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007F12 RID: 32530 RVA: 0x002EEB51 File Offset: 0x002ECD51
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || (ID == BeforeApplyDamageEvent.ID && this.DamageBonus != null) || (ID == ModifyDefendingSaveEvent.ID && this.SavePenalty != 0);
		}

		// Token: 0x06007F13 RID: 32531 RVA: 0x002EEB8A File Offset: 0x002ECD8A
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Bleeding", E))
			{
				E.Roll -= this.SavePenalty;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F14 RID: 32532 RVA: 0x002EEBB4 File Offset: 0x002ECDB4
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Damage.HasAttribute("Bleeding"))
			{
				E.Damage.Amount = Math.Max(0, E.Damage.Amount + this.DamageBonus.RollCached());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F15 RID: 32533 RVA: 0x002EEC02 File Offset: 0x002ECE02
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0 && this.Duration != 9999)
			{
				this.Duration--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F16 RID: 32534 RVA: 0x002EEC30 File Offset: 0x002ECE30
		public override bool Apply(GameObject Object)
		{
			AnemoneEffect anemoneEffect;
			if (Object.TryGetEffect<AnemoneEffect>(out anemoneEffect))
			{
				anemoneEffect.Duration += this.Duration;
				return false;
			}
			return true;
		}

		// Token: 0x040038B4 RID: 14516
		public const string DAMAGE_BONUS_ATTR = "Bleeding";

		// Token: 0x040038B5 RID: 14517
		public const string SAVE_PENALTY_VS = "Bleeding";

		// Token: 0x040038B6 RID: 14518
		public string DamageBonus;

		// Token: 0x040038B7 RID: 14519
		public int SavePenalty;
	}
}
