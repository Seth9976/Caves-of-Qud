using System;
using System.Text;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020010FB RID: 4347
	[Serializable]
	public class VampiricWeapon : IActivePart
	{
		// Token: 0x0600AC09 RID: 44041 RVA: 0x003F0EA2 File Offset: 0x003EF0A2
		public VampiricWeapon()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AC0A RID: 44042 RVA: 0x003F0ED5 File Offset: 0x003EF0D5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AttackerDealtDamageEvent.ID && this.WorksAsAttacker) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AC0B RID: 44043 RVA: 0x003F0EFB File Offset: 0x003EF0FB
		public override bool HandleEvent(AttackerDealtDamageEvent E)
		{
			if (this.WorksAsAttacker)
			{
				this.Absorb(E.Damage, E.Actor, E.Object, E.Weapon, E.Projectile, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC0C RID: 44044 RVA: 0x003F0F31 File Offset: 0x003EF131
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC0D RID: 44045 RVA: 0x003F0F54 File Offset: 0x003EF154
		public void AppendEffect(StringBuilder SB)
		{
			SB.Append("Heal for ");
			SB.Append(this.Percent.IsNullOrEmpty() ? "100" : this.Percent);
			SB.Append("% of damage dealt");
			if (this.RequiresLivingTarget)
			{
				SB.Append(" vs. organic creatures");
			}
			if (!this.Reduction.IsNullOrEmpty())
			{
				SB.Append(", -").Append(this.Reduction);
			}
			if (!this.Maximum.IsNullOrEmpty())
			{
				SB.Append(", max ").Append(this.Maximum);
			}
		}

		// Token: 0x0600AC0E RID: 44046 RVA: 0x003F0FF6 File Offset: 0x003EF1F6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AC0F RID: 44047 RVA: 0x003F0FF9 File Offset: 0x003EF1F9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.WorksInMelee)
			{
				Registrar.Register("WeaponDealDamage");
			}
			if (this.WorksThrown)
			{
				Registrar.Register("WeaponThrowHit");
			}
			if (this.WorksAsProjectile)
			{
				Registrar.Register("ProjectileHit");
			}
		}

		// Token: 0x0600AC10 RID: 44048 RVA: 0x003F1034 File Offset: 0x003EF234
		public override bool FireEvent(Event E)
		{
			if ((this.WorksInMelee && E.ID == "WeaponDealDamage") || (this.WorksThrown && E.ID == "WeaponThrowHit") || (this.WorksAsProjectile && E.ID == "ProjectileHit"))
			{
				this.Absorb(E.GetParameter("Damage") as Damage, E.GetGameObjectParameter("Attacker"), E.GetGameObjectParameter("Defender"), E.GetGameObjectParameter("Weapon"), E.GetGameObjectParameter("Projectile"), E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AC11 RID: 44049 RVA: 0x003F10DC File Offset: 0x003EF2DC
		public void Absorb(Damage Damage, GameObject Attacker, GameObject Defender, GameObject Weapon, GameObject Projectile, IEvent ParentEvent)
		{
			if (!Attacker.IsValid())
			{
				return;
			}
			if (Damage == null || Damage.Amount <= 0)
			{
				return;
			}
			if (this.RequiresLivingTarget && !Defender.IsAlive)
			{
				return;
			}
			int num = this.Chance;
			if (this.WorksInMelee || this.WorksThrown || this.WorksAsProjectile)
			{
				num = GetSpecialEffectChanceEvent.GetFor(Attacker, Weapon, "Part VampiricWeapon Activation", this.Chance, Defender, Projectile, true, false);
			}
			if (!num.in100())
			{
				return;
			}
			if (this.RealityDistortionBased)
			{
				Cell currentCell = Defender.CurrentCell;
				Event @event = Event.New("InitiateRealityDistortionTransit", 0, 0, 0);
				@event.SetParameter("Object", Attacker);
				@event.SetParameter("Device", Weapon ?? this.ParentObject);
				@event.SetParameter("Operator", Attacker);
				@event.SetParameter("Cell", currentCell);
				if ((Attacker.IsValid() && !Attacker.FireEvent(@event, ParentEvent)) || (currentCell != null && !currentCell.FireEvent(@event, ParentEvent)))
				{
					return;
				}
			}
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				int num2 = Damage.Amount;
				if (!string.IsNullOrEmpty(this.Percent))
				{
					num2 = num2 * Stat.Roll(this.Percent, null) / 100;
				}
				if (!string.IsNullOrEmpty(this.Reduction))
				{
					num2 -= Stat.Roll(this.Reduction, null);
				}
				if (!string.IsNullOrEmpty(this.Maximum))
				{
					num2 = Math.Min(num2, Stat.Roll(this.Maximum, null));
				}
				if (num2 > 0)
				{
					Attacker.Heal(num2, true, true, false);
				}
			}
		}

		// Token: 0x04004731 RID: 18225
		public int Chance = 100;

		// Token: 0x04004732 RID: 18226
		public string Percent;

		// Token: 0x04004733 RID: 18227
		public string Reduction;

		// Token: 0x04004734 RID: 18228
		public string Maximum;

		// Token: 0x04004735 RID: 18229
		public bool WorksInMelee = true;

		// Token: 0x04004736 RID: 18230
		public bool WorksThrown = true;

		// Token: 0x04004737 RID: 18231
		public bool WorksAsProjectile = true;

		// Token: 0x04004738 RID: 18232
		public bool WorksAsAttacker = true;

		// Token: 0x04004739 RID: 18233
		public bool RealityDistortionBased;

		// Token: 0x0400473A RID: 18234
		public bool RequiresLivingTarget;
	}
}
