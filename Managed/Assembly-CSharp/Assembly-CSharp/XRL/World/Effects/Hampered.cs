using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D43 RID: 3395
	[Serializable]
	public class Hampered : Effect
	{
		// Token: 0x06008609 RID: 34313 RVA: 0x00304567 File Offset: 0x00302767
		public Hampered()
		{
			this.DisplayName = "{{w|hampered}}";
			this.Duration = 1;
		}

		// Token: 0x0600860A RID: 34314 RVA: 0x00304589 File Offset: 0x00302789
		public Hampered(int Penalty = 25)
			: this()
		{
			this.Penalty = Penalty;
		}

		// Token: 0x0600860B RID: 34315 RVA: 0x00304598 File Offset: 0x00302798
		public override string GetDescription()
		{
			return this.DisplayName;
		}

		// Token: 0x0600860C RID: 34316 RVA: 0x003045A0 File Offset: 0x003027A0
		public override int GetEffectType()
		{
			return 50331776;
		}

		// Token: 0x0600860D RID: 34317 RVA: 0x003045A7 File Offset: 0x003027A7
		public override bool SameAs(Effect e)
		{
			return (e as Hampered).Penalty == this.Penalty && base.SameAs(e);
		}

		// Token: 0x0600860E RID: 34318 RVA: 0x003045C5 File Offset: 0x003027C5
		public override string GetDetails()
		{
			return "-" + this.Penalty.ToString() + " move speed for wielding a heavy weapon.";
		}

		// Token: 0x0600860F RID: 34319 RVA: 0x003045E1 File Offset: 0x003027E1
		public override bool Apply(GameObject Object)
		{
			if (!Hampered.Applicable(Object))
			{
				return false;
			}
			if (Object.HasEffect<Hampered>())
			{
				return false;
			}
			this.ApplyStats();
			return true;
		}

		// Token: 0x06008610 RID: 34320 RVA: 0x003045FE File Offset: 0x003027FE
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x06008611 RID: 34321 RVA: 0x00304606 File Offset: 0x00302806
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.Penalty, false);
		}

		// Token: 0x06008612 RID: 34322 RVA: 0x00304626 File Offset: 0x00302826
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008613 RID: 34323 RVA: 0x00304639 File Offset: 0x00302839
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008614 RID: 34324 RVA: 0x0030464F File Offset: 0x0030284F
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.CheckApplicable(false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008615 RID: 34325 RVA: 0x0030465F File Offset: 0x0030285F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008616 RID: 34326 RVA: 0x0030467F File Offset: 0x0030287F
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

		// Token: 0x06008617 RID: 34327 RVA: 0x003046BA File Offset: 0x003028BA
		public static bool Applicable(GameObject Subject)
		{
			return !Subject.HasSkill("HeavyWeapons_Tank") && Subject.HasHeavyWeaponEquipped();
		}

		// Token: 0x06008618 RID: 34328 RVA: 0x003046D1 File Offset: 0x003028D1
		public void CheckApplicable(bool Immediate = false)
		{
			if ((Immediate || this.Duration > 0) && !Hampered.Applicable(base.Object))
			{
				if (Immediate)
				{
					base.Object.RemoveEffect(this, true);
					return;
				}
				this.Duration = 0;
			}
		}

		// Token: 0x040039CE RID: 14798
		public int Penalty = 25;
	}
}
