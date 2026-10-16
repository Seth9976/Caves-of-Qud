using System;
using NUnit.Framework.Internal;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200013C RID: 316
	public class SamePathConstraint : PathConstraint
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x00016D57 File Offset: 0x00014F57
		public SamePathConstraint(string expected)
			: base(expected)
		{
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00016D60 File Offset: 0x00014F60
		public override string Description
		{
			get
			{
				return "Path matching " + MsgUtils.FormatValue(this.expected);
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00016D77 File Offset: 0x00014F77
		protected override bool Matches(string actual)
		{
			return actual != null && StringUtil.StringsEqual(base.Canonicalize(this.expected), base.Canonicalize(actual), this.caseInsensitive);
		}
	}
}
