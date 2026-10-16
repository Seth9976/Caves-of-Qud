using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000140 RID: 320
	public class StartsWithConstraint : StringConstraint
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x0001748D File Offset: 0x0001568D
		public StartsWithConstraint(string expected)
			: base(expected)
		{
			this.descriptionText = "String starting with";
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x000174A1 File Offset: 0x000156A1
		protected override bool Matches(string actual)
		{
			if (this.caseInsensitive)
			{
				return actual != null && actual.ToLower().StartsWith(this.expected.ToLower());
			}
			return actual != null && actual.StartsWith(this.expected);
		}
	}
}
