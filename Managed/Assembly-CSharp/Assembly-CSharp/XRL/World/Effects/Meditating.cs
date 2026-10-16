using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D64 RID: 3428
	[Serializable]
	public class Meditating : Effect, ITierInitialized
	{
		// Token: 0x060087A6 RID: 34726 RVA: 0x0030A8C8 File Offset: 0x00308AC8
		public Meditating()
		{
			this.DisplayName = "meditating";
			this.Duration = 1;
		}

		// Token: 0x060087A7 RID: 34727 RVA: 0x0030A8E2 File Offset: 0x00308AE2
		public Meditating(int Duration = 1, bool FromResting = false)
			: this()
		{
			this.Duration = Duration;
			this.FromResting = FromResting;
		}

		// Token: 0x060087A8 RID: 34728 RVA: 0x0030A8F8 File Offset: 0x00308AF8
		public override int GetEffectType()
		{
			return 218103810;
		}

		// Token: 0x060087A9 RID: 34729 RVA: 0x0030A900 File Offset: 0x00308B00
		public override bool SameAs(Effect e)
		{
			Meditating meditating = e as Meditating;
			return meditating.DamageLeft == this.DamageLeft && meditating.FromResting == this.FromResting && base.SameAs(e);
		}

		// Token: 0x060087AA RID: 34730 RVA: 0x0030A93C File Offset: 0x00308B3C
		public override string GetDetails()
		{
			return "Regenerates at thrice the normal rate.\nNegative status effects wear off at thrice the normal rate.\nStops meditating if another action is taken or " + this.GetDamageThreshold().ToString() + " damage is taken in a single round.";
		}

		// Token: 0x060087AB RID: 34731 RVA: 0x0030A968 File Offset: 0x00308B68
		public override bool Apply(GameObject Object)
		{
			Meditating effect = Object.GetEffect<Meditating>();
			if (effect != null)
			{
				effect.Duration = Math.Max(effect.Duration, this.Duration);
				return false;
			}
			this.DamageLeft = this.GetDamageThreshold();
			if (!Object.FireEvent(Event.New("ApplyMeditating", "Effect", this)))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			base.DidX("begin", "meditating", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x060087AC RID: 34732 RVA: 0x0030AA0B File Offset: 0x00308C0B
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (this.FromResting && this.DamageLeft > 0)
				{
					IComponent<GameObject>.AddPlayerMessage("You stop meditating and feel refreshed.", 'G', true);
				}
				else
				{
					IComponent<GameObject>.AddPlayerMessage("You stop meditating.", 'R', true);
				}
			}
			base.Remove(Object);
		}

		// Token: 0x060087AD RID: 34733 RVA: 0x0030AA49 File Offset: 0x00308C49
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<UseEnergyEvent>.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x060087AE RID: 34734 RVA: 0x0030AA70 File Offset: 0x00308C70
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.DamageLeft = this.GetDamageThreshold();
			int i = 0;
			int count = base.Object.Effects.Count;
			while (i < count)
			{
				Effect effect = base.Object.Effects[i];
				if (effect.Duration > 1 && effect.Duration != 9999 && effect.IsOfTypes(100663296))
				{
					effect.Duration--;
					if (effect.Duration > 1)
					{
						effect.Duration--;
					}
				}
				i++;
			}
			Bleeding effect2 = base.Object.GetEffect<Bleeding>();
			if (effect2 != null && !effect2.RecoveryChance(1, false))
			{
				effect2.RecoveryChance(1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060087AF RID: 34735 RVA: 0x0030AB2C File Offset: 0x00308D2C
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == base.Object)
			{
				this.DamageLeft -= E.Damage.Amount;
				if (this.DamageLeft <= 0)
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060087B0 RID: 34736 RVA: 0x0030AB7D File Offset: 0x00308D7D
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (!E.Passive || (E.Type != null && !E.Type.Contains("Pass")))
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060087B1 RID: 34737 RVA: 0x0030ABB8 File Offset: 0x00308DB8
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 25 && num < 35)
				{
					E.Tile = null;
					E.RenderString = "Z";
					E.ColorString = "&G";
				}
			}
			return true;
		}

		// Token: 0x060087B2 RID: 34738 RVA: 0x0030ABFF File Offset: 0x00308DFF
		public int GetDamageThreshold()
		{
			return Math.Max(base.Object.Stat("Willpower", 0) * 3 - 60, 1);
		}

		// Token: 0x04003A25 RID: 14885
		public int DamageLeft;

		// Token: 0x04003A26 RID: 14886
		public bool FromResting;
	}
}
