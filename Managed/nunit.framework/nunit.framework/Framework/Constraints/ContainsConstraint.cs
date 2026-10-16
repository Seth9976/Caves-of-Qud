using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000FF RID: 255
	public class ContainsConstraint : Constraint
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x0001347B File Offset: 0x0001167B
		public ContainsConstraint(object expected)
			: base(new object[0])
		{
			this._expected = expected;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00013490 File Offset: 0x00011690
		public override string Description
		{
			get
			{
				if (this._realConstraint == null)
				{
					return "containing " + MsgUtils.FormatValue(this._expected);
				}
				return this._realConstraint.Description;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x000134BB File Offset: 0x000116BB
		public ContainsConstraint IgnoreCase
		{
			get
			{
				this._ignoreCase = true;
				return this;
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000134C8 File Offset: 0x000116C8
		public override ConstraintResult ApplyTo(object actual)
		{
			if (actual is string)
			{
				StringConstraint stringConstraint = new SubstringConstraint((string)this._expected);
				if (this._ignoreCase)
				{
					stringConstraint = stringConstraint.IgnoreCase;
				}
				this._realConstraint = stringConstraint;
			}
			else
			{
				this._realConstraint = new CollectionContainsConstraint(this._expected);
			}
			return this._realConstraint.ApplyTo(actual);
		}

		// Token: 0x040001B9 RID: 441
		private readonly object _expected;

		// Token: 0x040001BA RID: 442
		private Constraint _realConstraint;

		// Token: 0x040001BB RID: 443
		private bool _ignoreCase;
	}
}
