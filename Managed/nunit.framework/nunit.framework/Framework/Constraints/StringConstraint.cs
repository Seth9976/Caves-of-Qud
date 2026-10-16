using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000141 RID: 321
	public abstract class StringConstraint : Constraint
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x000174D8 File Offset: 0x000156D8
		public override string Description
		{
			get
			{
				string text = string.Format("{0} {1}", this.descriptionText, MsgUtils.FormatValue(this.expected));
				if (this.caseInsensitive)
				{
					text += ", ignoring case";
				}
				return text;
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00011D90 File Offset: 0x0000FF90
		protected StringConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00017516 File Offset: 0x00015716
		protected StringConstraint(string expected)
			: base(new object[] { expected })
		{
			this.expected = expected;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0001752F File Offset: 0x0001572F
		public StringConstraint IgnoreCase
		{
			get
			{
				this.caseInsensitive = true;
				return this;
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0001753C File Offset: 0x0001573C
		public override ConstraintResult ApplyTo(object actual)
		{
			string text = actual as string;
			if (actual != null && text == null)
			{
				throw new ArgumentException("Actual value must be a string", "actual");
			}
			return new ConstraintResult(this, actual, this.Matches(text));
		}

		// Token: 0x06000992 RID: 2450
		protected abstract bool Matches(string actual);

		// Token: 0x040001FC RID: 508
		protected string expected;

		// Token: 0x040001FD RID: 509
		protected bool caseInsensitive;

		// Token: 0x040001FE RID: 510
		protected string descriptionText;
	}
}
