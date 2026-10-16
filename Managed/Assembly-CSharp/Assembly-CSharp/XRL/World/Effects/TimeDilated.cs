using System;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D9F RID: 3487
	[Serializable]
	public class TimeDilated : ITimeDilated
	{
		// Token: 0x06008B2D RID: 35629 RVA: 0x003187FC File Offset: 0x003169FC
		public TimeDilated()
		{
		}

		// Token: 0x06008B2E RID: 35630 RVA: 0x00318804 File Offset: 0x00316A04
		public TimeDilated(GameObject Mutant)
			: this()
		{
			this.Mutant = Mutant;
		}

		// Token: 0x06008B2F RID: 35631 RVA: 0x00318814 File Offset: 0x00316A14
		public override bool DoTimeDilationVisualEffects()
		{
			TimeDilation part = this.Mutant.GetPart<TimeDilation>();
			return part != null && part.Duration > 0;
		}

		// Token: 0x06008B30 RID: 35632 RVA: 0x0031883B File Offset: 0x00316A3B
		public override bool Apply(GameObject Object)
		{
			bool flag = base.Apply(Object);
			if (flag)
			{
				this.Sync();
			}
			return flag;
		}

		// Token: 0x06008B31 RID: 35633 RVA: 0x0031884D File Offset: 0x00316A4D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008B32 RID: 35634 RVA: 0x00318870 File Offset: 0x00316A70
		private bool SyncInner()
		{
			if (!GameObject.Validate(ref this.Mutant))
			{
				return false;
			}
			if (!GameObject.Validate(this.Mutant) || !GameObject.Validate(base.Object))
			{
				return false;
			}
			TimeDilation part = this.Mutant.GetPart<TimeDilation>();
			if (part == null || part.Duration <= 0)
			{
				return false;
			}
			if (this.Duration > 0)
			{
				double num = part.ParentObject.RealDistanceTo(base.Object);
				if (num > (double)part.Range)
				{
					return false;
				}
				double num2 = TimeDilation.CalculateQuicknessPenaltyMultiplier(num, part.Range, part.Level);
				this.UnapplyChanges();
				this.SpeedPenalty = Math.Max((int)((double)base.Object.BaseStat("Speed", 0) * num2), 1);
				this.ApplyChanges();
			}
			return true;
		}

		// Token: 0x06008B33 RID: 35635 RVA: 0x0031892A File Offset: 0x00316B2A
		public void Sync()
		{
			if (!this.SyncInner())
			{
				this.Duration = 0;
			}
		}

		// Token: 0x06008B34 RID: 35636 RVA: 0x0031893B File Offset: 0x00316B3B
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn" || E.ID == "EnteredCell")
			{
				this.Sync();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003AC8 RID: 15048
		public GameObject Mutant;
	}
}
