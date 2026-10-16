using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000FE RID: 254
	public class ConstraintResult
	{
		// Token: 0x06000849 RID: 2121 RVA: 0x000133DF File Offset: 0x000115DF
		public ConstraintResult(IConstraint constraint, object actualValue)
		{
			this._constraint = constraint;
			this.ActualValue = actualValue;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000133F5 File Offset: 0x000115F5
		public ConstraintResult(IConstraint constraint, object actualValue, ConstraintStatus status)
			: this(constraint, actualValue)
		{
			this.Status = status;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00013406 File Offset: 0x00011606
		public ConstraintResult(IConstraint constraint, object actualValue, bool isSuccess)
			: this(constraint, actualValue)
		{
			this.Status = (isSuccess ? ConstraintStatus.Success : ConstraintStatus.Failure);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001341D File Offset: 0x0001161D
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00013425 File Offset: 0x00011625
		public object ActualValue { get; private set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001342E File Offset: 0x0001162E
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00013436 File Offset: 0x00011636
		public ConstraintStatus Status { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0001343F File Offset: 0x0001163F
		public virtual bool IsSuccess
		{
			get
			{
				return this.Status == ConstraintStatus.Success;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001344A File Offset: 0x0001164A
		public string Name
		{
			get
			{
				return this._constraint.DisplayName;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00013457 File Offset: 0x00011657
		public string Description
		{
			get
			{
				return this._constraint.Description;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00013464 File Offset: 0x00011664
		public virtual void WriteMessageTo(MessageWriter writer)
		{
			writer.DisplayDifferences(this);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0001346D File Offset: 0x0001166D
		public virtual void WriteActualValueTo(MessageWriter writer)
		{
			writer.WriteActualValue(this.ActualValue);
		}

		// Token: 0x040001B6 RID: 438
		private IConstraint _constraint;
	}
}
