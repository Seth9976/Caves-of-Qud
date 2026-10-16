using System;
using System.Text;

namespace XRL.World.Effects
{
	/// This class is not used in the base game.
	// Token: 0x02000D1D RID: 3357
	[Serializable]
	public class CyberneticRejectionSyndrome : Effect
	{
		// Token: 0x06008423 RID: 33827 RVA: 0x002FC1F1 File Offset: 0x002FA3F1
		public CyberneticRejectionSyndrome()
		{
			this.DisplayName = "{{c|cybernetic rejection syndrome}}";
			this.Duration = 1;
		}

		// Token: 0x06008424 RID: 33828 RVA: 0x002FC212 File Offset: 0x002FA412
		public CyberneticRejectionSyndrome(int Level)
			: this()
		{
			this.Level = Level;
		}

		// Token: 0x06008425 RID: 33829 RVA: 0x002FC221 File Offset: 0x002FA421
		public override int GetEffectType()
		{
			return 50331652;
		}

		// Token: 0x06008426 RID: 33830 RVA: 0x002FC228 File Offset: 0x002FA428
		public override string GetDescription()
		{
			return "{{c|cybernetic rejection syndrome}}";
		}

		// Token: 0x06008427 RID: 33831 RVA: 0x002FC22F File Offset: 0x002FA42F
		public override string GetStateDescription()
		{
			return "{{c|subject to cybernetic rejection syndrome}}";
		}

		// Token: 0x06008428 RID: 33832 RVA: 0x002FC238 File Offset: 0x002FA438
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int moveSpeedPenalty = this.GetMoveSpeedPenalty();
			if (moveSpeedPenalty != 0)
			{
				stringBuilder.Append(-moveSpeedPenalty).Append(" move speed.\n");
			}
			int regenerationReductionPercentage = this.GetRegenerationReductionPercentage();
			if (regenerationReductionPercentage != 0)
			{
				stringBuilder.Append("Natural healing reduced by ").Append(regenerationReductionPercentage).Append("%.\n");
			}
			int healingReductionPercentage = this.GetHealingReductionPercentage();
			if (healingReductionPercentage != 0)
			{
				stringBuilder.Append("External healing reduced by ").Append(healingReductionPercentage).Append("%.\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008429 RID: 33833 RVA: 0x002FC2BC File Offset: 0x002FA4BC
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("ApplyCyberneticRejectionSyndrome"))
			{
				return false;
			}
			CyberneticRejectionSyndrome cyberneticRejectionSyndrome;
			if (Object.TryGetEffect<CyberneticRejectionSyndrome>(out cyberneticRejectionSyndrome))
			{
				if (this.Level > 0)
				{
					cyberneticRejectionSyndrome.Level += this.Level;
					if (Object.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("Your feverish feeling is getting worse.", 'r', true);
					}
					cyberneticRejectionSyndrome.ApplyChanges();
				}
				return false;
			}
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You feel feverish.", 'r', true);
			}
			this.ApplyChanges();
			return base.Apply(Object);
		}

		// Token: 0x0600842A RID: 33834 RVA: 0x002FC33D File Offset: 0x002FA53D
		public override void Remove(GameObject Object)
		{
			this.UnapplyChanges();
			if (Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("You feel less feverish.", 'g', true);
			}
			base.Remove(Object);
		}

		// Token: 0x0600842B RID: 33835 RVA: 0x002FC361 File Offset: 0x002FA561
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Healing");
			Registrar.Register("Regenerating");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600842C RID: 33836 RVA: 0x002FC384 File Offset: 0x002FA584
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Healing")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) * (100 - this.GetHealingReductionPercentage()) / 100);
			}
			else if (E.ID == "Regenerating")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) * (100 - this.GetRegenerationReductionPercentage()) / 100);
			}
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600842D RID: 33837 RVA: 0x002FC43A File Offset: 0x002FA63A
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.GetMoveSpeedPenalty(), false);
		}

		// Token: 0x0600842E RID: 33838 RVA: 0x002FC45A File Offset: 0x002FA65A
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x0600842F RID: 33839 RVA: 0x002FC467 File Offset: 0x002FA667
		public int GetHealingReductionPercentage()
		{
			return Math.Min(this.Level * 5, 50);
		}

		// Token: 0x06008430 RID: 33840 RVA: 0x002FC478 File Offset: 0x002FA678
		public int GetRegenerationReductionPercentage()
		{
			return Math.Min(this.Level * 7, 60);
		}

		// Token: 0x06008431 RID: 33841 RVA: 0x002FC489 File Offset: 0x002FA689
		public int GetMoveSpeedPenalty()
		{
			return Math.Min(this.Level, 10);
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x002FC498 File Offset: 0x002FA698
		public void Reduce(int By)
		{
			if (By >= this.Level)
			{
				base.Object.RemoveEffect(this, true);
				return;
			}
			if (base.Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your feverish feeling eases up a bit.", 'g', true);
			}
			this.Level -= By;
		}

		// Token: 0x04003971 RID: 14705
		public int Level = 1;
	}
}
