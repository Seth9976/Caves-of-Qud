using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000107 RID: 263
	public class EndsWithConstraint : StringConstraint
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x00013AC9 File Offset: 0x00011CC9
		public EndsWithConstraint(string expected)
			: base(expected)
		{
			this.descriptionText = "String ending with";
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00013ADD File Offset: 0x00011CDD
		protected override bool Matches(string actual)
		{
			if (this.caseInsensitive)
			{
				return actual != null && actual.ToLower().EndsWith(this.expected.ToLower());
			}
			return actual != null && actual.EndsWith(this.expected);
		}
	}
}
