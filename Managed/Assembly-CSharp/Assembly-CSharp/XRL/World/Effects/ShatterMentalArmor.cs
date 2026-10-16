using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D87 RID: 3463
	[Serializable]
	public class ShatterMentalArmor : IShatterEffect, ITierInitialized
	{
		// Token: 0x060089B0 RID: 35248 RVA: 0x003134CB File Offset: 0x003116CB
		public ShatterMentalArmor()
		{
			this.DisplayName = "{{psionic|psionically cleaved}}";
		}

		// Token: 0x060089B1 RID: 35249 RVA: 0x003134E5 File Offset: 0x003116E5
		public ShatterMentalArmor(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060089B2 RID: 35250 RVA: 0x003134F4 File Offset: 0x003116F4
		public ShatterMentalArmor(int Duration, GameObject Owner = null, int MAPenalty = 1)
			: this(Duration)
		{
			this.Owner = Owner;
			this.MAPenalty = MAPenalty;
		}

		// Token: 0x060089B3 RID: 35251 RVA: 0x0031350B File Offset: 0x0031170B
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
			this.MAPenalty = Stat.Random(1, 10);
		}

		// Token: 0x060089B4 RID: 35252 RVA: 0x00313541 File Offset: 0x00311741
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060089B5 RID: 35253 RVA: 0x00313544 File Offset: 0x00311744
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x060089B6 RID: 35254 RVA: 0x0031354C File Offset: 0x0031174C
		public override bool SameAs(Effect e)
		{
			ShatterMentalArmor shatterMentalArmor = e as ShatterMentalArmor;
			return shatterMentalArmor.MAPenalty == this.MAPenalty && shatterMentalArmor.Owner == this.Owner && base.SameAs(e);
		}

		// Token: 0x060089B7 RID: 35255 RVA: 0x00313587 File Offset: 0x00311787
		public override string GetDescription()
		{
			return "{{psionic|psionically cleaved (-" + this.MAPenalty.ToString() + " MA)}}";
		}

		// Token: 0x060089B8 RID: 35256 RVA: 0x003135A3 File Offset: 0x003117A3
		public override string GetStateDescription()
		{
			return "{{psionic|psionically cleaved}}";
		}

		// Token: 0x060089B9 RID: 35257 RVA: 0x003135AA File Offset: 0x003117AA
		public override string GetDetails()
		{
			return "-" + this.MAPenalty.ToString() + " MA";
		}

		// Token: 0x060089BA RID: 35258 RVA: 0x003135C6 File Offset: 0x003117C6
		public override int GetPenalty()
		{
			return this.MAPenalty;
		}

		// Token: 0x060089BB RID: 35259 RVA: 0x003135CE File Offset: 0x003117CE
		public override void IncrementPenalty()
		{
			this.MAPenalty++;
		}

		// Token: 0x060089BC RID: 35260 RVA: 0x003135DE File Offset: 0x003117DE
		public override GameObject GetOwner()
		{
			return this.Owner;
		}

		// Token: 0x060089BD RID: 35261 RVA: 0x003135E6 File Offset: 0x003117E6
		public override void SetOwner(GameObject Owner)
		{
			this.Owner = Owner;
		}

		// Token: 0x060089BE RID: 35262 RVA: 0x003135F0 File Offset: 0x003117F0
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasStat("MA"))
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyShatterMentalArmor", "Owner", this.Owner, "Effect", this)) && ApplyEffectEvent.Check(Object, "ShatterMentalArmor", this, null))
			{
				return false;
			}
			Object.PlayWorldSound("breakage", 0.5f, 0f, true, 0f, 1f, 1f, int.MaxValue);
			ShatterMentalArmor shatterMentalArmor;
			if (Object.TryGetEffect<ShatterMentalArmor>(out shatterMentalArmor))
			{
				if (this.Duration > shatterMentalArmor.Duration)
				{
					shatterMentalArmor.Duration = this.Duration;
				}
				shatterMentalArmor.UnapplyStats();
				shatterMentalArmor.MAPenalty += this.MAPenalty;
				shatterMentalArmor.ApplyStats();
				Object.ParticleText("*psionic cleave (-" + shatterMentalArmor.MAPenalty.ToString() + " MA)*", 'b', false, 1.5f, -8f);
				return false;
			}
			this.ApplyStats();
			Object.ParticleText("*psionic cleave (-" + this.MAPenalty.ToString() + " MA)*", 'b', false, 1.5f, -8f);
			return true;
		}

		// Token: 0x060089BF RID: 35263 RVA: 0x00313710 File Offset: 0x00311910
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x060089C0 RID: 35264 RVA: 0x00313718 File Offset: 0x00311918
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("MA", -this.MAPenalty, false);
		}

		// Token: 0x060089C1 RID: 35265 RVA: 0x00313733 File Offset: 0x00311933
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x060089C2 RID: 35266 RVA: 0x00313740 File Offset: 0x00311940
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060089C3 RID: 35267 RVA: 0x00313760 File Offset: 0x00311960
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 40 && num < 55)
				{
					E.Tile = null;
					E.RenderString = "X";
					E.ColorString = "&M^k";
				}
			}
			return true;
		}

		// Token: 0x060089C4 RID: 35268 RVA: 0x003137A7 File Offset: 0x003119A7
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

		// Token: 0x04003A83 RID: 14979
		public int MAPenalty = 1;

		// Token: 0x04003A84 RID: 14980
		public GameObject Owner;
	}
}
