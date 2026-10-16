using System;
using System.Text.RegularExpressions;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000138 RID: 312
	public class RegexConstraint : StringConstraint
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x00016C6A File Offset: 0x00014E6A
		public RegexConstraint(string pattern)
			: base(pattern)
		{
			this.descriptionText = "String matching";
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00016C7E File Offset: 0x00014E7E
		protected override bool Matches(string actual)
		{
			return actual != null && Regex.IsMatch(actual, this.expected, this.caseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None);
		}
	}
}
