using System;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200013D RID: 317
	public class SamePathOrUnderConstraint : PathConstraint
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x00016D57 File Offset: 0x00014F57
		public SamePathOrUnderConstraint(string expected)
			: base(expected)
		{
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00016D9C File Offset: 0x00014F9C
		public override string Description
		{
			get
			{
				return "Path under or matching " + MsgUtils.FormatValue(this.expected);
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00016DB4 File Offset: 0x00014FB4
		protected override bool Matches(string actual)
		{
			if (actual == null)
			{
				return false;
			}
			string text = base.Canonicalize(this.expected);
			string text2 = base.Canonicalize(actual);
			return StringUtil.StringsEqual(text, text2, this.caseInsensitive) || base.IsSubPath(text, text2);
		}
	}
}
