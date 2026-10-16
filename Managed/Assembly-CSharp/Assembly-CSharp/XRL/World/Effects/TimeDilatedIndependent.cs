using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000DA0 RID: 3488
	[Serializable]
	public class TimeDilatedIndependent : ITimeDilated, ITierInitialized
	{
		// Token: 0x06008B35 RID: 35637 RVA: 0x0031896E File Offset: 0x00316B6E
		public TimeDilatedIndependent()
		{
		}

		// Token: 0x06008B36 RID: 35638 RVA: 0x00318976 File Offset: 0x00316B76
		public TimeDilatedIndependent(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008B37 RID: 35639 RVA: 0x00318985 File Offset: 0x00316B85
		public TimeDilatedIndependent(int Duration, double SpeedPenaltyMultiplier)
			: this(Duration)
		{
			this.SpeedPenaltyMultiplier = new double?(SpeedPenaltyMultiplier);
		}

		// Token: 0x06008B38 RID: 35640 RVA: 0x0031899C File Offset: 0x00316B9C
		public override void Initialize(int Tier)
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
			this.Duration = Stat.Random(30, 40);
			this.SpeedPenaltyMultiplier = new double?((double)Stat.Random(1, 9));
			this.SpeedPenaltyMultiplier /= (double)10;
		}

		// Token: 0x06008B39 RID: 35641 RVA: 0x00318A1A File Offset: 0x00316C1A
		public override bool DoTimeDilationVisualEffects()
		{
			return this.Duration > 0;
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x00318A25 File Offset: 0x00316C25
		public override bool Apply(GameObject Object)
		{
			bool flag = base.Apply(Object);
			if (flag)
			{
				this.Sync();
			}
			return flag;
		}

		// Token: 0x06008B3B RID: 35643 RVA: 0x00318A37 File Offset: 0x00316C37
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008B3C RID: 35644 RVA: 0x00318A4C File Offset: 0x00316C4C
		public void Sync()
		{
			if (this.SpeedPenaltyMultiplier != null)
			{
				int num = Math.Max((int)((double)base.Object.BaseStat("Speed", 0) * this.SpeedPenaltyMultiplier).Value, 1);
				if (num != this.SpeedPenalty)
				{
					this.UnapplyChanges();
					this.SpeedPenalty = num;
					this.ApplyChanges();
				}
			}
		}

		// Token: 0x06008B3D RID: 35645 RVA: 0x00318AD0 File Offset: 0x00316CD0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				int num = this.Duration - 1;
				this.Duration = num;
				if (num > 0)
				{
					this.Sync();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003AC9 RID: 15049
		public double? SpeedPenaltyMultiplier;
	}
}
