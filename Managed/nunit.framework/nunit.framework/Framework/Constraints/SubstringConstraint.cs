using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000143 RID: 323
	public class SubstringConstraint : StringConstraint
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x000175AB File Offset: 0x000157AB
		public SubstringConstraint(string expected)
			: base(expected)
		{
			this.descriptionText = "String containing";
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000175C0 File Offset: 0x000157C0
		protected override bool Matches(string actual)
		{
			if (this.caseInsensitive)
			{
				return actual != null && actual.ToLower().IndexOf(this.expected.ToLower()) >= 0;
			}
			return actual != null && actual.IndexOf(this.expected) >= 0;
		}
	}
}
