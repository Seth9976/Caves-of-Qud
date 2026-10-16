using System;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000142 RID: 322
	public class SubPathConstraint : PathConstraint
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x00016D57 File Offset: 0x00014F57
		public SubPathConstraint(string expected)
			: base(expected)
		{
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00017574 File Offset: 0x00015774
		public override string Description
		{
			get
			{
				return "Subpath of " + MsgUtils.FormatValue(this.expected);
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0001758B File Offset: 0x0001578B
		protected override bool Matches(string actual)
		{
			return actual != null && base.IsSubPath(base.Canonicalize(this.expected), base.Canonicalize(actual));
		}
	}
}
