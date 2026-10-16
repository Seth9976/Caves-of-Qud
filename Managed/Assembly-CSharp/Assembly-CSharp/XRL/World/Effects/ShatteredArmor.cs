using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D88 RID: 3464
	[Serializable]
	public class ShatteredArmor : IShatterEffect, ITierInitialized
	{
		// Token: 0x060089C5 RID: 35269 RVA: 0x003137E2 File Offset: 0x003119E2
		public ShatteredArmor()
		{
			this.DisplayName = "cracked";
			this.Duration = 1;
		}

		// Token: 0x060089C6 RID: 35270 RVA: 0x003137FC File Offset: 0x003119FC
		public ShatteredArmor(int Amount)
			: this()
		{
			this.Amount = Amount;
		}

		// Token: 0x060089C7 RID: 35271 RVA: 0x0031380B File Offset: 0x00311A0B
		public ShatteredArmor(int Amount, int Duration)
			: this(Amount)
		{
			this.Duration = Duration;
		}

		// Token: 0x060089C8 RID: 35272 RVA: 0x0031381B File Offset: 0x00311A1B
		public ShatteredArmor(int Amount, int Duration, GameObject Owner)
			: this(Amount, Duration)
		{
			this.Owner = Owner;
		}

		// Token: 0x060089C9 RID: 35273 RVA: 0x0031382C File Offset: 0x00311A2C
		public void Initialize(int Tier)
		{
			Tier = Stat.Random(Tier - 2, Tier + 2);
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Duration = 300;
			this.Amount = Stat.Random(1, 10);
		}

		// Token: 0x060089CA RID: 35274 RVA: 0x00313862 File Offset: 0x00311A62
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060089CB RID: 35275 RVA: 0x00313865 File Offset: 0x00311A65
		public override int GetEffectType()
		{
			return 117441536;
		}

		// Token: 0x060089CC RID: 35276 RVA: 0x0031386C File Offset: 0x00311A6C
		public override bool SameAs(Effect e)
		{
			ShatteredArmor shatteredArmor = e as ShatteredArmor;
			return shatteredArmor.Amount == this.Amount && shatteredArmor.Owner == this.Owner && base.SameAs(e);
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x003138A7 File Offset: 0x00311AA7
		public override int GetPenalty()
		{
			return this.Amount;
		}

		// Token: 0x060089CE RID: 35278 RVA: 0x003138AF File Offset: 0x00311AAF
		public override void IncrementPenalty()
		{
			this.Amount++;
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x003138BF File Offset: 0x00311ABF
		public override GameObject GetOwner()
		{
			return this.Owner;
		}

		// Token: 0x060089D0 RID: 35280 RVA: 0x003138C7 File Offset: 0x00311AC7
		public override void SetOwner(GameObject Owner)
		{
			this.Owner = Owner;
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x003138D0 File Offset: 0x00311AD0
		public override string GetDetails()
		{
			return "-" + this.Amount.ToString() + " AV";
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x003138EC File Offset: 0x00311AEC
		public override string GetDescription()
		{
			return "{{r|cracked}}";
		}

		// Token: 0x060089D3 RID: 35283 RVA: 0x003138F3 File Offset: 0x00311AF3
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x060089D4 RID: 35284 RVA: 0x003138FC File Offset: 0x00311AFC
		public override bool Apply(GameObject Object)
		{
			if (Object == null || Object.Equipped == null)
			{
				return false;
			}
			Armor part = Object.GetPart<Armor>();
			if (part == null)
			{
				return false;
			}
			if (this.Amount > part.AV)
			{
				this.Amount = part.AV;
			}
			if (this.Amount <= 0)
			{
				return false;
			}
			if (!Object.FireEvent("ApplyShatteredArmor"))
			{
				return false;
			}
			bool flag = true;
			Object.PlayWorldSound("breakage", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
			ShatteredArmor shatteredArmor;
			if (Object.TryGetEffect<ShatteredArmor>(out shatteredArmor))
			{
				shatteredArmor.UnapplyStats();
				shatteredArmor.Amount += this.Amount;
				shatteredArmor.ApplyStats();
				if (shatteredArmor.Duration < this.Duration)
				{
					shatteredArmor.Duration = this.Duration;
				}
				flag = false;
			}
			else
			{
				this.ApplyStats();
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_physicalRupture", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Object != null)
			{
				GameObject equipped = Object.Equipped;
				if (equipped != null)
				{
					equipped.ParticleText("*" + Object.ShortDisplayNameStripped + " cracked*", IComponent<GameObject>.ConsequentialColorChar(null, Object.Equipped), false, 1.5f, -8f);
				}
			}
			if (Object != null)
			{
				GameObject equipped2 = Object.Equipped;
				bool? flag2 = ((equipped2 != null) ? new bool?(equipped2.IsPlayer()) : null);
				bool flag3 = true;
				if ((flag2.GetValueOrDefault() == flag3) & (flag2 != null))
				{
					IComponent<GameObject>.AddPlayerMessage(Object.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " cracked.", 'R', true);
				}
			}
			return flag;
		}

		// Token: 0x060089D5 RID: 35285 RVA: 0x00313AB8 File Offset: 0x00311CB8
		private void ApplyStats()
		{
			GameObject @object = base.Object;
			Armor armor = ((@object != null) ? @object.GetPart<Armor>() : null);
			if (armor != null)
			{
				armor.AV -= this.Amount;
			}
		}

		// Token: 0x060089D6 RID: 35286 RVA: 0x00313AF0 File Offset: 0x00311CF0
		private void UnapplyStats()
		{
			GameObject @object = base.Object;
			Armor armor = ((@object != null) ? @object.GetPart<Armor>() : null);
			if (armor != null)
			{
				armor.AV += this.Amount;
			}
		}

		// Token: 0x060089D7 RID: 35287 RVA: 0x00313B26 File Offset: 0x00311D26
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AdjustValueEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<IsRepairableEvent>.ID || ID == PooledEvent<RepairedEvent>.ID;
		}

		// Token: 0x060089D8 RID: 35288 RVA: 0x00313B54 File Offset: 0x00311D54
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				E.AddTag("[{{r|cracked}}]", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060089D9 RID: 35289 RVA: 0x00313B71 File Offset: 0x00311D71
		public override bool HandleEvent(AdjustValueEvent E)
		{
			if (this.Amount > 1)
			{
				E.AdjustValue(1.0 / (double)this.Amount);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060089DA RID: 35290 RVA: 0x00313B9A File Offset: 0x00311D9A
		public override bool HandleEvent(IsRepairableEvent E)
		{
			return false;
		}

		// Token: 0x060089DB RID: 35291 RVA: 0x00313B9D File Offset: 0x00311D9D
		public override bool HandleEvent(RepairedEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x060089DC RID: 35292 RVA: 0x00313BB4 File Offset: 0x00311DB4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060089DD RID: 35293 RVA: 0x00313BD4 File Offset: 0x00311DD4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A85 RID: 14981
		public int Amount;

		// Token: 0x04003A86 RID: 14982
		public GameObject Owner;
	}
}
