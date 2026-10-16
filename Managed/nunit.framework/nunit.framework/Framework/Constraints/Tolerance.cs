using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000147 RID: 327
	public class Tolerance
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001776E File Offset: 0x0001596E
		public static Tolerance Default
		{
			get
			{
				return new Tolerance(0, ToleranceMode.Unset);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x0001777C File Offset: 0x0001597C
		public static Tolerance Exact
		{
			get
			{
				return new Tolerance(0, ToleranceMode.Linear);
			}
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001778A File Offset: 0x0001598A
		public Tolerance(object amount)
			: this(amount, ToleranceMode.Linear)
		{
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x00017794 File Offset: 0x00015994
		private Tolerance(object amount, ToleranceMode mode)
		{
			this.amount = amount;
			this.mode = mode;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000177AA File Offset: 0x000159AA
		public ToleranceMode Mode
		{
			get
			{
				return this.mode;
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000177B2 File Offset: 0x000159B2
		private void CheckLinearAndNumeric()
		{
			if (this.mode != ToleranceMode.Linear)
			{
				throw new InvalidOperationException((this.mode == ToleranceMode.Unset) ? "Tolerance amount must be specified before setting mode" : "Tried to use multiple tolerance modes at the same time");
			}
			if (!Numerics.IsNumericType(this.amount))
			{
				throw new InvalidOperationException("A numeric tolerance is required");
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000177EF File Offset: 0x000159EF
		public object Value
		{
			get
			{
				return this.amount;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x000177F7 File Offset: 0x000159F7
		public Tolerance Percent
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(this.amount, ToleranceMode.Percent);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0001780B File Offset: 0x00015A0B
		public Tolerance Ulps
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(this.amount, ToleranceMode.Ulps);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0001781F File Offset: 0x00015A1F
		public Tolerance Days
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(TimeSpan.FromDays(Convert.ToDouble(this.amount)));
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00017841 File Offset: 0x00015A41
		public Tolerance Hours
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(TimeSpan.FromHours(Convert.ToDouble(this.amount)));
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x00017863 File Offset: 0x00015A63
		public Tolerance Minutes
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(TimeSpan.FromMinutes(Convert.ToDouble(this.amount)));
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00017885 File Offset: 0x00015A85
		public Tolerance Seconds
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(TimeSpan.FromSeconds(Convert.ToDouble(this.amount)));
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000178A7 File Offset: 0x00015AA7
		public Tolerance Milliseconds
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(TimeSpan.FromMilliseconds(Convert.ToDouble(this.amount)));
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x000178C9 File Offset: 0x00015AC9
		public Tolerance Ticks
		{
			get
			{
				this.CheckLinearAndNumeric();
				return new Tolerance(TimeSpan.FromTicks(Convert.ToInt64(this.amount)));
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000178EB File Offset: 0x00015AEB
		public bool IsUnsetOrDefault
		{
			get
			{
				return this.mode == ToleranceMode.Unset;
			}
		}

		// Token: 0x04000201 RID: 513
		private readonly ToleranceMode mode;

		// Token: 0x04000202 RID: 514
		private readonly object amount;

		// Token: 0x04000203 RID: 515
		private const string ModeMustFollowTolerance = "Tolerance amount must be specified before setting mode";

		// Token: 0x04000204 RID: 516
		private const string MultipleToleranceModes = "Tried to use multiple tolerance modes at the same time";

		// Token: 0x04000205 RID: 517
		private const string NumericToleranceRequired = "A numeric tolerance is required";
	}
}
